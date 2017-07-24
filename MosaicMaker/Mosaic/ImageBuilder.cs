using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace MosaicMaker
{
    public sealed class ImageBuilder : IClearable, IExecutable
    {
        #region Properties

        public Bitmap FinalImage { get; private set; }

        #endregion

        #region Constructors

        public ImageBuilder(Bitmap image, List<List<ColorBlock>> newImage,
            DoWorkEventArgs e)
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