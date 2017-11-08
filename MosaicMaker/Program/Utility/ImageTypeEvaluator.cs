using System.IO;

namespace MosaicMakerNS
{
    public static class ImageTypeEvaluator
    {
        /// <summary>
        /// Returns the ImageType of the given file
        /// </summary>
        public static ImageType GetImageType(string path)
        {
            const byte MAX_BYTES = 4;
            byte[] header = null;

            using (FileStream stream = StreamUtil.GetFileStream(path))
            {
                if (stream == null)
                    return ImageType.Error;

                if (stream.Length < MAX_BYTES)
                    return ImageType.Unknown;

                header = new byte[MAX_BYTES];
                stream.Read(header, 0, MAX_BYTES);
            }

            return CheckHeader(header);
        }

        #region Image checks

        /// <summary>
        /// Returns the ImageType based on magic numbers in the header
        /// </summary>
        private static ImageType CheckHeader(byte[] header)
        {
            if (CheckJPEG(header))
                return ImageType.Jpeg;

            if (CheckPNG(header))
                return ImageType.Png;

            if (CheckBMP(header))
                return ImageType.Bmp;

            if (CheckTIFF(header))
                return ImageType.Tiff;

            return ImageType.Unknown;
        }

        private static bool CheckJPEG(byte[] header)
        {
            return header[0] == 0xFF && header[1] == 0xD8;
        }

        private static bool CheckPNG(byte[] header)
        {
            return header[0] == 0x89 && header[1] == 0x50 &&
                header[2] == 0x4E && header[3] == 0x47;
        }

        private static bool CheckBMP(byte[] header)
        {
            return header[0] == 0x42 && header[1] == 0x4D;
        }

        private static bool CheckTIFF(byte[] header)
        {
            bool ii = header[0] == 0x49 && header[1] == 0x49 &&
                header[2] == 0x2A && header[3] == 0x00;
            bool mm = header[0] == 0x4D && header[1] == 0x4D &&
                header[2] == 0x00 && header[3] == 0x2A;

            return ii || mm;
        }

        #endregion
    }
}