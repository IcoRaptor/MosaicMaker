using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace MosaicMakerNS
{
    public sealed class BitmapProperties
    {
        #region Properties

        public IntPtr Scan0 { get; private set; }
        public int BytesPerPixel { get; private set; }
        public int HeightInPixels { get; private set; }
        public int Stride { get; private set; }
        public int WidthInBytes { get; private set; }
        public int Pixels { get; private set; }

        #endregion

        #region Constructors

        public BitmapProperties(BitmapData bmpData, PixelFormat format)
        {
            Scan0 = bmpData.Scan0;
            BytesPerPixel = Image.GetPixelFormatSize(format) / 8;
            HeightInPixels = bmpData.Height;
            Stride = bmpData.Stride;
            WidthInBytes = bmpData.Width * BytesPerPixel;
            Pixels = HeightInPixels * (WidthInBytes / BytesPerPixel);
        }

        #endregion
    }
}