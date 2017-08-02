using System;
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

        public ColorBlock(Bitmap bmp)
        {
            if (bmp == null)
                throw new ArgumentNullException("bmp");

            _pixels = new Color[bmp.Width, bmp.Height];

            Utility.EditImage(bmp, CalcPixels);
            CalcAverageColor();
        }

        public ColorBlock(Color[,] pixels)
        {
            _pixels = pixels;
            CalcAverageColor();
        }

        #endregion

        private unsafe void CalcPixels(BitmapProperties bmpP)
        {
            byte* ptr = (byte*)bmpP.Scan0;

            for (int y = 0; y < bmpP.HeightInPixels; y++)
            {
                byte* line = ptr + y * bmpP.Stride;

                for (int x = 0; x < bmpP.WidthInBytes; x += bmpP.BytesPerPixel)
                {
                    int red = line[x + 2];
                    int green = line[x + 1];
                    int blue = line[x + 0];

                    Color c = Color.FromArgb(255, red, green, blue);
                    _pixels[x / bmpP.BytesPerPixel, y] = c;
                }
            }
        }

        private void CalcAverageColor()
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

            AverageColor = Color.FromArgb(255, red, green, blue);
        }

        public Color[,] GetPixels()
        {
            return _pixels;
        }
    }
}