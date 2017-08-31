using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace MosaicMakerNS
{
    /// <summary>
    /// Turns the sliced image into a bitmap
    /// </summary>
    public sealed class ImageBuilder : IMosaicWorker
    {
        #region Variables

        private readonly ProgressData _pData;
        private readonly List<BlockLine> _newImageLines;
        private readonly int _elementWidth;
        private readonly int _elementHeight;

        #endregion

        #region Properties

        /// <summary>
        /// The final image with the original size
        /// </summary>
        public Bitmap FinalImage { get; private set; }

        #endregion

        #region Constructors

        public ImageBuilder(List<BlockLine> newImageLines, ProgressData pData)
        {
            _pData = pData ??
                throw new ArgumentNullException("pData");

            _elementWidth = _pData.ElementSize.Width;
            _elementHeight = _pData.ElementSize.Height;

            _newImageLines = newImageLines;

            FinalImage = new Bitmap(_pData.ImageSize.Width,
                _pData.ImageSize.Height, PixelFormat.Format24bppRgb);
        }

        #endregion

        public void Execute()
        {
            Utility.EditBitmap(FinalImage, BuildImage);
        }

        /// <summary>
        /// Turns the BlockLines into a bitmap
        /// </summary>
        private unsafe void BuildImage(LockBitsData data)
        {
            List<int> steps = Utility.GetSteps(data.HeightInPixels, _elementHeight);

            byte* ptr = (byte*)data.Scan0;

            Parallel.ForEach(steps, y =>
            {
                // Get the appropriate number of lines

                byte*[] lines = new byte*[_elementHeight];

                for (int i = 0; i < _elementHeight; i++)
                    lines[i] = ptr + (y + i) * data.Stride;

                int index = y / _elementHeight;
                BuildBlockLine(lines, index, data.BytesPerPixel);

                _pData.Dialog.IncrementProgress();
            });
        }

        /// <summary>
        /// Turns the lines into a BlockLine
        /// </summary>
        private unsafe void BuildBlockLine(byte*[] lines, int index, int bpp)
        {
            BlockLine blockLine = _newImageLines[index];

            // Advance one block width at a time

            int step = _elementWidth * bpp;

            for (int i = 0; i < blockLine.Count; i++)
            {
                int offset = i * step;
                BuildBlock(lines, blockLine.GetBlock(i), offset, step, bpp);
            }
        }

        /// <summary>
        /// Sets a ColorBlock into the bitmap
        /// </summary>
        private static unsafe void BuildBlock(byte*[] lines, ColorBlock imgBlock,
            int offset, int step, int bpp)
        {
            Color[,] pixels = imgBlock.GetPixels();

            for (int y = 0; y < lines.Length; y++)
            {
                // The position of the block in the line

                byte* block = lines[y] + offset;

                // Set all pixels in the block

                for (int x = 0; x < step; x += bpp)
                {
                    Color c = pixels[x / bpp, y];

                    block[x + 2] = c.R;
                    block[x + 1] = c.G;
                    block[x + 0] = c.B;
                }
            }
        }

        public void Clear()
        {
            _newImageLines.Clear();
            FinalImage.Dispose();
        }
    }
}