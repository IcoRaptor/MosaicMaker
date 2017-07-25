﻿using System.Collections.Generic;
using System.Drawing;

namespace MosaicMakerNS
{
    public sealed class ImageSlicer : IMosaicWorker
    {
        #region Variables

        private Bitmap _resizedImage;
        private int _numVerticalLines;
        private int _numBlocksPerLine;
        private Size _elementSize;
        private ProgressWindow _pWin;

        #endregion

        #region Properties

        public List<List<Color[,]>> SlicedImageLines { get; private set; }

        #endregion

        #region Constructors

        public ImageSlicer(Bitmap resizedImage, Size elementSize, ProgressWindow pWin)
        {
            _resizedImage = resizedImage;
            _elementSize = elementSize;
            _pWin = pWin;

            _numVerticalLines = resizedImage.Size.Width / elementSize.Width;
            _numBlocksPerLine = resizedImage.Size.Height / elementSize.Height;

            SlicedImageLines = new List<List<Color[,]>>();
        }

        #endregion

        public void Execute()
        {
            for (int line = 0; line < _numVerticalLines; line++)
            {
                SlicedImageLines.Add(GetBlocks(line));
                _pWin.UpdateProgress(1, null);
            }
        }

        private List<Color[,]> GetBlocks(int line)
        {
            List<Color[,]> blockLine = new List<Color[,]>();

            for (int block = 0; block < _numBlocksPerLine; block++)
                blockLine.Add(GetPixels(line, block));

            return blockLine;
        }

        private Color[,] GetPixels(int line, int block)
        {
            int horizontal = line * _elementSize.Width;
            int vertical = block * _elementSize.Height;

            Color[,] pixels = new Color[_elementSize.Width, _elementSize.Height];

            for (int x = 0; x < _elementSize.Width; x++)
                for (int y = 0; y < _elementSize.Height; y++)
                    pixels[x, y] = _resizedImage.GetPixel(x + horizontal, y + vertical);

            return pixels;
        }

        public void Clear()
        {
            _pWin = null;
            _resizedImage = null;
            SlicedImageLines.Clear();
        }
    }
}