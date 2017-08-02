using System;
using System.Collections.Generic;
using System.Drawing;

namespace MosaicMakerNS
{
    public sealed class ImageSlicer : IParallelWorker
    {
        #region Variables

        private readonly object _handle = new object();
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

        private ColorBlock GetPixels(int line, int block)
        {
            int horizontal = line * _elementSize.Height;
            int vertical = block * _elementSize.Width;

            Color[,] pixels = new Color[_elementSize.Width, _elementSize.Height];

            for (int x = 0; x < _elementSize.Width; x++)
                for (int y = 0; y < _elementSize.Height; y++)
                    pixels[x, y] = _resizedImage.GetPixel(x + horizontal, y + vertical);

            return new ColorBlock(pixels);
        }

        #endregion

        public void ExecuteParallel()
        {
            Utility.EditImage(_resizedImage, DoStuff);

            if (Settings.MirrorModeVertical)
                SlicedImageLines.Reverse();

            if (Settings.MirrorModeHorizontal)
                foreach (var blockLine in SlicedImageLines)
                    blockLine.Reverse();
        }

        private unsafe void DoStuff(BitmapProperties bmpP)
        {
        }

        public void Clear()
        {
            _resizedImage.Dispose();
            SlicedImageLines.Clear();
        }
    }
}