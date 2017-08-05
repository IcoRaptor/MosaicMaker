using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace MosaicMakerNS
{
    public sealed class ImageBuilder : IMosaicWorker
    {
        #region Variables

        private readonly ProgressData _pData;
        private readonly List<BlockLine> _newImageLines;
        private readonly Size _elementSize;

        #endregion

        #region Properties

        public Bitmap FinalImage { get; private set; }

        #endregion

        #region Constructors

        public ImageBuilder(Size imgSize, Size elementSize,
            List<BlockLine> newImageLines, ProgressData pData)
        {
            _pData = pData ??
                throw new ArgumentNullException("pData");

            _elementSize = elementSize;
            _newImageLines = newImageLines;

            FinalImage = new Bitmap(imgSize.Width, imgSize.Height,
                PixelFormat.Format24bppRgb);
        }

        #endregion

        public void Execute()
        {
            Utility.EditImage(FinalImage, FillImage);
        }

        private unsafe void FillImage(BitmapProperties bmpP)
        {
            List<int> steps = Utility.GetSteps(bmpP.HeightInPixels,
                _elementSize.Height);

            byte* ptr = (byte*)bmpP.Scan0;

            Parallel.ForEach(steps, y =>
            {
                byte*[] lines = new byte*[_elementSize.Height];

                for (int i = 0; i < _elementSize.Height; i++)
                    lines[i] = ptr + (y + i) * bmpP.Stride;

                int index = y / _elementSize.Height;
                FillBlockLine(lines, index, bmpP.BytesPerPixel);
                _pData.ProgDialog.IncrementProgress();
            });
        }

        private unsafe void FillBlockLine(byte*[] lines, int index, int bpp)
        {
            BlockLine blockLine = _newImageLines[index];

            int offset = _elementSize.Width * bpp;

            for (int i = 0; i < blockLine.Count; i++)
                FillBlock(lines, blockLine.GetBlock(i), i * offset, bpp);
        }

        private unsafe void FillBlock(byte*[] lines, ColorBlock block,
            int offset, int bpp)
        {
            for (int y = 0; y < lines.Length; y++)
            {
                byte* line = lines[y] + offset;

                int step = _elementSize.Width * bpp;

                for (int x = 0; x < step; x += bpp)
                {
                    Color c = block.GetPixels()[x / bpp, y];

                    line[x + 2] = c.R;
                    line[x + 1] = c.G;
                    line[x + 0] = c.B;
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