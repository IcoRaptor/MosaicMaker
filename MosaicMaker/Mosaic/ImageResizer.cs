using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace MosaicMaker
{
    public class ImageResizer : IClearable
    {
        #region Variables

        private List<string> _paths;
        private Bitmap _image;

        #endregion

        #region Properties

        public Color[,] ImagePixels { get; private set; }
        public Size OrigSize { get; private set; }
        public Size NewSize { get; private set; }
        public List<Color[,]> ElementPixels { get; private set; }
        public Size ElementSize { get; private set; }

        #endregion

        #region Constructors

        public ImageResizer(List<string> paths, Size elementSize, Bitmap img)
        {
            _paths = paths;
            _image = img;

            ElementSize = elementSize;
            ElementPixels = new List<Color[,]>();
            OrigSize = _image.Size;
            NewSize = Utility.GetNewImageSize(_image, ElementSize);
        }

        #endregion

        public void ResizeAll()
        {
            ResizeLoadedImage();
            ResizeElements();
        }

        private void ResizeLoadedImage()
        {
            using (_image = Resize(_image, NewSize))
            {
                ImagePixels = new Color[_image.Width, _image.Height];
                for (int x = 0; x < _image.Width; x++)
                    for (int y = 0; y < _image.Height; y++)
                        ImagePixels[x, y] = _image.GetPixel(x, y);
            }
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
            ImagePixels = null;
            ElementPixels.Clear();
        }
    }
}