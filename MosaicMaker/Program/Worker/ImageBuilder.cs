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
        private readonly List<BlockLine> _newImageColumns;
        private readonly Size _elementSize;

        #endregion

        #region Properties

        public Bitmap FinalImage { get; private set; }

        #endregion

        #region Constructors

        public ImageBuilder(Size imgSize, Size elementSize,
            List<BlockLine> newImageColumns, ProgressData pData)
        {
            _pData = pData ??
                throw new ArgumentNullException("pData");

            _elementSize = elementSize;
            _newImageColumns = newImageColumns;

            FinalImage = new Bitmap(imgSize.Width, imgSize.Height, PixelFormat.Format24bppRgb);
        }

        #endregion

        public void ExecuteParallel()
        {
            Rectangle rect = new Rectangle(0, 0, FinalImage.Width, FinalImage.Height);
            PixelFormat format = FinalImage.PixelFormat;

            BitmapData bmpData = FinalImage.LockBits(rect, ImageLockMode.WriteOnly, format);

            BitmapProperties props = new BitmapProperties(bmpData, format);
            FillImage(props);

            FinalImage.UnlockBits(bmpData);
        }

        private unsafe void FillImage(BitmapProperties props)
        {
            List<int> steps = new List<int>();
            for (int i = 0; i < props.HeightInPixels; i += _elementSize.Height)
                steps.Add(i);

            byte* ptr = (byte*)props.Scan0;

            Parallel.ForEach(steps, y =>
            {
                byte*[] lines = new byte*[_elementSize.Height];

                for (int i = 0; i < lines.Length; i++)
                {
                    byte* line = ptr + (y + i) * props.Stride;
                    lines[i] = line;

                    for (int x = 0; x < props.WidthInBytes; x += props.BytesPerPixel)
                    {
                        /* x + 2 = red
                         * x + 1 = green
                         * x + 0 = blue
                         */

                        line[x + 2] = 0xFF;
                        line[x + 1] = 0x4E;
                        line[x + 0] = 0x4E;
                    }
                }
            });
        }

        public void Clear()
        {
            _newImageColumns.Clear();
            FinalImage.Dispose();
        }

        #region Legacy

        public void Execute()
        {
            for (int col = 0; col < _newImageColumns.Count; col++)
            {
                BuildColumn(col);
                _pData.ProgWin.IncrementProgress();
            }
        }

        private void BuildColumn(int col)
        {
            BlockLine blockCol = _newImageColumns[col];

            for (int block = 0; block < blockCol.Count; block++)
                DrawBlock(col, block, blockCol.GetBlock(block));
        }

        private void DrawBlock(int col, int block, ImageBlock colorBlock)
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