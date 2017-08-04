namespace MosaicMakerNS
{
    public interface IParallelWorker : IClearable
    {
        void ExecuteParallel();
    }

    public interface IMosaicWorker : IClearable
    {
        void Execute();
    }

    public interface IClearable
    {
        void Clear();
    }
}