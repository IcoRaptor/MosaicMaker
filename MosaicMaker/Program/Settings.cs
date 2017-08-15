namespace MosaicMakerNS
{
    /// <summary>
    /// Global settings
    /// </summary>
    public static class Settings
    {
        #region Variables

        private static MirrorMode _mirrorMode = MirrorMode.Default;
        private static bool _pixelMode = false;
        private static bool _pixelStrip = false;
        private static bool _negative = false;
        private static bool _grayscale = false;

        #endregion

        #region Properties

        /// <summary>
        /// Is the image being mirrored
        /// </summary>
        public static bool MirrorImage
        {
            get { return MirrorModeHorizontal || MirrorModeVertical; }
        }

        /// <summary>
        /// Mirror the image horzontally
        /// </summary>
        public static bool MirrorModeHorizontal
        {
            get
            {
                return _mirrorMode == MirrorMode.Horizontal ||
                  _mirrorMode == MirrorMode.Full;
            }
        }

        /// <summary>
        /// Mirror the image vertically
        /// </summary>
        public static bool MirrorModeVertical
        {
            get
            {
                return _mirrorMode == MirrorMode.Vertical ||
                  _mirrorMode == MirrorMode.Full;
            }
        }

        /// <summary>
        /// Is the program in PixelMode
        /// </summary>
        public static bool PixelMode
        {
            get { return _pixelMode || _pixelStrip; }
        }

        /// <summary>
        /// Turn the image into a pixel strip
        /// </summary>
        public static bool PixelStrip { get { return _pixelStrip; } }

        /// <summary>
        /// Invert the colors
        /// </summary>
        public static bool NegativeImage { get { return _negative; } }

        /// <summary>
        /// Turn the image gray
        /// </summary>
        public static bool GrayscaleImage { get { return _grayscale; } }

        #endregion

        /// <summary>
        /// Sets the MirrorMode
        /// </summary>
        public static void SetMirrorMode(MirrorMode mode)
        {
            _mirrorMode = mode;
        }

        /// <summary>
        /// Toggle the PixelImage property
        /// </summary>
        public static void TogglePixelImage()
        {
            _pixelMode = !_pixelMode;

            if (_pixelMode)
                _pixelStrip = false;
        }

        /// <summary>
        /// Toggle the PixelStrip property
        /// </summary>
        public static void TogglePixelStrip()
        {
            _pixelStrip = !_pixelStrip;

            if (_pixelStrip)
                _pixelMode = false;
        }

        /// <summary>
        /// Toggle the NegativeImage property
        /// </summary>
        public static void ToggleNegativeImage()
        {
            _negative = !_negative;
        }

        /// <summary>
        /// Toggle the GrayscaleImage property
        /// </summary>
        public static void ToggleGrayscaleImage()
        {
            _grayscale = !_grayscale;
        }
    }
}