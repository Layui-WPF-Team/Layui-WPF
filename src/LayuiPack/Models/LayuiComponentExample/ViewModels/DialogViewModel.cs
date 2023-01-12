using Layui.Core.Base;
using Layui.Core.Resource;
using LayUI.Wpf.Global;
using Prism.Commands;
using Prism.Ioc;
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
        public DialogViewModel(IContainerExtension container) : base(container)
        {

        }
        public DelegateCommand<string> DialogShowCommand => new DelegateCommand<string>(DialogShow);
        private void DialogShow(string IsModel)
        {
            var data = LayDialog.Resolve<IContainerExtension>();
            LayDialogParameter dialogParameter;
            if (IsModel == "true")
            {
                dialogParameter = new LayDialogParameter();
                dialogParameter.Add("IsModel", "这是模态弹窗");
                LayDialog.ShowDialog(SystemResource.DialogMessageView, dialogParameter, rest =>
                {
                }, "RootDialogToken");
                Dialog.Show("DefaultDialog", null,null);
            }
            else
            {
                dialogParameter = new LayDialogParameter();
                dialogParameter.Add("IsModel", "这是非模态弹窗");
                LayDialog.Show(SystemResource.DialogMessageView, dialogParameter, rest =>
                {
                    switch (rest.Result)
                    {
                        case LayUI.Wpf.Enum.ButtonResult.Yes:
                            MessageBox.Show("我也是这么认为的");
                            break;
                        case LayUI.Wpf.Enum.ButtonResult.No:
                            MessageBox.Show("你确定你是认真的吗?");
                            break;
                        case LayUI.Wpf.Enum.ButtonResult.Default:
                            break;
                        default:
                            break;
                    }
                }, "RootDialogToken");
            }
        }
        private DelegateCommand _LocalDialogShowCommand;
        public DelegateCommand LocalDialogShowCommand =>
            _LocalDialogShowCommand ?? (_LocalDialogShowCommand = new DelegateCommand(ExecuteLocalDialogShowCommand));

        void ExecuteLocalDialogShowCommand()
        {
            LayDialogParameter dialogParameter = new LayDialogParameter();
            dialogParameter.Add("IsModel", "这是非模态弹窗");
            LayDialog.Show(SystemResource.DialogMessageView, dialogParameter, rest =>
            {
                switch (rest.Result)
                {
                    case LayUI.Wpf.Enum.ButtonResult.Yes:
                        break;
                    case LayUI.Wpf.Enum.ButtonResult.No:
                        break;
                    case LayUI.Wpf.Enum.ButtonResult.Default:
                        break;
                    default:
                        break;
                }

            }, "Dialog");
        }
        private DelegateCommand _CloseDialogCommand;
        public DelegateCommand CloseDialogCommand =>
            _CloseDialogCommand ?? (_CloseDialogCommand = new DelegateCommand(ExecuteCloseDialogCommand));

        void ExecuteCloseDialogCommand()
        {
            LayDialog.Close("Dialog");
        }
        private DelegateCommand<string> _DialogShowWindowCommand;
        public DelegateCommand<string> DialogShowWindowCommand =>
            _DialogShowWindowCommand ?? (_DialogShowWindowCommand = new DelegateCommand<string>(ExecuteDialogShowWindowCommand));

        void ExecuteDialogShowWindowCommand(string IsModel)
        {
            LayDialogParameter dialogParameter;
            if (IsModel == "true")
            {
                dialogParameter = new LayDialogParameter();
                dialogParameter.Add("IsModel", "这是模态弹窗");
                LayDialog.ShowDialogWindow(SystemResource.DialogMessageView, dialogParameter, rest =>
                {
                    switch (rest.Result)
                    {
                        case LayUI.Wpf.Enum.ButtonResult.Yes:
                            MessageBox.Show("我也是这么认为的");
                            break;
                        case LayUI.Wpf.Enum.ButtonResult.No:
                            MessageBox.Show("你确定你是认真的吗?");
                            break;
                        case LayUI.Wpf.Enum.ButtonResult.Default:
                            break;
                        default:
                            break;
                    }
                }, "window");
            }
            else
            {
                dialogParameter = new LayDialogParameter();
                dialogParameter.Add("IsModel", "这是非模态弹窗");
                LayDialog.ShowWindow(SystemResource.DialogMessageView, dialogParameter, rest =>
                {
                    switch (rest.Result)
                    {
                        case LayUI.Wpf.Enum.ButtonResult.Yes:
                            MessageBox.Show("我也是这么认为的");
                            break;
                        case LayUI.Wpf.Enum.ButtonResult.No:
                            MessageBox.Show("你确定你是认真的吗?");
                            break;
                        case LayUI.Wpf.Enum.ButtonResult.Default:
                            break;
                        default:
                            break;
                    }
                }, "window");
            }
        }
    }
}
