using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace MosaicMakerNS
{
    public sealed class ImageResizer : IMosaicWorker
    {
        #region Variables

        private List<string> _paths;

        #endregion

        #region Properties

        public Bitmap ResizedImage { get; private set; }
        public Size OrigSize { get; private set; }
        public List<Color[,]> ElementPixels { get; private set; }
        public Size ElementSize { get; private set; }

        #endregion

        #region Constructors

        public ImageResizer(MosaicData data)
        {
            _paths = data.Paths;

            ResizedImage = data.LoadedImage;
            OrigSize = ResizedImage.Size;
            ElementPixels = new List<Color[,]>();
            ElementSize = data.ElementSize;
        }

        #endregion

        public void Execute()
        {
            ResizedImage = Resize(ResizedImage,
               Utility.GetNewImageSize(ResizedImage, ElementSize));

            ResizeElements();
        }

        private void ResizeElements()
        {
            foreach (var p in _paths)
            {
                using (FileStream s = Utility.TryGetFileStream(p))
                {
                    if (s == null)
                        continue;

                    using (Bitmap bmp = Resize(
                        Image.FromStream(s), ElementSize))
                    {
                        Color[,] pixels = new Color[bmp.Width, bmp.Height];

                        for (int x = 0; x < bmp.Width; x++)
                            for (int y = 0; y < bmp.Height; y++)
                                pixels[x, y] = bmp.GetPixel(x, y);

                        ElementPixels.Add(pixels);
                    }
                }
            }
        }

        public Bitmap Resize(Image image, Size size)
        {
            Bitmap destImage = new Bitmap(size.Width, size.Height);
            Rectangle destRect = new Rectangle(0, 0, size.Width, size.Height);

            destImage.SetResolution(image.HorizontalResolution,
                image.VerticalResolution);

            using (Graphics g = SetupGraphics(destImage))
            {
                g.DrawImage(image, destRect, 0, 0,
                    image.Width, image.Height, GraphicsUnit.Pixel);
            }

            return destImage;
        }

        private Graphics SetupGraphics(Image image)
        {
            Graphics g = Graphics.FromImage(image);

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
            ResizedImage = null;
            ElementPixels.Clear();
        }
    }
}