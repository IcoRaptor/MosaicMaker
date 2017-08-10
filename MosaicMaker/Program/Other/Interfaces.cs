namespace MosaicMakerNS
{
    public interface IMosaicWorker : IClearable
    {
        void Execute();
    }

    public interface IClearable
    {
        void Clear();
    }
}