using Layui.Core.Base;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Windows;
using System.Windows.Input;

namespace LayuiApp.ViewModels
{
    public class MainWindowViewModel : LayuiViewModelBase
    {

        public MainWindowViewModel(IRegionManager regionManager, IDialogService dialogServic) :base(regionManager, dialogServic)
        {

        }

        #region 视图属性
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
        private Thickness _GlassFrameThickness ;

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
