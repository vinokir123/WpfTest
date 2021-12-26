using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClearWpf.Infrastructure.Commands.Base
{
    internal abstract class Command : ICommand
    {
        public abstract bool CanExecute(object parameter);

        public abstract void Execute(object parameter);
        private event EventHandler CanExecuteChangedHandlers;

        protected virtual void OnCanExecuteChanged(EventArgs e = null) => CanExecuteChangedHandlers?.Invoke(this, e ?? EventArgs.Empty);

        /// <summary>Событие возникает при изменении возможности исполнения команды</summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                CanExecuteChangedHandlers += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
                CanExecuteChangedHandlers -= value;
            }
        }
    }
}
