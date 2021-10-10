using LayuiTemplate.Dialog;
using LayuiTemplate.Dialog.Interface;
using LayuiTemplate.Enum.Dialog;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayuiComponentExample.ViewModels
{
    public class DialogMessageViewModel : ILayDialogAware
    {
        public event Action<ILayDialogResult> RequestClose;

        public void OnDialogOpened(ILayDialogParameter parameters)
        {
            
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
