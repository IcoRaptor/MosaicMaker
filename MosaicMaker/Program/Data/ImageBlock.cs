using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace MosaicMakerNS
{
    public sealed class ImageBlock
    {
        #region Properties

        public Bitmap BlockImage { get; set; }
        public Color AverageColor { get; private set; }

        #endregion

        #region Constructors

        public ImageBlock(Bitmap bmp)
        {
            BlockImage = bmp ??
                throw new ArgumentNullException("bmp");

            AverageColor = GetAverageColor();
        }

        #endregion

        private Color GetAverageColor()
        {
            Rectangle rect = new Rectangle(new Point(0, 0), BlockImage.Size);
            PixelFormat format = BlockImage.PixelFormat;

            BitmapData bmpData = BlockImage.LockBits(rect, ImageLockMode.ReadOnly, format);

            BitmapProperties props = new BitmapProperties(bmpData, format);
            Color avg = CalcColor(props);

            BlockImage.UnlockBits(bmpData);

            return avg;
        }

        private unsafe Color CalcColor(BitmapProperties props)
        {
            int red = 0, green = 0, blue = 0;

            byte* ptr = (byte*)props.Scan0;

            for (int y = 0; y < props.HeightInPixels; y++)
            {
                byte* line = ptr + y * props.Stride;

                for (int x = 0; x < props.WidthInBytes; x += props.BytesPerPixel)
                {
                    red += line[x + 2];
                    green += line[x + 1];
                    blue += line[x + 0];
                }
            }

            red /= props.Pixels;
            green /= props.Pixels;
            blue /= props.Pixels;

            return Color.FromArgb(255, red, green, blue);
        }
    }
}