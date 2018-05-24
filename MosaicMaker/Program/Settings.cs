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
            get { return _pixelMode || PixelStrip; }
        }

        /// <summary>
        /// Turn the image into a pixel strip
        /// </summary>
        public static bool PixelStrip { get; private set; } = false;

        /// <summary>
        /// Invert the colors
        /// </summary>
        public static bool NegativeImage { get; private set; } = false;

        /// <summary>
        /// Turn the image gray
        /// </summary>
        public static bool GrayscaleImage { get; private set; } = false;

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
                PixelStrip = false;
        }

        /// <summary>
        /// Toggle the PixelStrip property
        /// </summary>
        public static void TogglePixelStrip()
        {
            PixelStrip = !PixelStrip;

            if (PixelStrip)
                _pixelMode = false;
        }

        /// <summary>
        /// Toggle the NegativeImage property
        /// </summary>
        public static void ToggleNegativeImage()
        {
            NegativeImage = !NegativeImage;
        }

        /// <summary>
        /// Toggle the GrayscaleImage property
        /// </summary>
        public static void ToggleGrayscaleImage()
        {
            GrayscaleImage = !GrayscaleImage;
        }
    }
}