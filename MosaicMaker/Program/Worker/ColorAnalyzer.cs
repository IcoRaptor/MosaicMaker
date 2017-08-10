using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MosaicMakerNS
{
    public sealed class ColorAnalyzer : IMosaicWorker
    {
        #region Variables

        private readonly ProgressData _pData;
        private readonly List<ColorBlock> _elementBlocks;
        private readonly List<BlockLine> _slicedImageLines;

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

            NewImageLines = Settings.Pixelate ?
                _slicedImageLines : new List<BlockLine>(_pData.Lines);
        }

        #endregion

        public void Execute()
        {
            if (Settings.Pixelate)
                return;

            for (int i = 0; i < _slicedImageLines.Count; i++)
                NewImageLines.Add(new BlockLine(_pData.Columns, LineFillMode.FillNull));

            Parallel.For(0, _slicedImageLines.Count, y =>
            {
                GenerateErrors(y, _slicedImageLines[y]);
                _pData.Dialog.IncrementProgress();
            });
        }

        private void GenerateErrors(int y, BlockLine blockLine)
        {
            for (int x = 0; x < blockLine.Count; x++)
            {
                int index = -1;

                List<float> errors = new List<float>(_elementBlocks.Count);
                ColorBlock a = blockLine.GetBlock(x);

                for (int i = 0; i < _elementBlocks.Count; i++)
                {
                    ColorBlock b = _elementBlocks[i];
                    int error = ErrorMetrics.SquaredError(a, b);

                    errors.Add(error);
                }

                index = errors.FindIndexOfSmallestElement();
                NewImageLines[y].SetBlockAt(_elementBlocks[index], x);
            }
        }

        public void Clear()
        {
            _elementBlocks.Clear();
            _slicedImageLines.Clear();
            NewImageLines.Clear();
        }
    }
}