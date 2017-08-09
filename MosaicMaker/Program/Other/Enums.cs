namespace MosaicMakerNS
{
    public enum ImageType
    {
        Error = -1,
        Jpeg,
        Png,
        Bmp,
        Tiff,
        Unknown
    }

    public enum MirrorMode
    {
        Default,
        Horizontal,
        Vertical,
        Full
    }

    public enum AverageMode
    {
        Pixel,
        Element
    }
}