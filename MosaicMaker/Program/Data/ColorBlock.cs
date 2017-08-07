using System;
using System.Drawing;

namespace MosaicMakerNS
{
    public sealed class ColorBlock : ISettings
    {
        #region Variables

        private readonly Color[,] _pixels;
        private int _red = 0;
        private int _green = 0;
        private int _blue = 0;

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

            Utility.EditBitmap(bmp, GetPixelColors);
            AverageColor = CalcAverageColor(AverageMode.Element);
        }

        public ColorBlock(Color[,] pixels)
        {
            _pixels = pixels;
            AverageColor = CalcAverageColor(AverageMode.Image);
        }

        #endregion

        private unsafe void GetPixelColors(BitmapProperties ppts)
        {
            byte* ptr = (byte*)ppts.Scan0;

            for (int y = 0; y < ppts.HeightInPixels; y++)
            {
                byte* line = ptr + y * ppts.Stride;

                for (int x = 0; x < ppts.WidthInBytes; x += ppts.BytesPerPixel)
                {
                    _red = line[x + 2];
                    _green = line[x + 1];
                    _blue = line[x + 0];

                    ApplySettings();

                    Color c = Color.FromArgb(255, _red, _green, _blue);
                    _pixels[x / ppts.BytesPerPixel, y] = c;
                }
            }
        }

        private Color CalcAverageColor(AverageMode mode)
        {
            ResetColors();

            foreach (var c in _pixels)
            {
                _red += c.R;
                _green += c.G;
                _blue += c.B;
            }

            _red /= _pixels.Length;
            _green /= _pixels.Length;
            _blue /= _pixels.Length;

            if (mode == AverageMode.Element)
                ApplySettings();

            return Color.FromArgb(0xFF, _red, _green, _blue);
        }

        public Color[,] GetPixels()
        {
            return _pixels;
        }

        public void ApplySettings()
        {
            if (Settings.NegativeImage)
            {
                _red = 0xFF - _red;
                _green = 0xFF - _green;
                _blue = 0xFF - _blue;
            }
        }

        private void ResetColors()
        {
            _red = 0;
            _green = 0;
            _blue = 0;
        }
    }
}