using LayUI.Wpf.Global;
using LayUI.Wpf.Enum;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LayUI.Wpf.Extensions;
using Layui.Core;

namespace LayuiComponentExample.ViewModels
{
    public class DialogMessageViewModel : BindableBase, ILayDialogWindowAware
    {
        ILayLanguage Language;
        public DialogMessageViewModel(ILayLanguage language) {
            Language=language;
        }
        public event Action<ILayDialogResult> RequestClose;
        private string _Title;

        public string Title
        {
            get { return _Title; }
            set 
            {
                _Title = value; RaisePropertyChanged();
                Language.Refresh();
            }
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

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            
        }
    }
}
