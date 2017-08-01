namespace MosaicMakerNS
{
    public interface IMosaicWorker : IClearable, IExecutable { }

    public interface IClearable
    {
        void Clear();
    }

    public interface IExecutable
    {
        void Execute();
    }
}