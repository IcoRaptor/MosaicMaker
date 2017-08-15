using System;
using System.Collections.Generic;
using System.Drawing;
using static System.Windows.Forms.CheckedListBox;

namespace MosaicMakerNS
{
    /// <summary>
    /// Data used to initialize the mosaic workers
    /// </summary>
    public sealed class MosaicData
    {
        #region Properties

        /// <summary>
        /// The size of the mosaic elements
        /// </summary>
        public Size ElementSize { get; private set; }

        /// <summary>
        /// The loaded image
        /// </summary>
        public Bitmap LoadedImage { get; private set; }

        /// <summary>
        /// List of the paths to the mosaic elements
        /// </summary>
        public List<string> Paths { get; private set; }

        #endregion

        #region Constructors

        public MosaicData(CheckedItemCollection names,
            Dictionary<string, string> nameToPath, Size size, Bitmap bmp)
        {
            if (names == null)
                throw new ArgumentNullException("names");

            if (nameToPath == null)
                throw new ArgumentNullException("nameToPath");

            ElementSize = size;
            LoadedImage = bmp;

            Paths = new List<string>(names.Count);

            foreach (var n in names)
                Paths.Add(nameToPath[(string)n]);
        }

        #endregion
    }
}