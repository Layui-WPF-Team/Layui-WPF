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
    ///  LayMessage
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-05-19 下午 8:00:06</para>
    /// </summary>
    public class LayMessage
    {
        /// <summary>
        /// 全局Message组件
        /// </summary>
        internal static Dictionary<string, LayMessageHost> MessageHosts { get; set; } = MessageHosts ?? new Dictionary<string, LayMessageHost>();

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
            DependencyProperty.RegisterAttached("Token", typeof(string), typeof(LayMessage), new PropertyMetadata(OnTookenChange));
        /// <summary>
        /// 设置组件
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnTookenChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                LayMessageHost host = d as LayMessageHost;
                if (host == null) return;
                var token = GetToken(host);
                if (MessageHosts.ContainsKey(token)) MessageHosts.Remove(token);
                MessageHosts.Add(token, host);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 默认提示信息（无图标效果）
        /// </summary>
        /// <param name="message">提示信息</param>
        /// <param name="tooken">自定义指定提示窗口</param>
        /// <param name="time">停留时间</param>
        public static void Default(object message, string tooken = null, double time = 3) => Show(message, MessageType.Default, tooken, time);
        /// <summary>
        /// 成功信息提示
        /// </summary>
        /// <param name="message">提示信息</param>
        /// <param name="tooken">自定义指定提示窗口</param>
        /// <param name="time">停留时间</param>
        public static void Success(object message, string tooken = null, double time = 3) => Show(message, MessageType.Success, tooken, time);
        /// <summary>
        /// 警告信息提示
        /// </summary>
        /// <param name="message">提示信息</param>
        /// <param name="tooken">自定义指定提示窗口</param>
        /// <param name="time">停留时间</param>
        public static void Warning(object message, string tooken = null, double time = 3) => Show(message, MessageType.Warning, tooken, time);
        /// <summary>
        /// 错误信息提示
        /// </summary>
        /// <param name="message">提示信息</param>
        /// <param name="tooken">自定义指定提示窗口</param>
        /// <param name="time">停留时间</param>
        public static void Error(object message, string tooken = null, double time = 3) => Show(message, MessageType.Error, tooken, time);
        /// <summary>
        /// 显示提示信息
        /// </summary>
        /// <param name="message">提示信息</param>
        /// <param name="type">提示类型</param>
        /// <param name="tooken">自定义指定提示窗口</param>
        /// <param name="time">停留时间</param>
        private static void Show(object message, MessageType type, string tooken, double time)
        {
            if (tooken == null)
            {
                if (!MessageHosts.ContainsKey("RootMessageToken")) return;
                var view = MessageHosts.Where(o => o.Key.Equals("RootMessageToken")).FirstOrDefault().Value;
                view.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() =>
                {
                    view.Items.Add(new LayMessageControl() { Type = type, Content = message, Time = time, Uid = Guid.NewGuid().ToString() });
                }));
            }
            else
            {

                if (!MessageHosts.ContainsKey(tooken)) return;
                var view = MessageHosts.Where(o => o.Key.Equals(tooken)).FirstOrDefault().Value;
                view.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() =>
                 {
                     view.Items.Add(new LayMessageControl() { Type = type, Content = message, Time = time, Uid = Guid.NewGuid().ToString() });
                 }));
            }
        }
    }
}
