using Layui.Core.Base;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace LayuiFundamentalElement.ViewModels
{
    /// <summary>
    /// 视图输入框VM
    /// </summary>
    public class FormViewModel :  LayuiViewModelBase
    {
        public FormViewModel(IRegionManager regionManager, IDialogService dialogServic) : base(regionManager, dialogServic)
        {
            Password = "123123";
        }
        private bool _Error=false;

        public bool Error
        {
            get { return _Error; }
            set { _Error = value; RaisePropertyChanged(); }
        }
        private string _Password;

        public string Password
        {
            get { return _Password; }
            set { _Password = value; RaisePropertyChanged(); }
        }

        public DelegateCommand SaveCommand => new DelegateCommand(Save);

        private void Save()
        {
            Error = true;
            MessageBox.Show(Password);
        }
    }
}
