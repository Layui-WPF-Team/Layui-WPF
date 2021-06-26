using Layui.Core.Resource;
using LayuiHome.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace LayuiHome
{
    /// <summary>
    /// 首页模块注册
    /// </summary>
    public class LayuiHomeModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();

            regionManager.RegisterViewWithRegion(SystemResource.Nav_MainContent, typeof(Home));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}