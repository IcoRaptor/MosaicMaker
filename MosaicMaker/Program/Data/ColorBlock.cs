using System.Drawing;

namespace MosaicMakerNS
{
    public sealed class ColorBlock
    {
        #region Variables

        private readonly Color[,] _pixels;

        #endregion

        #region Properties

        public Color AverageColor { get; private set; }

        #endregion

        #region Constructors

        public ColorBlock(Color[,] pixels)
        {
            _pixels = pixels;
            AverageColor = CalcAverageColor();
        }

        #endregion

        private Color CalcAverageColor()
        {
            int red = 0, green = 0, blue = 0;

            foreach (var c in _pixels)
            {
                red += c.R;
                green += c.G;
                blue += c.B;
            }

            red /= _pixels.Length;
            green /= _pixels.Length;
            blue /= _pixels.Length;

            return Color.FromArgb(255, red, green, blue);
        }

        public Color[,] GetPixels()
        {
            return _pixels;
        }
    }
}