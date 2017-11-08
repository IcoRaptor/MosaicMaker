using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace MosaicMakerNS
{
    public static class BitmapEditor
    {
        /// <summary>
        /// Locks the bitmap into memory and calls the EditAction
        /// </summary>
        public static void Edit(Bitmap bmp, EditAction action)
        {
            if (bmp == null)
                throw new ArgumentNullException("bmp");

            if (action == null)
                throw new ArgumentNullException("action");

            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            PixelFormat format = bmp.PixelFormat;

            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, format);
            LockBitsData data = new LockBitsData(bmpData, format);

            action(data);

            bmp.UnlockBits(bmpData);
        }
    }
}