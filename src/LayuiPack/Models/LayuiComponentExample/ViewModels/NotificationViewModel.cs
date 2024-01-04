using Layui.Core.Base;
using LayUI.Wpf.Global;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LayuiComponentExample.ViewModels
{
    public class NotificationViewModel : LayuiViewModelBase
    {
        public NotificationViewModel(IContainerExtension container) : base(container)
        {
        }
        private DelegateCommand _GoCommand;
        public DelegateCommand GoCommand =>
            _GoCommand ?? (_GoCommand = new DelegateCommand(ExecuteGoCommand));

        async void ExecuteGoCommand()
        {
            LayNotification.Default("这是标题", "这是内容", (o) => {
                LayMessage.Success($"显示：{5}秒");
            },null,5.0);
            await Task.Delay(100);
            LayNotification.Info("这是标题", "这是内容");
            await Task.Delay(100);
            LayNotification.Success("这是标题", "这是内容");
            await Task.Delay(100);
            LayNotification.Warning("这是标题", "这是内容");
            await Task.Delay(100);
            LayNotification.Error("这是标题", "这是内容", "确定", (o) =>
            {
                LayMessage.Success($"这是回调状态：{o}");
            });
        }
    }
}
