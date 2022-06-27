using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LayuiTemplate.Extend
{
    /// <summary>
    ///  输入框帮助类型
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-06-27 上午 10:55:39</para>
    /// </summary>
    public class LayTextBoxHelper
    {
        public static LayuiTemplate.Enum.InputType GetInputType(DependencyObject obj)
        {
            return (LayuiTemplate.Enum.InputType)obj.GetValue(InputTypeProperty);
        }

        public static void SetInputType(DependencyObject obj, LayuiTemplate.Enum.InputType value)
        {
            obj.SetValue(InputTypeProperty, value);
        }

        // Using a DependencyProperty as the backing store for InputType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InputTypeProperty =
            DependencyProperty.RegisterAttached("InputType", typeof(LayuiTemplate.Enum.InputType), typeof(LayTextBoxHelper), new PropertyMetadata(OnIsInputTypeChanged));


        private static void OnIsInputTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox text)
            {
                text.PreviewTextInput -= Text_PreviewTextInput;
                text.PreviewTextInput += Text_PreviewTextInput;
                text.PreviewKeyDown -= Text_KeyDown;
                text.PreviewKeyDown += Text_KeyDown;
            }
        }

        private static void Text_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Space) {
                e.Handled = true;
            }
        }

        private static void Text_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            switch (GetInputType(sender as TextBox))
            {
                case Enum.InputType.Default:
                    e.Handled = false;
                    break;
                case Enum.InputType.Number:
                    e.Handled = new Regex(@"[^0-9|\-|\.]").IsMatch(e.Text);
                    break;
                case Enum.InputType.Phone:
                    e.Handled = IsPhone(e);
                    break;
                default:
                    e.Handled = false;
                    break;
            }
        }
        /// <summary>
        /// 检验手机号
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private static bool IsPhone(System.Windows.Input.TextCompositionEventArgs e)
        {
            if ((e.OriginalSource as TextBox).Text.ToCharArray().Length > 10) return true;
            return System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"[^(1)\d{10}$]");
        }
    }
}
