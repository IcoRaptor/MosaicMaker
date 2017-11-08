using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MosaicMakerNS
{
    /// <summary>
    /// Compares the sliced image with the mosaic elements
    /// </summary>
    public sealed class ColorAnalyzer : IMosaicWorker
    {
        #region Variables

        private readonly ProgressData _pData;
        private readonly List<ColorBlock> _elementBlocks;
        private readonly List<BlockLine> _slicedImageLines;

        #endregion

        #region Properties

        /// <summary>
        /// The BlockLines build from the element blocks
        /// </summary>
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

            NewImageLines = Settings.PixelMode ?
                _slicedImageLines : new List<BlockLine>(_pData.Lines);
        }

        #endregion

        public void Execute()
        {
            if (Settings.PixelMode)
                return;

            // Prefill the list and the BlockLines

            for (int i = 0; i < _slicedImageLines.Count; i++)
                NewImageLines.Add(new BlockLine(_pData.Columns, LineFillMode.FillNull));

            Parallel.For(0, _slicedImageLines.Count, y =>
            {
                GenerateErrors(y, _slicedImageLines[y]);

                _pData.Increment();
            });
        }

        /// <summary>
        /// Generates the errors for the given BlockLine
        /// </summary>
        private void GenerateErrors(int y, BlockLine blockLine)
        {
            for (int x = 0; x < blockLine.Count; x++)
            {
                List<int> errors = new List<int>(_elementBlocks.Count);
                ColorBlock img = blockLine.GetBlock(x);

                // Compare the ColorBlock with every mosaic element

                for (int i = 0; i < _elementBlocks.Count; i++)
                {
                    ColorBlock element = _elementBlocks[i];
                    errors.Add(MathUtil.SquaredError(img, element));
                }

                // Set the best fitting block in the list

                int index = errors.FindIndexOfSmallestElement();
                NewImageLines[y].SetBlock(_elementBlocks[index], x);
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