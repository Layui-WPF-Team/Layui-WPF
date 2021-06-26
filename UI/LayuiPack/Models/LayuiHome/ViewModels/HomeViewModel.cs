using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LayuiHome.ViewModels
{
    public class HomeViewModel : BindableBase
    {
        public HomeViewModel()
        {
            MenuItemList = new ObservableCollection<Menu>();
            MenuItemList.Add(new Menu()
            {
                Title = "基本元素",
                Data = new ObservableCollection<Item>()
                    {
                        new Item()
                        {
                            ItemTitle="按钮"
                        },new Item()
                        {
                            ItemTitle="表单"
                        },new Item()
                        {
                            ItemTitle="基础菜单"
                        },new Item()
                        {
                            ItemTitle="选项卡"
                        },new Item()
                        {
                            ItemTitle="进度条"
                        },new Item()
                        {
                            ItemTitle="面板"
                        },new Item()
                        {
                            ItemTitle="徽章"
                        },new Item()
                        {
                            ItemTitle="动画"
                        }
                    }
            }); MenuItemList.Add(new Menu()
            {
                Title = "组件示例",
                Data = new ObservableCollection<Item>()
                    {
                        new Item()
                        {
                            ItemTitle="弹出层"
                        },new Item()
                        {
                            ItemTitle="日期与时间选择"
                        },new Item()
                        {
                            ItemTitle="数据表格"
                        },new Item()
                        {
                            ItemTitle="下拉菜单"
                        },new Item()
                        {
                            ItemTitle="文件上传"
                        },new Item()
                        {
                            ItemTitle="穿梭格"
                        },new Item()
                        {
                            ItemTitle="树形组件"
                        },new Item()
                        {
                            ItemTitle="滑块"
                        },new Item()
                        {
                            ItemTitle="评分"
                        },new Item()
                        {
                            ItemTitle="轮播图"
                        }
                    }
            });
        }
        private ObservableCollection<Menu> _MenuItemList;

        public ObservableCollection<Menu> MenuItemList
        {
            get { return _MenuItemList; }
            set { _MenuItemList = value; }
        }

    }
    public class Menu
    {
        public string Title { get; set; }
        public ObservableCollection<Item> Data { get; set; }
    }
    public class Item
    {
        public string ItemTitle { get; set; }

    }
}
