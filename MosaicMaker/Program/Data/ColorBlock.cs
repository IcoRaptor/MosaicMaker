using System.Drawing;

namespace MosaicMakerNS
{
    public sealed class ColorBlock
    {
        #region Properties

        public Color[,] Pixels { get; private set; }
        public Color AverageColor { get; private set; }

        #endregion

        #region Constructors

        public ColorBlock(Color[,] pixels)
        {
            Pixels = pixels;
            AverageColor = GetAverageColor();
        }

        #endregion

        private Color GetAverageColor()
        {
            int red = 0, green = 0, blue = 0;

            foreach (var c in Pixels)
            {
                red += c.R;
                green += c.G;
                blue += c.B;
            }

            red /= Pixels.Length;
            green /= Pixels.Length;
            blue /= Pixels.Length;

            return Color.FromArgb(255, red, green, blue);
        }
    }
}