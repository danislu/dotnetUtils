namespace Utils
{
    using System;
    using System.Windows.Input;

    public class DelegateCommand<T> : ICommand 
    {
        private readonly Action<T> execute;
        private readonly Func<T, bool> canExecute;

        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public DelegateCommand(Action<T> execute) : this(execute, null)
        {
        }

        public void InvokeCanExecuteChanged()
        {
            EventHandler handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        #region Implementation of ICommand

        public void Execute(object parameter)
        {
            if (parameter is T)
            {
                this.execute((T)parameter);
            }
        }

        public bool CanExecute(object parameter)
        {
            return (this.canExecute == null) || ((parameter is T) && this.canExecute((T)parameter));
        }

        public event EventHandler CanExecuteChanged;

        #endregion
    }
}
