using System;
using System.Collections.Generic;
using System.Drawing;

namespace MosaicMaker
{
    public class ColorAnalyzer : IClearable
    {
        #region Variables

        private List<ColorBlock> _elementBlocks =
            new List<ColorBlock>();
        private List<List<ColorBlock>> _slicedImage;

        #endregion

        #region Properties

        public List<List<int>> Errors { get; private set; }
        public Dictionary<int[], int> IndexErrors { get; set; }

        #endregion

        #region Constructors

        public ColorAnalyzer(List<Color[,]> elementPixels,
            List<List<ColorBlock>> slicedImage)
        {
            foreach (var c in elementPixels)
                _elementBlocks.Add(new ColorBlock(c));

            _slicedImage = slicedImage;

            Errors = new List<List<int>>();
            IndexErrors = new Dictionary<int[], int>();
        }

        #endregion

        public void AnalyzeColors()
        {
            GenerateLineErrors();
            FillIndexErrors();
        }

        private void GenerateLineErrors()
        {
            foreach (var c in _elementBlocks)
            {
                foreach (var line in _slicedImage)
                {
                    List<int> lineErrors = new List<int>();

                    foreach (var block in line)
                        lineErrors.Add(SquaredError(block, c));

                    Errors.Add(lineErrors);
                }
            }
        }

        private void FillIndexErrors()
        {
            for (int i = 0; i < Errors.Count; i++)
            {
                List<int> line = Errors[i];

                for (int j = 0; j < line.Count; j++)
                {
                    int[] index = { i, j };
                    IndexErrors.Add(index, FindIndexOfSmallestError(line));
                }
            }
        }

        private int SquaredError(ColorBlock image, ColorBlock element)
        {
            int red = image.AverageColor.R - element.AverageColor.R;
            int green = image.AverageColor.G - element.AverageColor.G;
            int blue = image.AverageColor.B - element.AverageColor.B;

            int total = red + green + blue;

            return total * total;
        }

        private int FindIndexOfSmallestError(List<int> line)
        {
            int index = 0;
            int smallest = line[0];

            for (int i = 0; i < line.Count; i++)
            {
                int num = line[i];

                if (num < smallest)
                {
                    smallest = num;
                    index = i;
                }
            }

            return index;
        }

        public void Clear()
        {
            _elementBlocks.Clear();
            _slicedImage.Clear();
        }
    }
}