using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace MosaicMakerNS
{
    public sealed class ImageSlicer : IMosaicWorker
    {
        #region Variables

        private readonly ProgressData _pData;
        private readonly Bitmap _resizedImage;
        private readonly Size _elementSize;

        #endregion

        #region Properties

        public List<BlockLine> SlicedImageLines { get; private set; }

        #endregion

        #region Constructors

        public ImageSlicer(Bitmap resizedImage, Size elementSize, ProgressData pData)
        {
            _resizedImage = resizedImage ??
                throw new ArgumentNullException("resizedImage");

            _pData = pData ??
                throw new ArgumentNullException("pData");

            _elementSize = elementSize;

            SlicedImageLines = new List<BlockLine>();
        }

        #endregion

        public void Execute()
        {
            Utility.EditImage(_resizedImage, SliceImage);

            if (Settings.MirrorModeVertical)
                SlicedImageLines.Reverse();

            if (Settings.MirrorModeHorizontal)
                foreach (var blockLine in SlicedImageLines)
                    blockLine.Reverse();
        }

        private unsafe void SliceImage(BitmapProperties bmpP)
        {
            List<int> steps = Utility.GetSteps(bmpP.HeightInPixels,
                _elementSize.Height);

            for (int i = 0; i < steps.Count; i++)
                SlicedImageLines.Add(null);

            byte* ptr = (byte*)bmpP.Scan0;

            Parallel.ForEach(steps, y =>
            {
                byte*[] lines = new byte*[_elementSize.Height];

                for (int i = 0; i < _elementSize.Height; i++)
                    lines[i] = ptr + (y + i) * bmpP.Stride;

                int index = y / _elementSize.Height;
                SlicedImageLines[index] = GetBlockLine(lines, bmpP);
                _pData.ProgDialog.IncrementProgress();
            });
        }

        private unsafe BlockLine GetBlockLine(byte*[] lines, BitmapProperties bmpP)
        {
            BlockLine blockLine = new BlockLine();

            int step = _elementSize.Width * bmpP.BytesPerPixel;

            for (int offset = 0; offset < bmpP.WidthInBytes; offset += step)
                blockLine.Add(GetPixels(lines, offset, step, bmpP.BytesPerPixel));

            return blockLine;
        }

        private unsafe ColorBlock GetPixels(byte*[] lines, int offset,
            int step, int bpp)
        {
            Color[,] pixels = new Color[_elementSize.Width, _elementSize.Height];

            for (int y = 0; y < lines.Length; y++)
            {
                byte* block = lines[y] + offset;

                for (int x = 0; x < step; x += bpp)
                {
                    int red = block[x + 2];
                    int green = block[x + 1];
                    int blue = block[x + 0];

                    pixels[x / bpp, y] = Color.FromArgb(255, red, green, blue);
                }
            }

            return new ColorBlock(pixels);
        }

        public void Clear()
        {
            _resizedImage.Dispose();
            SlicedImageLines.Clear();
        }
    }
}