using Layui.Core.Base;
using LayUI.Wpf.Extend;
using LayUI.Wpf.Tools;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace LayuiComponentExample.ViewModels
{
    public class UploadViewModel : LayuiViewModelBase
    {
        private BitmapImage _ImageSource;
        public BitmapImage ImageSource
        {
            get { return _ImageSource; }
            set { SetProperty(ref _ImageSource, value); }
        }
        public UploadViewModel(IContainerExtension container) : base(container)
        {
        }
        private DelegateCommand<object> _DropCommand;
        public DelegateCommand<object> DropCommand =>
            _DropCommand ?? (_DropCommand = new DelegateCommand<object>(ExecuteDropCommand));

        void ExecuteDropCommand(object obj)
        {
            if (obj is IDataObject data)
            {
                if (!data.GetDataPresent(DataFormats.FileDrop))
                {
                    return;
                }
                string[] files = (string[])data.GetData(DataFormats.FileDrop);
                if (files.Length < 0) return;
                ImageSource = LayImageHelper.GetBitmapImage(new Uri(files[0]));
            }

        }
    }
}