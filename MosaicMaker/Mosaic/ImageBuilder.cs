using System.Collections.Generic;
using System.Drawing;

namespace MosaicMakerNS
{
    public sealed class ImageBuilder : IMosaicWorker
    {
        #region Variables

        private const int _REMAINING_PROGRESS = 50000;

        private ProgressWindow _pWin;
        private List<List<ColorBlock>> _newImage;
        private Size _elementSize;

        #endregion

        #region Properties

        public Bitmap FinalImage { get; private set; }

        #endregion

        #region Constructors

        public ImageBuilder(Bitmap image, Size elementSize,
            List<List<ColorBlock>> newImage, ProgressWindow pWin)
        {
            _elementSize = elementSize;
            _newImage = newImage;
            _pWin = pWin;

            FinalImage = new Bitmap(image.Width, image.Height);
        }

        #endregion

        public void Execute()
        {
            int progress = _REMAINING_PROGRESS / _newImage.Count;
            progress = progress < 1 ? 1 : progress;

            for (int line = 0; line < _newImage.Count; line++)
            {
                List<ColorBlock> blockLine = _newImage[line];

                for (int block = 0; block < blockLine.Count; block++)
                    FillImageBlock(line, block, blockLine[block]);

                _pWin.UpdateProgress(progress, null);
            }
        }

        private void FillImageBlock(int line, int block, ColorBlock color)
        {
            int horizontal = line * _elementSize.Width;
            int vertical = block * _elementSize.Height;

            for (int x = 0; x < _elementSize.Width; x++)
            {
                for (int y = 0; y < _elementSize.Height; y++)
                {
                    FinalImage.SetPixel(x + horizontal, y + vertical,
                        color.PixelColors[x, y]);
                }
            }
        }

        public void Clear()
        {
            _newImage.Clear();
            _pWin = null;
            FinalImage = null;
        }
    }
}