using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Course.Commands
{
   public  class RelayCommand : ICommand
    {
        //private Action<object> execute;
        //private Func<object, bool> canExecute;

        //public event EventHandler CanExecuteChanged
        //{
        //    add { CommandManager.RequerySuggested += value; }
        //    remove { CommandManager.RequerySuggested -= value; }
        //}

        //public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        //{
        //    this.execute = execute;
        //    this.canExecute = canExecute;
        //}

        //public bool CanExecute(object parameter)
        //{
        //    return this.canExecute == null || this.canExecute(parameter);
        //}

        //public void Execute(object parameter)
        //{
        //    this.execute(parameter);
        //}

        /// <summary>
        /// Выполнить делегат
        /// </summary>
        readonly Action<object> _execute;

        /// <summary>
        /// Проверка возможности выполнения функции
        /// </summary>
        readonly Predicate<object> _canexecute;

        public RelayCommand(Action<object> execute, Predicate<object> canexecute)
        {
            if (execute is null)
                throw new NullReferenceException("execute");

            _execute = execute;
            _canexecute = canexecute;
        }

        public RelayCommand(Action<object> execute) : this(execute, x => true)
        {

        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canexecute is null ? false : _canexecute(parameter);
        }

        public void Execute(object parameter = null)
        {
            _execute.Invoke(parameter);
        }

    }
}
