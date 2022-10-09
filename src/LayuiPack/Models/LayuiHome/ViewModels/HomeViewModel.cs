using Layui.Core.AppHelper;
using Layui.Core.Base;
using Layui.Core.Resource;
using LayuiHome.Models;
using LayuiTemplate.Enum;
using LayuiTemplate.Global;
using LayuiTemplate.Tools;
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
                   AnimationType = AnimationType.RotateOut;
                   await Task.Delay(300);
                   Region.RequestNavigate(SystemResource.Nav_HomeContent, MenuItemModel.PageKey);
                   AnimationType = AnimationType.RotateIn;
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
                    ItemTitle = "基本元素",
                    Data = new ObservableCollection<MenuItemModel>()
                        {
                            new MenuItemModel()
                            {
                                ItemTitle="按钮", PageKey=SystemResource.Page_ButtonView
                            },new MenuItemModel()
                            {
                                ItemTitle="表单", PageKey=SystemResource.Page_FormView
                            },new MenuItemModel()
                            {
                                ItemTitle="基础菜单", PageKey=SystemResource.Page_MenuView
                            },new MenuItemModel()
                            {
                                ItemTitle="选项卡", PageKey=SystemResource.Page_TabControl
                            },new MenuItemModel()
                            {
                                ItemTitle="进度条", PageKey=SystemResource.Page_ProgressBar
                            },new MenuItemModel()
                            {
                                ItemTitle="面板", PageKey=SystemResource.Page_PanelView
                            },new MenuItemModel()
                            {
                                ItemTitle="折叠板", PageKey=SystemResource.Page_ExpanderView
                            },new MenuItemModel()
                            {
                                ItemTitle="过渡动画", PageKey=SystemResource.Page_TransitionControlView
                            },new MenuItemModel()
                            {
                                ItemTitle="加载动画", PageKey=SystemResource.Page_LoadingView
                            },new MenuItemModel()
                            {
                                ItemTitle="Gif动画", PageKey=SystemResource.Page_ImageView
                            },new MenuItemModel()
                            {
                                ItemTitle="时间线", PageKey=SystemResource.Page_TimelineView
                            },new MenuItemModel()
                            {
                                ItemTitle="辅助元素", PageKey=SystemResource.Page_AuxiliaryElementView
                            }
                        }
                },new MenuItemModel()
                {
                    ItemTitle = "组件示例",
                    Data = new ObservableCollection<MenuItemModel>()
                        {
                            new MenuItemModel()
                            {
                                ItemTitle="关键帧动画命令绑定", PageKey=SystemResource.Page_AnimationCommandView
                            },new MenuItemModel()
                            {
                                ItemTitle="鼠标移上信息", PageKey=SystemResource.Page_ToolTipView
                            },new MenuItemModel()
                            {
                                ItemTitle="标记", PageKey=SystemResource.Page_BadgeView
                            },new MenuItemModel()
                            {
                                ItemTitle="弹出框", PageKey=SystemResource.Page_PopupBoxView
                            },new MenuItemModel()
                            {
                                ItemTitle="对话框", PageKey=SystemResource.Page_DialogView
                            },new MenuItemModel()
                            {
                                ItemTitle="抽屉", PageKey=SystemResource.Page_DrawerView
                            },new MenuItemModel()
                            {
                                ItemTitle="日期与时间选择", PageKey=SystemResource.Page_DateTimeView
                            },new MenuItemModel()
                            {
                                ItemTitle="数据表格", PageKey=SystemResource.Page_DataGrid
                            },new MenuItemModel()
                            {
                                ItemTitle="分页", PageKey=SystemResource.Page_PaginationView
                            },new MenuItemModel()
                            {
                                ItemTitle="下拉菜单", PageKey=SystemResource.Page_ButtonView
                            },new MenuItemModel()
                            {
                                ItemTitle="文件上传", PageKey=SystemResource.Page_UploadView
                            },new MenuItemModel()
                            {
                                ItemTitle="穿梭格", PageKey=SystemResource.Page_ButtonView
                            },new MenuItemModel()
                            {
                                ItemTitle="树形组件", PageKey=SystemResource.Page_TreeView
                            },new MenuItemModel()
                            {
                                ItemTitle="滑块", PageKey=SystemResource.Page_Slider
                            },new MenuItemModel()
                            {
                                ItemTitle="评分", PageKey=SystemResource.Page_ButtonView
                            },new MenuItemModel()
                            {
                                ItemTitle="轮播图", PageKey=SystemResource.Page_CarouselView
                            },new MenuItemModel()
                            {
                                ItemTitle="提示信息", PageKey=SystemResource.Page_MessageView
                            },new MenuItemModel()
                            {
                                ItemTitle="键盘", PageKey=SystemResource.Page_KeyboardView
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
