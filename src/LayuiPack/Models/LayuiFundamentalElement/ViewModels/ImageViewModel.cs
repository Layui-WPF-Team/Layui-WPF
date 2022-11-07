using Layui.Core.Base;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace LayuiFundamentalElement.ViewModels
{
    public class ImageViewModel : LayuiViewModelBase
    {
        public ImageViewModel(IContainerExtension container) : base(container)
        {
            
        }
        private string _Uri;
        public string Uri
        {
            get { return _Uri; }
            set { SetProperty(ref _Uri, value); }
        }
        private DelegateCommand<string> _ChangedSourceCommand;
        public DelegateCommand<string> ChangedSourceCommand =>
            _ChangedSourceCommand ?? (_ChangedSourceCommand = new DelegateCommand<string>(ExecuteChangedSourceCommand));

        void ExecuteChangedSourceCommand(string uri)
        {
            Uri = uri;
        }
    }
}
