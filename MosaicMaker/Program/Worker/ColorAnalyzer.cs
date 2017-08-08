using System;
using System.Collections.Generic;
using System.Drawing;

namespace MosaicMakerNS
{
    public sealed class ColorAnalyzer : IMosaicWorker
    {
        #region Variables

        private readonly ProgressData _pData;
        private readonly List<ColorBlock> _elementBlocks;
        private readonly List<BlockLine> _slicedImageLines;
        private readonly Dictionary<Point, ColorBlock> _listPointToBlock;

        #endregion

        #region Properties

        public List<BlockLine> NewImageLines { get; private set; }

        #endregion

        #region Constructors

        public ColorAnalyzer(List<ColorBlock> elementBlocks,
            List<BlockLine> slicedImageLines, ProgressData pData)
        {
            _pData = pData ??
                throw new ArgumentNullException("pData");

            _slicedImageLines = slicedImageLines ??
                throw new ArgumentNullException("slicedImageLines");

            _elementBlocks = elementBlocks ??
                throw new ArgumentNullException("elementBlocks");

            _listPointToBlock = new Dictionary<Point, ColorBlock>(
                _slicedImageLines.Count * _elementBlocks.Count);

            NewImageLines = new List<BlockLine>(_pData.Lines);
        }

        #endregion

        public void Execute()
        {
            for (int y = 0; y < _slicedImageLines.Count; y++)
            {
                GenerateErrors(y, _slicedImageLines[y]);
                IncrementHalf(y);
            }

            for (int y = 0; y < _slicedImageLines.Count; y++)
            {
                GenerateNewImageLine(y, _slicedImageLines[y].Count);
                IncrementHalf(y);
            }
        }

        private void GenerateErrors(int y, BlockLine blockLine)
        {
            for (int x = 0; x < blockLine.Count; x++)
            {
                List<int> errors = new List<int>(_elementBlocks.Count);
                ColorBlock imgBlock = blockLine.GetBlock(x);

                for (int i = 0; i < _elementBlocks.Count; i++)
                {
                    ColorBlock elementBlock = _elementBlocks[i];
                    int error = SquaredError(imgBlock, elementBlock);

                    errors.Add(error);
                }

                int index = errors.FindIndexOfSmallestElement();
                _listPointToBlock.Add(new Point(x, y), _elementBlocks[index]);
            }
        }

        private static int SquaredError(ColorBlock imgBlock, ColorBlock elementBlock)
        {
            int red = imgBlock.AverageColor.R - elementBlock.AverageColor.R;
            int green = imgBlock.AverageColor.G - elementBlock.AverageColor.G;
            int blue = imgBlock.AverageColor.B - elementBlock.AverageColor.B;

            return red * red + green * green + blue * blue;
        }

        private void GenerateNewImageLine(int y, int blockCount)
        {
            BlockLine newBlockLine = new BlockLine(_pData.Columns);

            for (int x = 0; x < blockCount; x++)
                newBlockLine.Add(_listPointToBlock[new Point(x, y)]);

            NewImageLines.Add(newBlockLine);
        }

        private void IncrementHalf(int y)
        {
            if (y % 2 == 0)
                _pData.Dialog.IncrementProgress();
        }

        public void Clear()
        {
            _elementBlocks.Clear();
            _slicedImageLines.Clear();
            _listPointToBlock.Clear();
            NewImageLines.Clear();
        }
    }
}