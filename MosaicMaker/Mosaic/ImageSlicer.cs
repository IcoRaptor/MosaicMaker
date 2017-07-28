using System;
using System.Collections.Generic;
using System.Drawing;

namespace MosaicMakerNS
{
    public sealed class ImageSlicer : IMosaicWorker
    {
        #region Variables

        private Bitmap _resizedImage;
        private Size _elementSize;
        private ProgressWindow _pWin;
        private int _columns;
        private int _blocksPerColumn;

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
                _pWin.UpdateProgress(1);
            }

            if (Settings.MirrorImage)
                SlicedImageColumns.Reverse();
        }

        private BlockColumn GetBlockColumn(int col)
        {
            BlockColumn blockLine = new BlockColumn();

            for (int block = 0; block < _blocksPerColumn; block++)
                blockLine.Add(GetPixels(col, block));

            return blockLine;
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
            _pWin = null;
            _resizedImage.Dispose();
            SlicedImageColumns.Clear();
        }
    }
}