using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace MosaicMaker
{
    public class ImageResizer : IDisposable
    {
        #region Variables

        private List<string> _paths;
        private Bitmap _image;

        #endregion

        #region Properties

        public Color[,] ImagePixels { get; private set; }
        public Size OrigSize { get; private set; }
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
        }

        #endregion

        /// <summary>
        /// Resizes all images
        /// </summary>
        public void Resize()
        {
            // Resize loaded image
            Size size = Utility.GetNewImageSize(_image, ElementSize);
            using (_image = ResizeImage(_image, size))
            {
                ImagePixels = new Color[_image.Width, _image.Height];
                for (int x = 0; x < _image.Width; x++)
                    for (int y = 0; y < _image.Height; y++)
                        ImagePixels[x, y] = _image.GetPixel(x, y);
            }

            // Resize mosaic elements
            foreach (var p in _paths)
            {
                using (FileStream s = Utility.TryGetFileStream(p))
                {
                    if (s == null)
                        continue;

                    using (Bitmap bmp = ResizeImage(
                        Image.FromStream(s), ElementSize))
                    {
                        Color[,] pixels = new Color[bmp.Width, bmp.Height];

                        for (int x = 0; x < bmp.Width; x++)
                        {
                            for (int y = 0; y < bmp.Height; y++)
                            {
                                pixels[x, y] = bmp.GetPixel(x, y);
                                ElementPixels.Add(pixels);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Resizes an image to the given size
        /// </summary>
        public Bitmap ResizeImage(Image image, Size size)
        {
            Bitmap destImage = new Bitmap(size.Width, size.Height);
            Rectangle destRect = new Rectangle(0, 0, size.Width, size.Height);

            destImage.SetResolution(image.HorizontalResolution,
                image.VerticalResolution);

            using (Graphics g = Graphics.FromImage(destImage))
            {
                g.CompositingMode = CompositingMode.SourceCopy;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (ImageAttributes attrs = new ImageAttributes())
                {
                    attrs.SetWrapMode(WrapMode.TileFlipXY);
                    g.DrawImage(image, destRect, 0, 0,
                        image.Width, image.Height,
                        GraphicsUnit.Pixel, attrs);
                }
            }

            return destImage;
        }

        public void Dispose()
        {
            ImagePixels = null;
            ElementPixels.Clear();
            _paths.Clear();
        }
    }
}