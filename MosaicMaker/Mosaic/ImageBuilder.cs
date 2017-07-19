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

        public void Build()
        {
            System.Threading.Thread.Sleep(1000);
        }

        public void Clear()
        {
            FinishedImage = null;
        }
    }
}