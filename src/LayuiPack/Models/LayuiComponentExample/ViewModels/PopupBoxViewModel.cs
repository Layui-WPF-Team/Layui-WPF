using Layui.Core.Base;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LayuiComponentExample.ViewModels
{
    public class PopupBoxViewModel : LayuiViewModelBase
    {
        public PopupBoxViewModel(IContainerExtension container) : base(container)
        {
        }
        private bool _IsOpen;
        public bool IsOpen
        {
            get { return _IsOpen; }
            set { SetProperty(ref _IsOpen, value); }
        }
        private DelegateCommand _ClosePopupBoxCommand;
        public DelegateCommand ClosePopupBoxCommand =>
            _ClosePopupBoxCommand ?? (_ClosePopupBoxCommand = new DelegateCommand(ExecuteClosePopupBoxCommand));

        void ExecuteClosePopupBoxCommand()
        {
            IsOpen = false;
        }
    }
}
