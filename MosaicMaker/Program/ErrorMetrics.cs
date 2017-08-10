using System.Drawing;

namespace MosaicMakerNS
{
    public static class ErrorMetrics
    {
        public static float CalculateError(ErrorMetric metric, ColorBlock img, ColorBlock element)
        {
            if (metric == ErrorMetric.MutualInformation)
                return MutualInformation(img, element);

            return SquaredError(img, element);
        }

        private static float SquaredError(ColorBlock img, ColorBlock element)
        {
            float metric = 0;

            int red = img.AverageColor.R - element.AverageColor.R;
            int green = img.AverageColor.G - element.AverageColor.G;
            int blue = img.AverageColor.B - element.AverageColor.B;

            metric = red * red + green * green + blue * blue;

            return metric;
        }

        private static float MutualInformation(ColorBlock img, ColorBlock element)
        {
            const int TOTAL = 256 * 3;
            float metric = 0f;

            float pixels = img.GetPixels().GetLength(0) * img.GetPixels().GetLength(1);

            float[] probImg = new float[TOTAL];
            float[] probElement = new float[TOTAL];
            float[] prob = new float[TOTAL * TOTAL];

            Color[] histoImg = new Color[TOTAL];
            Color[] histoElelemt = new Color[TOTAL];
            Color[] histo = new Color[TOTAL * TOTAL];

            return metric;
        }
    }
}