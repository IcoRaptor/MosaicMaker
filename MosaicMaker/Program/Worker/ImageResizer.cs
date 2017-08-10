using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace MosaicMakerNS
{
    public sealed class ImageResizer : IMosaicWorker
    {
        #region Variables

        private readonly ProgressData _pData;
        private readonly List<string> _paths;

        #endregion

        #region Properties

        public Bitmap ResizedImage { get; private set; }
        public Size OriginalSize { get; private set; }
        public List<ColorBlock> ElementPixels { get; private set; }

        #endregion

        #region Constructors

        public ImageResizer(MosaicData mData, ProgressData pData)
        {
            if (mData == null)
                throw new ArgumentNullException("mData");

            _pData = pData ??
                throw new ArgumentNullException("pData");

            _paths = mData.Paths;

            ResizedImage = mData.LoadedImage;
            OriginalSize = ResizedImage.Size;
            ElementPixels = new List<ColorBlock>(_paths.Count);

            for (int i = 0; i < _paths.Count; i++)
                ElementPixels.Add(null);
        }

        #endregion

        public void Execute()
        {
            ResizedImage = Resize(ResizedImage, _pData.ImageSize);

            if (Settings.Pixelate)
                return;

            Parallel.For(0, _paths.Count, i =>
            {
                ResizeElement(_paths[i], i);
                _pData.Dialog.IncrementProgress();
            });
        }

        private void ResizeElement(string path, int index)
        {
            using (FileStream stream = Utility.GetFileStream(path))
            {
                if (stream == null)
                    return;

                using (Bitmap bmp = Resize(Image.FromStream(stream), _pData.ElementSize))
                    ElementPixels[index] = new ColorBlock(bmp);
            }
        }

        public static Bitmap Resize(Image img, Size size)
        {
            if (img == null)
                throw new ArgumentNullException("img");

            Bitmap bmp = null;

            try
            {
                Rectangle rect = new Rectangle(0, 0, size.Width, size.Height);
                bmp = new Bitmap(size.Width, size.Height, PixelFormat.Format24bppRgb);
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