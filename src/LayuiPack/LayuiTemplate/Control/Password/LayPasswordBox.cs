
using LayuiTemplate.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LayuiTemplate.Control
{
    /// <summary>
    /// 密码框
    /// </summary>
    public class LayPasswordBox : LayTextBox
    {
        private Button passwordChangeBtn;
        private PasswordBox passwordBox;//用于存储模板中抓取的密码框
        private bool IsPasswordChanging;
        /// <summary>
        ///密码框字符串暗码
        /// </summary>
        public char PasswordChar
        {
            get { return (char)GetValue(PasswordCharProperty); }
            set { SetValue(PasswordCharProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PasswordChar.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordCharProperty =
            DependencyProperty.Register("PasswordChar", typeof(char), typeof(LayPasswordBox));
        /// <summary>
        /// 初始化模板
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            passwordBox = this.Template.FindName("password", this) as PasswordBox;
            if (passwordBox == null) return;
            passwordChangeBtn = this.Template.FindName("passwordChangeBtn", this) as Button;
            if (passwordChangeBtn == null) return;
            passwordChangeBtn.Click -= PasswordChangeBtn_Click;
            passwordChangeBtn.Click += PasswordChangeBtn_Click;
            passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;
            passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
            passwordBox.Loaded -= PasswordBox_Loaded;
            passwordBox.Loaded += PasswordBox_Loaded;
        }
        private void PasswordChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (passwordBox == null) return;
            passwordBox.Focus();
            //使得密码框初始化将光标定位到最后一位
            SetSelection(passwordBox, passwordBox.Password.Length, passwordBox.Password.Length);
            this.Select(passwordBox.Password.Length, passwordBox.Password.Length);
        }

        /// <summary>
        /// 初始化密码框,获取绑定内容的值，并且将光标定位到最后一位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasswordBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (passwordBox == null) return;
            passwordBox.Password = this.Text;
            passwordBox.Focus();
            //使得密码框初始化将光标定位到最后一位
            this.Select(passwordBox.Password.Length, passwordBox.Password.Length);
            SetSelection(passwordBox, passwordBox.Password.Length, passwordBox.Password.Length);
        }
        /// <summary>
        /// 判断密码框的密码是否改变，若改变就修改文本框内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (passwordBox == null) return;
            IsPasswordChanging = true;
            this.Text = passwordBox.Password;
            this.Select(passwordBox.Password.Length, passwordBox.Password.Length);
            IsPasswordChanging = false;
        }
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            if (passwordBox == null) return;
            if (!IsPasswordChanging)
            {
                passwordBox.Password = this.Text;
            }
        }
        /// <summary>
        /// 光标定位
        /// </summary>
        /// <param name="passwordBox">密码框</param>
        /// <param name="start">光标定位起始长度</param>
        /// <param name="length">密码框的密码长度</param>
        private void SetSelection(PasswordBox passwordBox, int start, int length)
        {
            passwordBox.GetType().GetMethod("Select", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(passwordBox, new object[] { start, length });
        }
    }
}
