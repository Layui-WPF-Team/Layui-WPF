using Layui.Core.Base;
using Layui.Core.Resource;
using LayuiTemplate.Global;
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
    public class DataGridViewModel : LayuiViewModelBase
    {
        public DataGridViewModel(IContainerExtension container) : base(container)
        {
        }
        private bool _IsActive;
        public bool IsActive
        {
            get { return _IsActive; }
            set { SetProperty(ref _IsActive, value); }
        }
        private ObservableCollection<object> _ListData;
        public ObservableCollection<object> ListData
        {
            get { return _ListData; }
            set { SetProperty(ref _ListData, value); }
        }
        private Task<ObservableCollection<object>> LoadedListData()
        {
            try
            {
                var random = new Random();
                ObservableCollection<object> ListData = new ObservableCollection<object>();
                for (int i = 0; i < 50; i++)
                {
                    int num = random.Next(1, 101);
                    ListData.Add(new { Index = i, Name = "测试" + i, ProgressBarValue = num });
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
        private DelegateCommand<object> _GetItemsCommand;
        public DelegateCommand<object> GetItemsCommand =>
            _GetItemsCommand ?? (_GetItemsCommand = new DelegateCommand<object>(ExecuteGetItemsCommand));

        void ExecuteGetItemsCommand(object data)
        {
            LayDialogParameter dialogParameter = new LayDialogParameter();
            dialogParameter.Add("Message", JsonConvert.SerializeObject(data));
            LayDialog.Show("DialogAlert", dialogParameter);
        }
        private DelegateCommand<object> _DeleteCommand;
        public DelegateCommand<object> DeleteCommand =>
            _DeleteCommand ?? (_DeleteCommand = new DelegateCommand<object>(ExecuteDeleteCommand));

        void ExecuteDeleteCommand(object data)
        {
            ListData.Remove(data);
        }
    }
}
