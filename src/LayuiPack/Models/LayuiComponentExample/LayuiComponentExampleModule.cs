﻿using Layui.Core.Resource;
using LayuiComponentExample.Views;
using LayUI.Wpf.Global;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using LayuiComponentExample.ViewModels;

namespace LayuiComponentExample
{
    public class LayuiComponentExampleModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
 
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<DialogMessageViewModel>();
            containerRegistry.RegisterForNavigation<Views.Slider>(SystemResource.Page_Slider);
            containerRegistry.RegisterForNavigation<Views.DataGrid>(SystemResource.Page_DataGrid);
            containerRegistry.RegisterForNavigation<Views.Dialog>(SystemResource.Page_DialogView);
            containerRegistry.RegisterForNavigation<Views.Message>(SystemResource.Page_MessageView);
            containerRegistry.RegisterForNavigation<Views.Carousel>(SystemResource.Page_CarouselView);
            containerRegistry.RegisterForNavigation<Views.DateTime>(SystemResource.Page_DateTimeView);
            containerRegistry.RegisterForNavigation<Views.Badge>(SystemResource.Page_BadgeView);
            containerRegistry.RegisterForNavigation<Views.Pagination>(SystemResource.Page_PaginationView); 
            containerRegistry.RegisterForNavigation<Views.PopupBox>(SystemResource.Page_PopupBoxView);
            containerRegistry.RegisterForNavigation<Views.Drawer>(SystemResource.Page_DrawerView);
            containerRegistry.RegisterForNavigation<Views.ToolTip>(SystemResource.Page_ToolTipView);
            containerRegistry.RegisterForNavigation<Views.AnimationCommand>(SystemResource.Page_AnimationCommandView);
            containerRegistry.RegisterForNavigation<Views.Upload>(SystemResource.Page_UploadView);
            containerRegistry.RegisterForNavigation<Views.FlowItemsControl>(SystemResource.Page_FlowItemsControlView);
            containerRegistry.RegisterForNavigation<Views.NotificationView>(SystemResource.Page_NotificationView);
            containerRegistry.RegisterForNavigation<Views.TreeSelect>(SystemResource.Page_TreeSelectView);
            containerRegistry.RegisterForNavigation<Views.Icon>(SystemResource.Page_IconView);
            containerRegistry.RegisterForNavigation<Views.Tag>(SystemResource.Page_TagView);
            containerRegistry.RegisterForNavigation<Views.NoticeBar>(SystemResource.Page_NoticeBarView); 
            containerRegistry.RegisterForNavigation<Views.Ripple>(SystemResource.Page_RippleView); 
            containerRegistry.RegisterForNavigation<Views.LayCascader>(SystemResource.Page_LayCascaderView);
            containerRegistry.RegisterForNavigation<Views.VR>(SystemResource.Page_VRView);
            containerRegistry.RegisterForNavigation<Views.DecimalText>(SystemResource.Page_DecimalTextView);
            containerRegistry.RegisterForNavigation<Views.PropertyGrid>(SystemResource.Page_PropertyGridView);
            containerRegistry.RegisterForNavigation<Views.CodeBox>(SystemResource.Page_CodeBoxView);
            containerRegistry.RegisterForNavigation<Views.WaterfallPanel>(SystemResource.Page_WaterfallPanelView);
            LayDialog.RegisterDialog<DialogMessageView>(SystemResource.DialogMessageView);  
            containerRegistry.RegisterDialog<DefaultDialog>(nameof(DefaultDialog));
            LayDialog.RegisterDialog<DialogAlert>("DialogAlert");
        }
    }
}