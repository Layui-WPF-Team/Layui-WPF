using Layui.Core.Resource;
using LayuiComponentExample.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace LayuiComponentExample
{
    public class LayuiComponentExampleModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
 
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

            containerRegistry.RegisterForNavigation<Views.Slider>(SystemResource.Page_Slider);
        }
    }
}