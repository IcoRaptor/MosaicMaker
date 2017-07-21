using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;

namespace MosaicMaker
{
    public class ImageSlicer : IClearable
    {
        #region Variables

        private Bitmap _image;
        private int _numVerticalLines;
        private int _numBlocksPerLine;
        private Size _elementSize;

        #endregion

        #region Properties

        public List<List<ColorBlock>> SlicedImage { get; private set; }

        #endregion

        #region Constructors

        public ImageSlicer(Bitmap image, Size elementSize)
        {
            Debug.Assert(image.Size.Width % elementSize.Width == 0);
            Debug.Assert(image.Size.Height % elementSize.Height == 0);

            _image = image;
            _elementSize = elementSize;

            _numVerticalLines = image.Size.Width / elementSize.Width;
            _numBlocksPerLine = image.Size.Height / elementSize.Height;

            SlicedImage = new List<List<ColorBlock>>();
        }

        #endregion

        public void SliceImage()
        {
            for (int line = 0; line < _numVerticalLines; line++)
                SlicedImage.Add(GetBlocks(line));
        }

        private List<ColorBlock> GetBlocks(int line)
        {
            List<ColorBlock> lineBlocks = new List<ColorBlock>();

            for (int block = 0; block < _numBlocksPerLine; block++)
                lineBlocks.Add(GetColorBlock(line, block));

            return lineBlocks;
        }

        private ColorBlock GetColorBlock(int line, int block)
        {
            int horizontal = line * _elementSize.Width;
            int vertical = block * _elementSize.Height;

            Color[,] pixels = new Color[_elementSize.Width, _elementSize.Height];

            for (int x = 0; x < _elementSize.Width; x++)
                for (int y = 0; y < _elementSize.Height; y++)
                    pixels[x, y] = _image.GetPixel(x + horizontal, y + vertical);

            return new ColorBlock(pixels);
        }

        public void Clear()
        {
            _image = null;
            SlicedImage.Clear();
        }
    }
}