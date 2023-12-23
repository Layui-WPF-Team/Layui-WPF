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
using System.Diagnostics;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LayuiApp.ViewModels
{
    public class MainWindowViewModel : LayuiViewModelBase
    {
        private string _Message= "------------------新增公告栏控件，详情请看Demo，LayNoticeBar控件-------------《点击我可以直接跳转本项目地址，获取最新代码》";
        public string Message
        {
            get { return _Message; }
            set { SetProperty(ref _Message, value); }
        }
        public MainWindowViewModel(IContainerExtension container) : base(container)
        {
        }
        #region 视图属性
        private bool _Network=true;
        public bool Network
        {
            get { return _Network; }
            set { SetProperty(ref _Network, value); }
        }
        private string _title = "欢迎来到Layui-WPF";
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
