using Layui.Core.Base;
using Prism.Commands;
using Prism.Mvvm;
using LayUI.Wpf.Global;
using LayUI.Wpf.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Ioc;

namespace LayuiFundamentalElement.ViewModels
{
    public class VerificationBoxViewModel : LayuiViewModelBase
    {
        public VerificationBoxViewModel(IContainerExtension container) :base(container) { } 
        private DelegateCommand _SoccessCommand;
        public DelegateCommand SoccessCommand =>
            _SoccessCommand ?? (_SoccessCommand = new DelegateCommand(ExecuteSoccessCommand));

        void ExecuteSoccessCommand()
        {
            LayMessage.Success(language.GetValue("解锁成功"));
        }
    }
}
