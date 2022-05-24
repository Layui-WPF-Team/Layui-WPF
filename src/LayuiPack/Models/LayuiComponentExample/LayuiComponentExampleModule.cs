using Layui.Core.Resource;
using LayuiComponentExample.Views;
using LayuiTemplate.Global;
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
            containerRegistry.RegisterForNavigation<Views.DataGrid>(SystemResource.Page_DataGrid);
            containerRegistry.RegisterForNavigation<Views.Dialog>(SystemResource.Page_DialogView);
            containerRegistry.RegisterForNavigation<Views.Message>(SystemResource.Page_MessageView);
            LayDialog.RegisterDialog<DialogMessageView>(SystemResource.DialogMessageView);
            LayDialog.RegisterDialog<DialogAlert>("DialogAlert");
        }
    }
}