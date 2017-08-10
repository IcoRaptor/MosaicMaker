namespace MosaicMakerNS
{
    public static class ErrorMetrics
    {
        public static int SquaredError(ColorBlock a, ColorBlock b)
        {
            int red = a.AverageColor.R - b.AverageColor.R;
            int green = a.AverageColor.G - b.AverageColor.G;
            int blue = a.AverageColor.B - b.AverageColor.B;

            return red * red + green * green + blue * blue;
        }
    }
}