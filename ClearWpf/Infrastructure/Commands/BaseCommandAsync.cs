using ClearWpf.Infrastructure.Commands.Base;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ClearWpf.Infrastructure.Commands
{
    internal class BaseCommandAsync : Command
    {
        private readonly Func<object, Task> _ExecuteAsync;
        private readonly Func<object, bool> _CanExecuteAsync;

        private volatile Task _ExecutingTask;
        Func<bool> emptyDelegate = null;
        public BaseCommandAsync(Func<Task> ExecuteAsync, Func<bool> CanExecute = null)
            : this(
                ExecuteAsync is null ? throw new ArgumentNullException(nameof(ExecuteAsync)) : new Func<object, Task>(_ => ExecuteAsync()),
                CanExecute is null ? null : new Func<object, bool>(_ => CanExecute()))
        { }

        public BaseCommandAsync(Func<object, Task> ExecuteAsync, Func<bool> CanExecute = null)
            : this(ExecuteAsync, CanExecute is null ? null : new Func<object, bool>(_ => CanExecute()))
        { }

        public BaseCommandAsync(Func<object, Task> ExecuteAsync, Func<object, bool> CanExecuteAsync = null)
        {
            _ExecuteAsync = ExecuteAsync ?? throw new ArgumentNullException(nameof(ExecuteAsync));
            _CanExecuteAsync = CanExecuteAsync;
        }

        public override bool CanExecute(object parameter) =>
            (_ExecutingTask is null || _ExecutingTask.IsCompleted)
            && (_CanExecuteAsync?.Invoke(parameter) ?? true);

        public override async void Execute(object parameter)
        {
            if (!CanExecute(parameter)) return;

            var execute_async = _ExecuteAsync(parameter);
            _ = Interlocked.Exchange(ref _ExecutingTask, execute_async);
            _ExecutingTask = execute_async;
            OnCanExecuteChanged();

            try
            {
                await execute_async.ConfigureAwait(true);
            }
            catch (OperationCanceledException)
            {

            }

            OnCanExecuteChanged();
        }
    }
}
