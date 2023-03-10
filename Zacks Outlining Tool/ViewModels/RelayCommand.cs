using System;
using System.Windows.Input;

namespace Zacks_Outlining_Tool.ViewModels
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> executeMethod;
        private readonly Func<object, bool> canexecuteMethod;

        public RelayCommand(Action <object> executeMethod, Func <object, bool> canexecuteMethod)
        {
            this.executeMethod = executeMethod;
            this.canexecuteMethod = canexecuteMethod;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if(canexecuteMethod != null)
            {
                return true;
            } 
            else
            {
                return false;
            }
        }

        public void Execute(object parameter)
        {
            executeMethod(parameter);
        }
    }
}
