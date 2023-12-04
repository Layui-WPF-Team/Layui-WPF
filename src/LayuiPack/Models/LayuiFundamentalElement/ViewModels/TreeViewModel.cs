using Layui.Core.Base;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LayuiFundamentalElement.ViewModels
{
    public class TreeViewModel : LayuiViewModelBase
    {
        private ObservableCollection<CheckBoxTreeViewModel> _ListApps;
        /// <summary>
        /// 数据集合
        /// </summary>
        public ObservableCollection<CheckBoxTreeViewModel> ListApps
        {
            get { return _ListApps; }
            set { SetProperty(ref _ListApps, value); }
        }
        private ObservableCollection<CheckBoxTreeViewModel> _SelectItems = new ObservableCollection<CheckBoxTreeViewModel>();
        /// <summary>
        /// 备选集合
        /// </summary>
        public ObservableCollection<CheckBoxTreeViewModel> SelectItems
        {
            get { return _SelectItems; }
            set { SetProperty(ref _SelectItems, value); }
        }
        public TreeViewModel(IContainerExtension container) : base(container)
        {
            ListApps = new ObservableCollection<CheckBoxTreeViewModel>()
            {
                new CheckBoxTreeViewModel(){
                    Id="1",
                    Title="年级",
                    Child=new ObservableCollection<CheckBoxTreeViewModel>()
                    {
                        new CheckBoxTreeViewModel(){
                            FatherId="1",
                            Id="1-1",
                            Title="一年级",
                            Child=new ObservableCollection<CheckBoxTreeViewModel>()
                            {
                                new CheckBoxTreeViewModel(){
                                    FatherId="1-1",
                                    Id="1-1-1",
                                    Title="一班",
                                    Child=new ObservableCollection<CheckBoxTreeViewModel>()
                                    {
                                        new CheckBoxTreeViewModel(){
                                            FatherId="1-1-1",
                                            Id="1-1-1-1",
                                            Title="张三",
                                            Child=null
                                        },new CheckBoxTreeViewModel(){
                                            FatherId="1-1-1",
                                            Id="1-1-1-2",
                                            Title="李四",
                                            Child=null
                                        }
                                    }
                                },new CheckBoxTreeViewModel(){
                                    FatherId="1-1",
                                    Id="1-1-2",
                                    Title="二班",
                                    Child=new ObservableCollection<CheckBoxTreeViewModel>()
                                    {
                                        new CheckBoxTreeViewModel(){
                                            FatherId="1-1-2",
                                            Id="1-1-2-1",
                                            Title="王五",
                                            Child=null
                                        },new CheckBoxTreeViewModel(){
                                            FatherId="1-1-2",
                                            Id="1-1-2-2",
                                            Title="赵六",
                                            Child=null
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
            };
        }
        private DelegateCommand<CheckBoxTreeViewModel> _CheckedCommand;
        /// <summary>
        /// 复选框点击效果
        /// </summary>
        public DelegateCommand<CheckBoxTreeViewModel> CheckedCommand =>
            _CheckedCommand ?? (_CheckedCommand = new DelegateCommand<CheckBoxTreeViewModel>(ExecuteCheckedCommand));

        void ExecuteCheckedCommand(CheckBoxTreeViewModel parameter)
        {
            CheckPrent(ListApps, parameter);
            CheckItems(parameter.Child, parameter);
        }
        private DelegateCommand _CheckedItemCommand;
        /// <summary>
        /// 设置选中数据效果
        /// </summary>
        public DelegateCommand CheckedItemCommand =>
            _CheckedItemCommand ?? (_CheckedItemCommand = new DelegateCommand(ExecuteCheckedItemCommand));

        void ExecuteCheckedItemCommand()
        {
            var data = new CheckBoxTreeViewModel()
            {
                IsChecked = true,
                FatherId = "1-1-1",
                Id = "1-1-1-1",
                Title = "四级菜单"
            };
            OnCheckPrent(ListApps, data);
            RefreshItemsIsChecked(ListApps, data);
        }
        private DelegateCommand _GetCheckedItemsCommand;
        /// <summary>
        /// 抓取被选中数据
        /// </summary>
        public DelegateCommand GetCheckedItemsCommand =>
            _GetCheckedItemsCommand ?? (_GetCheckedItemsCommand = new DelegateCommand(ExecuteGetCheckedItemsCommand));

        void ExecuteGetCheckedItemsCommand()
        {
            SelectItems = GetCheckItems(ListApps, new ObservableCollection<CheckBoxTreeViewModel>());
        }
        /// <summary>
        /// 设置父类选中状态
        /// </summary>
        /// <param name="apps">需要设置的集合</param>
        /// <param name="info">被操纵对象</param>
        private void CheckPrent(ObservableCollection<CheckBoxTreeViewModel> apps, CheckBoxTreeViewModel info)
        {
            OnCheckPrent(apps, info);
            UnCheckPrent(apps, info);
        }
        /// <summary>
        /// 递归刷新全选和反选状态效果
        /// </summary>
        /// <param name="apps">需要设置的集合</param>
        /// <param name="info">被操纵对象</param>
        private void CheckItems(ObservableCollection<CheckBoxTreeViewModel> apps, CheckBoxTreeViewModel info)
        {
            if (apps == null) return;
            foreach (var item in apps)
            {
                item.IsChecked = info.IsChecked;
                CheckItems(item.Child, info);
            }
        }
        /// <summary>
        /// 设置父类选中状态
        /// </summary>
        /// <param name="apps">需要设置的集合</param>
        /// <param name="info">被操纵对象</param>
        private void OnCheckPrent(ObservableCollection<CheckBoxTreeViewModel> apps, CheckBoxTreeViewModel info)
        {
            foreach (var item in apps)
            {
                if (item.Id == info.FatherId)
                {
                    if (info.IsChecked == true)
                    {
                        item.IsChecked = true;
                        if (item.FatherId == null) return;
                        OnCheckPrent(ListApps, item);
                    }
                }
                else
                {
                    if (item.Child == null) return;
                    OnCheckPrent(item.Child, info);
                }
            }
        }
        /// <summary>
        ///设置父类取消选中状态
        /// </summary>
        /// <param name="apps">需要设置的集合</param>
        /// <param name="info">被操纵对象</param>
        private void UnCheckPrent(ObservableCollection<CheckBoxTreeViewModel> apps, CheckBoxTreeViewModel info)
        {
            foreach (var item in apps)
            {
                if (item.Id == info.FatherId)
                {
                    if (!info.IsChecked == true)
                    {
                        /////////////设置抓取复选框///////////
                        var unCheckNum = item.Child.ToList().Where(o => o.IsChecked == false).Count();
                        if (unCheckNum == item.Child.Count) item.IsChecked = false;
                        if (item.FatherId == null) return;
                        UnCheckPrent(ListApps, item);
                    }
                }
                else
                {
                    if (item.Child == null) return;
                    UnCheckPrent(item.Child, info);
                }
            }
        }
        /// <summary>
        /// 初始化加载刷新集合列表选中状态
        /// </summary>
        /// <param name="apps">需要设置的集合</param>
        /// <param name="info">被操纵对象</param>
        private void RefreshItemsIsChecked(ObservableCollection<CheckBoxTreeViewModel> apps, CheckBoxTreeViewModel info)
        {
            if (apps == null) return;
            foreach (var item in apps)
            {
                if (item.Id == info.Id)
                {
                    item.IsChecked = true;
                }
                else
                {
                    RefreshItemsIsChecked(item.Child, info);
                }
            }
        }
        /// <summary>
        /// 抓取现有集合中的复选数据
        /// </summary>
        /// <param name="apps">需要设置的集合</param>
        /// <param name="items">被填充数据集合</param>
        /// <returns></returns>
        private ObservableCollection<CheckBoxTreeViewModel> GetCheckItems(ObservableCollection<CheckBoxTreeViewModel> apps, ObservableCollection<CheckBoxTreeViewModel> items)
        {
            if (items == null) return null;
            if (apps == null) return null;
            foreach (var item in apps)
            {
                if (item.Child != null) GetCheckItems(item.Child, items);
                else
                {
                    if (item.IsChecked==true) items.Add(item);
                }

            }
            return items;
        }
    }
    /// <summary>
    /// 树结构模型
    /// </summary>
    public class CheckBoxTreeViewModel : BindableBase
    {
        private string _Id;
        /// <summary>
        /// 当前ID
        /// </summary>
        public string Id
        {
            get { return _Id; }
            set { SetProperty(ref _Id, value); }
        }
        private string _FatherId;
        /// <summary>
        /// 父级ID
        /// </summary>
        public string FatherId
        {
            get { return _FatherId; }
            set { SetProperty(ref _FatherId, value); }
        }
        private bool? _IsChecked=false;
        /// <summary>
        /// 复选框状态
        /// </summary>
        public bool? IsChecked
        {
            get { return _IsChecked; }
            set { SetProperty(ref _IsChecked, value); }
        }
        private string _Title;
        /// <summary>
        /// 标题信息
        /// </summary>
        public string Title
        {
            get { return _Title; }
            set { SetProperty(ref _Title, value); }
        }
        private ObservableCollection<CheckBoxTreeViewModel> _Child;
        /// <summary>
        /// 子集
        /// </summary>
        public ObservableCollection<CheckBoxTreeViewModel> Child
        {
            get { return _Child; }
            set { SetProperty(ref _Child, value); }
        }
    }
}
