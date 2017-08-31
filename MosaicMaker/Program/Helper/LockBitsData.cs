using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace MosaicMakerNS
{
    /// <summary>
    /// Contains information about a bitmap
    /// </summary>
    public sealed class LockBitsData
    {
        #region Properties

        /// <summary>
        /// Pointer to the first pixel
        /// </summary>
        public IntPtr Scan0 { get; private set; }

        /// <summary>
        /// Number of bytes per pixel
        /// </summary>
        public int BytesPerPixel { get; private set; }

        /// <summary>
        /// Scan width
        /// </summary>
        public int Stride { get; private set; }

        /// <summary>
        /// The width in pixels
        /// </summary>
        public int WidthInPixels { get; private set; }

        /// <summary>
        /// The height in pixels
        /// </summary>
        public int HeightInPixels { get; private set; }

        /// <summary>
        /// The width in bytes
        /// </summary>
        public int WidthInBytes { get; private set; }

        #endregion

        #region Constructors

        public LockBitsData(BitmapData bmpData, PixelFormat format)
        {
            if (bmpData == null)
                throw new ArgumentNullException("bmpData");

            Scan0 = bmpData.Scan0;
            BytesPerPixel = Image.GetPixelFormatSize(format) / 8;
            Stride = bmpData.Stride;
            WidthInPixels = bmpData.Width;
            HeightInPixels = bmpData.Height;
            WidthInBytes = BytesPerPixel * WidthInPixels;
        }

        #endregion
    }
}