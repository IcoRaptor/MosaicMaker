using System.Collections.Generic;

namespace MosaicMakerNS
{
    public static class MathUtil
    {
        /// <summary>
        /// Calculates the squared distance between the average colors
        ///  of the ColorBlocks
        /// </summary>
        public static int SquaredError(ColorBlock img, ColorBlock element)
        {
            int red = img.AverageColor.R - element.AverageColor.R;
            int green = img.AverageColor.G - element.AverageColor.G;
            int blue = img.AverageColor.B - element.AverageColor.B;

            return red * red + green * green + blue * blue;
        }

        /// <summary>
        /// Calculates the luminosity of the given color
        /// </summary>
        public static int Luminosity(int red, int green, int blue)
        {
            return (int)(red * 0.21f + green * 0.72f + blue * 0.07f);
        }

        /// <summary>
        /// Returns a list containing the steps from 0 to height
        /// </summary>
        public static List<int> GetSteps(int heightInPixels, int elementHeight)
        {
            List<int> steps = new List<int>(heightInPixels / elementHeight);

            for (int i = 0; i < heightInPixels; i += elementHeight)
                steps.Add(i);

            return steps;
        }

        /// <summary>
        /// Clamps the value between min and max
        /// </summary>
        public static int Clamp(int value, int min, int max)
        {
            if (value > max)
                return max;

            if (value < min)
                return min;

            return value;
        }
    }
}