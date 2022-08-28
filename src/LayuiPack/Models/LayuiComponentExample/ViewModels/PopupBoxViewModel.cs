using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LayuiComponentExample.ViewModels
{
    public class PopupBoxViewModel : BindableBase
    {
        private bool _IsOpen;
        public bool IsOpen
        {
            get { return _IsOpen; }
            set { SetProperty(ref _IsOpen, value); }
        }
        public PopupBoxViewModel()
        {

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
