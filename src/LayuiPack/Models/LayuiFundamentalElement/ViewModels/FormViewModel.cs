using Layui.Core.Base;
using Prism.Commands;
using Prism.Ioc;
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
        public FormViewModel(IContainerExtension container) : base(container)
        {
            Password = "123123";
        }
        public List<string> ListDatas { get; set; } = new List<string>()
        {
            "1","2","3","4","5","6","7","8","9","10","1","2","3","4","5","6","7","8","9","10","1","2","3","4","5","6","7","8","9","10","1","2","3","4","5","6","7","8","9","10","1","2","3","4","5","6","7","8","9","10","1","2","3","4","5","6","7","8","9","10","1","2","3","4","5","6","7","8","9","10"
        };
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
        public override void ExecuteLoadedCommand()
        {
            base.ExecuteLoadedCommand();

        }
        public DelegateCommand SaveCommand => new DelegateCommand(Save);

        private void Save()
        {
            Error = true;
            MessageBox.Show(Password);
        }
    }
}
