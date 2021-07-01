using Layui.Core.Base;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LayuiComponentExample.ViewModels
{
    public class SliderViewModel : LayuiViewModelBase
    {
        public SliderViewModel(IRegionManager regionManager, IDialogService dialogServic) : base(regionManager, dialogServic)
        {

        }
    }
}
