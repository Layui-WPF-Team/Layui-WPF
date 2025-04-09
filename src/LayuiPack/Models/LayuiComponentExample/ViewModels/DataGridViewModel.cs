using Layui.Core.Base;
using Layui.Core.Resource;
using LayUI.Wpf.Global;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace LayuiComponentExample.ViewModels
{
    public class DataInfo : BindableBase
    {
        private bool _IsChecked;
        public bool IsChecked
        {
            get { return _IsChecked; }
            set { SetProperty(ref _IsChecked, value); }
        }
        private int _Index;
        public int Index
        {
            get { return _Index; }
            set { SetProperty(ref _Index, value); }
        }
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { SetProperty(ref _Name, value); }
        }
        private double _ProgressBarValue;
        public double ProgressBarValue
        {
            get { return _ProgressBarValue; }
            set { SetProperty(ref _ProgressBarValue, value); }
        }
    }
    public class DataGridViewModel : LayuiViewModelBase
    {
        public List<int> Limits { get; set; } = new List<int>()
        {
            10,20,30,40,50
        };
        private int _PageIndex=1;
        /// <summary>
        /// 记录当前的页数为第几页
        /// </summary>
        public int PageIndex
        {
            get { return _PageIndex; }
            set { SetProperty(ref _PageIndex, value); }
        }
        private int _PageSize = 30;
        /// <summary>
        /// 每一页显示数据的条数，在这里让每一页显示12条
        /// </summary>
        public int PageSize
        {
            get { return _PageSize; }
            set { SetProperty(ref _PageSize, value); }
        }
        private int _Total;
        public int Total
        {
            get { return _Total; }
            private set { SetProperty(ref _Total, value); }
        }
        public DataGridViewModel(IContainerExtension container) : base(container)
        {
        }
        private Visibility _Visibility;
        /// <summary>
        /// 控制当前的列展示状态
        /// </summary>
        public Visibility Visibility
        {
            get { return _Visibility; }
            set { SetProperty(ref _Visibility, value); }
        }
        private bool _IsShow=true;
        public bool IsShow
        {
            get { return _IsShow; }
            set { 
                SetProperty(ref _IsShow, value);
                if (value) Visibility = Visibility.Visible;
                else Visibility = Visibility.Collapsed;
            }
        }
        private bool _IsActive;
        public bool IsActive
        {
            get { return _IsActive; }
            set { SetProperty(ref _IsActive, value); }
        }
        public ObservableCollection<DataInfo> _Data; 
        private ObservableCollection<DataInfo> _ListData;
        public ObservableCollection<DataInfo> ListData
        {
            get { return _ListData; }
            set { SetProperty(ref _ListData, value); }
        }
        private Task<ObservableCollection<DataInfo>> LoadedListData()
        {
            try
            {
                var random = new Random();
                ObservableCollection<DataInfo> ListData = new ObservableCollection<DataInfo>();
                for (int i = 0; i < 10000; i++)
                {
                    int num = random.Next(1, 101);
                    ListData.Add(new DataInfo(){ Index = i, Name = "测试" + i, ProgressBarValue = num });
                };
                return Task.FromResult(ListData);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async override void ExecuteLoadedCommand()
        {
            base.ExecuteLoadedCommand();
            IsActive = true;
            await Task.Delay(0);
            _Data = await LoadedListData();
            Total= _Data.Count;
            ExecutePageUpdatedCommand(PageIndex);
            IsActive = false;

        }
        private DelegateCommand<DataInfo> _GetItemsCommand;
        public DelegateCommand<DataInfo> GetItemsCommand =>
            _GetItemsCommand ?? (_GetItemsCommand = new DelegateCommand<DataInfo>(ExecuteGetItemsCommand));

        void ExecuteGetItemsCommand(DataInfo data)
        {
            LayDialogParameter dialogParameter = new LayDialogParameter();
            dialogParameter.Add("Message", JsonConvert.SerializeObject(data));
            LayDialog.Show("DialogAlert", dialogParameter);
        }
        private DelegateCommand<DataInfo> _DeleteCommand;
        public DelegateCommand<DataInfo> DeleteCommand =>
            _DeleteCommand ?? (_DeleteCommand = new DelegateCommand<DataInfo>(ExecuteDeleteCommand));

        void ExecuteDeleteCommand(DataInfo data)
        {
            ListData.Remove(data);
        }
        private DelegateCommand<object> _PageUpdatedCommand;
        public DelegateCommand<object> PageUpdatedCommand =>
            _PageUpdatedCommand ?? (_PageUpdatedCommand = new DelegateCommand<object>(ExecutePageUpdatedCommand));

        void ExecutePageUpdatedCommand(object obj)
        {
            if (obj is int index) PageIndex = index;
            ListData = new ObservableCollection<DataInfo>(_Data.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList());
        }
    }
}
