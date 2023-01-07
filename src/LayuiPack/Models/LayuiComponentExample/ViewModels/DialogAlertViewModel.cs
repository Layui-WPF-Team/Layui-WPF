using Layui.Core.Base;
using LayUI.Wpf.Global;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LayuiComponentExample.ViewModels
{
    public class DialogAlertViewModel : BindableBase, ILayDialogAware
    {
        private string _Message;
        public string Message
        {
            get { return _Message; }
            set { SetProperty(ref _Message, value); }
        }
        public DialogAlertViewModel()
        {

        }
        private DelegateCommand _OkCommand;
        public DelegateCommand OkCommand =>
            _OkCommand ?? (_OkCommand = new DelegateCommand(ExecuteOkCommand));

        void ExecuteOkCommand()
        {
            RequestClose?.Invoke(null);
        }
        public event Action<ILayDialogResult> RequestClose;

        public void OnDialogOpened(ILayDialogParameter parameters)
        {
            Message = parameters.GetValue<string>("Message");
        }
    }
}
