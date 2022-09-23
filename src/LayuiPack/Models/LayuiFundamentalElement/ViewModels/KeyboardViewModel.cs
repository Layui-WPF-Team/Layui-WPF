using Layui.Core.Base;
using LayuiTemplate.Tools;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace LayuiFundamentalElement.ViewModels
{
    public class KeyboardViewModel : LayuiViewModelBase
    {
        public KeyboardViewModel(IContainerExtension container) : base(container)
        {

        }
        private DelegateCommand<object> _TestCommand;
        public DelegateCommand<object> TestCommand =>
            _TestCommand ?? (_TestCommand = new DelegateCommand<object>(ExecuteTestCommand));

        void ExecuteTestCommand(object key)
        {
            LayKeyboardKeyHelper.Keyboard_Event(Key.LeftShift, LayKeyboardKeyHelper.KeyDown);
            LayKeyboardKeyHelper.Keyboard_Event(Key.D1, LayKeyboardKeyHelper.KeyDown);
            LayKeyboardKeyHelper.Keyboard_Event(Key.LeftShift, LayKeyboardKeyHelper.KeyUp);
            LayKeyboardKeyHelper.Keyboard_Event(Key.D1, LayKeyboardKeyHelper.KeyUp);
        }
    }
}
