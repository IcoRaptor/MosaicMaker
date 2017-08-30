using System.Drawing.Imaging;

namespace MosaicMakerNS
{
    /// <summary>
    /// Helper to get the selected ImageFormat
    /// </summary>
    public class FormatResolver
    {
        #region Variables

        private const int _PNG = 1;
        private const int _JPG = 2;
        private const int _BMP = 3;
        private const int _TIF = 4;

        #endregion

        #region Properties

        public ImageFormat Format { get; private set; }

        #endregion

        #region Constructors

        public FormatResolver(int filterIndex)
        {
            switch (filterIndex)
            {
                case _PNG:
                    Format = ImageFormat.Png;
                    break;

                case _JPG:
                    Format = ImageFormat.Jpeg;
                    break;

                case _BMP:
                    Format = ImageFormat.Bmp;
                    break;

                case _TIF:
                    Format = ImageFormat.Tiff;
                    break;

                default:
                    Format = ImageFormat.Png;
                    break;
            }
        }

        #endregion
    }
}