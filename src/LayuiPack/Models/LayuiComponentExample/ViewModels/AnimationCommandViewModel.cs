using Layui.Core.Base;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace LayuiComponentExample.ViewModels
{
    public class AnimationCommandViewModel : LayuiViewModelBase
    {
        public AnimationCommandViewModel(IContainerExtension container) : base(container)
        {
        }
        private DelegateCommand _ColorAnimationCompletedCommand;
        public DelegateCommand ColorAnimationCompletedCommand =>
            _ColorAnimationCompletedCommand ?? (_ColorAnimationCompletedCommand = new DelegateCommand(ExecuteColorAnimationCompletedCommand));

        void ExecuteColorAnimationCompletedCommand()
        {
            MessageBox.Show("我是颜色动画 我执行完成了");
        }
        private DelegateCommand _DoubleAnimationCompletedCommand;
        public DelegateCommand DoubleAnimationCompletedCommand =>
            _DoubleAnimationCompletedCommand ?? (_DoubleAnimationCompletedCommand = new DelegateCommand(ExecuteDoubleAnimationCompletedCommand));

        void ExecuteDoubleAnimationCompletedCommand()
        {
            MessageBox.Show("我是位移动画 我执行完成了");
        }
    }
}
