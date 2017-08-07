using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace MosaicMakerNS
{
    public sealed class ImageSlicer : IMosaicWorker, ISettings
    {
        #region Variables

        private readonly ProgressData _pData;
        private readonly Bitmap _resizedImage;
        private readonly int _width;
        private readonly int _height;

        #endregion

        #region Properties

        public List<BlockLine> SlicedImageLines { get; private set; }

        #endregion

        #region Constructors

        public ImageSlicer(Bitmap resizedImage, ProgressData pData)
        {
            _resizedImage = resizedImage ??
                throw new ArgumentNullException("resizedImage");

            _pData = pData ??
                throw new ArgumentNullException("pData");

            _width = _pData.ElementSize.Width;
            _height = _pData.ElementSize.Height;

            SlicedImageLines = new List<BlockLine>(_pData.Lines);
        }

        #endregion

        public void Execute()
        {
            Utility.EditBitmap(_resizedImage, SliceImage);
            ApplySettings();
        }

        private unsafe void SliceImage(BitmapProperties ppts)
        {
            List<int> steps = Utility.GetSteps(ppts.HeightInPixels, _height);

            for (int i = 0; i < steps.Count; i++)
                SlicedImageLines.Add(null);

            byte* ptr = (byte*)ppts.Scan0;

            Parallel.ForEach(steps, y =>
            {
                byte*[] lines = new byte*[_height];

                for (int i = 0; i < _height; i++)
                    lines[i] = ptr + (y + i) * ppts.Stride;

                int index = y / _height;
                SlicedImageLines[index] = GetBlockLine(lines, ppts);
                _pData.Dialog.IncrementProgress();
            });
        }

        private unsafe BlockLine GetBlockLine(byte*[] lines, BitmapProperties ppts)
        {
            BlockLine blockLine = new BlockLine(_pData.Columns);

            int step = _width * ppts.BytesPerPixel;

            for (int offset = 0; offset < ppts.WidthInBytes; offset += step)
                blockLine.Add(GetPixels(lines, offset, step, ppts.BytesPerPixel));

            return blockLine;
        }

        private unsafe ColorBlock GetPixels(byte*[] lines, int offset, int step, int bpp)
        {
            Color[,] pixels = new Color[_width, _height];

            for (int y = 0; y < lines.Length; y++)
            {
                byte* block = lines[y] + offset;

                for (int x = 0; x < step; x += bpp)
                {
                    int red = block[x + 2];
                    int green = block[x + 1];
                    int blue = block[x + 0];

                    pixels[x / bpp, y] = Color.FromArgb(255, red, green, blue);
                }
            }

            return new ColorBlock(pixels);
        }

        public void Clear()
        {
            _resizedImage.Dispose();
            SlicedImageLines.Clear();
        }

        public void ApplySettings()
        {
            if (Settings.MirrorModeVertical)
                SlicedImageLines.Reverse();

            if (Settings.MirrorModeHorizontal)
                foreach (var blockLine in SlicedImageLines)
                    blockLine.Reverse();
        }
    }
}