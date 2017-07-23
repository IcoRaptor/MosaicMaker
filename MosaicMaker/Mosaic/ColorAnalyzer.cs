using System.Collections.Generic;
using System.Drawing;

namespace MosaicMaker
{
    public sealed class ColorAnalyzer : IClearable, IExecutable
    {
        #region Variables

        private List<ColorBlock> _elementBlocks =
            new List<ColorBlock>();

        private List<List<ColorBlock>> _slicedImage
            = new List<List<ColorBlock>>();

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
            Dictionary<Point, ColorBlock> indexFittingBlock =
                new Dictionary<Point, ColorBlock>();
        }

        private int SquaredError(ColorBlock image, ColorBlock element)
        {
            int red = image.AverageColor.R - element.AverageColor.R;
            int green = image.AverageColor.G - element.AverageColor.G;
            int blue = image.AverageColor.B - element.AverageColor.B;

            int total = red + green + blue;

            return total * total;
        }

        public void Clear()
        {
            _elementBlocks.Clear();
            _slicedImage.Clear();
        }
    }
}