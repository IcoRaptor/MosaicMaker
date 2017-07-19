using System;
using System.Collections.Generic;

namespace MosaicMaker
{
    public class ImageSlicer : IClearable
    {
        #region Properties

        #endregion

        #region Constructors

        public ImageSlicer()
        {
        }

        #endregion

        public void Slice()
        {
            System.Threading.Thread.Sleep(1000);
        }

        public void Clear()
        {
        }
    }
}