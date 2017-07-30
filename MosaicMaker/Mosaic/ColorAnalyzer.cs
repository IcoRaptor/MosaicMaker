using System.Collections.Generic;
using System.Drawing;

namespace MosaicMakerNS
{
    public sealed class ColorAnalyzer : IMosaicWorker
    {
        #region Variables

        private readonly List<ColorBlock> _elementBlocks =
            new List<ColorBlock>();

        private readonly List<BlockColumn> _slicedImageColumns
            = new List<BlockColumn>();

        private readonly Dictionary<Point, ColorBlock> _listIndexToBlock =
            new Dictionary<Point, ColorBlock>();

        private readonly ProgressWindow _pWin;

        #endregion

        #region Properties

        public List<BlockColumn> NewImageColumns { get; private set; }

        #endregion

        #region Constructors

        public ColorAnalyzer(List<ColorBlock> elementBlocks,
            List<BlockColumn> slicedImageColumns, ProgressWindow pWin)
        {
            _elementBlocks = elementBlocks;
            _slicedImageColumns = slicedImageColumns;
            _pWin = pWin;

            NewImageColumns = new List<BlockColumn>();
        }

        #endregion

        public void Execute()
        {
            for (int x = 0; x < _slicedImageColumns.Count; x++)
            {
                GetErrors(x, _slicedImageColumns[x]);
                _pWin.IncrementProgress();
            }

            GenerateNewImageColumns();
        }

        private void GetErrors(int x, BlockColumn blockCol)
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

        private static int SquaredError(ColorBlock imgBlock, ColorBlock elementBlock)
        {
            int red = imgBlock.AverageColor.R - elementBlock.AverageColor.R;
            int green = imgBlock.AverageColor.G - elementBlock.AverageColor.G;
            int blue = imgBlock.AverageColor.B - elementBlock.AverageColor.B;

            return red * red + green * green + blue * blue;
        }

        private void GenerateNewImageColumns()
        {
            for (int x = 0; x < _slicedImageColumns.Count; x++)
            {
                BlockColumn blockCol = _slicedImageColumns[x];
                BlockColumn newBlockCol = new BlockColumn();

                for (int y = 0; y < blockCol.Count; y++)
                    newBlockCol.Add(_listIndexToBlock[new Point(x, y)]);

                NewImageColumns.Add(newBlockCol);
            }
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