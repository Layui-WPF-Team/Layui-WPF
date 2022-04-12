using LayuiTemplate.Control;
using LayuiTemplate.Dialog.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public static Dictionary<string, Type> DialogViewCollection { get; set; } = DialogViewCollection ?? new Dictionary<string, Type>();
        internal static Dictionary<string, LayDialogHost> DialogHosts { get; set; } = DialogHosts ?? new Dictionary<string, LayDialogHost>();
        public LayDialog()
        {
        }
        /// <summary>
        /// 获取Tooken
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetTooken(DependencyObject obj)
        {
            return (string)obj.GetValue(TookenProperty);
        }
        /// <summary>
        /// 设置Tooken
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static void SetTooken(DependencyObject obj, string value)
        {
            obj.SetValue(TookenProperty, value);
        }

        // Using a DependencyProperty as the backing store for Tooken.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TookenProperty =
            DependencyProperty.RegisterAttached("Tooken", typeof(string), typeof(LayDialog), new PropertyMetadata(OnTookenChange));
        private static void OnTookenChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                LayDialogHost host = d as LayDialogHost;
                if (host == null) return;
                var tooken = LayDialog.GetTooken(host);
                if (LayDialog.DialogHosts.ContainsKey(tooken)) throw new Exception($"{tooken}已存在");
                LayDialog.DialogHosts.Add(tooken, host);
            }
            catch (Exception ex)
            {

                throw ex;
            }
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
                if(DialogViewCollection.ContainsKey(dialogName)) throw new Exception($"{dialogName}弹窗视图多次注入");
                DialogViewCollection.Add(dialogName, typeof(TView));
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
        /// <param name="tooken">需要通知弹窗的唯一健值</param>
        public void Show(string dialogName, ILayDialogParameter parameters, string tooken = null) => Alert(dialogName, parameters, null, false, tooken);
        /// <summary>
        /// 普通弹窗
        /// </summary>
        /// <param name="dialogName">窗体名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="callback">回调</param>
        public void Show(string dialogName, ILayDialogParameter parameters, Action<ILayDialogResult> callback, string tooken = null) => Alert(dialogName, parameters, callback, false, tooken);
        /// <summary>
        /// 模态对话框
        /// </summary>
        /// <param name="dialogName">窗体名称</param>
        /// <param name="parameters">参数</param>
        public void ShowDialog(string dialogName, ILayDialogParameter parameters, string tooken = null) => Alert(dialogName, parameters, null, true, tooken);
        /// <summary>
        /// 模态对话框
        /// </summary>
        /// <param name="dialogName">窗体名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="callback">回调</param>
        public void ShowDialog(string dialogName, ILayDialogParameter parameters, Action<ILayDialogResult> callback, string tooken = null) => Alert(dialogName, parameters, callback, true, tooken);
        /// <summary>
        /// 弹窗业务
        /// </summary>
        /// <param name="dialogName">窗体名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="callback">回调</param>
        /// <param name="isModel">是否为模态</param>
        private void Alert(string dialogName, ILayDialogParameter parameters, Action<ILayDialogResult> callback, bool isModel, string tooken)
        {
            try
            {
                var view = Activator.CreateInstance(DialogViewCollection[dialogName]) as UserControl;
                if (view == null) throw new Exception($"{dialogName}未找到");
                LayDialogHost host = null;
                if (tooken == null) host = DialogHosts[LayDialogToken.RootDialogTooken];
                else host = DialogHosts[tooken];
                if (host == null) throw new Exception($"未找到弹窗组件");
                if (host.IsOpen) throw new Exception($"{view.GetType().Name}已开启，请先关闭当前窗体");
                LayDialogWindow DialogView = new LayDialogWindow()
                {
                    Content = view,
                    DataContext = view.DataContext
                };
                host.Content = DialogView;
                if (DialogView == null) return;
                host.IsOpen = true;
                ILayDialogWindow dialog = DialogView as ILayDialogWindow;
                Action<ILayDialogResult> requestCloseHandler = null;
                //窗体关闭的回调方法
                requestCloseHandler = (o) =>
                {
                    DialogView.Result = o;
                    //关闭窗体
                    host.IsOpen = false;
                    host.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(async () =>
                    {
                        await Task.Delay(100);
                        //窗体关闭后数据置空
                        DialogView = null;
                        host.Content = null;
                        host.DataContext = null;
                    }));
                };
                RoutedEventHandler LoadedHandler = null;
                LoadedHandler = (o, e) =>
                {
                    DialogView.Loaded -= LoadedHandler;
                    DialogView.GetDialogViewModel().RequestClose += requestCloseHandler;
                };
                dialog.Loaded += LoadedHandler;
                RoutedEventHandler UnloadedHandler = null;
                //窗体销毁后的事件
                UnloadedHandler = (o, e) =>
                {
                    dialog.Unloaded -= UnloadedHandler;
                    dialog.GetDialogViewModel().RequestClose -= requestCloseHandler;
                    //抓取回调后的数据并回传
                    if (dialog.Result == null) dialog.Result = new LayDialogResult() { Result = Enum.Dialog.ButtonResult.Default };
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
                if (!(view.DataContext is ILayDialogAware viewModel))
                    throw new NullReferenceException("对话框的 ViewModel 必须实现 IDialogAware 接口 ");
                //给对应的ViewModel传值
                ViewAndViewModelAction<ILayDialogAware>(viewModel, d => d.OnDialogOpened(parameters));
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
        /// <summary>
        /// 关闭当前窗体的弹窗
        /// </summary>
        /// <param name="tooken">需要关闭的的窗体Tooken</param>
        public void Close(string tooken)
        {
            if (!DialogHosts.ContainsKey(tooken)) return;
            LayDialogHost host = DialogHosts[tooken];
            host.IsOpen = false;
            host.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(async () =>
            {
                await Task.Delay(100);
                //窗体关闭后数据置空
                host.Content = null;
                host.DataContext = null;
            }));
        }
        /// <summary>
        /// 关闭所有弹窗
        /// </summary>
        public void CloseAll()
        {
            foreach (var item in DialogHosts)
            {
                LayDialogHost host = DialogHosts[item.Key];
                host.IsOpen = false;
                host.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(async () =>
                {
                    await Task.Delay(100);
                    //窗体关闭后数据置空
                    host.Content = null;
                    host.DataContext = null;
                }));
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
