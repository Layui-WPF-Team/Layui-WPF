using LayuiTemplate.Dialog;
using LayuiTemplate.Dialog.Interface;
using LayuiTemplate.Enum;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayuiComponentExample.ViewModels
{
    public class DialogMessageViewModel : BindableBase, ILayDialogAware
    {
        public event Action<ILayDialogResult> RequestClose;
        private string _Title;

        public string Title
        {
            get { return _Title; }
            set { _Title = value; RaisePropertyChanged(); }
        }

        public void OnDialogOpened(ILayDialogParameter parameters)
        {
            Title = parameters.GetValue<string>("IsModel");
        }
        public DelegateCommand LikeCommand => new DelegateCommand(Like);
        public DelegateCommand NoCommand => new DelegateCommand(No);

        private void No()
        {
            LayDialogResult dialogResult = new LayDialogResult();
            dialogResult.Result = ButtonResult.No;
            RequestClose?.Invoke(dialogResult);
        }

        private void Like()
        {
            LayDialogResult dialogResult = new LayDialogResult();
            dialogResult.Result = ButtonResult.Yes;
            RequestClose?.Invoke(dialogResult);
        }
    }
}
