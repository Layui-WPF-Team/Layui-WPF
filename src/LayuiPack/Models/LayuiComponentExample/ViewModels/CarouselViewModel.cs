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
    public class CarouselViewModel : LayuiViewModelBase
    {
        private ObservableCollection<string> _Images;
        public ObservableCollection<string> Images
        {
            get { return _Images; }
            set { SetProperty(ref _Images, value); }
        }
        public CarouselViewModel(IContainerExtension container) : base(container)
        {
            Images = new ObservableCollection<string>() {
                "pack://SiteOfOrigin:,,,/Images/1.jpeg",
                "pack://SiteOfOrigin:,,,/Images/2.jpg",
                "pack://SiteOfOrigin:,,,/Images/1.jpeg",
                "pack://SiteOfOrigin:,,,/Images/2.jpg",
                "pack://SiteOfOrigin:,,,/Images/1.jpeg",
                "pack://SiteOfOrigin:,,,/Images/2.jpg",
            };
        }
        private DelegateCommand _DeleteCommand;
        public DelegateCommand DeleteCommand =>
            _DeleteCommand ?? (_DeleteCommand = new DelegateCommand(ExecuteDeleteCommand));

        void ExecuteDeleteCommand()
        {
            Images.Remove(Images[0]);
        }
    }
}
