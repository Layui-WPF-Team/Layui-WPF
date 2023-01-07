using LayUI.Wpf.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Wpf.Dialog.Interface
{
    public interface ILayDialog
    {
        /// <summary>
        /// 普通弹窗
        /// </summary>
        /// <param name="dialogName">窗体名称</param>
        /// <param name="parameters">参数</param>
        void Show(string dialogName, ILayDialogParameter parameters, string tooken);
        /// <summary>
        /// 普通弹窗
        /// </summary>
        /// <param name="dialogName">窗体名称</param>
        /// <param name="parameters">参数</param>
        void ShowWindow(string dialogName, ILayDialogParameter parameters, string windowName);
        /// <summary>
        /// 普通弹窗
        /// </summary>
        /// <param name="dialogName">窗体名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="callback">回调</param>
        void Show(string dialogName, ILayDialogParameter parameters,Action<ILayDialogResult> callback, string tooken);
        /// <summary>
        /// 普通弹窗
        /// </summary>
        /// <param name="dialogName">窗体名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="callback">回调</param>
        void ShowWindow(string dialogName, ILayDialogParameter parameters, Action<ILayDialogResult> callback, string windowName);
        /// <summary>
        /// 模态对话框
        /// </summary>
        /// <param name="dialogName">窗体名称</param>
        /// <param name="parameters">参数</param>
        void ShowDialog(string dialogName, ILayDialogParameter parameters, string tooken);
        /// <summary>
        /// 模态对话框
        /// </summary>
        /// <param name="dialogName">窗体名称</param>
        /// <param name="parameters">参数</param>
        void ShowDialogWindow(string dialogName, ILayDialogParameter parameters,  string windowName);
        /// <summary>
        /// 模态对话框
        /// </summary>
        /// <param name="dialogName">窗体名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="callback">回调</param>
        void ShowDialog(string dialogName, ILayDialogParameter parameters, Action<ILayDialogResult> callback, string tooken);
        /// <summary>
        /// 模态对话框
        /// </summary>
        /// <param name="dialogName">窗体名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="callback">回调</param>
        void ShowDialogWindow(string dialogName, ILayDialogParameter parameters, Action<ILayDialogResult> callback,string windowName);
    }
}
