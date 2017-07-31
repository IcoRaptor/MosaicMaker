using System.Drawing;

namespace MosaicMakerNS
{
    public sealed class ProgressData
    {
        #region Properties

        public ProgressWindow ProgWin { get; private set; }
        public int NumColumns { get; private set; }

        #endregion

        #region Constructors

        public ProgressData(ProgressWindow pWin, Size imgSize, Size elementSize)
        {
            ProgWin = pWin;
            NumColumns = imgSize.Width / elementSize.Width;
        }

        #endregion
    }
}