using System;
using System.Collections.Generic;
using System.Drawing;

namespace MosaicMakerNS
{
    public sealed class ColorAnalyzer : IMosaicWorker
    {
        #region Variables

        private readonly ProgressData _pData;

        private readonly List<ImageBlock> _elementBlocks =
            new List<ImageBlock>();

        private readonly List<BlockColumn> _slicedImageColumns
            = new List<BlockColumn>();

        private readonly Dictionary<Point, ImageBlock> _listIndexToBlock =
            new Dictionary<Point, ImageBlock>();

        #endregion

        #region Properties

        public List<BlockColumn> NewImageColumns { get; private set; }

        #endregion

        #region Constructors

        public ColorAnalyzer(List<ImageBlock> elementBlocks,
            List<BlockColumn> slicedImageColumns, ProgressData pData)
        {
            _pData = pData ??
                throw new ArgumentNullException("pData");

            _elementBlocks = elementBlocks;
            _slicedImageColumns = slicedImageColumns;

            NewImageColumns = new List<BlockColumn>();
        }

        #endregion

        public void Execute()
        {
            for (int x = 0; x < _slicedImageColumns.Count; x++)
            {
                GenerateErrors(x, _slicedImageColumns[x]);
                IncrementHalf(x);
            }

            for (int x = 0; x < _slicedImageColumns.Count; x++)
            {
                GenerateNewImageColumn(x);
                IncrementHalf(x);
            }
        }

        private void GenerateErrors(int x, BlockColumn blockCol)
        {
            for (int y = 0; y < blockCol.Count; y++)
            {
                List<int> errors = new List<int>();

                for (int i = 0; i < _elementBlocks.Count; i++)
                    errors.Add(SquaredError(blockCol.GetBlock(y), _elementBlocks[i]));

                int index = errors.FindIndexOfSmallestElement();
                _listIndexToBlock.Add(new Point(x, y), _elementBlocks[index]);
            }
        }

        private static int SquaredError(ImageBlock imgBlock, ImageBlock elementBlock)
        {
            int red = imgBlock.AverageColor.R - elementBlock.AverageColor.R;
            int green = imgBlock.AverageColor.G - elementBlock.AverageColor.G;
            int blue = imgBlock.AverageColor.B - elementBlock.AverageColor.B;

            return red * red + green * green + blue * blue;
        }

        private void GenerateNewImageColumn(int x)
        {
            BlockColumn blockCol = _slicedImageColumns[x];
            BlockColumn newBlockCol = new BlockColumn();

            for (int y = 0; y < blockCol.Count; y++)
                newBlockCol.Add(_listIndexToBlock[new Point(x, y)]);

            NewImageColumns.Add(newBlockCol);
        }

        private void IncrementHalf(int x)
        {
            if (x % 2 == 0)
                _pData.ProgWin.IncrementProgress();
        }

        public void Clear()
        {
            _elementBlocks.Clear();
            _slicedImageColumns.Clear();
            _listIndexToBlock.Clear();
            NewImageColumns.Clear();
        }
    }
}