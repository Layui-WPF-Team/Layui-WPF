using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LayUI.Wpf
{
    public class LayDelegateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Action _executeMethod;
        private readonly Func<bool> _canExecuteMethod;

        public LayDelegateCommand(Action executeMethod, Func<bool> canExecuteMethod = null)
        {
            _executeMethod = executeMethod ?? throw new ArgumentNullException(nameof(executeMethod), "DelegateCommand 委托不能为空");
            _canExecuteMethod = canExecuteMethod ?? (() => true); // 默认的 CanExecute 方法
        }

        public bool CanExecute(object parameter) => _canExecuteMethod?.Invoke() ?? true;

        public void Execute(object parameter)
        {
            _executeMethod();
            // 如果需要在执行后检查可执行性并触发事件，可以在这里调用
            RaiseCanExecuteChanged();
        }

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    public class LayDelegateCommand<T> : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Action<T> _executeMethod;
        private readonly Func<T, bool> _canExecuteMethod;

        public LayDelegateCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod = null)
        {
            _executeMethod = executeMethod ?? throw new ArgumentNullException(nameof(executeMethod), "DelegateCommand 委托不能为空");
            _canExecuteMethod = canExecuteMethod ?? (_ => true); // 默认的 CanExecute 方法
        }

        public bool CanExecute(object parameter)
        {
            if (parameter is T typedParameter)
            {
                return _canExecuteMethod?.Invoke(typedParameter) ?? true;
            }
            return false; // 如果类型不匹配，返回 false
        }

        public void Execute(object parameter)
        {
            if (parameter is T typedParameter)
            {
                _executeMethod(typedParameter);
                // 如果需要在执行后检查可执行性并触发事件，可以在这里调用
                RaiseCanExecuteChanged();
            }
            else
            {
                throw new ArgumentException($"Parameter must be of type {typeof(T).Name}", nameof(parameter));
            }
        }

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
