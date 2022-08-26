using Layui.Core.Base;
using LayuiTemplate.Enum;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LayuiComponentExample.ViewModels
{
    public class DrawerViewModel : LayuiViewModelBase
    {
        public DrawerViewModel(IContainerExtension container) : base(container)
        {
        }
        private DrawerHostStyle _Type;
        public DrawerHostStyle Type
        {
            get { return _Type; }
            set { SetProperty(ref _Type, value); }
        }
        private bool _DrawerOpen;
        public bool DrawerOpen
        {
            get { return _DrawerOpen; }
            set { SetProperty(ref _DrawerOpen, value); }
        }
        private DelegateCommand<object> _CheckedCommand;
        public DelegateCommand<object> CheckedCommand =>
            _CheckedCommand ?? (_CheckedCommand = new DelegateCommand<object>(ExecuteCheckadCommand));

        void ExecuteCheckadCommand(object type)
        {
            if (type is DrawerHostStyle style)
            {
                Type = style;
            }
        }
    }
}
