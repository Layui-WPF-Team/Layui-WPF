using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LayuiTemplate.Tools
{
    /// <summary>
    ///  WebBrowser帮助类（支持绑定）
    ///  <para>注意：针对于IE浏览器</para>
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-08-30 下午 1:17:26</para>
    /// </summary>
    public class WebBrowserHelper : DependencyObject
    {
        /// <summary>
        /// 获取当前IE最低版本
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int GetMinIEVersion(DependencyObject obj)
        {
            return (int)obj.GetValue(MinIEVersionProperty);
        }
        /// <summary>
        /// 设置当前IE最低版本
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetMinIEVersion(DependencyObject obj, int value)
        {
            obj.SetValue(MinIEVersionProperty, value);
        }

        // Using a DependencyProperty as the backing store for MinIEVersion.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinIEVersionProperty =
            DependencyProperty.RegisterAttached("MinIEVersion", typeof(int), typeof(WebBrowserHelper), new PropertyMetadata(8));
        /// <summary>
        /// 获取当前浏览器初始化启动地址
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetSource(DependencyObject obj)
        {
            return (string)obj.GetValue(SourceProperty);
        }
        /// <summary>
        /// 设置当前浏览器初始化启动地址
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetSource(DependencyObject obj, string value)
        {
            obj.SetValue(SourceProperty, value);
        }

        // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.RegisterAttached("Source", typeof(string), typeof(WebBrowserHelper), new PropertyMetadata(OnSourceChanged));

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is WebBrowser browser)
            {
                try
                {
                    RegistryKey mainKey = Microsoft.Win32.Registry.LocalMachine;
                    RegistryKey subKey = mainKey.OpenSubKey(@"SOFTWARE\Microsoft\Internet Explorer");
                    if (subKey == null)
                    {
                        MessageBox.Show("未安装IE浏览器");
                        throw new Exception();
                    }
                    else
                    {
                        var Version = Convert.ToInt32(subKey.GetValue("Version").ToString().Split('.')[0]);
                        if (Version < GetMinIEVersion(browser))
                        {
                            MessageBox.Show($"不能低于IE{GetMinIEVersion(browser)}版本");
                            throw new Exception();
                        }
                    }
                    Uri uri = new Uri($"{e.NewValue}");
                    browser.Source = uri;
                    browser.Navigated -= Browser_Navigated;
                    browser.Navigated += Browser_Navigated;
                }
                catch
                {
                    browser.Source = null;
                    browser.Navigated -= Browser_Navigated;
                }
            }
        }
        /// <summary>
        /// 警用JS脚本提示错误弹窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Browser_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            FieldInfo fiComWebBrowser = typeof(WebBrowser).GetField("_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fiComWebBrowser == null) return;

            object objComWebBrowser = fiComWebBrowser.GetValue(sender);
            if (objComWebBrowser == null) return;

            objComWebBrowser.GetType().InvokeMember("Silent", BindingFlags.SetProperty, null, objComWebBrowser, new object[] { true });
        }
    }
}
