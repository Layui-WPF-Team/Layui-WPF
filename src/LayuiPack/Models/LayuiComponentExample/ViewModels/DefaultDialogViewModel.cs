using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LayuiComponentExample.ViewModels
{
    public class DefaultDialogViewModel : BindableBase, IDialogAware
    {
        public DefaultDialogViewModel()
        {

        }
        private DelegateCommand _OKCommand;
        public DelegateCommand OKCommand =>
            _OKCommand ?? (_OKCommand = new DelegateCommand(ExecuteOKCommand));

        void ExecuteOKCommand()
        {
            RequestClose?.Invoke(null);
        }
        public string Title =>"默认对话框";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
           
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
           
        }
    }
}
