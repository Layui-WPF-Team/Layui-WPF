using Layui.Core.Base;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LayuiComponentExample.ViewModels
{
    public class TreeSelectViewModel : LayuiViewModelBase
    {
        public TreeSelectViewModel(IContainerExtension container) : base(container)
        {
        }
        private ObservableCollection<int> _Items=new ObservableCollection<int>() { 1,2};
        public ObservableCollection<int> Items
        {
            get { return _Items; }
            set { SetProperty(ref _Items, value); }
        }
    }
}
