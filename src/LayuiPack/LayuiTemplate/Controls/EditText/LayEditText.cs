using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace LayuiTemplate.Controls
{
    /// <summary>
    /// 可编辑文本
    /// </summary>
    public class LayEditText : System.Windows.Controls.ContentControl
    {
        /// <summary>
        /// 可点击背景按钮
        /// </summary>
        private Button PART_Button;
        /// <summary>
        /// 编辑内容
        /// </summary>
        public object EditContent
        {
            get { return (object)GetValue(EditContentProperty); }
            set { SetValue(EditContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EditContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EditContentProperty =
            DependencyProperty.Register("EditContent", typeof(object), typeof(LayEditText));

        /// <summary>
        /// 编辑状态
        /// </summary>
        internal bool IsEditing
        {
            get { return (bool)GetValue(IsEditingProperty); }
            set { SetValue(IsEditingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsEditing.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty IsEditingProperty =
            DependencyProperty.Register("IsEditing", typeof(bool), typeof(LayEditText));
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_Button = GetTemplateChild("PART_Button") as Button;
            //防呆
            if (PART_Button != null)
            {
                PART_Button.Click -= PART_Button_Click;
                PART_Button.Click += PART_Button_Click;
            }
            //防呆
            if (EditContent != null) RefreshEditContent();
        }
        /// <summary>
        /// 刷新编辑控件
        /// </summary>
        private void RefreshEditContent()
        {
            //检查当前控件是否为输入框
            if (EditContent is TextBox textBox)
            {
                textBox = EditContent as TextBox;
                textBox.IsVisibleChanged -= Element_IsVisibleChanged;
                textBox.IsVisibleChanged += Element_IsVisibleChanged;
                textBox.LostFocus -= Element_LostFocus;
                textBox.LostFocus += Element_LostFocus;
            }
        }
        /// <summary>
        /// 输入框离焦关闭编辑状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Element_LostFocus(object sender, RoutedEventArgs e) => IsEditing = false;
        /// <summary>
        /// 背景点击让当前控件获得焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PART_Button_Click(object sender, RoutedEventArgs e)
        {
            //检查当前控件是否为输入框
            if (EditContent is TextBox textBox)
            {
                textBox?.Focus();
            }
        }
        /// <summary>
        /// 展示状态监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Element_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //检查当前控件是否为输入框
            if (EditContent is TextBox textBox)
            {
                textBox?.Focus();
                textBox?.SelectAll();
            }
        }
        /// <summary>
        /// 监听当前元素是否被双击并且切换编辑状态
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
        {
            IsEditing = true;
            e.Handled = true;
        }
    }
}
