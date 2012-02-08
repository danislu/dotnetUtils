namespace Utils.Tap
{
    using System;
    using System.Threading;


    public class EventProgress<T> : IProgress<T>
    {
        public event EventHandler<ProgressReportedEventArgs<T>> ProgressReported;

        public void Report(T value)
        {
            InvokeProgressReported(value);
        }

        private void InvokeProgressReported(T value)
        {
            EventHandler<ProgressReportedEventArgs<T>> handler = ProgressReported;
            if (handler != null)
            {
                handler(this, new ProgressReportedEventArgs<T>(value));
            }
        }
    }
}
