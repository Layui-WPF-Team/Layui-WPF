using Layui.Core.Base;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace LayuiComponentExample.ViewModels
{
    public class FlowItemsControlViewModel : LayuiViewModelBase
    {
        private bool _IsActive;
        public bool IsActive
        {
            get { return _IsActive; }
            set { SetProperty(ref _IsActive, value); }
        }
        public FlowItemsControlViewModel(IContainerExtension container) : base(container)
        {
        }

        private ObservableCollection<string> _Items = new ObservableCollection<string>() { "", };
        public ObservableCollection<string> Items
        {
            get { return _Items; }
            set { SetProperty(ref _Items, value); }
        }
        private DelegateCommand _GetItemsCommand;
        public DelegateCommand GetItemsCommand =>
            _GetItemsCommand ?? (_GetItemsCommand = new DelegateCommand(ExecuteGetItemsCommand));

        async void ExecuteGetItemsCommand()
        {
            IsActive=true;
            await Task.Delay(500);
            Items.Add("");
            IsActive = false;
        }
    }
}
