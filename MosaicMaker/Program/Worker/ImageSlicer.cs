using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace MosaicMakerNS
{
    /// <summary>
    /// Slices the resized image into BlockLines
    /// </summary>
    public sealed class ImageSlicer : IMosaicWorker
    {
        #region Variables

        private readonly ProgressData _pData;
        private readonly Bitmap _resizedImage;
        private readonly int _elementWidth;
        private readonly int _elementHeight;

        #endregion

        #region Properties

        /// <summary>
        /// The BlockLines of the image
        /// </summary>
        public List<BlockLine> SlicedImageLines { get; private set; }

        #endregion

        #region Constructors

        public ImageSlicer(Bitmap resizedImage, ProgressData pData)
        {
            _resizedImage = resizedImage ??
                throw new ArgumentNullException("resizedImage");

            _pData = pData ??
                throw new ArgumentNullException("pData");

            _elementWidth = _pData.ElementSize.Width;
            _elementHeight = _pData.ElementSize.Height;

            SlicedImageLines = new List<BlockLine>(_pData.Lines);

            // Prefill the list

            for (int i = 0; i < _pData.Lines; i++)
                SlicedImageLines.Add(null);
        }

        #endregion

        public void Execute()
        {
            Utility.EditBitmap(_resizedImage, SliceImage);
            ApplySettings(Settings.MirrorImage);
        }

        /// <summary>
        /// Fill the list of BlockLines
        /// </summary>
        private unsafe void SliceImage(BitmapProperties ppts)
        {
            List<int> steps = Utility.GetSteps(ppts.HeightInPixels, _elementHeight);

            byte* ptr = (byte*)ppts.Scan0;

            Parallel.ForEach(steps, y =>
            {
                // Get the appropriate number of lines

                byte*[] lines = new byte*[_elementHeight];

                for (int i = 0; i < _elementHeight; i++)
                    lines[i] = ptr + (y + i) * ppts.Stride;

                int index = y / _elementHeight;
                SlicedImageLines[index] = GetBlockLine(lines, ppts);

                _pData.Dialog.IncrementProgress();
            });
        }

        /// <summary>
        /// Turns the lines into a BlockLine
        /// </summary>
        private unsafe BlockLine GetBlockLine(byte*[] lines, BitmapProperties ppts)
        {
            BlockLine blockLine = new BlockLine(_pData.Columns, LineFillMode.Default);

            // Advance one block width at a time

            int step = _elementWidth * ppts.BytesPerPixel;

            for (int offset = 0; offset < ppts.WidthInBytes; offset += step)
                blockLine.Add(GetPixels(lines, offset, step, ppts.BytesPerPixel));

            return blockLine;
        }

        /// <summary>
        /// Returns the ColorBlock from the given position
        /// </summary>
        private unsafe ColorBlock GetPixels(byte*[] lines, int offset, int step, int bpp)
        {
            Color[,] pixels = new Color[_elementWidth, _elementHeight];

            for (int y = 0; y < lines.Length; y++)
            {
                // The position of the block in the line

                byte* block = lines[y] + offset;

                // Go over the pixels in the block

                for (int x = 0; x < step; x += bpp)
                {
                    int red = block[x + 2];
                    int green = block[x + 1];
                    int blue = block[x + 0];

                    pixels[x / bpp, y] = Color.FromArgb(red, green, blue);
                }
            }

            return new ColorBlock(pixels);
        }

        /// <summary>
        /// Mirror the image if necessary
        /// </summary>
        private void ApplySettings(bool mirror)
        {
            if (!mirror)
                return;

            if (Settings.MirrorModeVertical)
                SlicedImageLines.Reverse();

            if (Settings.MirrorModeHorizontal)
                foreach (var blockLine in SlicedImageLines)
                    blockLine.Reverse();
        }

        public void Clear()
        {
            _resizedImage.Dispose();
            SlicedImageLines.Clear();
        }
    }
}