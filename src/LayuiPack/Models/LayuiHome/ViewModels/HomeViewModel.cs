using Layui.Core.AppHelper;
using Layui.Core.Base;
using Layui.Core.Resource;
using LayuiHome.Models;
using LayUI.Wpf.Enum;
using LayUI.Wpf.Global;
using LayUI.Wpf.Tools;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LayuiHome.ViewModels
{
    public class HomeViewModel : LayuiViewModelBase
    {
        private AnimationType _AnimationType = AnimationType.RotateOut;
        public AnimationType AnimationType
        {
            get { return _AnimationType; }
            set { SetProperty(ref _AnimationType, value); }
        }
        public HomeViewModel(IContainerExtension container) : base(container)
        {
        }
        #region 视图属性
        private bool _Network;
        public bool Network
        {
            get { return _Network; }
            set { SetProperty(ref _Network, value); }
        }
        private MenuItemModel _MenuItemModel;
        public MenuItemModel MenuItemModel
        {
            get { return _MenuItemModel; }
            set
            {
                SetProperty(ref _MenuItemModel, value);
                new Action(async () =>
               {
                   AnimationType = AnimationType.SlideOutToLeft;
                   await Task.Delay(300);
                   Region.RequestNavigate(SystemResource.Nav_HomeContent, MenuItemModel.PageKey);
                   AnimationType = AnimationType.SlideInToRight;
               }).Invoke();
            }
        }
        private ObservableCollection<MenuItemModel> _MenuItemList;
        /// <summary>
        /// 导航目录
        /// </summary>
        public ObservableCollection<MenuItemModel> MenuItemList
        {
            get { return _MenuItemList; }
            set { _MenuItemList = value; RaisePropertyChanged(); }
        }
        #endregion
        #region 命令
        /// <summary>
        /// 导航界面
        /// </summary>
        private DelegateCommand<MenuItemModel> _GoPageCommand;
        public DelegateCommand<MenuItemModel> GoPageCommand =>
            _GoPageCommand ?? (_GoPageCommand = new DelegateCommand<MenuItemModel>(GoPage));

        #endregion

        #region 核心方法
        /// <summary>
        /// 跳转界面
        /// </summary>
        /// <param name="PageKey"></param>
        private void GoPage(MenuItemModel item)
        {
            if (item == null) return;
            MenuItemModel = item; 
        }
        #endregion
        public async override void ExecuteLoadedCommand()
        {
            IsAvailable = NetworkHelper.GetLocalNetworkStatus();
            NetworkHelper.NetworkAvailabilityChanged += Network_NetworkAvailabilityChanged;
            MenuItemList = await GetData();
            MenuItemModel = MenuItemList.First().Data.First();
            MenuItemList.First().IsSelected = true;
            MenuItemModel.IsSelected = true;
            LayMessage.Success("欢迎使用Layui—WPF组件库", "RootMessageTooken");
        }

        bool IsAvailable = false;
        private void Network_NetworkAvailabilityChanged(bool isAvailable)
        {
            Network = isAvailable;
            if (!IsAvailable && Network == true)
            {
                Application.Current.Dispatcher.BeginInvoke(new Action(async () =>
                {
                    MenuItemList = await GetData();
                    MenuItemModel = MenuItemList.First().Data.First();
                    MenuItemList.First().IsSelected = true;
                    MenuItemModel.IsSelected = true;
                    LayMessage.Success("网络已恢复", "RootMessageTooken");
                    LayMessage.Success("程序已重新加载", "RootMessageTooken");
                }));
            }
            IsAvailable = Network;
        }
        private Task<ObservableCollection<MenuItemModel>> GetData()
        {

            return Task.FromResult(
                new ObservableCollection<MenuItemModel>()
                {
                new MenuItemModel()
                {
                    ItemTitle = "BasicElements",
                    Data = new ObservableCollection<MenuItemModel>()
                        {
                            new MenuItemModel()
                            {
                                ItemTitle="Icon", PageKey=SystemResource.Page_IconView,IsNew=true
                            },new MenuItemModel()
                            {
                                ItemTitle="Skeleton", PageKey=SystemResource.Page_SkeletonView,IsNew=true
                            },
                            new MenuItemModel()
                            {
                                ItemTitle="Button", PageKey=SystemResource.Page_ButtonView
                            },new MenuItemModel()
                            {
                                ItemTitle="Form", PageKey=SystemResource.Page_FormView
                            },new MenuItemModel()
                            {
                                ItemTitle="Menu", PageKey=SystemResource.Page_MenuView
                            },new MenuItemModel()
                            {
                                ItemTitle="TabControl", PageKey=SystemResource.Page_TabControl
                            },new MenuItemModel()
                            {
                                ItemTitle="ProgressBar", PageKey=SystemResource.Page_ProgressBar
                            },new MenuItemModel()
                            {
                                ItemTitle="Panel", PageKey=SystemResource.Page_PanelView
                            },new MenuItemModel()
                            {
                                ItemTitle="Tag", PageKey=SystemResource.Page_TagView,IsNew=true
                            },new MenuItemModel()
                            {
                                ItemTitle="Expander", PageKey=SystemResource.Page_ExpanderView
                            },new MenuItemModel()
                            {
                                ItemTitle="Transition", PageKey=SystemResource.Page_TransitionControlView
                            },new MenuItemModel()
                            {
                                ItemTitle="Loading", PageKey=SystemResource.Page_LoadingView
                            },new MenuItemModel()
                            {
                                ItemTitle="GIF", PageKey=SystemResource.Page_GIFImageView
                            },new MenuItemModel()
                            {
                                ItemTitle="ScaleImage", PageKey=SystemResource.Page_ScaleImageView
                            },new MenuItemModel()
                            {
                                ItemTitle="Timeline", PageKey=SystemResource.Page_TimelineView
                            },new MenuItemModel()
                            {
                                ItemTitle="AuxiliaryElement", PageKey=SystemResource.Page_AuxiliaryElementView
                            },new MenuItemModel()
                            {
                                ItemTitle="FlowItemsControl", PageKey=SystemResource.Page_FlowItemsControlView
                            }
                        }
                },new MenuItemModel()
                {
                    ItemTitle = "ComponentExamples",
                    Data = new ObservableCollection<MenuItemModel>()
                        {
                            
                            new MenuItemModel()
                            {
                                ItemTitle="AnimationCommand", PageKey=SystemResource.Page_AnimationCommandView
                            },new MenuItemModel()
                            {
                                ItemTitle="ToolTip", PageKey=SystemResource.Page_ToolTipView
                            },new MenuItemModel()
                            {
                                ItemTitle="CodeBox", PageKey=SystemResource.Page_CodeBoxView
                            },new MenuItemModel()
                            {
                                ItemTitle="Badge", PageKey=SystemResource.Page_BadgeView
                            },new MenuItemModel()
                            {
                                ItemTitle="Ripple", PageKey=SystemResource.Page_RippleView,IsNew=true
                            },new MenuItemModel()
                            {
                                ItemTitle="PopupBox", PageKey=SystemResource.Page_PopupBoxView
                            },new MenuItemModel()
                            {
                                ItemTitle="Dialog", PageKey=SystemResource.Page_DialogView
                            },new MenuItemModel()
                            {
                                ItemTitle="Drawer", PageKey=SystemResource.Page_DrawerView
                            },new MenuItemModel()
                            {
                                ItemTitle="DateTime", PageKey=SystemResource.Page_DateTimeView
                            },new MenuItemModel()
                            {
                                ItemTitle="DataGrid", PageKey=SystemResource.Page_DataGrid
                            },new MenuItemModel()
                            {
                                ItemTitle="DataGrid", PageKey=SystemResource.Page_DataGrid
                            },new MenuItemModel()
                            {
                                ItemTitle="Pagination", PageKey=SystemResource.Page_PaginationView
                            },new MenuItemModel()
                            {
                                ItemTitle="DropDownMenu", PageKey=SystemResource.Page_ButtonView
                            },new MenuItemModel()
                            {
                                ItemTitle="Upload", PageKey=SystemResource.Page_UploadView
                            },new MenuItemModel()
                            {
                                ItemTitle="ShuttleGrid", PageKey=SystemResource.Page_ButtonView
                            },new MenuItemModel()
                            {
                                ItemTitle="TreeView", PageKey=SystemResource.Page_TreeView
                            },new MenuItemModel()
                            {
                                ItemTitle="Cascader", PageKey=SystemResource.Page_LayCascaderView
                            },new MenuItemModel()
                            {
                                ItemTitle="TreeSelect", PageKey=SystemResource.Page_TreeSelectView
                            },new MenuItemModel()
                            {
                                ItemTitle="Slider", PageKey=SystemResource.Page_Slider
                            },new MenuItemModel()
                            {
                                ItemTitle="Score", PageKey=SystemResource.Page_ButtonView
                            },new MenuItemModel()
                            {
                                ItemTitle="Carousel", PageKey=SystemResource.Page_CarouselView
                            },new MenuItemModel()
                            {
                                ItemTitle="Message", PageKey=SystemResource.Page_MessageView
                            },new MenuItemModel()
                            {
                                ItemTitle="Notification", PageKey=SystemResource.Page_NotificationView
                            },new MenuItemModel()
                            {
                                ItemTitle="NoticeBar", PageKey=SystemResource.Page_NoticeBarView,IsNew=true
                            },new MenuItemModel()
                            {
                                ItemTitle="VRImage", PageKey=SystemResource.Page_VRView,IsNew=true
                            },new MenuItemModel()
                            {
                                ItemTitle="PropertyGrid", PageKey=SystemResource.Page_PropertyGridView,IsNew=true
                            },new MenuItemModel()
                            {
                                ItemTitle="DecimalText", PageKey=SystemResource.Page_DecimalTextView,IsNew=true
                            },new MenuItemModel()
                            {
                                ItemTitle="VerificationBox", PageKey=SystemResource.Page_VerificationBoxView,IsNew=true
                            },new MenuItemModel()
                            {
                                ItemTitle="Keyboard", PageKey=SystemResource.Page_KeyboardView
                            }
                        }
                },new MenuItemModel()
                {
                    ItemTitle = "递归菜单",
                    Data = new ObservableCollection<MenuItemModel>()
                    {
                        new MenuItemModel()
                        {
                            ItemTitle="二级菜单",
                            Data = new ObservableCollection<MenuItemModel>()
                            {
                                new MenuItemModel()
                                {
                                    ItemTitle="三级菜单",
                                    Data = new ObservableCollection<MenuItemModel>()
                                    {
                                        new MenuItemModel()
                                        {
                                            ItemTitle="四级菜单",
                                            Data = new ObservableCollection<MenuItemModel>()
                                            {
                                                new MenuItemModel()
                                                {
                                                    ItemTitle="五级菜单",
                                                    Data = new ObservableCollection<MenuItemModel>()
                                                    {
                                                        new MenuItemModel()
                                                        {
                                                            ItemTitle="六级菜单"
                                                        },
                                                    }
                                                },
                                            }
                                        },
                                    }
                                },
                            }
                        },
                    }
                }
                });
        }
    }
}
