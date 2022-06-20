using Layui.Core.Base;
using LayuiTemplate.Enum.Transitions;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LayuiFundamentalElement.ViewModels
{
    public class TransitionControlViewModel : LayuiViewModelBase
    {
        public TransitionControlViewModel(IContainerExtension container) : base(container)
        {

        }
        private bool _IsChecked=false;
        public bool IsChecked
        {
            get { return _IsChecked; }
            set { SetProperty(ref _IsChecked, value); }
        }
        private DelegateCommand _AnimationCommand;
        public DelegateCommand AnimationCommand =>
            _AnimationCommand ?? (_AnimationCommand = new DelegateCommand(ExecuteAnimationCommand));

        public  void ExecuteAnimationCommand()
        {
        }
    }
}
