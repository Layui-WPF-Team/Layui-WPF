using Layui.Core.Base;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LayuiComponentExample.ViewModels
{
    public class CodeBoxViewModel : LayuiViewModelBase
    {
        public CodeBoxViewModel()
        {

        }
        private string _Value=
@"<UserControl
    x:Class=""LayuiComponentExample.Views.CodeBox""
    xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
    xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
    xmlns:Lay=""clr-namespace:LayUI.Wpf.Controls;assembly=LayUI.Wpf""
    xmlns:prism=""http://prismlibrary.com/""
    prism:ViewModelLocator.AutoWireViewModel=""True"">
    <Lay:LayCodeBox Margin=""10"" Text=""{Binding Value}"" />
</UserControl>";
        public string Value
        {
            get { return _Value; }
            set { SetProperty(ref _Value, value); }
        }
    }
}
