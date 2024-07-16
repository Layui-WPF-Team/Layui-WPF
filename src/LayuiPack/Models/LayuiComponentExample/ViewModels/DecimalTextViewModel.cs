using Layui.Core.Base;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LayuiComponentExample.ViewModels
{
    public class DecimalTextViewModel : LayuiViewModelBase
    {
        public DecimalTextViewModel(IContainerExtension container) : base(container)
        {
        }
        private double _Value;
        public double Value
        {
            get { return _Value; }
            set { SetProperty(ref _Value, value); }
        } 
        private DelegateCommand _ValueCommand;
        public DelegateCommand ValueCommand =>
            _ValueCommand ?? (_ValueCommand = new DelegateCommand(ExecuteValueCommand));

        void ExecuteValueCommand()
        {
            Value = GetRandomNumber(0, 9000000, 10);
        }
        public double GetRandomNumber(double minimum, double maximum, int Len)   //Len小数点保留位数
        {
            Random random = new Random();
            return Math.Round(random.NextDouble() * (maximum - minimum) + minimum, Len);
        }
    }
}
