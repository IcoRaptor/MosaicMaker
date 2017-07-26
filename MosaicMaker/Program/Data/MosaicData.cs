using System;
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
            Dictionary<string, string> nameToPath, Size size, Bitmap img)
        {
            if (names == null)
                throw new ArgumentNullException("names");

            if (nameToPath == null)
                throw new ArgumentNullException("nameToPath");

            ElementSize = size;
            LoadedImage = img;

            Paths = new List<string>();

            foreach (var n in names)
                Paths.Add(nameToPath[(string)n]);
        }

        #endregion
    }
}