using System;
using System.Collections.Generic;
using System.Drawing;

namespace MosaicMakerNS
{
    public sealed class ImageSlicer : IMosaicWorker
    {
        #region Variables

        private readonly Bitmap _resizedImage;
        private readonly Size _elementSize;
        private readonly ProgressWindow _pWin;
        private readonly int _columns;
        private readonly int _blocksPerColumn;

        #endregion

        #region Properties

        public List<BlockColumn> SlicedImageColumns { get; private set; }

        #endregion

        #region Constructors

        public ImageSlicer(Bitmap resizedImage, Size elementSize, ProgressWindow pWin)
        {
            _resizedImage = resizedImage ??
                throw new ArgumentNullException("resizedImage");

            _elementSize = elementSize;
            _pWin = pWin;

            _columns = resizedImage.Size.Width / elementSize.Width;
            _blocksPerColumn = resizedImage.Size.Height / elementSize.Height;

            SlicedImageColumns = new List<BlockColumn>();
        }

        #endregion

        public void Execute()
        {
            for (int col = 0; col < _columns; col++)
            {
                SlicedImageColumns.Add(GetBlockColumn(col));
                _pWin.IncrementProgress();
            }

            if (Settings.MirrorModeHorizontal)
                SlicedImageColumns.Reverse();

            if (Settings.MirrorModeVertical)
                foreach (var blockCol in SlicedImageColumns)
                    blockCol.Reverse();
        }

        private BlockColumn GetBlockColumn(int col)
        {
            BlockColumn blockCol = new BlockColumn();

            for (int block = 0; block < _blocksPerColumn; block++)
                blockCol.Add(GetPixels(col, block));

            return blockCol;
        }

        private ColorBlock GetPixels(int col, int block)
        {
            int horizontal = col * _elementSize.Width;
            int vertical = block * _elementSize.Height;

            Color[,] pixels = new Color[_elementSize.Width, _elementSize.Height];

            for (int x = 0; x < _elementSize.Width; x++)
                for (int y = 0; y < _elementSize.Height; y++)
                    pixels[x, y] = _resizedImage.GetPixel(x + horizontal, y + vertical);

            return new ColorBlock(pixels);
        }

        public void Clear()
        {
            _resizedImage.Dispose();
            SlicedImageColumns.Clear();
        }
    }
}