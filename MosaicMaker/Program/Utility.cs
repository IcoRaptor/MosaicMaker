using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.CheckedListBox;

namespace MosaicMaker
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
                    int w = int.Parse(splits[0]);
                    int h = int.Parse(splits[2]);

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

        public static ImageType GetImageType(string path)
        {
            const byte MAX_BYTES = 8;
            byte[] header = null;

            using (FileStream stream = TryGetFileStream(path))
            {
                if (stream == null)
                    return ImageType.ERROR;

                if (stream.Length < MAX_BYTES)
                    return ImageType.UNKNOWN;

                header = new byte[MAX_BYTES];
                stream.Read(header, 0, MAX_BYTES);
            }

            return CheckHeader(header);
        }

        /// <summary>
        /// Tries to open a filestream for reading.
        ///  May return null
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

        #region Image checks

        private static ImageType CheckHeader(byte[] header)
        {
            if (CheckJPEG(header))
                return ImageType.JPEG;

            if (CheckPNG(header))
                return ImageType.PNG;

            if (CheckBMP(header))
                return ImageType.BMP;

            if (CheckTIFF(header))
                return ImageType.TIFF;

            return ImageType.UNKNOWN;
        }

        private static bool CheckJPEG(byte[] header)
        {
            return header[0] == 0xFF && header[1] == 0xD8;
        }

        private static bool CheckPNG(byte[] header)
        {
            return header[0] == 0x89 && header[1] == 0x50 &&
                header[2] == 0x4E && header[3] == 0x47 &&
                header[4] == 0x0D && header[5] == 0x0A &&
                header[6] == 0x1A && header[7] == 0x0A;
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

    public class MosaicData
    {
        #region Properties

        public CheckedItemCollection Names { get; private set; }
        public Dictionary<string, string> NamePath { get; private set; }
        public Size ElementSize { get; private set; }
        public Bitmap LoadedImage { get; private set; }

        #endregion

        #region Constructors

        public MosaicData(CheckedItemCollection names,
            Dictionary<string, string> namePath, Size size, Bitmap img)
        {
            Names = names;
            NamePath = namePath;
            ElementSize = size;
            LoadedImage = img;
        }

        #endregion
    }

    public class ColorBlock
    {
        #region Properties

        public Color[,] PixelColors { get; private set; }
        public Color AverageColor { get; private set; }

        #endregion

        #region Constructors

        public ColorBlock(Color[,] pixelColors)
        {
            PixelColors = pixelColors;
            AverageColor = CalcAverageColor();
        }

        #endregion

        private Color CalcAverageColor()
        {
            int red = 0, green = 0, blue = 0;

            foreach (Color c in PixelColors)
            {
                red += c.R;
                green += c.G;
                blue += c.B;
            }

            red /= PixelColors.Length;
            green /= PixelColors.Length;
            blue /= PixelColors.Length;

            return Color.FromArgb(red, green, blue);
        }
    }

    public interface IClearable
    {
        void Clear();
    }

    public enum ImageType
    {
        ERROR = -1,
        JPEG,
        PNG,
        BMP,
        TIFF,
        UNKNOWN
    }
}