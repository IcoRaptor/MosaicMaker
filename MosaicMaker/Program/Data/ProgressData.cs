using System.Drawing;

namespace MosaicMakerNS
{
    /// <summary>
    /// Data used by the mosaic workers
    /// </summary>
    public sealed class ProgressData
    {
        #region Properties

        /// <summary>
        /// Reference to the ProgressDialog
        /// </summary>
        public ProgressDialog Dialog { get; private set; }

        /// <summary>
        /// The size of the resized image
        /// </summary>
        public Size ImageSize { get; private set; }

        /// <summary>
        /// The size of the mosaic elements
        /// </summary>
        public Size ElementSize { get; private set; }

        /// <summary>
        /// The number of BlockLines in the image
        /// </summary>
        public int Lines { get; private set; }

        /// <summary>
        /// The number of ColorBlocks in a BlockLine
        /// </summary>
        public int Columns { get; private set; }

        #endregion

        #region Constructors

        public ProgressData(ProgressDialog dialog, Size imgSize, Size elementSize)
        {
            Dialog = dialog;
            ImageSize = imgSize;
            ElementSize = elementSize;
            Lines = imgSize.Height / elementSize.Height;
            Columns = imgSize.Width / elementSize.Width;
        }

        #endregion
    }
}