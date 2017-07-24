using System.Collections.Generic;
using System.Drawing;
using static System.Windows.Forms.CheckedListBox;

namespace MosaicMaker
{
    public sealed class MosaicData
    {
        #region Properties

        public CheckedItemCollection Names { get; private set; }
        public Dictionary<string, string> NamePath { get; private set; }
        public Size ElementSize { get; private set; }
        public Bitmap LoadedImage { get; private set; }

        #endregion

        #region Constructors

        public MosaicData(CheckedItemCollection names,
            Dictionary<string, string> namePath, Size size, Bitmap img)
        {
            Names = names;
            NamePath = namePath;
            ElementSize = size;
            LoadedImage = img;
        }

        #endregion
    }
}