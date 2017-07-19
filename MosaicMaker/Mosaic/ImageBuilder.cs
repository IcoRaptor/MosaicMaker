using System;
using System.Collections.Generic;
using System.Drawing;

namespace MosaicMaker
{
    public class ImageBuilder : IClearable
    {
        #region Properties

        public Bitmap FinishedImage { get; set; }

        #endregion

        #region Constructors

        public ImageBuilder()
        {
        }

        #endregion

        public void Clear()
        {
            FinishedImage = null;
        }
    }
}