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
        private readonly int _width;
        private readonly int _height;

        #endregion

        #region Properties

        public Bitmap FinalImage { get; private set; }

        #endregion

        #region Constructors

        public ImageBuilder(List<BlockLine> newImageLines, ProgressData pData)
        {
            _pData = pData ??
                throw new ArgumentNullException("pData");

            _width = _pData.ElementSize.Width;
            _height = _pData.ElementSize.Height;

            _newImageLines = newImageLines;

            FinalImage = new Bitmap(_pData.ImageSize.Width,
                _pData.ImageSize.Height, PixelFormat.Format24bppRgb);
        }

        #endregion

        public void Execute()
        {
            Utility.EditBitmap(FinalImage, FillImage);
        }

        private unsafe void FillImage(BitmapProperties ppts)
        {
            List<int> steps = Utility.GetSteps(ppts.HeightInPixels, _height);

            byte* ptr = (byte*)ppts.Scan0;

            Parallel.ForEach(steps, y =>
            {
                byte*[] lines = new byte*[_height];

                for (int i = 0; i < _height; i++)
                    lines[i] = ptr + (y + i) * ppts.Stride;

                int index = y / _height;
                FillBlockLine(lines, index, ppts.BytesPerPixel);
                _pData.Dialog.IncrementProgress();
            });
        }

        private unsafe void FillBlockLine(byte*[] lines, int index, int bpp)
        {
            BlockLine blockLine = _newImageLines[index];

            int offset = _width * bpp;

            for (int i = 0; i < blockLine.Count; i++)
                FillBlock(lines, blockLine.GetBlock(i), i * offset, bpp);
        }

        private unsafe void FillBlock(byte*[] lines, ColorBlock block,
            int offset, int bpp)
        {
            for (int y = 0; y < lines.Length; y++)
            {
                byte* line = lines[y] + offset;

                int step = _width * bpp;

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