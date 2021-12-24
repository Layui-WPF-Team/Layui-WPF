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
        private AnimationType _ZoomAnimationType= AnimationType.Default;
        public AnimationType ZoomAnimationType
        {
            get { return _ZoomAnimationType; }
            set { SetProperty(ref _ZoomAnimationType, value); }
        }
        private AnimationType _GradientAnimationType = AnimationType.Default;
        public AnimationType GradientAnimationType
        {
            get { return _GradientAnimationType; }
            set { SetProperty(ref _GradientAnimationType, value); }
        }
        private DelegateCommand _AnimationCommand;
        public DelegateCommand AnimationCommand =>
            _AnimationCommand ?? (_AnimationCommand = new DelegateCommand(ExecuteAnimationCommand));

        public  void ExecuteAnimationCommand()
        {
            if (IsChecked)
            {
                ZoomAnimationType = AnimationType.Zoom;
                GradientAnimationType = AnimationType.Gradient;
            }
            else {
                ZoomAnimationType = AnimationType.Default;
                GradientAnimationType = AnimationType.Default;
            }
        }
    }
}
