using System.Collections.Generic;
using System.Drawing;

namespace MosaicMakerNS
{
    public sealed class ImageBuilder : IMosaicWorker
    {
        #region Variables

        private ProgressWindow _pWin;
        private List<List<ColorBlock>> _newImageLines;
        private Size _elementSize;

        #endregion

        #region Properties

        public Bitmap FinalImage { get; private set; }

        #endregion

        #region Constructors

        public ImageBuilder(Size imageSize, Size elementSize,
            List<List<ColorBlock>> newImageLines, ProgressWindow pWin)
        {
            _elementSize = elementSize;
            _newImageLines = newImageLines;
            _pWin = pWin;

            FinalImage = new Bitmap(imageSize.Width, imageSize.Height);
        }

        #endregion

        public void Execute()
        {
            for (int line = 0; line < _newImageLines.Count; line++)
            {
                List<ColorBlock> blockLine = _newImageLines[line];

                for (int block = 0; block < blockLine.Count; block++)
                    FillImageBlock(line, block, blockLine[block]);

                _pWin.UpdateProgress(1);
            }
        }

        private void FillImageBlock(int line, int block, ColorBlock colorBlock)
        {
            int horizontal = line * _elementSize.Width;
            int vertical = block * _elementSize.Height;

            for (int x = 0; x < _elementSize.Width; x++)
            {
                for (int y = 0; y < _elementSize.Height; y++)
                {
                    FinalImage.SetPixel(x + horizontal, y + vertical,
                        colorBlock.PixelColors[x, y]);
                }
            }
        }

        public void Clear()
        {
            _newImageLines.Clear();
            _pWin = null;
            FinalImage = null;
        }
    }
}