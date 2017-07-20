using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;

namespace MosaicMaker
{
    public class ImageSlicer : IClearable
    {
        #region Variables

        private Color[,] _imagePixels;
        private int _numLines;
        private int _numBlocksPerLine;

        #endregion

        #region Properties

        public List<List<ColorBlock>> SlicedImageLines { get; private set; }

        #endregion

        #region Constructors

        public ImageSlicer(Color[,] imagePixels, Size imageSize, Size elementSize)
        {
            Debug.Assert(imageSize.Height % elementSize.Height == 0);
            Debug.Assert(imageSize.Width % elementSize.Width == 0);

            _imagePixels = imagePixels;
            _numLines = imageSize.Height / elementSize.Height;
            _numBlocksPerLine = imageSize.Width / elementSize.Width;

            SlicedImageLines = new List<List<ColorBlock>>();
        }

        #endregion

        public void SliceImage()
        {
            for (int line = 0; line < _numLines; line++)
            {
                List<ColorBlock> lineBlocks = new List<ColorBlock>();

                for (int block = 0; block < _numBlocksPerLine; block++)
                    lineBlocks.Add(GetColorBlock(line, block));

                SlicedImageLines.Add(lineBlocks);
            }

            System.Threading.Thread.Sleep(1000);
        }

        private ColorBlock GetColorBlock(int line, int block)
        {
            // TODO

            return null;
        }

        public void Clear()
        {
            _imagePixels = null;
            SlicedImageLines.Clear();
        }
    }
}