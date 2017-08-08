namespace MosaicMakerNS
{
    public static class Settings
    {
        #region Variables

        private static MirrorMode _mirrorMode = MirrorMode.Default;
        private static bool _negative = false;

        #endregion

        #region Properties

        public static bool NegativeImage { get { return _negative; } }

        public static bool MirrorImage
        {
            get { return MirrorModeHorizontal || MirrorModeVertical; }
        }

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

        public static void ToggleNegativeImage()
        {
            _negative = !_negative;
        }
    }
}