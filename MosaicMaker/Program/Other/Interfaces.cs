namespace MosaicMakerNS
{
    /// <summary>
    /// Provides Clear and Execute functions
    /// </summary>
    public interface IMosaicWorker : IClearable
    {
        void Execute();
    }

    /// <summary>
    /// Provides a Clear function
    /// </summary>
    public interface IClearable
    {
        void Clear();
    }
}