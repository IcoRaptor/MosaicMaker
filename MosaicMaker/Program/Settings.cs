namespace MosaicMakerNS
{
    public static class Settings
    {
        #region Variables

        private static MirrorMode _mirrorMode = MirrorMode.Default;

        #endregion

        #region Properties

        public static bool PowerMode
        {
            get { return false; }
        }

        public static bool MirrorModeHorizontal
        {
            get
            {
                return _mirrorMode == MirrorMode.MirrorHorizontal ||
                  _mirrorMode == MirrorMode.FullMirror;
            }
        }

        public static bool MirrorModeVertical
        {
            get
            {
                return _mirrorMode == MirrorMode.MirrorVertical ||
                  _mirrorMode == MirrorMode.FullMirror;
            }
        }

        #endregion

        public static void SetMirrorMode(MirrorMode mode)
        {
            _mirrorMode = mode;
        }
    }
}