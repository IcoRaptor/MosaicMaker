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
        public int Stride { get; private set; }
        public int WidthInPixels { get; private set; }
        public int HeightInPixels { get; private set; }
        public int WidthInBytes { get; private set; }
        public int Pixels { get; private set; }

        #endregion

        #region Constructors

        public BitmapProperties(BitmapData bmpData, PixelFormat format)
        {
            if (bmpData == null)
                throw new ArgumentNullException("bmpData");

            Scan0 = bmpData.Scan0;
            BytesPerPixel = Image.GetPixelFormatSize(format) / 8;
            Stride = bmpData.Stride;
            WidthInPixels = bmpData.Width;
            HeightInPixels = bmpData.Height;
            WidthInBytes = BytesPerPixel * bmpData.Width;
            Pixels = HeightInPixels * WidthInPixels;
        }

        #endregion
    }
}