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
    public class TagViewModel : LayuiViewModelBase
    {
        public TagViewModel(IContainerExtension container) : base(container)
        {
        }
        private ObservableCollection<string> _Tags=new ObservableCollection<string>();
        public ObservableCollection<string> Tags
        {
            get { return _Tags; }
            set { SetProperty(ref _Tags, value); }
        }
        private DelegateCommand<string> _RemoveCommand;
        public DelegateCommand<string> RemoveCommand =>
            _RemoveCommand ?? (_RemoveCommand = new DelegateCommand<string>(ExecuteRemoveCommand));

        void ExecuteRemoveCommand(string value)
        {
            Tags.Remove(value);
        }
    }
}
