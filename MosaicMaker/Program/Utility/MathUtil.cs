using System.Collections.Generic;
using System.Drawing;

namespace MosaicMakerNS
{
    public static class MathUtil
    {
        /// <summary>
        /// Calculates the squared distance between the colors
        /// </summary>
        public static int SquaredError(Color img, Color mosaic)
        {
            int red = img.R - mosaic.R;
            int green = img.G - mosaic.G;
            int blue = img.B - mosaic.B;

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
        /// Returns a list containing the steps from 0 to target
        /// </summary>
        public static List<int> GetSteps(int target, int step)
        {
            List<int> steps = new List<int>(target / step);

            for (int i = 0; i < target; i += step)
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