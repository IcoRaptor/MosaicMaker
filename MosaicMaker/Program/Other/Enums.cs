namespace MosaicMakerNS
{
    /// <summary>
    /// Indicates the type of an image
    /// </summary>
    public enum ImageType
    {
        Error = -1,
        Jpeg,
        Png,
        Bmp,
        Tiff,
        Unknown
    }

    /// <summary>
    /// Indicates how the image should be mirrored
    /// </summary>
    public enum MirrorMode
    {
        Default,
        Horizontal,
        Vertical,
        Full
    }

    /// <summary>
    /// Incicates the type of a ColorBlock
    /// </summary>
    public enum AverageMode
    {
        Pixel,
        Element
    }

    /// <summary>
    /// Indicates if a BlockLine should be prefilled
    /// </summary>
    public enum LineFillMode
    {
        Default,
        FillNull
    }
}