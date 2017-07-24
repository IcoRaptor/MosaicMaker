using System.Collections.Generic;
using System.Drawing;

namespace MosaicMakerNS
{
    public sealed class ColorAnalyzer : IMosaicWorker
    {
        #region Variables

        private List<ColorBlock> _elementBlocks =
            new List<ColorBlock>();

        private List<List<ColorBlock>> _slicedImage
            = new List<List<ColorBlock>>();

        private Dictionary<Point, ColorBlock> _indexFittingBlock =
            new Dictionary<Point, ColorBlock>();

        #endregion

        #region Properties

        public List<List<ColorBlock>> NewImage { get; private set; }

        #endregion

        #region Constructors

        public ColorAnalyzer(List<Color[,]> elementPixels,
            List<List<Color[,]>> slicedImage)
        {
            foreach (var c in elementPixels)
                _elementBlocks.Add(new ColorBlock(c));

            foreach (var line in slicedImage)
            {
                List<ColorBlock> lineBlocks = new List<ColorBlock>();

                foreach (var block in line)
                    lineBlocks.Add(new ColorBlock(block));

                _slicedImage.Add(lineBlocks);
            }

            NewImage = new List<List<ColorBlock>>();
        }

        #endregion

        public void Execute()
        {
            for (int x = 0; x < _slicedImage.Count; x++)
            {
                List<ColorBlock> blocks = _slicedImage[x];

                for (int y = 0; y < blocks.Count; y++)
                {
                    List<int> errors = new List<int>();

                    for (int z = 0; z < _elementBlocks.Count; z++)
                        errors.Add(SquaredError(blocks[y], _elementBlocks[z]));

                    _indexFittingBlock.Add(new Point(x, y),
                        _elementBlocks[errors.FindIndexOfSmallestElement()]);
                }
            }

            GenerateNewImage();
        }

        private int SquaredError(ColorBlock image, ColorBlock element)
        {
            int red = image.AverageColor.R - element.AverageColor.R;
            int green = image.AverageColor.G - element.AverageColor.G;
            int blue = image.AverageColor.B - element.AverageColor.B;

            int total = red + green + blue;

            return total * total;
        }

        private void GenerateNewImage()
        {
            for (int x = 0; x < _slicedImage.Count; x++)
            {
                List<ColorBlock> newBlocks = new List<ColorBlock>();
                List<ColorBlock> blocks = _slicedImage[x];

                for (int y = 0; y < blocks.Count; y++)
                    newBlocks.Add(_indexFittingBlock[new Point(x, y)]);

                NewImage.Add(newBlocks);
            }
        }

        public void Clear()
        {
            _elementBlocks.Clear();
            _slicedImage.Clear();
        }
    }
}