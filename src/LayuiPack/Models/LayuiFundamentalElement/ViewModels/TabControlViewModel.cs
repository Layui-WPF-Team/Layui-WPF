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
    public class TabControlViewModel : LayuiViewModelBase
    {
        public TabControlViewModel(IContainerExtension container) : base(container)
        {

        }
        private PageModel _PageModel;
        public PageModel PageModel
        {
            get { return _PageModel; }
            set { SetProperty(ref _PageModel, value); }
        }
        private ObservableCollection<PageModel> _PageList = new ObservableCollection<PageModel>() {
        new PageModel(){ Title="选项卡一",Content="123"},
        new PageModel(){ Title="选项卡二",Content="123"},
        new PageModel(){ Title="选项卡三",Content="qwe"},
        };
        public ObservableCollection<PageModel> PageList
        {
            get { return _PageList; }
            set { SetProperty(ref _PageList, value); }
        }
        private DelegateCommand _AddCommand;
        public DelegateCommand AddCommand =>
            _AddCommand ?? (_AddCommand = new DelegateCommand(ExecuteAddCommand));

        void ExecuteAddCommand()
        {
            PageModel model = new PageModel() { Title = "新增标签", Content = "新增标签内容" };
            PageList.Add(model);
            PageModel = model;
        }
        private DelegateCommand<PageModel> _DeleteCommand;
        public DelegateCommand<PageModel> DeleteCommand =>
            _DeleteCommand ?? (_DeleteCommand = new DelegateCommand<PageModel>(ExecuteDeleteCommand));

        void ExecuteDeleteCommand(PageModel model)
        {
            PageList.Remove(model);
            PageModel = PageList.FirstOrDefault();
        }
    }
    public class PageModel : BindableBase
    {
        private string _Title;
        public string Title
        {
            get { return _Title; }
            set { SetProperty(ref _Title, value); }
        }
        private object _Content;
        public object Content
        {
            get { return _Content; }
            set { SetProperty(ref _Content, value); }
        }
    }
}
