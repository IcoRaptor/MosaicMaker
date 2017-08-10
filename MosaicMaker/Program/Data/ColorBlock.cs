using System;
using System.Drawing;

namespace MosaicMakerNS
{
    public sealed class ColorBlock
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
            AverageColor = CalcAverageColor(AverageMode.Pixel);
            ApplySettingsPixelate(Settings.Pixelate);
        }

        #endregion

        public Color[,] GetPixels()
        {
            return _pixels;
        }

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

                    ApplySettingsNegative(Settings.NegativeImage);

                    Color c = Color.FromArgb(_red, _green, _blue);
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

            if (mode == AverageMode.Element || Settings.Pixelate)
                ApplySettingsNegative(Settings.NegativeImage);

            return Color.FromArgb(_red, _green, _blue);
        }

        private void ApplySettingsNegative(bool negative)
        {
            if (!negative)
                return;

            _red = 0xFF - _red;
            _green = 0xFF - _green;
            _blue = 0xFF - _blue;
        }

        private void ApplySettingsPixelate(bool pixelate)
        {
            if (!pixelate)
                return;

            int width = _pixels.GetLength(0);
            int height = _pixels.GetLength(1);

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    _pixels[x, y] = AverageColor;
        }

        private void ResetColors()
        {
            _red = 0;
            _green = 0;
            _blue = 0;
        }
    }
}