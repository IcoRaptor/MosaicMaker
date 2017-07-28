namespace MosaicMakerNS
{
    public static class Settings
    {
        #region Variables

        private static BuildMode _buildMode = BuildMode.Default;

        #endregion

        public static void ToggleBuildMode()
        {
            _buildMode = _buildMode == BuildMode.Default ?
                BuildMode.Mirrored : BuildMode.Default;
        }

        public static bool MirrorImage
        {
            get { return _buildMode == BuildMode.Mirrored; }
        }
    }
}