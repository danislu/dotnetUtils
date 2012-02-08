namespace Utils.Tap
{
    using System;

    public class ProgressReportedEventArgs<T> : EventArgs
    {
        public ProgressReportedEventArgs(T value)
        {
            Value = value;
        }

        public T Value { get; set; }
    }
}