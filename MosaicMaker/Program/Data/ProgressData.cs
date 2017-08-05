using System.Drawing;

namespace MosaicMakerNS
{
    public sealed class ProgressData
    {
        #region Properties

        public ProgressDialog ProgDialog { get; private set; }
        public int Lines { get; private set; }

        #endregion

        #region Constructors

        public ProgressData(ProgressDialog pDialog, Size imgSize, Size elementSize)
        {
            ProgDialog = pDialog;
            Lines = imgSize.Height / elementSize.Height;
        }

        #endregion
    }
}