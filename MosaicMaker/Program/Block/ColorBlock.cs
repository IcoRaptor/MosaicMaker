using System;
using System.Drawing;

namespace MosaicMakerNS
{
    /// <summary>
    /// Represents a block of pixels
    /// </summary>
    public sealed class ColorBlock
    {
        #region Variables

        private readonly Color[,] _pixels;
        private int _red = 0;
        private int _green = 0;
        private int _blue = 0;

        #endregion

        #region Properties

        /// <summary>
        /// The average color of the pixels
        /// </summary>
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
            ApplySettings(Settings.PixelMode);
        }

        #endregion

        /// <summary>
        /// Returns the pixel array
        /// </summary>
        public Color[,] GetPixels()
        {
            return _pixels;
        }

        /// <summary>
        /// Gets the pixel colors from a bitmap
        /// </summary>
        private unsafe void GetPixelColors(LockBitsData data)
        {
            byte* ptr = (byte*)data.Scan0;

            for (int y = 0; y < data.HeightInPixels; y++)
            {
                byte* line = ptr + y * data.Stride;

                for (int x = 0; x < data.WidthInBytes; x += data.BytesPerPixel)
                {
                    _red = line[x + 2];
                    _green = line[x + 1];
                    _blue = line[x + 0];

                    ApplySettings2(Settings.NegativeImage);
                    ApplySettings3(Settings.GrayscaleImage);

                    Color c = Color.FromArgb(_red, _green, _blue);
                    _pixels[x / data.BytesPerPixel, y] = c;
                }
            }
        }

        /// <summary>
        /// Calculates the average color of the block
        /// </summary>
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

            if (CheckApplySettingsAverage(mode))
            {
                ApplySettings2(Settings.NegativeImage);
                ApplySettings3(Settings.GrayscaleImage);
            }

            return Color.FromArgb(_red, _green, _blue);
        }

        /// <summary>
        /// Checks if settings should be applied to the average color
        /// </summary>
        private static bool CheckApplySettingsAverage(AverageMode mode)
        {
            return mode == AverageMode.Element || Settings.PixelMode;
        }

        /// <summary>
        /// Assign the average color to all pixels
        /// </summary>
        private void ApplySettings(bool pixelMode)
        {
            if (!pixelMode)
                return;

            int width = _pixels.GetLength(0);
            int height = _pixels.GetLength(1);

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    _pixels[x, y] = AverageColor;
        }

        /// <summary>
        /// Invert the colors
        /// </summary>
        private void ApplySettings2(bool negative)
        {
            if (!negative)
                return;

            _red = 0xFF - _red;
            _green = 0xFF - _green;
            _blue = 0xFF - _blue;
        }

        /// <summary>
        /// Apply grayscale to the colors
        /// </summary>
        private void ApplySettings3(bool grayscale)
        {
            if (!grayscale)
                return;

            _red = _green = _blue = Metrics.Luminosity(_red, _green, _blue);
        }

        /// <summary>
        /// Set the RGB values to 0
        /// </summary>
        private void ResetColors()
        {
            _red = 0;
            _green = 0;
            _blue = 0;
        }
    }
}