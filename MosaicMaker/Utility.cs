﻿using System.IO;
using System.Windows.Forms;

namespace MosaicMaker
{
    /// <summary>
    /// Provides static utility functions
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// Checks if a file is an image
        /// </summary>
        public static ImageType GetImageType(string path)
        {
            const byte NUM_BYTES = 11;
            byte[] header = null;
            FileStream stream = null;

            try
            {
                stream = new FileStream(path, FileMode.Open);
            }
            catch
            {
                return ImageType.ERROR;
            }

            using (stream)
            {
                if (stream.Length < NUM_BYTES)
                    return ImageType.UNKNOWN;

                header = new byte[NUM_BYTES];
                stream.Read(header, 0, NUM_BYTES);
            }

            #region JPEG

            if (header[0] == 0xFF &&
                header[1] == 0xD8 && ((
                header[6] == 0x4A &&
                header[7] == 0x46 &&
                header[8] == 0x49 &&
                header[9] == 0x46) || (
                header[6] == 0x45 &&
                header[7] == 0x78 &&
                header[8] == 0x69 &&
                header[9] == 0x66)) &&
                header[10] == 0x00)
            {
                return ImageType.JPEG;
            }

            #endregion

            #region PNG

            if (header[0] == 0x89 &&
                header[1] == 0x50 &&
                header[2] == 0x4E &&
                header[3] == 0x47 &&
                header[4] == 0x0D &&
                header[5] == 0x0A &&
                header[6] == 0x1A &&
                header[7] == 0x0A)
            {
                return ImageType.PNG;
            }

            #endregion

            #region GIF

            if (header[0] == 0x47 &&
                header[1] == 0x49 &&
                header[2] == 0x46)
            {
                return ImageType.GIF;
            }

            #endregion

            #region BMP

            if (header[0] == 0x42 &&
                header[1] == 0x4D)
            {
                return ImageType.BMP;
            }

            #endregion

            #region TIFF

            if ((header[0] == 0x49 &&
                header[1] == 0x49 &&
                header[2] == 0x2A &&
                header[3] == 0x00) ||
                header[0] == 0x4D &&
                header[1] == 0x4D &&
                header[2] == 0x00 &&
                header[3] == 0x2A)
            {
                return ImageType.TIFF;
            }

            #endregion

            return ImageType.UNKNOWN;
        }

        /// <summary>
        /// Returns the selected mosaic element size
        /// </summary>
        public static int GetElementSize(params RadioButton[] args)
        {
            int res = 0;

            foreach (var rb in args)
            {
                if (!rb.Checked)
                    continue;

                res = int.Parse(rb.Text.Split(' ')[0]);
                break;
            }

            return res;
        }
    }

    /// <summary>
    /// Indicates if a file is an image
    /// </summary>
    public enum ImageType
    {
        ERROR = -1,
        JPEG,
        PNG,
        GIF,
        BMP,
        TIFF,
        UNKNOWN
    }
}