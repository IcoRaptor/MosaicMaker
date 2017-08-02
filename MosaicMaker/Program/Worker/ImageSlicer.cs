using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace MosaicMakerNS
{
    public sealed class ImageSlicer : IParallelWorker
    {
        #region Variables

        private readonly object _handle = new object();
        private readonly object _handle2 = new object();
        private readonly ProgressData _pData;
        private readonly Bitmap _resizedImage;
        private readonly Size _elementSize;
        private readonly int _lines;
        private readonly int _blocksPerLine;

        #endregion

        #region Properties

        public List<BlockLine> SlicedImageLines { get; private set; }

        #endregion

        #region Constructors

        public ImageSlicer(Bitmap resizedImage, Size elementSize, ProgressData pData)
        {
            _resizedImage = resizedImage ??
                throw new ArgumentNullException("resizedImage");

            _pData = pData ??
                throw new ArgumentNullException("pData");

            _elementSize = elementSize;

            _lines = resizedImage.Size.Height / elementSize.Height;
            _blocksPerLine = resizedImage.Size.Width / elementSize.Width;

            SlicedImageLines = new List<BlockLine>();
        }

        #endregion

        #region Legacy

        public void Execute()
        {
            for (int line = 0; line < _lines; line++)
            {
                SlicedImageLines.Add(GetBlockLine(line));
                _pData.ProgWin.IncrementProgress();
            }
        }

        private BlockLine GetBlockLine(int line)
        {
            BlockLine blockLine = new BlockLine();

            for (int block = 0; block < _blocksPerLine; block++)
                blockLine.Add(GetPixels(line, block));

            return blockLine;
        }

        private ImageBlock GetPixels(int line, int block)
        {
            int horizontal = line * _elementSize.Height;
            int vertical = block * _elementSize.Width;

            Color[,] pixels = new Color[_elementSize.Width, _elementSize.Height];

            for (int x = 0; x < _elementSize.Width; x++)
                for (int y = 0; y < _elementSize.Height; y++)
                    pixels[x, y] = _resizedImage.GetPixel(x + horizontal, y + vertical);

            return new ImageBlock(new Bitmap(_elementSize.Width, _elementSize.Height));
        }

        #endregion

        public void ExecuteParallel()
        {
            /*Parallel.For(0, _lines, line =>
            {
                BlockLine blockLine = GenerateBlockLine(line);
                _pData.ProgWin.IncrementProgress();

                lock (_handle)
                {
                    SlicedImageLines.Add(blockLine);
                }
            });*/

            for (int i = 0; i < _lines; i++)
            {
                BlockLine blockLine = GenerateBlockLine(i);
                _pData.ProgWin.IncrementProgress();
                SlicedImageLines.Add(blockLine);
            }

            if (Settings.MirrorModeVertical)
                SlicedImageLines.Reverse();

            if (Settings.MirrorModeHorizontal)
                foreach (var blockLine in SlicedImageLines)
                    blockLine.Reverse();
        }

        private BlockLine GenerateBlockLine(int line)
        {
            BlockLine blockLine = new BlockLine();

            for (int block = 0; block < _blocksPerLine; block++)
                blockLine.Add(new ImageBlock(CropImage(line, block)));

            return blockLine;
        }

        private Bitmap CropImage(int line, int block)
        {
            int offsetX = block * _elementSize.Width;
            int offsetY = line * _elementSize.Height;

            Rectangle crop = new Rectangle(new Point(offsetX, offsetY),
                _elementSize);

            Bitmap bmp = new Bitmap(_elementSize.Width, _elementSize.Height,
              PixelFormat.Format24bppRgb);

            return bmp.Clone(crop, bmp.PixelFormat);
        }

        public void Clear()
        {
            _resizedImage.Dispose();
            SlicedImageLines.Clear();
        }
    }
}