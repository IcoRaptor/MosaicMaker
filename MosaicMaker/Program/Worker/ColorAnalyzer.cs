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

            NewImageLines = new List<BlockLine>(_pData.Lines);

            for (int i = 0; i < _pData.Lines; i++)
                NewImageLines.Add(new BlockLine(_pData.Columns, true));
        }

        #endregion

        public void Execute()
        {
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

                if (!Settings.Pixelate)
                {
                    List<float> errors = new List<float>(_elementBlocks.Count);
                    ColorBlock imgBlock = blockLine.GetBlock(x);

                    for (int i = 0; i < _elementBlocks.Count; i++)
                    {
                        ColorBlock elementBlock = _elementBlocks[i];
                        float error = ErrorMetrics.CalculateError(
                            Settings.Error, imgBlock, elementBlock);

                        errors.Add(error);
                    }

                    index = errors.FindIndexOfSmallestElement();
                }

                ApplySettings(Settings.Pixelate, x, y, index);
            }
        }

        private void ApplySettings(bool pixelate, int x, int y, int index)
        {
            if (pixelate)
                NewImageLines[y].SetBlockAt(_slicedImageLines[y].GetBlock(x), x);
            else
                NewImageLines[y].SetBlockAt(_elementBlocks[index], x);
        }

        public void Clear()
        {
            _elementBlocks.Clear();
            _slicedImageLines.Clear();
            NewImageLines.Clear();
        }
    }
}