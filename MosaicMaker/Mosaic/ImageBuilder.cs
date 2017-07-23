using System.Collections.Generic;
using System.Drawing;

namespace MosaicMaker
{
    public sealed class ImageBuilder : IClearable, IExecutable
    {
        #region Properties

        public Bitmap FinalImage { get; private set; }

        #endregion

        #region Constructors

        public ImageBuilder(Bitmap image, List<List<ColorBlock>> newImage)
        {
            FinalImage = image;
        }

        #endregion

        public void Execute()
        {
        }

        public void Clear()
        {
            FinalImage = null;
        }
    }
}