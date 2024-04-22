using Layui.Core.AppHelper;
using Layui.Core.Base;
using Layui.Core.Resource;
using LayUI.Wpf.Tools;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using LayUI.Wpf.Extensions;

namespace LayuiApp.ViewModels
{
    public class Language: BindableBase
    {
        private string _Title;
        public string Title
        {
            get { return _Title; }
            set { SetProperty(ref _Title, value); }
        }
        private string _Icon;
        public string Icon
        {
            get { return _Icon; }
            set { SetProperty(ref _Icon, value); }
        }
        private string _Key;
        public string Key
        {
            get { return _Key; }
            set { SetProperty(ref _Key, value); }
        }
    }
    public class MainWindowViewModel : LayuiViewModelBase
    {
        private Language _Language;
        public Language Language
        {
            get { return _Language; }
            set 
            { 
                SetProperty(ref _Language, value);
                LanguageExtension.LoadResourceKey(Language.Key);
            }
        } 
        private List<Language> _Languages=new List<Language>() 
        {  
            new Language(){ Title="中文",Icon="/Images/Svg/cn.svg",Key="zh_CN" },
            new Language(){ Title="英语",Icon="/Images/Svg/um.svg",Key="en_US" },
        };
        public List<Language> Languages
        {
            get { return _Languages; }
            set { SetProperty(ref _Languages, value); }
        }

        private string _Message= "项目地址：https://github.com/Layui-WPF-Team/Layui-WPF";
        public string Message
        {
            get { return _Message; }
            set { SetProperty(ref _Message, value); }
        }
        public MainWindowViewModel(IContainerExtension container) : base(container)
        {
            Language = Languages.FirstOrDefault();
        }
        #region 视图属性
        private bool _Network=true;
        public bool Network
        {
            get { return _Network; }
            set { SetProperty(ref _Network, value); }
        }
        private string _title = nameof(Title);
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        private WindowState _WindowState;
        /// <summary>
        /// 窗体状态
        /// </summary>
        public WindowState WindowState
        {
            get { return _WindowState; }
            set { _WindowState = value; RaisePropertyChanged(); }
        }
        private Thickness _GlassFrameThickness;

        public Thickness GlassFrameThickness
        {
            get { return _GlassFrameThickness; }
            set { _GlassFrameThickness = value; RaisePropertyChanged(); }
        }

        #endregion
        #region 窗体命令 
        private DelegateCommand<string> _GoBrowser;
        public DelegateCommand<string> GoBrowser =>
            _GoBrowser ?? (_GoBrowser = new DelegateCommand<string>(ExecuteGoBrowser));

        void ExecuteGoBrowser(string uri)
        {
            Process.Start(new ProcessStartInfo(uri));
        }
        #endregion
        #region 核心方法
        public override void ExecuteLoadedCommand()
        {
            base.ExecuteLoadedCommand();
            NetworkHelper.NetworkAvailabilityChanged += Network_NetworkAvailabilityChanged;
        }
        private void Network_NetworkAvailabilityChanged(bool isAvailable)
        {
            Network = isAvailable;
        }
        private DelegateCommand _GoQQ;
        public DelegateCommand GoQQ =>
            _GoQQ ?? (_GoQQ = new DelegateCommand(ExecuteGoQQ));

        void ExecuteGoQQ()
        {
            Process.Start(new ProcessStartInfo("https://qm.qq.com/cgi-bin/qm/qr?k=ewLfhryw080flnV8-Zic4JH3N8IP_aGt&jump_from=webapi&authKey=MVumLNpztW43NPgVMwCzLMTm+T2puC4YN3mjg9eDl6/Fet+Elx+8WYQmRAXISrAF"));
        }
         
        #endregion
    }
}
