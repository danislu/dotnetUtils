namespace Utils.Tap
{
    using System.Reactive.Linq;
    using System.Threading;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ObservableProgress<T> : IProgress<T>
    {
        IObservable<ProgressReportedEventArgs<T>> observable;

        public event EventHandler<ProgressReportedEventArgs<T>> ProgressReported;

        public IObservable<ProgressReportedEventArgs<T>> Progress
        {
            get
            {
                return observable ?? (observable = CreateObservable());
            }
        }

        private IObservable<ProgressReportedEventArgs<T>> CreateObservable()
        {
            return Observable
                .FromEventPattern<ProgressReportedEventArgs<T>>(h => this.ProgressReported += h, h => this.ProgressReported -= h)
                .Select(t => t.EventArgs as ProgressReportedEventArgs<T>);
        }

        public void Report(T value)
        {
            ProgressReported(this, new ProgressReportedEventArgs<T>(value));
        }
    }
}
