using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BusQuery
{
    public class CommandEx<T> : ICommand
    {
        public CommandEx(Action<T> execute)
        {
            ExecuteAction = execute;
        }
        public CommandEx(Action<T> execute, Func<T, bool> canExecute)
            : this(execute)
        {
            ExecuteCanExecute = canExecute;
        }

        public Action<T> ExecuteAction { get; set; }
        public Func<T, bool> ExecuteCanExecute { get; set; }

        public event EventHandler CanExecuteChanged;

        public virtual bool CanExecute(object parameter)
        {
            if (ExecuteCanExecute == null)
            {
                return true;
            }
            return ExecuteCanExecute((T)parameter);
        }
        public virtual void Execute(object parameter)
        {
            if (ExecuteAction != null)
            {
                ExecuteAction((T)parameter);
            }
        }
    }
}
