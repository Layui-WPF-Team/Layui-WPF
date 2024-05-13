using Layui.Core.Base;
using LayUI.Wpf.Enum;
using LayUI.Wpf.Extensions;
using LayUI.Wpf.Global;
using LayUI.Wpf.Tools;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LayuiComponentExample.ViewModels
{
    public class IconViewModel : LayuiViewModelBase
    {
        public IconViewModel(IContainerExtension container) : base(container)
        {

        }
        private Dictionary<string, string> _Items;
        public Dictionary<string, string> Items
        {
            get { return _Items; }
            set { SetProperty(ref _Items, value); }
        }
        private DelegateCommand<string> _CopyCommand;
        public DelegateCommand<string> CopyCommand =>
            _CopyCommand ?? (_CopyCommand = new DelegateCommand<string>(ExecuteCopyCommand));

        void ExecuteCopyCommand(string value)
        {
            Clipboard.SetDataObject($"<TextBlock FontFamily=\"{{DynamicResource IconFont}}\" Text=\"{value}\" />");
            LayMessage.Success(language.GetValue("ReplicatingSuccess"));  
        }
        public async override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            Items = await Task.Run(() => { return LayFontsHelper.GetIconFontUnicode("IconFont"); }); 
        }
    }
}
