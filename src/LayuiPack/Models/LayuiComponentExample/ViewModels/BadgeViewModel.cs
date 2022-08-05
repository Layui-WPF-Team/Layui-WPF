using Layui.Core.Base;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LayuiComponentExample.ViewModels
{
    public class BadgeViewModel : LayuiViewModelBase
    {
        private int _Value=100;
        public int Value
        {
            get { return _Value; }
            set { SetProperty(ref _Value, value); }
        }
        public BadgeViewModel(IContainerExtension container) : base(container)
        {

        }
    }
}
