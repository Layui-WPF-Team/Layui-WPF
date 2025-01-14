using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace LayUI.Wpf.Extend
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
            var Element = d as FrameworkElement;
            if (d is IInputElement input)
            {
                if (GetIsFocus(d) && Element.IsInitialized)
                {
                    if (d is TextBox text) text.SelectionStart = int.MaxValue;
                    if (d is PasswordBox password) SetSelection(password, int.MaxValue, int.MaxValue);
                    input.Focus();
                }
                else
                {
                    input.LostKeyboardFocus -= Input_LostKeyboardFocus;
                    input.LostKeyboardFocus += Input_LostKeyboardFocus;
                    if (!Element.IsInitialized)
                    {
                        Element.Loaded -= Input_Loaded;
                        Element.Loaded += Input_Loaded;
                    }
                }
            }
        }
        /// <summary>
        /// 取消输入框状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Input_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            SetIsFocus(sender as DependencyObject, false);
        }
        /// <summary>
        /// 初始化焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Input_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is IInputElement input)
            {
                if (sender is TextBox text) text.SelectionStart = int.MaxValue;
                if (sender is PasswordBox password) SetSelection(password, int.MaxValue, int.MaxValue);
                input.Focus();
            }
            (sender as FrameworkElement).Loaded -= Input_Loaded;
        }
        /// <summary>
        /// 设置密码光标位置
        /// </summary>
        /// <param name="passwordBox">密码框</param>
        /// <param name="start">起始位置</param>
        /// <param name="length">长度</param>
        public static void SetSelection(PasswordBox passwordBox, int start, int length)
        {
            passwordBox.GetType()
               .GetMethod("Select", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
               .Invoke(passwordBox, new object[] { start, length });
        }

    }
}
