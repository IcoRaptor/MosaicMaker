using System.Collections.Generic;
using System.Drawing;

namespace MosaicMakerNS
{
    public sealed class ColorAnalyzer : IMosaicWorker
    {
        #region Variables

        private ProgressWindow _pWin;

        private List<ColorBlock> _elementBlocks =
            new List<ColorBlock>();

        private List<BlockColumn> _slicedImageLines
            = new List<BlockColumn>();

        private Dictionary<Point, ColorBlock> _listIndexToBlock =
            new Dictionary<Point, ColorBlock>();

        #endregion

        #region Properties

        public List<BlockColumn> NewImageLines { get; private set; }

        #endregion

        #region Constructors

        public ColorAnalyzer(List<ColorBlock> elementBlocks,
            List<BlockColumn> slicedImageLines, ProgressWindow pWin)
        {
            _elementBlocks = elementBlocks;
            _slicedImageLines = slicedImageLines;
            _pWin = pWin;

            NewImageLines = new List<BlockColumn>();
        }

        #endregion

        public void Execute()
        {
            for (int x = 0; x < _slicedImageLines.Count; x++)
            {
                GetErrors(x, _slicedImageLines[x]);
                _pWin.UpdateProgress(1);
            }

            GenerateNewImageLines();
        }

        private void GetErrors(int x, BlockColumn blockLine)
        {
            for (int y = 0; y < blockLine.Count; y++)
            {
                List<int> errors = new List<int>();

                for (int z = 0; z < _elementBlocks.Count; z++)
                    errors.Add(SquaredError(blockLine.GetBlock(y), _elementBlocks[z]));

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

        private void GenerateNewImageLines()
        {
            for (int x = 0; x < _slicedImageLines.Count; x++)
            {
                BlockColumn blockLine = _slicedImageLines[x];
                BlockColumn newBlockLine = new BlockColumn();

                for (int y = 0; y < blockLine.Count; y++)
                    newBlockLine.Add(_listIndexToBlock[new Point(x, y)]);

                NewImageLines.Add(newBlockLine);
            }
        }

        public void Clear()
        {
            _pWin = null;
            _elementBlocks.Clear();
            _slicedImageLines.Clear();
            _listIndexToBlock.Clear();
            NewImageLines.Clear();
        }
    }
}