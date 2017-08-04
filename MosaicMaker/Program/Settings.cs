namespace MosaicMakerNS
{
    public static class Settings
    {
        #region Variables

        private static MirrorMode _mirrorMode = MirrorMode.Default;

        #endregion

        #region Properties

        public static bool PowerMode { get { return true; } }

        public static bool MirrorModeHorizontal
        {
            get
            {
                return _mirrorMode == MirrorMode.Horizontal ||
                  _mirrorMode == MirrorMode.Full;
            }
        }

        public static bool MirrorModeVertical
        {
            get
            {
                return _mirrorMode == MirrorMode.Vertical ||
                  _mirrorMode == MirrorMode.Full;
            }
        }

        #endregion

        public static void SetMirrorMode(MirrorMode mode)
        {
            _mirrorMode = mode;
        }
    }
}