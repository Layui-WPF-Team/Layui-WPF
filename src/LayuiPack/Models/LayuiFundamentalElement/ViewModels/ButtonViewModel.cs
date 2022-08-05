using Layui.Core.Base;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LayuiFundamentalElement.ViewModels
{
    public class ButtonViewModel : LayuiViewModelBase
    {
        private bool _BtnIsLoading;
        public bool BtnIsLoading
        {
            get { return _BtnIsLoading; }
            set { SetProperty(ref _BtnIsLoading, value); }
        }
        public ButtonViewModel(IContainerExtension container) : base(container)
        {

        }
        private DelegateCommand _BtnLoadingCommand;
        public DelegateCommand BtnLoadingCommand =>
            _BtnLoadingCommand ?? (_BtnLoadingCommand = new DelegateCommand(ExecuteBtnLoadingCommand));

        async  void ExecuteBtnLoadingCommand()
        {
            BtnIsLoading = true;
            await Task.Delay(3000);
            BtnIsLoading = false;
        }
    }
}
