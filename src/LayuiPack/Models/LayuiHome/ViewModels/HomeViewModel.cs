using Layui.Core.Base;
using Layui.Core.Resource;
using LayuiHome.Models;
using LayuiTemplate.Global;
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

namespace LayuiHome.ViewModels
{
    public class HomeViewModel : LayuiViewModelBase
    {
        public HomeViewModel(IContainerExtension container) : base(container)
        {
        }
        #region 视图属性
        private MenuItemModel _MenuItemModel;
        public MenuItemModel MenuItemModel
        {
            get { return _MenuItemModel; }
            set
            {
                SetProperty(ref _MenuItemModel, value);
                Region.RequestNavigate(SystemResource.Nav_HomeContent, MenuItemModel.PageKey);
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
            MenuItemList = await GetData();
            MenuItemModel = MenuItemList.First().Data.First();
            MenuItemList.First().IsSelected = true;
            MenuItemModel.IsSelected = true;
            LayMessage.Success("欢迎使用Layui—WPF组件库", "RootMessageTooken");
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
                                ItemTitle="徽章", PageKey=SystemResource.Page_ButtonView
                            },new MenuItemModel()
                            {
                                ItemTitle="过渡动画", PageKey=SystemResource.Page_TransitionControlView
                            },new MenuItemModel()
                            {
                                ItemTitle="加载动画", PageKey=SystemResource.Page_LoadingView
                            },new MenuItemModel()
                            {
                                ItemTitle="Gif动画", PageKey=SystemResource.Page_GIFView
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
                                ItemTitle="弹出层", PageKey=SystemResource.Page_DialogView
                            },new MenuItemModel()
                            {
                                ItemTitle="日期与时间选择", PageKey=SystemResource.Page_DateTimeView
                            },new MenuItemModel()
                            {
                                ItemTitle="数据表格", PageKey=SystemResource.Page_DataGrid
                            },new MenuItemModel()
                            {
                                ItemTitle="下拉菜单", PageKey=SystemResource.Page_ButtonView
                            },new MenuItemModel()
                            {
                                ItemTitle="文件上传", PageKey=SystemResource.Page_ButtonView
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
