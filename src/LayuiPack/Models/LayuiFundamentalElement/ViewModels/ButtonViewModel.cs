using Layui.Core.Base;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LayuiFundamentalElement.ViewModels
{
    public class ButtonViewModel : LayuiViewModelBase
    {
        public ButtonViewModel(IRegionManager regionManager, IDialogService dialogServic) : base(regionManager, dialogServic)
        {

        }
        
    }
}
