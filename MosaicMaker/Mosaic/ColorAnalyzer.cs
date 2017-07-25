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

        private List<List<ColorBlock>> _slicedImageLines
            = new List<List<ColorBlock>>();

        private Dictionary<Point, ColorBlock> _listIndexToBlock =
            new Dictionary<Point, ColorBlock>();

        #endregion

        #region Properties

        public List<List<ColorBlock>> NewImageLines { get; private set; }

        #endregion

        #region Constructors

        public ColorAnalyzer(List<Color[,]> elementPixels,
            List<List<Color[,]>> slicedImage, ProgressWindow pWin)
        {
            _pWin = pWin;

            foreach (var c in elementPixels)
                _elementBlocks.Add(new ColorBlock(c));

            foreach (var line in slicedImage)
            {
                List<ColorBlock> lineBlocks = new List<ColorBlock>();

                foreach (var block in line)
                    lineBlocks.Add(new ColorBlock(block));

                _slicedImageLines.Add(lineBlocks);
            }

            NewImageLines = new List<List<ColorBlock>>();
        }

        #endregion

        public void Execute()
        {
            for (int x = 0; x < _slicedImageLines.Count; x++)
            {
                List<ColorBlock> blockLine = _slicedImageLines[x];

                for (int y = 0; y < blockLine.Count; y++)
                {
                    List<int> errors = new List<int>();

                    for (int z = 0; z < _elementBlocks.Count; z++)
                        errors.Add(SquaredError(blockLine[y], _elementBlocks[z]));

                    _listIndexToBlock.Add(new Point(x, y),
                        _elementBlocks[errors.FindIndexOfSmallestElement()]);
                }

                _pWin.UpdateProgress(1, null);
            }

            GenerateLines();
        }

        private static int SquaredError(ColorBlock image, ColorBlock element)
        {
            int red = image.AverageColor.R - element.AverageColor.R;
            int green = image.AverageColor.G - element.AverageColor.G;
            int blue = image.AverageColor.B - element.AverageColor.B;

            int total = red + green + blue;

            return total * total;
        }

        private void GenerateLines()
        {
            for (int x = 0; x < _slicedImageLines.Count; x++)
            {
                List<ColorBlock> blockLine = _slicedImageLines[x];
                List<ColorBlock> newBlockLine = new List<ColorBlock>();

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