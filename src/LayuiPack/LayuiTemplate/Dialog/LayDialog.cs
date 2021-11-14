using LayuiTemplate.Control;
using LayuiTemplate.Dialog.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Threading;

namespace LayuiTemplate.Dialog
{
    /// <summary>
    /// LayDialog控制器
    /// </summary>
    public class LayDialog : ILayDialog
    {
        private static DispatcherFrame dispatcherFrame = null;
        /// <summary>
        /// 初始化LayDialog
        /// </summary>
        public static LayDialog Dialog { get; set; } = Dialog ?? new LayDialog();
        /// <summary>
        /// 被注入的窗体集合
        /// </summary>
        public static Dictionary<string, LayDialogUserControl> DialogViewCollection { get; set; } = DialogViewCollection ?? new Dictionary<string, LayDialogUserControl>();
        public LayDialog()
        {
        }
        /// <summary>
        /// 注入弹窗试图
        /// </summary>
        /// <typeparam name="TView">需要注入的类型</typeparam>
        /// <param name="dialogName">视图名称</param>
        public static void RegisterDialog<TView>(string dialogName)
        {
            try
            {
                foreach (var item in DialogViewCollection)
                {
                    if (item.Key == dialogName) throw new Exception($"{dialogName}弹窗视图多次注入");
                }
                var data = (UserControl)Activator.CreateInstance(typeof(TView));
                DialogViewCollection.Add(dialogName, new LayDialogUserControl()
                {
                    Content = data,
                    DataContext = data.DataContext
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 普通弹窗
        /// </summary>
        /// <param name="dialogName">窗体名称</param>
        /// <param name="parameters">参数</param>
        public void Show(string dialogName, ILayDialogParameter parameters)
        {
            Alert(dialogName, parameters, null, false);
        }
        /// <summary>
        /// 普通弹窗
        /// </summary>
        /// <param name="dialogName">窗体名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="callback">回调</param>
        public void Show(string dialogName, ILayDialogParameter parameters, Action<ILayDialogResult> callback)
        {
            Alert(dialogName, parameters, callback, false);
        }
        /// <summary>
        /// 模态对话框
        /// </summary>
        /// <param name="dialogName">窗体名称</param>
        /// <param name="parameters">参数</param>
        public void ShowDialog(string dialogName, ILayDialogParameter parameters)
        {
            Alert(dialogName, parameters, null, true);
        }
        /// <summary>
        /// 模态对话框
        /// </summary>
        /// <param name="dialogName">窗体名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="callback">回调</param>
        public void ShowDialog(string dialogName, ILayDialogParameter parameters, Action<ILayDialogResult> callback)
        {
            Alert(dialogName, parameters, callback, true);
        }
        /// <summary>
        /// 弹窗业务
        /// </summary>
        /// <param name="dialogName">窗体名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="callback">回调</param>
        /// <param name="isModel">是否为模态</param>
        private void Alert(string dialogName, ILayDialogParameter parameters, Action<ILayDialogResult> callback, bool isModel)
        {

            try
            {

                //抓取主窗体模板中的DialogHost
                var layWindow = Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.IsActive);
                LayDialogHost layDialog = layWindow.Template.FindName("WindowDialogHostBody", layWindow) as LayDialogHost;
                //防呆
                if (layDialog == null || layDialog.Content != null) return;
                LayDialogUserControl DialogView = null;
                //抓取需要Show的窗体，并且赋值
                foreach (var item in DialogViewCollection)
                {
                    if (item.Key == dialogName)
                    {
                        if (layDialog.IsOpen) return;
                        DialogView = item.Value;
                        DialogView.DataContext = item.Value.DataContext;
                        layDialog.Content = DialogView;
                        layDialog.DataContext = DialogView.DataContext;
                        layDialog.IsOpen = true;
                    }
                }
                //防呆
                if (DialogView == null) return;
                //获得窗体
                ILayDialogUserControl dialog = DialogView as ILayDialogUserControl;
                Action<ILayDialogResult> requestCloseHandler = null;
                //窗体关闭的回调方法
                requestCloseHandler = (o) =>
                {
                    dialog.Result = o;
                    //关闭窗体
                    layDialog.IsOpen = false;
                    layDialog.Dispatcher.BeginInvoke(DispatcherPriority.Normal,new Action(async () =>
                    {
                        await Task.Delay(100);
                        //窗体关闭后数据置空
                        DialogView = null;
                        layDialog.Content = null;
                        layDialog.DataContext = null;
                    }));
                };
                RoutedEventHandler LoadedHandler = null;
                LoadedHandler = (o, e) =>
                {
                    dialog.Loaded -= LoadedHandler;
                    dialog.GetDialogViewModel().RequestClose += requestCloseHandler;
                };
                dialog.Loaded += LoadedHandler;
                RoutedEventHandler UnloadedHandler = null;
                //窗体销毁后的事件
                UnloadedHandler = (o, e) =>
                {
                    dialog.Unloaded -= UnloadedHandler;
                    dialog.GetDialogViewModel().RequestClose -= requestCloseHandler;
                    //抓取回调后的数据并回传
                    callback?.Invoke(dialog.Result);
                    //判断是否为模态弹窗
                    if (isModel)
                    {
                        //取消线程占用，允许进行ViewModel业务代码操作
                        dispatcherFrame.Continue = false;
                        ComponentDispatcher.PopModal();
                        dispatcherFrame = null;
                    }
                };
                dialog.Unloaded += UnloadedHandler;
                //抓取当前需要传递的参数并且传递给对应视图的ViewModel
                foreach (var item in DialogViewCollection)
                {
                    if (item.Key == dialogName)
                    {
                        if (!(item.Value.DataContext is ILayDialogAware viewModel))
                            throw new NullReferenceException("对话框的 ViewModel 必须实现 IDialogAware 接口 ");
                        //给对应的ViewModel传值
                        ViewAndViewModelAction<ILayDialogAware>(viewModel, d => d.OnDialogOpened(parameters));
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                //判断是否为模态弹窗
                if (isModel)
                {
                    //阻塞ViewModel业务操作
                    ComponentDispatcher.PushModal();
                    dispatcherFrame = new DispatcherFrame(true);
                    Dispatcher.PushFrame(dispatcherFrame);
                }
            }
        }
        private static void ViewAndViewModelAction<T>(object view, Action<T> action)
        {
            if (view is T viewAsT)
                action(viewAsT);

            if (view is FrameworkElement element && element.DataContext is T viewModelAsT)
            {
                action(viewModelAsT);
            }
        }
    }
}
