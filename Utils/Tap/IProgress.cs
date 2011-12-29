namespace System.Threading
{
    public interface IProgress<in T>
    {
        void Report(T value);
    }
}