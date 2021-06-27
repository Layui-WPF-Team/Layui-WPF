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
    public abstract class LayuiViewModelBase : BindableBase, INavigationAware, IConfirmNavigationRequest
    {
        /// <summary>
        /// 导航器
        /// </summary>
        public IRegionManager regionManager;
        /// <summary>
        /// 弹窗服务
        /// </summary>
        public IDialogService dialogService;
        public LayuiViewModelBase(IRegionManager regionManager,IDialogService dialogService) {
            this.regionManager = regionManager;
            this.dialogService = dialogService;
        }

        public virtual void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
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
