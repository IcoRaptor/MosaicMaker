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

        private List<BlockLine> _slicedImageLines
            = new List<BlockLine>();

        private Dictionary<Point, ColorBlock> _listIndexToBlock =
            new Dictionary<Point, ColorBlock>();

        #endregion

        #region Properties

        public List<BlockLine> NewImageLines { get; private set; }

        #endregion

        #region Constructors

        public ColorAnalyzer(List<ColorBlock> elementBlocks,
            List<BlockLine> slicedImageLines, ProgressWindow pWin)
        {
            _pWin = pWin;

            _elementBlocks = elementBlocks;
            _slicedImageLines = slicedImageLines;

            NewImageLines = new List<BlockLine>();
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

        private void GetErrors(int x, BlockLine blockLine)
        {
            for (int y = 0; y < blockLine.Blocks.Count; y++)
            {
                List<int> errors = new List<int>();

                for (int z = 0; z < _elementBlocks.Count; z++)
                    errors.Add(SquaredError(blockLine.Blocks[y], _elementBlocks[z]));

                _listIndexToBlock.Add(new Point(x, y),
                    _elementBlocks[errors.FindIndexOfSmallestElement()]);
            }
        }

        private static int SquaredError(ColorBlock image, ColorBlock element)
        {
            int red = image.AverageColor.R - element.AverageColor.R;
            int green = image.AverageColor.G - element.AverageColor.G;
            int blue = image.AverageColor.B - element.AverageColor.B;

            int total = red + green + blue;

            return total * total;
        }

        private void GenerateNewImageLines()
        {
            for (int x = 0; x < _slicedImageLines.Count; x++)
            {
                BlockLine blockLine = _slicedImageLines[x];
                BlockLine newBlockLine = new BlockLine();

                for (int y = 0; y < blockLine.Blocks.Count; y++)
                    newBlockLine.Blocks.Add(_listIndexToBlock[new Point(x, y)]);

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