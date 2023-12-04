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
            containerRegistry.RegisterForNavigation<Views.Form>(SystemResource.Page_FormView);
            containerRegistry.RegisterForNavigation<Views.Menu>(SystemResource.Page_MenuView);
            containerRegistry.RegisterForNavigation<Views.ProgressBar>(SystemResource.Page_ProgressBar);
            containerRegistry.RegisterForNavigation<Views.TabControl>(SystemResource.Page_TabControl);
            containerRegistry.RegisterForNavigation<Views.TransitionControl>(SystemResource.Page_TransitionControlView);
            containerRegistry.RegisterForNavigation<Views.Expander>(SystemResource.Page_ExpanderView);
            containerRegistry.RegisterForNavigation<Views.Loading>(SystemResource.Page_LoadingView);
            containerRegistry.RegisterForNavigation<Views.Panel>(SystemResource.Page_PanelView);
            containerRegistry.RegisterForNavigation<Views.Timeline>(SystemResource.Page_TimelineView);
            containerRegistry.RegisterForNavigation<Views.TreeView>(SystemResource.Page_TreeView);
            containerRegistry.RegisterForNavigation<Views.AuxiliaryElement>(SystemResource.Page_AuxiliaryElementView);
            containerRegistry.RegisterForNavigation<Views.GIFImage>(SystemResource.Page_GIFImageView);
            containerRegistry.RegisterForNavigation<Views.ScaleImage>(SystemResource.Page_ScaleImageView);
            containerRegistry.RegisterForNavigation<Views.Keyboard>(SystemResource.Page_KeyboardView);
        }
    }
}