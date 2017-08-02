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

        private readonly List<BlockLine> _slicedImageLines
            = new List<BlockLine>();

        private readonly Dictionary<Point, ImageBlock> _listIndexToBlock =
            new Dictionary<Point, ImageBlock>();

        #endregion

        #region Properties

        public List<BlockLine> NewImageLines { get; private set; }

        #endregion

        #region Constructors

        public ColorAnalyzer(List<ImageBlock> elementBlocks,
            List<BlockLine> slicedImageLines, ProgressData pData)
        {
            _pData = pData ??
                throw new ArgumentNullException("pData");

            _elementBlocks = elementBlocks;
            _slicedImageLines = slicedImageLines;

            NewImageLines = new List<BlockLine>();
        }

        #endregion

        public void Execute()
        {
            for (int x = 0; x < _slicedImageLines.Count; x++)
            {
                GenerateErrors(x, _slicedImageLines[x]);
                IncrementHalf(x);
            }

            for (int x = 0; x < _slicedImageLines.Count; x++)
            {
                GenerateNewImageLine(x);
                IncrementHalf(x);
            }
        }

        private void GenerateErrors(int x, BlockLine blockLine)
        {
            for (int y = 0; y < blockLine.Count; y++)
            {
                List<int> errors = new List<int>();

                for (int i = 0; i < _elementBlocks.Count; i++)
                    errors.Add(SquaredError(blockLine.GetBlock(y), _elementBlocks[i]));

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

        private void GenerateNewImageLine(int x)
        {
            BlockLine blockLine = _slicedImageLines[x];
            BlockLine newBlockLine = new BlockLine();

            for (int y = 0; y < blockLine.Count; y++)
                newBlockLine.Add(_listIndexToBlock[new Point(x, y)]);

            NewImageLines.Add(newBlockLine);
        }

        private void IncrementHalf(int x)
        {
            if (x % 2 == 0)
                _pData.ProgWin.IncrementProgress();
        }

        public void Clear()
        {
            _elementBlocks.Clear();
            _slicedImageLines.Clear();
            _listIndexToBlock.Clear();
            NewImageLines.Clear();
        }
    }
}