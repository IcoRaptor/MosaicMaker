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
        /// Checks if a file is an image
        /// </summary>
        public static ImageType GetImageType(string path)
        {
            const byte MAX_BYTES = 11;
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
        public static Size GetElementSize(params RadioButton[] args)
        {
            Size size = new Size();

            foreach (var rb in args)
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

        #region Image checks

        private static bool CheckJPEG(byte[] header)
        {
            return header[0] == 0xFF &&
                header[1] == 0xD8 && ((
                header[6] == 0x4A &&
                header[7] == 0x46 &&
                header[8] == 0x49 &&
                header[9] == 0x46) || (
                header[6] == 0x45 &&
                header[7] == 0x78 &&
                header[8] == 0x69 &&
                header[9] == 0x66)) &&
                header[10] == 0x00;
        }

        private static bool CheckPNG(byte[] header)
        {
            return header[0] == 0x89 &&
                header[1] == 0x50 &&
                header[2] == 0x4E &&
                header[3] == 0x47 &&
                header[4] == 0x0D &&
                header[5] == 0x0A &&
                header[6] == 0x1A &&
                header[7] == 0x0A;
        }

        private static bool CheckGIF(byte[] header)
        {
            return header[0] == 0x47 &&
                header[1] == 0x49 &&
                header[2] == 0x46;
        }

        private static bool CheckBMP(byte[] header)
        {
            return header[0] == 0x42 &&
                header[1] == 0x4D;
        }

        private static bool CheckTIFF(byte[] header)
        {
            return (header[0] == 0x49 &&
                header[1] == 0x49 &&
                header[2] == 0x2A &&
                header[3] == 0x00) ||
                header[0] == 0x4D &&
                header[1] == 0x4D &&
                header[2] == 0x00 &&
                header[3] == 0x2A;
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