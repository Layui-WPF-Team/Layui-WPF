using Layui.Core.Base;
using Layui.Core.Resource;
using LayuiTemplate.Global;
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
            LayDialogParameter dialogParameter ;
            if (IsModel=="true")
            {
                dialogParameter = new LayDialogParameter();
                dialogParameter.Add("IsModel", "这是模态弹窗");
                LayDialog.ShowDialog(SystemResource.DialogMessageView, dialogParameter, rest =>
                {
                    switch (rest.Result)
                    {
                        case LayuiTemplate.Enum.ButtonResult.Yes:
                            MessageBox.Show("我也是这么认为的");
                            break;
                        case LayuiTemplate.Enum.ButtonResult.No:
                            MessageBox.Show("你确定你是认真的吗?");
                            break;
                        case LayuiTemplate.Enum.ButtonResult.Default:
                            break;
                        default:
                            break;
                    }
                }, "RootDialogToken");
                MessageBox.Show("业务逻辑被阻塞");
            }
            else
            {
                dialogParameter = new LayDialogParameter();
                dialogParameter.Add("IsModel", "这是非模态弹窗");
                LayDialog.Show(SystemResource.DialogMessageView, dialogParameter, rest =>
                {
                    switch (rest.Result)
                    {
                        case LayuiTemplate.Enum.ButtonResult.Yes:
                            MessageBox.Show("我也是这么认为的");
                            break;
                        case LayuiTemplate.Enum.ButtonResult.No:
                            MessageBox.Show("你确定你是认真的吗?");
                            break;
                        case LayuiTemplate.Enum.ButtonResult.Default:
                            break;
                        default:
                            break;
                    }
                }, "RootDialogToken"); 
                LayDialog.Show(SystemResource.DialogMessageView, dialogParameter, rest =>
                {
                    switch (rest.Result)
                    {
                        case LayuiTemplate.Enum.ButtonResult.Yes:
                            MessageBox.Show("我也是这么认为的");
                            break;
                        case LayuiTemplate.Enum.ButtonResult.No:
                            MessageBox.Show("你确定你是认真的吗?");
                            break;
                        case LayuiTemplate.Enum.ButtonResult.Default:
                            break;
                        default:
                            break;
                    }
                }, "RootDialogToken");
                MessageBox.Show("业务逻辑未被阻塞");
            }
        }
        private DelegateCommand _LocalDialogShowCommand;
        public DelegateCommand LocalDialogShowCommand =>
            _LocalDialogShowCommand ?? (_LocalDialogShowCommand = new DelegateCommand(ExecuteLocalDialogShowCommand));

        void ExecuteLocalDialogShowCommand()
        {
            LayDialogParameter dialogParameter=new LayDialogParameter();
            dialogParameter.Add("IsModel", "这是非模态弹窗");
            LayDialog.Show(SystemResource.DialogMessageView, dialogParameter, rest => {
                switch (rest.Result)
                {
                    case LayuiTemplate.Enum.ButtonResult.Yes:
                        break;
                    case LayuiTemplate.Enum.ButtonResult.No:
                        break;
                    case LayuiTemplate.Enum.ButtonResult.Default:
                        break;
                    default:
                        break;
                }

            },"Dialog");
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
                        case LayuiTemplate.Enum.ButtonResult.Yes:
                            MessageBox.Show("我也是这么认为的");
                            break;
                        case LayuiTemplate.Enum.ButtonResult.No:
                            MessageBox.Show("你确定你是认真的吗?");
                            break;
                        case LayuiTemplate.Enum.ButtonResult.Default:
                            break;
                        default:
                            break;
                    }
                },"window");
                MessageBox.Show("业务逻辑被阻塞");
            }
            else
            {
                dialogParameter = new LayDialogParameter();
                dialogParameter.Add("IsModel", "这是非模态弹窗");
                LayDialog.ShowWindow(SystemResource.DialogMessageView, dialogParameter, rest =>
                {
                    switch (rest.Result)
                    {
                        case LayuiTemplate.Enum.ButtonResult.Yes:
                            MessageBox.Show("我也是这么认为的");
                            break;
                        case LayuiTemplate.Enum.ButtonResult.No:
                            MessageBox.Show("你确定你是认真的吗?");
                            break;
                        case LayuiTemplate.Enum.ButtonResult.Default:
                            break;
                        default:
                            break;
                    }
                },"window");
                MessageBox.Show("业务逻辑未被阻塞");
            }
        }
    }
}
