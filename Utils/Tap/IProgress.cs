namespace System.Threading
{
    /// <summary>
    /// Interface included in .Net 4.5
    /// Use this one util it is released.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IProgress<in T>
    {
        void Report(T value);
    }
}