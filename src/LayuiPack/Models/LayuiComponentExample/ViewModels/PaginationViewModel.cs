using Layui.Core.Base;
using LayuiTemplate.Global;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace LayuiComponentExample.ViewModels
{
    public class PaginationViewModel : LayuiViewModelBase
    {
        public List<int> Limits { get; set; } = new List<int>()
        {
            10,20,30,40,50
        };
        public PaginationViewModel(IContainerExtension container) : base(container)
        {
            
        }
        private bool _IsActive;
        public bool IsActive
        {
            get { return _IsActive; }
            set { SetProperty(ref _IsActive, value); }
        }
        private ObservableCollection<Data> _ListData;
        public ObservableCollection<Data> ListData
        {
            get { return _ListData; }
            set { SetProperty(ref _ListData, value); }
        }
        private Task<ObservableCollection<Data>> LoadedListData()
        {
            try
            {
                var random = new Random();
                ObservableCollection<Data> ListData = new ObservableCollection<Data>();
                for (int i = 0; i < 50; i++)
                {
                    int num = random.Next(1, 101);
                    ListData.Add(new Data() { Index = i, Name = "测试" + i, ProgressBarValue = num });
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
            await Task.Delay(2000);
            ListData = await LoadedListData();
            IsActive = false;

        }
        private DelegateCommand<Data> _GetItemsCommand;
        public DelegateCommand<Data> GetItemsCommand =>
            _GetItemsCommand ?? (_GetItemsCommand = new DelegateCommand<Data>(ExecuteGetItemsCommand));

        void ExecuteGetItemsCommand(Data data)
        {
            LayDialogParameter dialogParameter = new LayDialogParameter();
            dialogParameter.Add("Message", JsonConvert.SerializeObject(data));
            LayDialog.Show("DialogAlert", dialogParameter);
        }
        private DelegateCommand<Data> _DeleteCommand;
        public DelegateCommand<Data> DeleteCommand =>
            _DeleteCommand ?? (_DeleteCommand = new DelegateCommand<Data>(ExecuteDeleteCommand));

        void ExecuteDeleteCommand(Data data)
        {
            ListData.Remove(data);
        }
        public class Data : BindableBase
        {
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
            private int _ProgressBarValue;
            public int ProgressBarValue
            {
                get { return _ProgressBarValue; }
                set { SetProperty(ref _ProgressBarValue, value); }
            }
        }
        private DelegateCommand<int?> _PageUpdatedCommand;
        public DelegateCommand<int?> PageUpdatedCommand =>
            _PageUpdatedCommand ?? (_PageUpdatedCommand = new DelegateCommand<int?>(ExecutePageUpdatedCommand));

        void ExecutePageUpdatedCommand(int? JumpIndex)
        {
            LayMessage.Success($"查询当前第{JumpIndex}页数据");
        }
    }
}
