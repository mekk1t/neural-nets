using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ImageClusters
{
    public class CustomCommand : ICommand
    {
        private Action _action;
        public event EventHandler CanExecuteChanged;

        public CustomCommand(Action action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => _action();
    }
}
