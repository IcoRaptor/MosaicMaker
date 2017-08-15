namespace MosaicMakerNS
{
    /// <summary>
    /// Contains functions to calculate metrics
    /// </summary>
    public static class Metrics
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
    }
}