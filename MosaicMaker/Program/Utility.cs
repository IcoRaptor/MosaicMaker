using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.CheckedListBox;

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

            using (FileStream stream = TryGetFileStream(path))
            {
                if (stream == null)
                    return ImageType.ERROR;

                if (stream.Length < MAX_BYTES)
                    return ImageType.UNKNOWN;

                header = new byte[MAX_BYTES];
                stream.Read(header, 0, MAX_BYTES);
            }

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

        /// <summary>
        /// Tries to open the given path and returns the stream
        /// </summary>
        public static FileStream TryGetFileStream(string path)
        {
            FileStream stream;

            try
            {
                stream = new FileStream(path, FileMode.Open);
            }
            catch
            {
                stream = null;
            }

            return stream;
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

        /// <summary>
        /// Calculates the new image size based on the element size
        /// </summary>
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

    /// <summary>
    /// Data used to build the mosaic
    /// </summary>
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

    /// <summary>
    /// Represents a block of pixels
    /// </summary>
    public class ColorBlock
    {
        #region Properties

        public List<Color> PixelColors { get; private set; }
        public Color AverageColor { get; private set; }

        #endregion

        #region Constructors

        public ColorBlock(List<Color> pixelColors)
        {
            PixelColors = pixelColors;
            AverageColor = CalcAverage();
        }

        #endregion

        private Color CalcAverage()
        {
            int red = 0, green = 0, blue = 0;

            foreach (Color c in PixelColors)
            {
                red += c.R;
                green += c.G;
                blue += c.B;
            }

            red /= PixelColors.Count;
            green /= PixelColors.Count;
            blue /= PixelColors.Count;

            return Color.FromArgb(red, green, blue);
        }
    }

    /// <summary>
    /// Indicates what kind of image a file is
    /// </summary>
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