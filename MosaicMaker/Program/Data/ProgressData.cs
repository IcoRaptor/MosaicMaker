using System.Drawing;

namespace MosaicMakerNS
{
    public sealed class ProgressData
    {
        #region Properties

        public ProgressDialog Dialog { get; private set; }
        public Size ImageSize { get; private set; }
        public Size ElementSize { get; private set; }
        public int Lines { get; private set; }
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