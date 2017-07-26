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

        private ProgressWindow _pWin;
        private List<string> _paths;
        private Size _newSize;

        #endregion

        #region Properties

        public Bitmap ResizedImage { get; private set; }
        public Size OrigSize { get; private set; }
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
            OrigSize = ResizedImage.Size;
            ElementPixels = new List<ColorBlock>();
            ElementSize = data.ElementSize;
        }

        #endregion

        public void Execute()
        {
            ResizedImage = Resize(ResizedImage, _newSize);
            _pWin.UpdateProgress(1);

            ResizeElements();
        }

        private void ResizeElements()
        {
            foreach (var path in _paths)
            {
                using (FileStream stream = Utility.TryGetFileStream(path))
                {
                    if (stream == null)
                        continue;

                    using (Bitmap bmp = Resize(Image.FromStream(stream),
                        ElementSize))
                    {
                        ElementPixels.Add(GetPixels(bmp));
                    }
                }

                _pWin.UpdateProgress(1);
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

        public static Bitmap Resize(Image image, Size size)
        {
            if (image == null)
                throw new ArgumentNullException("image");

            Bitmap destImage = null;

            try
            {
                destImage = new Bitmap(size.Width, size.Height);
                Rectangle destRect = new Rectangle(0, 0, size.Width, size.Height);

                destImage.SetResolution(image.HorizontalResolution,
                    image.VerticalResolution);

                using (Graphics g = SetupGraphics(destImage))
                {
                    g.DrawImage(image, destRect, 0, 0,
                        image.Width, image.Height, GraphicsUnit.Pixel);
                }
            }
            catch (Exception e)
            {
                if (destImage != null)
                    destImage.Dispose();

                throw e;
            }

            return destImage;
        }

        private static Graphics SetupGraphics(Image image)
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
            _pWin = null;
            _paths.Clear();
            ResizedImage = null;
            ElementPixels.Clear();
        }
    }
}