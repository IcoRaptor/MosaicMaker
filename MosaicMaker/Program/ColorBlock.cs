using System.Drawing;

namespace MosaicMaker
{
    public sealed class ColorBlock
    {
        #region Properties

        public Color[,] PixelColors { get; set; }
        public Color AverageColor { get; private set; }

        #endregion

        #region Constructors

        public ColorBlock(Color[,] pixelColors)
        {
            PixelColors = pixelColors;
            AverageColor = CalcAverageColor();
        }

        #endregion

        private Color CalcAverageColor()
        {
            int red = 0, green = 0, blue = 0;

            foreach (Color c in PixelColors)
            {
                red += c.R;
                green += c.G;
                blue += c.B;
            }

            red /= PixelColors.Length;
            green /= PixelColors.Length;
            blue /= PixelColors.Length;

            return Color.FromArgb(red, green, blue);
        }
    }
}