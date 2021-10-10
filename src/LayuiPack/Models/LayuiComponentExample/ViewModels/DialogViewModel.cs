using Layui.Core.Base;
using Layui.Core.Resource;
using LayuiTemplate.Dialog;
using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LayuiComponentExample.ViewModels
{
    public class DialogViewModel : LayuiViewModelBase
    {
        public DialogViewModel(IRegionManager regionManager, IDialogService dialogServic) : base(regionManager, dialogServic)
        {

        }
        public DelegateCommand DialogShowCommand => new DelegateCommand(DialogShow);
        private void DialogShow()
        {
            LayDialog.Dialog.Show(SystemResource.DialogMessageView, null,rest=> {
                switch (rest.Result)
                {
                    case LayuiTemplate.Enum.Dialog.ButtonResult.Yes:
                        MessageBox.Show("我也是这么认为的");
                        break;
                    case LayuiTemplate.Enum.Dialog.ButtonResult.No:
                        MessageBox.Show("你确定你是认真的吗?");
                        break;
                    case LayuiTemplate.Enum.Dialog.ButtonResult.Default:
                        break;
                    default:
                        break;
                }
            });
        }
    }
}
