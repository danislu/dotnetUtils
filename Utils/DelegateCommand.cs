namespace Utils
{
    using System;
    using System.Windows.Input;

    public class DelegateCommand<T> : ICommand 
        where T : class 
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

        #region Implementation of ICommand

        public void Execute(object parameter)
        {
            if (parameter is T)
            {
                this.execute(parameter as T);
            }
        }

        public bool CanExecute(object parameter)
        {
            return (this.canExecute != null) && (parameter is T) && this.canExecute(parameter as T);
        }

        public event EventHandler CanExecuteChanged;

        #endregion
    }
}
