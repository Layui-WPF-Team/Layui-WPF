using LayUI.Wpf.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Threading;

namespace LayUI.Wpf.Global
{
    /// <summary>
    /// LayDialog控制器
    /// </summary>
    public class LayDialog
    {
        internal static Dictionary<string, object> Instance = new Dictionary<string, object>();
        /// <summary>
        /// 被注入的窗体集合
        /// </summary>
        internal static Dictionary<string, Type> DialogViewCollection { get; set; } = DialogViewCollection ?? new Dictionary<string, Type>();
        /// <summary>
        /// 被注入的窗体ViewModel集合
        /// </summary>
        internal static Dictionary<string, Type> DialogViewModelCollection { get; set; } = DialogViewModelCollection ?? new Dictionary<string, Type>();
        /// <summary>
        /// 对话框载体
        /// </summary>
        internal static Dictionary<string, Type> DialogWindow { get; set; } = DialogWindow ?? new Dictionary<string, Type>();
        /// <summary>
        /// 对话框组件
        /// </summary>
        internal static Dictionary<string, LayDialogHost> DialogHosts { get; set; } = DialogHosts ?? new Dictionary<string, LayDialogHost>();
        public LayDialog()
        {
        }
        /// <summary>
        /// 消费服务
        /// <para>用于配合IOC使用</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Resolve<T>()
        {
            try
            {
                var obj = Instance.Where(o => o.Key == typeof(T).Name).FirstOrDefault();
                if (obj.Value != null) return (T)obj.Value;
                else return default;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// 注册服务
        /// <para>用于配合IOC使用</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public static void Register<T>(T t)
        {
            try
            {
                if (Instance.ContainsKey(typeof(T).Name)) return;
                Instance.Add(typeof(T).Name, t);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetToken(DependencyObject obj)
        {
            return (string)obj.GetValue(TokenProperty);
        }
        /// <summary>
        /// 设置token
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static void SetToken(DependencyObject obj, string value)
        {
            obj.SetValue(TokenProperty, value);
        }

        // Using a DependencyProperty as the backing store for token.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TokenProperty =
            DependencyProperty.RegisterAttached("Token", typeof(string), typeof(LayDialog), new PropertyMetadata(OntokenChange));
        private static void OntokenChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                LayDialogHost host = d as LayDialogHost;
                if (host == null) return;
                var token = GetToken(host);
                if (token == null) token = Guid.NewGuid().ToString();
                if (DialogHosts.ContainsKey(token)) DialogHosts?.Remove(token);
                host.GUID = Guid.NewGuid().ToString();
                DialogHosts?.Add(token, host);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// Identifies the WindowStyle attached property.
        /// </summary>
        /// <remarks>
        /// This attached property is used to specify the style of a <see cref="IDialogWindow"/>.
        /// </remarks>
        public static readonly DependencyProperty WindowStyleProperty =
            DependencyProperty.RegisterAttached("WindowStyle", typeof(Style), typeof(LayDialog), new PropertyMetadata(null));

        /// <summary>
        /// Gets the value for the <see cref="WindowStyleProperty"/> attached property.
        /// </summary>
        /// <param name="obj">The target element.</param>
        /// <returns>The <see cref="WindowStyleProperty"/> attached to the <paramref name="obj"/> element.</returns>
        public static Style GetWindowStyle(DependencyObject obj)
        {
            return (Style)obj.GetValue(WindowStyleProperty);
        }

        /// <summary>
        /// Sets the <see cref="WindowStyleProperty"/> attached property.
        /// </summary>
        /// <param name="obj">The target element.</param>
        /// <param name="value">The Style to attach.</param>
        public static void SetWindowStyle(DependencyObject obj, Style value)
        {
            obj.SetValue(WindowStyleProperty, value);
        }
        /// <summary>
        /// 注入对话框窗体载体
        /// </summary>
        /// <typeparam name="TView">需要注入的类型</typeparam>
        /// <param name="dialogName">视图名称</param>
        public static void RegisterDialogWindow<TWindow>(string windowName) where TWindow : ILayDialogWindow
        {
            try
            {
                if (DialogWindow.ContainsKey(windowName)) throw new Exception($"{windowName}对话框窗体载体多次注入");
                DialogWindow.Add(windowName, typeof(TWindow));
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
                if (DialogViewCollection.ContainsKey(dialogName)) throw new Exception($"{dialogName}弹窗视图多次注入");
                DialogViewCollection.Add(dialogName, typeof(TView));
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
        /// <typeparam name="TViewModel">需要注入的ViewModel类型</typeparam>
        /// <param name="dialogName">视图名称</param>
        public static void RegisterDialog<TView, TViewModel>(string dialogName) where TViewModel : ILayDialogAware
        {
            try
            {
                if (DialogViewCollection.ContainsKey(dialogName)) throw new Exception($"{dialogName}弹窗视图多次注入");
                if (DialogViewModelCollection.ContainsKey(dialogName)) throw new Exception($"{dialogName}弹窗视图ViewModel多次注入");
                DialogViewCollection.Add(dialogName, typeof(TView));
                DialogViewModelCollection.Add(dialogName, typeof(TViewModel));
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
        /// <param name="token">需要通知弹窗的唯一健值</param>
        public static void Show(string dialogName, ILayDialogParameter parameters, string token = null) => Alert(dialogName, parameters, null, false, token, null);
        /// <summary>
        /// 普通弹窗
        /// </summary>
        /// <param name="content">视图</param>
        /// <param name="parameters">参数</param>
        /// <param name="token">需要通知弹窗的唯一健值</param>
        public static void Show(object content, ILayDialogParameter parameters, string token = null) => Alert(content, parameters, null, false, token, null);
        /// <summary>
        /// 普通弹窗
        /// </summary>
        /// <param name="dialogName">窗体名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="windowName">指定需要打开的窗体样式</param>
        public static void ShowWindow(string dialogName, ILayDialogParameter parameters, string windowName) => Alert(dialogName, parameters, null, false, null, windowName);
        /// <summary>
        /// 普通弹窗
        /// </summary>
        /// <param name="dialogName">窗体名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="callback">回调</param>
        /// <param name="token">需要通知弹窗的唯一健值</param>
        public static void Show(string dialogName, ILayDialogParameter parameters, Action<ILayDialogResult> callback, string token = null) => Alert(dialogName, parameters, callback, false, token, null);
        /// <summary>
        /// 普通弹窗
        /// </summary>
        /// <param name="content">视图</param>
        /// <param name="parameters">参数</param>
        /// <param name="callback">回调</param>
        /// <param name="token">需要通知弹窗的唯一健值</param>
        public static void Show(object content, ILayDialogParameter parameters, Action<ILayDialogResult> callback, string token = null) => Alert(content, parameters, callback, false, token, null);
        /// <summary>
        /// 普通弹窗
        /// </summary>
        /// <param name="dialogName">窗体名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="callback">回调</param>
        /// <param name="windowName">指定需要打开的窗体样式</param>
        public static void ShowWindow(string dialogName, ILayDialogParameter parameters, Action<ILayDialogResult> callback, string windowName) => Alert(dialogName, parameters, callback, false, null, windowName);
        /// <summary>
        /// 模态对话框
        /// </summary>
        /// <param name="dialogName">窗体名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="token">需要通知弹窗的唯一健值</param>
        public static void ShowDialog(string dialogName, ILayDialogParameter parameters, string token = null) => Alert(dialogName, parameters, null, true, token, null);
        /// <summary>
        /// 模态对话框
        /// </summary>
        /// <param name="dialogName">窗体名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="windowName">指定需要打开的窗体样式</param>
        public static void ShowDialogWindow(string dialogName, ILayDialogParameter parameters, string windowName = null) => Alert(dialogName, parameters, null, true, null, windowName);
        /// <summary>
        /// <summary>
        /// 模态对话框
        /// </summary>
        /// <param name="dialogName">窗体名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="callback">回调</param>
        /// <param name="token">需要通知弹窗的唯一健值,如果是Window窗体该值给Null</param>
        public static void ShowDialog(string dialogName, ILayDialogParameter parameters, Action<ILayDialogResult> callback, string token = null) => Alert(dialogName, parameters, callback, true, token, null);
        /// <summary>
        /// <summary>
        /// 模态对话框
        /// </summary>
        /// <param name="dialogName">窗体名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="callback">回调</param>
        /// <param name="windowName">指定需要打开的窗体样式</param>
        public static void ShowDialogWindow(string dialogName, ILayDialogParameter parameters, Action<ILayDialogResult> callback, string windowName = null) => Alert(dialogName, parameters, callback, true, null, windowName);
        /// <summary>
        /// 弹窗业务
        /// </summary>
        /// <param name="dialogName">窗体名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="callback">回调</param>
        /// <param name="isModel">是否为模态</param>
        /// <param name="token">需要通知弹窗的唯一健值,如果是Window窗体该值给Null</param>
        /// <param name="windowName">指定需要打开的窗体样式</param>
        private static void Alert(string dialogName, ILayDialogParameter parameters, Action<ILayDialogResult> callback, bool isModel, string token, string windowName)
        {
            if (windowName != null) AlertWindow(dialogName, parameters, callback, isModel, windowName);
            else AlertUserControl(dialogName, parameters, callback, isModel, token);
        }
        /// <summary>
        /// 弹窗业务
        /// </summary>
        /// <param name="view">视图</param>
        /// <param name="parameters">参数</param>
        /// <param name="callback">回调</param>
        /// <param name="isModel">是否为模态</param>
        /// <param name="token">需要通知弹窗的唯一健值,如果是Window窗体该值给Null</param>
        /// <param name="windowName">指定需要打开的窗体样式</param>
        private static void Alert(object content, ILayDialogParameter parameters, Action<ILayDialogResult> callback, bool isModel, string token, string windowName)
        {
            DispatcherFrame dispatcherFrame = null;
            try
            {
                if (!DialogHosts.ContainsKey(token)) throw new Exception($"未找到{nameof(token)}值为{token}的弹窗组件:{nameof(LayDialogHost)}");
                if (content is UserControl && content is ILayDialogAware)
                {
                    //抓取当前展示弹窗容器
                    LayDialogHost host = DialogHosts[token];
                    //创建内容视图
                    var view = content as UserControl;
                    //创建弹窗
                    LayDialogUserControlWindow dialogView = new LayDialogUserControlWindow()
                    {
                        Uid = nameof(content) + Guid.NewGuid().ToString(),
                        IsOpen = true,
                        Content = view,
                    };
                    host.DialogItems.Items.Add(dialogView);
                    ILayDialogUserControlWindow dialog = dialogView as ILayDialogUserControlWindow;
                    Action<ILayDialogResult> requestCloseHandler = null;
                    //窗体关闭的回调方法
                    requestCloseHandler = (o) =>
                    {
                        dialogView.Result = o;
                        //关闭窗体
                        host.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(async () =>
                        {
                            //窗体关闭后数据置空
                            dialogView.IsOpen = false;
                            await Task.Delay(100);
                            host.DialogItems.Items.Remove(dialogView);
                        }));
                    };
                    RoutedEventHandler LoadedHandler = null;
                    LoadedHandler = (o, e) =>
                    {
                        dialogView.Loaded -= LoadedHandler;
                        dialogView.GetDialogView().RequestClose += requestCloseHandler;
                    };
                    dialog.Loaded += LoadedHandler;
                    RoutedEventHandler UnloadedHandler = null;
                    //窗体销毁后的事件
                    UnloadedHandler = (o, e) =>
                    {
                        dialog.Unloaded -= UnloadedHandler;
                        dialog.GetDialogView().RequestClose -= requestCloseHandler;
                        //抓取回调后的数据并回传
                        if (dialog.Result == null) dialog.Result = new LayDialogResult() { Result = Enum.ButtonResult.Default };
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
                    if (!(view is ILayDialogAware vm))
                        throw new NullReferenceException("对话框的 content 必须实现 IDialogAware 接口 ");
                    //给对应的ViewModel传值
                    ViewAndViewModelAction<ILayDialogAware>(view, d => d.OnDialogOpened(parameters));
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// 弹起Window窗体
        /// </summary>
        /// <param name="dialogName"></param>
        /// <param name="parameters"></param>
        /// <param name="callback"></param>
        /// <param name="isModel"></param>
        /// <param name="token"></param>
        /// <param name="windowName"></param>
        private static void AlertWindow(string dialogName, ILayDialogParameter parameters, Action<ILayDialogResult> callback, bool isModel, string windowName)
        {
            try
            {
                ILayDialogWindow window = Activator.CreateInstance(DialogWindow[windowName]) as ILayDialogWindow;
                Action<ILayDialogResult> requestCloseHandler = null;
                requestCloseHandler = (o) =>
                {
                    window.Result = o;
                    window.Close();
                };
                RoutedEventHandler loadedHandler = null;
                loadedHandler = (o, e) =>
                {
                    window.Loaded -= loadedHandler;
                    window.GetDialogViewModel().RequestClose += requestCloseHandler;
                };
                window.Loaded += loadedHandler;
                CancelEventHandler closingHandler = null;
                closingHandler = (o, e) =>
                {
                    if (!window.GetDialogViewModel().CanCloseDialog()) e.Cancel = true;
                };
                window.Closing += closingHandler;
                EventHandler closedHandler = null;
                closedHandler = (o, e) =>
                {
                    window.Closed -= closedHandler;
                    window.Closing -= closingHandler;
                    window.GetDialogViewModel().RequestClose -= requestCloseHandler;
                    window.GetDialogViewModel().OnDialogClosed();
                    if (window.Result == null) window.Result = new LayDialogResult();
                    callback?.Invoke(window.Result);
                    window.DataContext = null;
                    window.Content = null;
                };
                window.Closed += closedHandler;
                var content = Activator.CreateInstance(DialogViewCollection[dialogName]);
                if (!(content is FrameworkElement dialogContent))
                    throw new NullReferenceException("对话框的内容必须是FrameworkElement");
                //抓取当前需要传递的参数并且传递给对应视图的ViewModel
                if (!(dialogContent.DataContext is ILayDialogWindowAware viewModel))
                    throw new NullReferenceException("对话框的 ViewModel 必须实现 ILayDialogWindowAware 接口 ");
                ///////设置窗体样式//////
                var windowStyle = GetWindowStyle(dialogContent);
                if (windowStyle != null) window.Style = windowStyle;
                ///////填充内容//////
                window.Content = dialogContent;
                /////////填充数据上下文/////////
                window.DataContext = dialogContent.DataContext;
                if (window.Owner == null) window.Owner = Application.Current?.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive);
                //给对应的ViewModel传值
                ViewAndViewModelAction<ILayDialogWindowAware>(viewModel, d => d.OnDialogOpened(parameters));
                if (isModel) window.ShowDialog();
                else window.Show();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// 弹起用户控件窗体
        /// </summary>
        /// <param name="dialogName"></param>
        /// <param name="parameters"></param>
        /// <param name="callback"></param>
        /// <param name="isModel"></param>
        /// <param name="token"></param>
        private static void AlertUserControl(string dialogName, ILayDialogParameter parameters, Action<ILayDialogResult> callback, bool isModel, string token)
        {
            DispatcherFrame dispatcherFrame = null;
            try
            {
                if (string.IsNullOrEmpty(token)) throw new Exception($"{nameof(token)}不能为空");
                if (string.IsNullOrEmpty(dialogName)) throw new Exception($"{nameof(dialogName)}不能为空");
                if (!DialogHosts.ContainsKey(token)) throw new Exception($"未找到{nameof(token)}值为{token}的弹窗组件:{nameof(LayDialogHost)}");
                if (!DialogViewCollection.ContainsKey(dialogName)) throw new Exception($"未找到{nameof(dialogName)}为{dialogName}的视图");
                //抓取当前展示弹窗容器
                LayDialogHost host = DialogHosts[token];
                //创建内容视图
                var view = Activator.CreateInstance(DialogViewCollection[dialogName]) as UserControl;
                //检查VM初始化被注入情况
                if (DialogViewModelCollection.ContainsKey(dialogName))
                {
                    //创建VM
                    view.DataContext = Activator.CreateInstance(DialogViewModelCollection[dialogName]);
                }
                //创建弹窗
                LayDialogUserControlWindow dialogView = new LayDialogUserControlWindow()
                {
                    Uid = dialogName + Guid.NewGuid().ToString(),
                    IsOpen = true,
                    Content = view,
                    DataContext = view.DataContext
                };
                host.DialogItems.Items.Add(dialogView);
                ILayDialogUserControlWindow dialog = dialogView as ILayDialogUserControlWindow;
                Action<ILayDialogResult> requestCloseHandler = null;
                //窗体关闭的回调方法
                requestCloseHandler = (o) =>
                {
                    dialogView.Result = o;
                    //关闭窗体
                    host.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(async () =>
                    {
                        //窗体关闭后数据置空
                        dialogView.IsOpen = false;
                        await Task.Delay(100);
                        host.DialogItems.Items.Remove(dialogView);
                    }));
                };
                RoutedEventHandler LoadedHandler = null;
                LoadedHandler = (o, e) =>
                {
                    dialogView.Loaded -= LoadedHandler;
                    dialogView.GetDialogViewModel().RequestClose += requestCloseHandler;
                };
                dialog.Loaded += LoadedHandler;
                RoutedEventHandler UnloadedHandler = null;
                //窗体销毁后的事件
                UnloadedHandler = (o, e) =>
                {
                    dialog.Unloaded -= UnloadedHandler;
                    dialog.GetDialogViewModel().RequestClose -= requestCloseHandler;
                    //抓取回调后的数据并回传
                    if (dialog.Result == null) dialog.Result = new LayDialogResult() { Result = Enum.ButtonResult.Default };
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
        /// VM转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="view"></param>
        /// <param name="action"></param>
        private static void ViewAndViewModelAction<T>(object view, Action<T> action)
        {
            try
            {
                if (view is T viewAsT)
                    action(viewAsT);

                if (view is FrameworkElement element && element.DataContext is T viewModelAsT)
                {
                    action(viewModelAsT);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// 关闭指定弹窗容器内所有窗体
        /// </summary>
        /// <param name="token">目标弹窗容器DialogHosts唯一token</param>
        public static void Close(string token)
        {
            try
            {
                if (string.IsNullOrEmpty(token)) throw new Exception($"{nameof(token)}不能为空");
                if (DialogHosts == null) return;
                if (!DialogHosts.ContainsKey(token)) return;
                LayDialogHost host = DialogHosts[token];
                host.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    host.DialogItems.Items.Clear();
                }));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// 关闭指定弹窗容器内所有窗体某一类型弹窗
        /// </summary>
        /// <param name="dialogName">目标弹窗类型</param>
        /// <param name="token">目标弹窗容器DialogHosts唯一token</param>
        public static void Close(string dialogName, string token)
        {
            try
            {
                if (string.IsNullOrEmpty(token)) throw new Exception($"{nameof(token)}不能为空");
                if (DialogHosts == null) return;
                if (!DialogHosts.ContainsKey(token)) return;
                LayDialogHost host = DialogHosts[token];
                host.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    foreach (UserControl item in host.DialogItems.Items)
                    {
                        if (item.Uid.Contains(dialogName))
                            host.DialogItems.Items.Remove(item);
                    }
                }));
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        /// <summary>
        /// 关闭所有弹窗
        /// </summary>
        public void CloseAll()
        {
            try
            {
                if (DialogHosts == null) return;
                foreach (var item in DialogHosts)
                {
                    LayDialogHost host = DialogHosts[item.Key];
                    host.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                    {
                        host.DialogItems.Items.Clear();
                    }));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
