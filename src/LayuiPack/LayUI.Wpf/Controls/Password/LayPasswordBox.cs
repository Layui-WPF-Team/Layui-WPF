
using LayUI.Wpf.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace LayUI.Wpf.Controls
{
    /// <summary>
    /// 密码框
    /// </summary>
    [TemplatePart(Name = "PART_PasswordBox", Type = typeof(PasswordBox))]
    [TemplatePart(Name = "PART_ToggleButton", Type = typeof(ToggleButton))]
    public class LayPasswordBox : LayTextBox
    {
        private ToggleButton PART_ToggleButton;
        private PasswordBox PART_PasswordBox;//用于存储模板中抓取的密码框
        /// <summary>
        ///密码框字符串暗码
        /// </summary>
        [Bindable(true)]
        public char PasswordChar
        {
            get { return (char)GetValue(PasswordCharProperty); }
            set { SetValue(PasswordCharProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PasswordChar.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordCharProperty =
            DependencyProperty.Register("PasswordChar", typeof(char), typeof(LayPasswordBox));


        [Bindable(true)]
        public bool IsShowPasswrod
        {
            get { return (bool)GetValue(IsShowPasswrodProperty); }
            set { SetValue(IsShowPasswrodProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsShowPasswrod.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsShowPasswrodProperty =
            DependencyProperty.Register("IsShowPasswrod", typeof(bool), typeof(LayPasswordBox), new PropertyMetadata(false, OnIsShowPasswrodChanged));

        private static void OnIsShowPasswrodChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LayPasswordBox passwordBox = d as LayPasswordBox;
            if (passwordBox.IsLoaded)
            {
                if (passwordBox.IsShowPasswrod)
                {
                    passwordBox.Text = passwordBox.PART_PasswordBox.Password;
                }
                else
                {
                    passwordBox.PART_PasswordBox.Password = passwordBox.Text;
                }
            }
        }
        /// <summary>
        /// 初始化模板
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_PasswordBox = GetTemplateChild("PART_PasswordBox") as PasswordBox;
            PART_ToggleButton = GetTemplateChild("PART_ToggleButton") as ToggleButton;
            if (PART_PasswordBox != null || PART_ToggleButton != null)
            {
                PART_PasswordBox.PasswordChanged -= PasswordBox_PasswordChanged;
                PART_PasswordBox.PasswordChanged += PasswordBox_PasswordChanged;
                if (!IsShowPasswrod)
                {
                    PART_PasswordBox.Password = Text;
                }
            }
        }

        /// <summary>
        /// 判断密码框的密码是否改变，若改变就修改文本框内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PART_PasswordBox == null) return;
            if (!IsShowPasswrod)
            {
                Text = PART_PasswordBox.Password;
            }
        }
        /// <summary>
        /// 光标定位
        /// </summary>
        /// <param name="PART_PasswordBox">密码框</param>
        /// <param name="start">光标定位起始长度</param>
        /// <param name="length">密码框的密码长度</param>
        private void SetSelection(PasswordBox PART_PasswordBox, int start, int length)
        {
            PART_PasswordBox.GetType().GetMethod("Select", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(PART_PasswordBox, new object[] { start, length });
        }

    }
}
