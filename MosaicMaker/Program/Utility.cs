using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace MosaicMakerNS
{
    public static class Utility
    {
        public static Size GetElementSize(params RadioButton[] buttons)
        {
            Size size = new Size();

            foreach (var rb in buttons)
            {
                if (rb.Checked)
                {
                    string[] splits = rb.Text.Split(' ');
                    int w = int.Parse(splits[0], CultureInfo.InvariantCulture);
                    int h = int.Parse(splits[2], CultureInfo.InvariantCulture);

                    size.Width = w;
                    size.Height = h;

                    break;
                }
            }

            return size;
        }

        public static Size GetNewImageSize(Image image, Size elementSize)
        {
            int width = image.Width;
            int height = image.Height;

            while (width % elementSize.Width != 0)
                ++width;

            while (height % elementSize.Height != 0)
                ++height;

            return new Size(width, height);
        }

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

        public static int Clamp(int value, int min, int max)
        {
            if (value < min)
                return min;

            if (value > max)
                return max;

            return value;
        }

        /// <summary>
        /// May return null
        /// </summary>
        public static FileStream TryGetFileStream(string path)
        {
            try
            {
                return new FileStream(path, FileMode.Open,
                    FileAccess.Read, FileShare.None);
            }
            catch
            {
                return null;
            }
        }

        public static ImageType GetImageType(string path)
        {
            const byte MAX_BYTES = 4;
            byte[] header = null;

            using (FileStream stream = TryGetFileStream(path))
            {
                if (stream == null)
                    return ImageType.Error;

                if (stream.Length < MAX_BYTES)
                    return ImageType.Unknown;

                header = new byte[MAX_BYTES];
                stream.Read(header, 0, MAX_BYTES);
            }

            return CheckHeader(header);
        }

        #region Image checks

        private static ImageType CheckHeader(byte[] header)
        {
            if (CheckJPEG(header))
                return ImageType.Jpeg;

            if (CheckPNG(header))
                return ImageType.Png;

            if (CheckBMP(header))
                return ImageType.Bmp;

            if (CheckTIFF(header))
                return ImageType.Tiff;

            return ImageType.Unknown;
        }

        private static bool CheckJPEG(byte[] header)
        {
            return header[0] == 0xFF && header[1] == 0xD8;
        }

        private static bool CheckPNG(byte[] header)
        {
            return header[0] == 0x89 && header[1] == 0x50 &&
                header[2] == 0x4E && header[3] == 0x47;
        }

        private static bool CheckBMP(byte[] header)
        {
            return header[0] == 0x42 && header[1] == 0x4D;
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
}