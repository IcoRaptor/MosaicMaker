using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace MosaicMakerNS
{
    public sealed class ImageBuilder : IParallelWorker
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

            FinalImage = new Bitmap(imgSize.Width, imgSize.Height, PixelFormat.Format24bppRgb);
        }

        #endregion

        public void ExecuteParallel()
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
                for (int i = 0; i < _elementSize.Height; i++)
                {
                    byte* line = ptr + (y + i) * bmpP.Stride;

                    for (int x = 0; x < bmpP.WidthInBytes; x += bmpP.BytesPerPixel)
                    {
                        /* x + 2 = red
                         * x + 1 = green
                         * x + 0 = blue
                         */

                        line[x + 2] = 0x35;
                        line[x + 1] = 0x97;
                        line[x + 0] = 0x97;
                    }
                }
            });
        }

        public void Clear()
        {
            _newImageLines.Clear();
            FinalImage.Dispose();
        }

        #region Legacy

        public void Execute()
        {
            for (int col = 0; col < _newImageLines.Count; col++)
            {
                BuildColumn(col);
                _pData.ProgWin.IncrementProgress();
            }
        }

        private void BuildColumn(int col)
        {
            BlockLine blockCol = _newImageLines[col];

            for (int block = 0; block < blockCol.Count; block++)
                DrawBlock(col, block, blockCol.GetBlock(block));
        }

        private void DrawBlock(int col, int block, ColorBlock colorBlock)
        {
            int horizontal = col * _elementSize.Width;
            int vertical = block * _elementSize.Height;

            for (int x = 0; x < _elementSize.Width; x++)
            {
                for (int y = 0; y < _elementSize.Height; y++)
                {
                    /*FinalImage.SetPixel(x + horizontal, y + vertical,
                        colorBlock.GetPixels()[x, y]);*/
                }
            }
        }

        #endregion
    }
}