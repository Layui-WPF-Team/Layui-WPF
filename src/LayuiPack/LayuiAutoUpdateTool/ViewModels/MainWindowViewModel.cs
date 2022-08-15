using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LayuiAutoUpdateTool.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _Message;
        /// <summary>
        /// 升级提示信息
        /// </summary>
        public string Message
        {
            get { return _Message; }
            set { SetProperty(ref _Message, value); }
        }
        private ObservableCollection<string> _Titles = new ObservableCollection<string>() {
        "这一款Layui-WPF版本UI组件","😊加油啊阿科"
        };
        public ObservableCollection<string> Titles
        {
            get { return _Titles; }
            set { SetProperty(ref _Titles, value); }
        }
        private double _FileSize;
        /// <summary>
        /// 文件大小
        /// </summary>
        public double FileSize
        {
            get { return _FileSize; }
            set { SetProperty(ref _FileSize, value); }
        }
        private double _DownloadedSize;
        /// <summary>
        /// 已解压文件大小
        /// </summary>
        public double DownloadedSize
        {
            get { return _DownloadedSize; }
            set { SetProperty(ref _DownloadedSize, value); }
        }
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {

        }
        private DelegateCommand _LoadedCommand;
        public DelegateCommand LoadedCommand =>
            _LoadedCommand ?? (_LoadedCommand = new DelegateCommand(ExecuteLoadedCommand));

        async void ExecuteLoadedCommand()
        {
            FileSize = 100;
            for (int i = 1; i <= 100; i++)
            {
                await Task.Delay(5);
                DownloadedSize = i;
                Message = $"当前进度{i}%";
            }
            System.Diagnostics.Process.Start($"{System.Environment.CurrentDirectory}\\LayuiApp.exe");
            System.Environment.Exit(0);
        }
    }
}
