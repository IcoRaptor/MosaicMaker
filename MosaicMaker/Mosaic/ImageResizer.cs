using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace MosaicMakerNS
{
    public sealed class ImageResizer : IMosaicWorker
    {
        #region Variables

        private readonly List<string> _paths;
        private readonly Size _newSize;
        private readonly ProgressWindow _pWin;

        #endregion

        #region Properties

        public Bitmap ResizedImage { get; private set; }
        public Size OriginalSize { get; private set; }
        public List<ColorBlock> ElementPixels { get; private set; }
        public Size ElementSize { get; private set; }

        #endregion

        #region Constructors

        public ImageResizer(MosaicData data, Size newSize, ProgressWindow pWin)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            _paths = data.Paths;
            _newSize = newSize;
            _pWin = pWin;

            ResizedImage = data.LoadedImage;
            OriginalSize = ResizedImage.Size;
            ElementPixels = new List<ColorBlock>();
            ElementSize = data.ElementSize;
        }

        #endregion

        public void Execute()
        {
            ResizedImage = Resize(ResizedImage, _newSize);

            foreach (var path in _paths)
            {
                ResizeElement(path);
                _pWin.IncrementProgress();
            }
        }

        private void ResizeElement(string path)
        {
            using (FileStream stream = Utility.TryGetFileStream(path))
            {
                if (stream == null)
                    return;

                using (Bitmap bmp = Resize(Image.FromStream(stream),
                    ElementSize))
                {
                    ElementPixels.Add(GetPixels(bmp));
                }
            }
        }

        private static ColorBlock GetPixels(Bitmap bmp)
        {
            Color[,] pixels = new Color[bmp.Width, bmp.Height];

            for (int x = 0; x < bmp.Width; x++)
                for (int y = 0; y < bmp.Height; y++)
                    pixels[x, y] = bmp.GetPixel(x, y);

            return new ColorBlock(pixels);
        }

        public static Bitmap Resize(Image img, Size size)
        {
            if (img == null)
                throw new ArgumentNullException("img");

            Bitmap bmp = null;

            try
            {
                Rectangle rect = new Rectangle(0, 0, size.Width, size.Height);
                bmp = new Bitmap(size.Width, size.Height);
                bmp.SetResolution(img.HorizontalResolution,
                    img.VerticalResolution);

                using (Graphics g = SetupGraphics(bmp))
                {
                    g.DrawImage(img, rect, 0, 0, img.Width,
                        img.Height, GraphicsUnit.Pixel);
                }
            }
            catch
            {
                if (bmp != null)
                    bmp.Dispose();

                throw;
            }

            return bmp;
        }

        private static Graphics SetupGraphics(Image img)
        {
            Graphics g = Graphics.FromImage(img);

            g.CompositingMode = CompositingMode.SourceCopy;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            return g;
        }

        public void Clear()
        {
            _paths.Clear();
            ResizedImage.Dispose();
            ElementPixels.Clear();
        }
    }
}