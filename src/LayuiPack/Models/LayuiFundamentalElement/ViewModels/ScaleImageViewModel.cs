using Layui.Core.Base;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LayuiFundamentalElement.ViewModels
{
    public class ScaleImageViewModel : LayuiViewModelBase
    {
        public ScaleImageViewModel(IContainerExtension container) : base(container)
        {
        } 
        private DelegateCommand _ResetCommand;
        public DelegateCommand ResetCommand =>
            _ResetCommand ?? (_ResetCommand = new DelegateCommand(() => _ResetCommand.RaiseCanExecuteChanged())); 
    }
}
