using System;
using System.Collections.Generic;
using System.Drawing;

namespace MosaicMakerNS
{
    public sealed class ImageBuilder : IMosaicWorker
    {
        #region Variables

        private readonly ProgressData _pData;
        private readonly List<BlockColumn> _newImageColumns;
        private readonly Size _elementSize;

        #endregion

        #region Properties

        public Bitmap FinalImage { get; private set; }

        #endregion

        #region Constructors

        public ImageBuilder(Size imgSize, Size elementSize,
            List<BlockColumn> newImageColumns, ProgressData pData)
        {
            _pData = pData ??
                throw new ArgumentNullException("pData");

            _elementSize = elementSize;
            _newImageColumns = newImageColumns;

            FinalImage = new Bitmap(imgSize.Width, imgSize.Height);
        }

        #endregion

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
            BlockColumn blockCol = _newImageColumns[col];

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
                    FinalImage.SetPixel(x + horizontal, y + vertical,
                        colorBlock.GetPixels()[x, y]);
                }
            }
        }

        public void Clear()
        {
            _newImageColumns.Clear();
            FinalImage.Dispose();
        }
    }
}