using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace MosaicMakerNS
{
    public sealed class ImageResizer : IParallelWorker
    {
        #region Variables

        private readonly object _handle = new object();
        private readonly ProgressData _pData;
        private readonly List<string> _paths;
        private readonly Size _newSize;

        #endregion

        #region Properties

        public Bitmap ResizedImage { get; private set; }
        public Size OriginalSize { get; private set; }
        public List<ImageBlock> ElementPixels { get; private set; }
        public Size ElementSize { get; private set; }

        #endregion

        #region Constructors

        public ImageResizer(MosaicData mData, Size newSize, ProgressData pData)
        {
            if (mData == null)
                throw new ArgumentNullException("mData");

            _pData = pData ??
                throw new ArgumentNullException("pData");

            _paths = mData.Paths;
            _newSize = newSize;

            ResizedImage = mData.LoadedImage;
            OriginalSize = ResizedImage.Size;
            ElementPixels = new List<ImageBlock>();
            ElementSize = mData.ElementSize;
        }

        #endregion

        public void ExecuteParallel()
        {
            ResizedImage = Resize(ResizedImage, _newSize);

            Parallel.ForEach(_paths, path =>
            {
                ResizeElement(path);
                _pData.ProgWin.IncrementProgress();
            });
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
                    ImageBlock block = new ImageBlock(bmp);

                    lock (_handle)
                    {
                        ElementPixels.Add(block);
                    }
                }
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

                using (Graphics g = Utility.SetupGraphics(bmp))
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

        public void Clear()
        {
            _paths.Clear();
            ResizedImage.Dispose();
            ElementPixels.Clear();
        }
    }
}