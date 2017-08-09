namespace MosaicMakerNS
{
    public static class Settings
    {
        #region Variables

        private static MirrorMode _mirrorMode = MirrorMode.Default;
        private static bool _negative = false;
        private static bool _pixelImage = false;
        private static bool _pixelStrip = false;
        private static bool _useElementWidth = true;

        #endregion

        #region Properties

        public static bool Pixelate
        {
            get { return _pixelImage || _pixelStrip; }
        }

        public static bool PixelStrip { get { return _pixelStrip; } }

        public static bool PixelStripUsesElementWidth { get { return _useElementWidth; } }

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

        public static void TogglePixelImage()
        {
            _pixelImage = !_pixelImage;

            if (_pixelImage)
                _pixelStrip = false;
        }

        public static void SetPixelStrip(bool value)
        {
            _pixelStrip = value;

            if (_pixelStrip)
                _pixelImage = false;
        }

        public static void SetUseElementWidth(bool value)
        {
            _useElementWidth = value;
        }
    }
}