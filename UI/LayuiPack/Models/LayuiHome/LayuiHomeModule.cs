using Layui.Core.Resource;
using LayuiHome.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace LayuiHome
{
    public class LayuiHomeModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();

            regionManager.RegisterViewWithRegion(SystemResource.NavMainContent, typeof(Home));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}