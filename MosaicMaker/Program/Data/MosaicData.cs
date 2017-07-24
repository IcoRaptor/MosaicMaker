using System.Collections.Generic;
using System.Drawing;
using static System.Windows.Forms.CheckedListBox;

namespace MosaicMakerNS
{
    public sealed class MosaicData
    {
        #region Properties

        public Size ElementSize { get; private set; }
        public Bitmap LoadedImage { get; private set; }
        public List<string> Paths { get; private set; }

        #endregion

        #region Constructors

        public MosaicData(CheckedItemCollection names,
            Dictionary<string, string> namePath, Size size, Bitmap img)
        {
            ElementSize = size;
            LoadedImage = img;

            Paths = new List<string>();

            foreach (var n in names)
                Paths.Add(namePath[(string)n]);
        }

        #endregion
    }
}