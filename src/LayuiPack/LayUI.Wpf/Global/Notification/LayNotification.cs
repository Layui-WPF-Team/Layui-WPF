using LayUI.Wpf.Controls;
using LayUI.Wpf.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LayUI.Wpf.Global
{
    /// <summary>
    /// 通知类组件工具
    /// </summary>
    public class LayNotification
    {
        /// <summary>
        /// 全局通知组件
        /// </summary>
        internal static Dictionary<string, LayNotificationHost> NotificationHosts { get; set; } = NotificationHosts ?? new Dictionary<string, LayNotificationHost>();

        public static string GetToken(DependencyObject obj)
        {
            return (string)obj.GetValue(TokenProperty);
        }

        public static void SetToken(DependencyObject obj, string value)
        {
            obj.SetValue(TokenProperty, value);
        }

        // Using a DependencyProperty as the backing store for Tooken.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TokenProperty =
            DependencyProperty.RegisterAttached("Token", typeof(string), typeof(LayNotification), new PropertyMetadata(OnTookenChange));
        /// <summary>
        /// 设置组件
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnTookenChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                LayNotificationHost host = d as LayNotificationHost;
                if (host == null) return;
                var token = GetToken(host);
                if (NotificationHosts.ContainsKey(token)) NotificationHosts.Remove(token);
                NotificationHosts.Add(token, host);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 默认提示信息（无图标效果）
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="Notification">提示信息</param>
        /// <param name="tooken">自定义指定提示窗口</param>
        /// <param name="time">停留时间</param>
        public static void Default(string title, object notification, string tooken = null, double time = 3) => Show(title, notification, null, NotificationType.Default, tooken, time);
        public static void Default(string title, object notification, object submitContent, string tooken = null, double time = 3) => Show(title, notification, submitContent, NotificationType.Default, tooken, time);
        /// <summary>
        /// 默认提示信息（无图标效果）
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="Notification">提示信息</param>
        /// <param name="callback">回调</param>
        /// <param name="tooken">自定义指定提示窗口</param>
        /// <param name="time">停留时间</param>
        public static void Default(string title, object notification, Action<bool> callback, string tooken = null, double time = 3) => Show(title, notification, null, NotificationType.Default, tooken, time, callback);
        public static void Default(string title, object notification, object submitContent, Action<bool> callback, string tooken = null, double time = 3) => Show(title, notification, submitContent, NotificationType.Default, tooken, time, callback);
        /// <summary>
        /// 详细信息提示
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="Notification">提示信息</param>
        /// <param name="tooken">自定义指定提示窗口</param>
        /// <param name="time">停留时间</param>
        public static void Info(string title, object notification, string tooken = null, double time = 3) => Show(title, notification, null, NotificationType.Info, tooken, time);
        public static void Info(string title, object notification, object submitContent, string tooken = null, double time = 3) => Show(title, notification, submitContent, NotificationType.Info, tooken, time);
        /// <summary>
        /// 详细信息提示
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="Notification">提示信息</param>
        /// <param name="callback">回调</param>
        /// <param name="tooken">自定义指定提示窗口</param>
        /// <param name="time">停留时间</param>
        public static void Info(string title, object notification, Action<bool> callback, string tooken = null, double time = 3) => Show(title, notification, null, NotificationType.Info, tooken, time, callback);
        public static void Info(string title, object notification, object submitContent, Action<bool> callback, string tooken = null, double time = 3) => Show(title, notification, submitContent, NotificationType.Info, tooken, time, callback);

        /// <summary>
        /// 成功信息提示
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="Notification">提示信息</param>
        /// <param name="tooken">自定义指定提示窗口</param>
        /// <param name="time">停留时间</param>
        public static void Success(string title, object notification, string tooken = null, double time = 3) => Show(title, notification, null, NotificationType.Success, tooken, time);
        public static void Success(string title, object notification, object submitContent, string tooken = null, double time = 3) => Show(title, notification, submitContent, NotificationType.Success, tooken, time);
        /// <summary>
        /// 警告信息提示
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="Notification">提示信息</param>
        /// <param name="callback">回调</param>
        /// <param name="tooken">自定义指定提示窗口</param>
        /// <param name="time">停留时间</param>
        public static void Success(string title, object notification, Action<bool> callback, string tooken = null, double time = 3) => Show(title, notification, null, NotificationType.Success, tooken, time, callback);
        public static void Success(string title, object notification, object submitContent, Action<bool> callback, string tooken = null, double time = 3) => Show(title, notification, submitContent, NotificationType.Success, tooken, time, callback);
        /// <summary>
        /// 警告信息提示
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="Notification">提示信息</param>
        /// <param name="tooken">自定义指定提示窗口</param>
        /// <param name="time">停留时间</param>
        public static void Warning(string title, object notification, string tooken = null, double time = 3) => Show(title, notification, null, NotificationType.Warning, tooken, time);
        public static void Warning(string title, object notification, object submitContent, string tooken = null, double time = 3) => Show(title, notification, submitContent, NotificationType.Warning, tooken, time);
        /// <summary>
        /// 警告信息提示
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="Notification">提示信息</param>
        /// <param name="callback">回调</param>
        /// <param name="tooken">自定义指定提示窗口</param>
        /// <param name="time">停留时间</param>
        public static void Warning(string title, object notification, Action<bool> callback, string tooken = null, double time = 3) => Show(title, notification, null, NotificationType.Warning, tooken, time, callback);
        public static void Warning(string title, object notification, object submitContent, Action<bool> callback, string tooken = null, double time = 3) => Show(title, notification, submitContent, NotificationType.Warning, tooken, time, callback);
        /// <summary>
        /// 错误信息提示
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="Notification">提示信息</param>
        /// <param name="tooken">自定义指定提示窗口</param>
        /// <param name="time">停留时间</param>
        public static void Error(string title, object notification, string tooken = null, double time = 3) => Show(title, notification, null, NotificationType.Error, tooken, time);
        public static void Error(string title, object notification, object submitContent, string tooken = null, double time = 3) => Show(title, notification, submitContent, NotificationType.Error, tooken, time);

        /// <summary>
        /// 错误信息提示
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="Notification">提示信息</param>
        /// <param name="tooken">自定义指定提示窗口</param>
        /// <param name="callback">回调</param>
        /// <param name="time"></param>
        public static void Error(string title, object notification, Action<bool> callback, string tooken = null, double time = 3) => Show(title, notification, null, NotificationType.Error, tooken, time, callback);
        public static void Error(string title, object notification, object submitContent, Action<bool> callback, string tooken = null, double time = 3) => Show(title, notification, submitContent, NotificationType.Error, tooken, time, callback);
        /// <summary>
        /// 显示提示信息
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="Notification">提示信息</param>
        /// <param name="type">提示类型</param>
        /// <param name="tooken">自定义指定提示窗口</param>
        /// <param name="time">停留时间</param>
        /// <param name="callback">回调</param>
        private static void Show(string title, object Notification, object SubmitContent, NotificationType type, string tooken, double time, Action<bool> callback = null)
        {
            if (tooken == null)
            {
                if (!NotificationHosts.ContainsKey("RootNotificationToken")) return;
                var view = NotificationHosts.Where(o => o.Key.Equals("RootNotificationToken")).FirstOrDefault().Value;
                view.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() =>
                {
                    var page = new LayNotificationControl() { Time=time,Title = title, Type = type, Content = Notification, SubmitContent = SubmitContent, ShowSubmit = SubmitContent == null ? false : true, Uid = Guid.NewGuid().ToString() };
                    Action<bool> close = (o) =>
                    {
                        callback?.Invoke(o);//执行回调方法
                        page.Close = null;
                        page.RemoveTime();
                        view.Items.Remove(page);
                    };
                    page.Close = close;
                    view.Items.Add(page);
                }));
            }
            else
            {
                if (!NotificationHosts.ContainsKey(tooken)) return;
                var view = NotificationHosts.Where(o => o.Key.Equals(tooken)).FirstOrDefault().Value;
                view.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() =>
                {
                    var page = new LayNotificationControl() { Time = time, Title = title, Type = type, Content = Notification, SubmitContent = SubmitContent, ShowSubmit = SubmitContent == null ? false : true, Uid = Guid.NewGuid().ToString() };
                    Action<bool> close = (o) =>
                    {
                        callback?.Invoke(o);//执行回调方法
                        page.Close = null;
                        page.RemoveTime();
                        view.Items.Remove(page);
                    };
                    page.Close = close;
                    view.Items.Add(page);
                }));
            }
        }
    }
}
