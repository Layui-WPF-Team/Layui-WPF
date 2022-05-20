using LayuiTemplate.Control;
using LayuiTemplate.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LayuiTemplate.Message
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

        public static string GetTooken(DependencyObject obj)
        {
            return (string)obj.GetValue(TookenProperty);
        }

        public static void SetTooken(DependencyObject obj, string value)
        {
            obj.SetValue(TookenProperty, value);
        }

        // Using a DependencyProperty as the backing store for Tooken.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TookenProperty =
            DependencyProperty.RegisterAttached("Tooken", typeof(string), typeof(LayMessage), new PropertyMetadata(OnTookenChange));
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
                var tooken = LayMessage.GetTooken(host);
                if (!LayMessage.MessageHosts.ContainsKey(tooken)) LayMessage.MessageHosts.Add(tooken, host);

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
        public static void Success(object message, string tooken=null, double time = 3) => Show(message, MessageType.Success, tooken, time);
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
            if (tooken == null) {
                if (!MessageHosts.ContainsKey("RootMessageTooken")) return;
                var view = MessageHosts.Where(o => o.Key.Equals("RootMessageTooken")).FirstOrDefault().Value;
                view.Items.Add(new LayMessageContent() { Type = type, Content = message, Time = time, Uid = Guid.NewGuid().ToString() });
            }
            else
            {

                if (!MessageHosts.ContainsKey(tooken)) return;
                var view = MessageHosts.Where(o => o.Key.Equals(tooken)).FirstOrDefault().Value;
                view.Items.Add(new LayMessageContent() { Type = type, Content = message, Time = time, Uid = Guid.NewGuid().ToString() });
            }
        }
    }
}
