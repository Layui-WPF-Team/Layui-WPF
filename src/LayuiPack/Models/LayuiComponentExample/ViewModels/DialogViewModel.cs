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
        public DelegateCommand<string> DialogShowCommand => new DelegateCommand<string>(DialogShow);
        private void DialogShow(string IsModel)
        {
            LayDialogParameter dialogParameter ;
            if (IsModel=="true")
            {
                dialogParameter = new LayDialogParameter();
                dialogParameter.Add("IsModel", "这是模态弹窗");
                LayDialog.Dialog.ShowDialog(SystemResource.DialogMessageView, dialogParameter, rest =>
                {
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
                MessageBox.Show("业务逻辑被阻塞");
            }
            else
            {
                dialogParameter = new LayDialogParameter();
                dialogParameter.Add("IsModel", "这是非模态弹窗");
                LayDialog.Dialog.Show(SystemResource.DialogMessageView, dialogParameter, rest =>
                {
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
                MessageBox.Show("业务逻辑未被阻塞");
            }
        }
    }
}
