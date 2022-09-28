using Layui.Core.AppHelper;
using Layui.Core.Log;
using LayuiApp.Views;
using LayuiComponentExample;
using LayuiFundamentalElement;
using LayuiHome;
using LayuiTemplate.Global;
using LayuiTemplate.Tools;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.IO;
using System.Windows;

namespace LayuiApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public App()
        {
            //初始化日志配置信息
            log4net.Config.XmlConfigurator.Configure();
            NetworkHelper.Initialization();
        }
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
        protected override void OnStartup(StartupEventArgs e)
        {

            base.OnStartup(e);
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }
        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //注入自定义接口
            containerRegistry.RegisterInstance<ILayLogger>(new LayLogger());
            LayDialog.RegisterDialogWindow<DialogWindowBase>("window");
        }
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<LayuiHomeModule>();
            moduleCatalog.AddModule<LayuiFundamentalElementModule>();
            moduleCatalog.AddModule<LayuiComponentExampleModule>();
        }
    }
}
