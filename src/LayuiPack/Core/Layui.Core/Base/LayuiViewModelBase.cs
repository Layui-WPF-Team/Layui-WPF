using Layui.Core.Log;
using LayuiTemplate.Enum;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layui.Core.Base
{
    public abstract class LayuiViewModelBase : BindableBase, INavigationAware, IRegionMemberLifetime, IConfirmNavigationRequest
    {
        /// <summary>
        /// 导航器
        /// </summary>
        public IRegionManager Region;
        /// <summary>
        /// 弹窗服务
        /// </summary>
        public IDialogService Dialog;
        /// <summary>
        /// 事件聚合器
        /// </summary>
        public IEventAggregator Event;
        /// <summary>
        /// 日志
        /// </summary>
        public ILayLogger Logger;
        public LayuiViewModelBase()
        {
        }
        public LayuiViewModelBase(IContainerExtension container)
        {
            this.Region = container.Resolve<IRegionManager>();
            this.Dialog = container.Resolve<IDialogService>();
            this.Event = container.Resolve<IEventAggregator>();
            this.Logger = container.Resolve<ILayLogger>();
        }
        private DelegateCommand _LoadedCommand;
        public DelegateCommand LoadedCommand =>
            _LoadedCommand ?? (_LoadedCommand = new DelegateCommand(ExecuteLoadedCommand));
        /// <summary>
        ///初始化界面加载
        /// </summary>
        public virtual void ExecuteLoadedCommand()
        {

        }
        /// <summary>
        /// 标记上一个视图时候被销毁
        /// </summary>
        public bool KeepAlive => false;

        public  virtual void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            continuationCallback(true);
        }
        /// <summary>
        /// 导航后的目标视图是否缓存
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <returns></returns>
        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }
        /// <summary>
        /// 导航前
        /// </summary>
        /// <param name="navigationContext"></param>
        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
        /// <summary>
        /// 导航后
        /// </summary>
        /// <param name="navigationContext"></param>
        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
        }
    }
}
