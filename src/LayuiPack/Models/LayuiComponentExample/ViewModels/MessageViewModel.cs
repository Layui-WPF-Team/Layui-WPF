using Layui.Core.Base;
using LayuiTemplate.Global;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LayuiComponentExample.ViewModels
{
    public class MessageViewModel : LayuiViewModelBase
    {
        public MessageViewModel(IContainerExtension container) : base(container)
        {

        }
        private DelegateCommand<string> _GlobalMessageCommand;
        public DelegateCommand<string> GlobalMessageCommand =>
            _GlobalMessageCommand ?? (_GlobalMessageCommand = new DelegateCommand<string>(ExecuteGlobalMessageCommand));

        void ExecuteGlobalMessageCommand(string type)
        {
            switch (type)
            {
                case "1":
                    LayMessage.Success("成功信息");
                    break;
                case "2":
                    LayMessage.Warning("警告信息");
                    break;
                case "3":
                    LayMessage.Error("错误信息");
                    break;
                default:
                    break;
            }
        }
        private DelegateCommand<string> _MessageCommand;
        public DelegateCommand<string> MessageCommand =>
            _MessageCommand ?? (_MessageCommand = new DelegateCommand<string>(ExecuteMessageCommand));

        void ExecuteMessageCommand(string type)
        {
            switch (type)
            {
                case "1":
                    LayMessage.Success("成功信息","Message");
                    break;
                case "2":
                    LayMessage.Warning("警告信息", "Message");
                    break;
                case "3":
                    LayMessage.Error("错误信息", "Message");
                    break;
                default:
                    break;
            }
        }
        private DelegateCommand _ControlsMessageCommand;
        public DelegateCommand ControlsMessageCommand =>
            _ControlsMessageCommand ?? (_ControlsMessageCommand = new DelegateCommand(ExecuteControlsMessageCommand));

        void ExecuteControlsMessageCommand()
        {
            LayMessage.Default(new System.Windows.Controls.TextBlock() { Text="自定义提示信息"});
        }
    }
}
