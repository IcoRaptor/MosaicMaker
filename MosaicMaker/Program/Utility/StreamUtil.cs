using System.IO;

namespace MosaicMakerNS
{
    public static class StreamUtil
    {
        /// <summary>
        /// Returns a stream for reading.
        ///  May return null
        /// </summary>
        public static FileStream GetFileStream(string path)
        {
            try
            {
                return new FileStream(path, FileMode.Open,
                    FileAccess.Read, FileShare.Read);
            }
            catch
            {
                return null;
            }
        }
    }
}