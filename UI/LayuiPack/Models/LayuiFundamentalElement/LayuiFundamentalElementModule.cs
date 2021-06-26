using Layui.Core.Resource;
using LayuiFundamentalElement.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace LayuiFundamentalElement
{
    /// <summary>
    /// 基本元素模块注册
    /// </summary>
    public class LayuiFundamentalElementModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.Button>(SystemResource.Page_ButtonView);
        }
    }
}