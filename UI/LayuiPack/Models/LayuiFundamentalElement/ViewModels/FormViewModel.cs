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
    /// <summary>
    /// 视图输入框VM
    /// </summary>
    public class FormViewModel :  LayuiViewModelBase
    {
        public FormViewModel(IRegionManager regionManager, IDialogService dialogServic) : base(regionManager, dialogServic)
        {

        }
        private bool _Error=false;

        public bool Error
        {
            get { return _Error; }
            set { _Error = value; RaisePropertyChanged(); }
        }

        public DelegateCommand SaveCommand => new DelegateCommand(Save);

        private void Save()
        {
            Error = true;
        }
    }
}
