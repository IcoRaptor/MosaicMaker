using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MosaicMaker
{
    /// <summary>
    /// Provides static utility functions
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// Checks the ImageType of a file
        /// </summary>
        public static ImageType GetImageType(string path)
        {
            const byte MAX_BYTES = 10;
            byte[] header = null;
            FileStream stream = null;

            try
            {
                stream = new FileStream(path, FileMode.Open);
            }
            catch
            {
                return ImageType.ERROR;
            }

            using (stream)
            {
                if (stream.Length < MAX_BYTES)
                    return ImageType.UNKNOWN;

                header = new byte[MAX_BYTES];
                stream.Read(header, 0, MAX_BYTES);
            }

            if (CheckJPEG(header))
                return ImageType.JPEG;

            if (CheckPNG(header))
                return ImageType.PNG;

            if (CheckGIF(header))
                return ImageType.GIF;

            if (CheckBMP(header))
                return ImageType.BMP;

            if (CheckTIFF(header))
                return ImageType.TIFF;

            return ImageType.UNKNOWN;
        }

        /// <summary>
        /// Returns the selected mosaic element size
        /// </summary>
        public static Size GetElementSize(params RadioButton[] buttons)
        {
            Size size = new Size();

            foreach (var rb in buttons)
            {
                if (rb.Checked)
                {
                    int s = int.Parse(rb.Text.Split(' ')[0]);
                    size.Height = s;
                    size.Width = s;
                    break;
                }
            }

            return size;
        }

        /// <summary>
        /// Sets the controls enabled property according to the given conditions
        /// </summary>
        public static void SetEnabled(Control ctrl, params bool[] conditions)
        {
            bool enabled = true;
            foreach (var c in conditions)
                enabled = enabled && c;

            ctrl.Enabled = enabled;
            ctrl.BackColor = enabled ?
                Color.FromArgb(255, ctrl.BackColor) :
                Color.FromArgb(125, ctrl.BackColor);
        }

        #region Image checks

        private static bool CheckJPEG(byte[] header)
        {
            bool soi = header[0] == 0xFF && header[1] == 0xD8;
            bool jfif = header[6] == 0x4A && header[7] == 0x46 &&
                header[8] == 0x49 && header[9] == 0x46;
            bool exif = header[6] == 0x45 && header[7] == 0x78 &&
                header[8] == 0x69 && header[9] == 0x66;

            return soi && (jfif || exif);
        }

        private static bool CheckPNG(byte[] header)
        {
            bool png = header[0] == 0x89 && header[1] == 0x50 &&
                header[2] == 0x4E && header[3] == 0x47 &&
                header[4] == 0x0D && header[5] == 0x0A &&
                header[6] == 0x1A && header[7] == 0x0A;

            return png;
        }

        private static bool CheckGIF(byte[] header)
        {
            bool gif = header[0] == 0x47 && header[1] == 0x49 &&
                header[2] == 0x46;

            return gif;
        }

        private static bool CheckBMP(byte[] header)
        {
            bool bm = header[0] == 0x42 && header[1] == 0x4D;

            return bm;
        }

        private static bool CheckTIFF(byte[] header)
        {
            bool ii = header[0] == 0x49 && header[1] == 0x49 &&
                header[2] == 0x2A && header[3] == 0x00;
            bool mm = header[0] == 0x4D && header[1] == 0x4D &&
                header[2] == 0x00 && header[3] == 0x2A;

            return ii || mm;
        }

        #endregion
    }

    /// <summary>
    /// Indicates what kind of image a file is
    /// </summary>
    public enum ImageType
    {
        ERROR = -1,
        JPEG,
        PNG,
        GIF,
        BMP,
        TIFF,
        UNKNOWN
    }
}