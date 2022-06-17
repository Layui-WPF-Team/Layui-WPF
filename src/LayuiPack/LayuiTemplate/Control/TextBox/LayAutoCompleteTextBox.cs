using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace LayuiTemplate.Control
{
    [TemplatePart(Name = "PART_Popup", Type = typeof(Popup))]
    public class LayAutoCompleteTextBox : LayTextBox
    {
        private Popup PART_Popup;
        /// <summary>
        /// 内容
        /// </summary>
        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(LayAutoCompleteTextBox));
        /// <summary>
        /// 是否展开
        /// </summary>
        public bool IsDropDownOpen
        {
            get { return (bool)GetValue(IsDropDownOpenProperty); }
            set { SetValue(IsDropDownOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsDropDownOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDropDownOpenProperty =
            DependencyProperty.Register("IsDropDownOpen", typeof(bool), typeof(LayAutoCompleteTextBox), new PropertyMetadata(OnIsDropDownOpen));

        private static void OnIsDropDownOpen(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LayAutoCompleteTextBox textBox)
            {
                textBox.GetSelector();
            }
        }
        /// <summary>
        /// 获取并设置Popup状态
        /// </summary>
        private void GetSelector()
        {
            if (Content != null)
            {
                if (Content is Selector selector)
                {
                    selector.SelectionChanged -= Selector_SelectionChanged;
                    selector.SelectionChanged += Selector_SelectionChanged;
                }
            }
        }
        /// <summary>
        /// 选中状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Selector_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            IsDropDownOpen = false;
            if (Content != null)
            {
                if (Content is Selector selector)
                {
                    if (selector.Items.Count > 0)
                        selector.SelectedIndex = -1;
                }
            }
            Focus();
        }
        protected override void OnPreviewKeyUp(KeyEventArgs e)
        {
            base.OnPreviewKeyUp(e);
            if (e.Key != Key.LeftShift && e.Key != Key.RightShift && e.Key != Key.LeftCtrl && e.Key != Key.RightCtrl && !IsDropDownOpen)
                IsDropDownOpen = true;

        }
        /// <summary>
        /// 取消焦点
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            IsDropDownOpen = false;
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_Popup = GetTemplateChild("PART_Popup") as Popup;
            if (PART_Popup != null)
            {
                PART_Popup.Opened -= PART_Popup_Opened;
                PART_Popup.Opened += PART_Popup_Opened;
            }
            GetSelector();
        }

        private void PART_Popup_Opened(object sender, EventArgs e)
        {
            var window = Window.GetWindow(this);
            if (window != null)
            {
                window.LocationChanged -= Window_LocationChanged;
                window.LocationChanged += Window_LocationChanged;
                window.PreviewMouseLeftButtonUp -= Window_PreviewMouseLeftButtonUp;
                window.PreviewMouseLeftButtonUp += Window_PreviewMouseLeftButtonUp;
            }
            if (Content != null)
            {
                if (Content is Selector selector)
                {
                    if (selector.Items.Count > 0)
                        selector.SelectedIndex = -1;
                }
            }
        }

        private void Window_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            IsDropDownOpen = false;
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            IsDropDownOpen = false;
        }
    }
}
