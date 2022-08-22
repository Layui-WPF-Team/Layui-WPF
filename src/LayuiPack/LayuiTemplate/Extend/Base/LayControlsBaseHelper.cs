using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace LayuiTemplate.Extend
{
    /// <summary>
    ///  LayCalendarHelper
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-06-23 下午 1:05:57</para>
    /// </summary>
    public class LayControlsBaseHelper
    {
        /// <summary>
        /// 获取圆角
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static CornerRadius GetCornerRadius(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(CornerRadiusProperty);
        }
        /// <summary>
        /// 设置圆角
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetCornerRadius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(CornerRadiusProperty, value);
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(LayControlsBaseHelper));
        /// <summary>
        /// 获取焦点
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool GetIsFocus(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsFocusProperty);
        }
        /// <summary>
        /// 设置焦点
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetIsFocus(DependencyObject obj, bool value)
        {
            obj.SetValue(IsFocusProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsFocus.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsFocusProperty =
            DependencyProperty.RegisterAttached("IsFocus", typeof(bool), typeof(LayControlsBaseHelper),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnFocusChanged))
                { DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });

        private static void OnFocusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {
                if(GetIsFocus(textBox)) textBox.Focus();
                textBox.LostFocus -= TextBox_LostFocus;
                textBox.LostFocus += TextBox_LostFocus;
            }
            if (d is PasswordBox passwordBox)
            {
                if(GetIsFocus(passwordBox)) passwordBox.Focus();
                passwordBox.LostFocus -= PasswordBox_LostFocus;
                passwordBox.LostFocus += PasswordBox_LostFocus;
            }
        }

        private static void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            SetIsFocus(sender as PasswordBox, false);
        }

        private static void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            SetIsFocus(sender as TextBox, false);
        }
    }
}
