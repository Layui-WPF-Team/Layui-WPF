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
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LayuiApp.ViewModels
{
    public class MainWindowViewModel : LayuiViewModelBase
    {

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
        private string _title = "Layui-WPF";
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
        /// <summary>
        /// 窗体最小化命令
        /// </summary>
        public DelegateCommand MinWindowCommand => new DelegateCommand(MinWindow);

        /// <summary>
        ///窗体最大化命令
        /// </summary>
        public DelegateCommand<bool?> MaxWindowCommand => new DelegateCommand<bool?>(MaxWindow);

        /// <summary>
        /// 关闭窗体
        /// </summary>
        public DelegateCommand CloseWindowCommand => new DelegateCommand(CloseWindow);
        private DelegateCommand<string> _GoBrowser;
        public DelegateCommand<string> GoBrowser =>
            _GoBrowser ?? (_GoBrowser = new DelegateCommand<string>(ExecuteGoBrowser));

        void ExecuteGoBrowser(string uri)
        {
            Process.Start(new ProcessStartInfo(uri));
        }
        #endregion
        #region 核心方法
        /// <summary>
        /// 窗体最小化
        /// </summary>
        private void MinWindow()
        {
            WindowState = WindowState.Minimized;
        }
        /// <summary>
        /// 窗体最大化
        /// </summary>
        /// <param name="obj"></param>
        private void MaxWindow(bool? obj)
        {
            if (obj == true)
            {
                GlassFrameThickness = new Thickness(1);
                WindowState = WindowState.Maximized;
            }
            else
            {
                GlassFrameThickness = new Thickness(0);
                WindowState = WindowState.Normal;
            }
        }
        public override void ExecuteLoadedCommand()
        {
            base.ExecuteLoadedCommand();
            NetworkHelper.NetworkAvailabilityChanged += Network_NetworkAvailabilityChanged;
        }
        private void Network_NetworkAvailabilityChanged(bool isAvailable)
        {
            Network = isAvailable;
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        private void CloseWindow()
        {
            Application.Current.Shutdown();
        }
        #endregion
    }
}
