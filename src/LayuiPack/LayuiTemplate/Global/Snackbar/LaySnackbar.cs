using LayuiTemplate.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LayuiTemplate.Global
{
    /// <summary>
    ///  通知提示组件
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-05-24 下午 1:12:03</para>
    /// </summary>
    public class LaySnackbar
    {
        /// <summary>
        /// 全局Snackbar组件
        /// </summary>
        internal static Dictionary<string, LaySnackbarControl> SnackbarHosts { get; set; } = SnackbarHosts ?? new Dictionary<string, LaySnackbarControl>();

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
            DependencyProperty.RegisterAttached("Tooken", typeof(string), typeof(LaySnackbar), new PropertyMetadata(OnTookenChange));
        /// <summary>
        /// 设置组件
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnTookenChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                LaySnackbarControl host = d as LaySnackbarControl;
                if (host == null) return;
                var tooken = LaySnackbar.GetTooken(host);
                if (!LaySnackbar.SnackbarHosts.ContainsKey(tooken)) LaySnackbar.SnackbarHosts.Add(tooken, host);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// 信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="tooken"></param>
        /// <param name="time"></param>
        public static void Show(object message, string tooken, double time = 5) => TaskShowAsync(message, tooken, time).GetAwaiter();
        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="message"></param>
        /// <param name="tooken"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        private static async Task TaskShowAsync(object message, string tooken, double time)
        {
            if (!SnackbarHosts.ContainsKey(tooken)) throw new Exception($"未找到 Tooken:{tooken}");
            var view = SnackbarHosts.Where(o => o.Key.Equals(tooken)).FirstOrDefault().Value;
            view.Content = message;
            if (view.IsActive)
            {
                view.Closed(0);
                view.Show(0.2);
                await Task.Delay(TimeSpan.FromSeconds(time));
                view.Closed(0.2);
            }
            else
            {
                view.Show(0.2);
                await Task.Delay(TimeSpan.FromSeconds(time));
                view.Closed(0.2);
            }
        }
    }
}
