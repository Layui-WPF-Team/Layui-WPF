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
    public class DateTimeViewModel : LayuiViewModelBase
    {
        public DateTimeViewModel(IContainerExtension container) : base(container)
        {
            SelectedDates = new DateTime?[] { DateTime.Now, DateTime .Now.AddDays(2)};
        }
        private DateTime?[] _SelectedDates;
        public DateTime?[] SelectedDates
        {
            get { return _SelectedDates; }
            set { SetProperty(ref _SelectedDates, value); }
        }
    }
}
