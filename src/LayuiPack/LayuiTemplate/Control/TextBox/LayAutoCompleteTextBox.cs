using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace LayuiTemplate.Control
{
    [TemplatePart(Name = "PART_Popup", Type = typeof(Popup))]
    [TemplatePart(Name = "PART_TextBox", Type = typeof(TextBox))]
    public class LayAutoCompleteTextBox : ListBox
    {
        private Popup PART_Popup;
        private TextBox PART_TextBox;
        /// <summary>
        /// 无数据提示信息
        /// </summary>
        public string NoDataTips
        {
            get { return (string)GetValue(NoDataTipsProperty); }
            set { SetValue(NoDataTipsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NoDataTips.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NoDataTipsProperty =
            DependencyProperty.Register("NoDataTips", typeof(string), typeof(LayAutoCompleteTextBox));


        /// <summary>
        /// 集合最大高度
        /// </summary>
        public double MaxDropDownHeight
        {
            get { return (double)GetValue(MaxDropDownHeightProperty); }
            set { SetValue(MaxDropDownHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxDropDownHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxDropDownHeightProperty =
            DependencyProperty.Register("MaxDropDownHeight", typeof(double), typeof(LayAutoCompleteTextBox));

        /// <summary>
        /// 当前文本
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(LayAutoCompleteTextBox));

        /// <summary>
        /// 值改变事件
        /// </summary>
        public event EventHandler<RoutedEventArgs> TextChanged
        {
            add => AddHandler(TextChangedEvent, value);
            remove => RemoveHandler(TextChangedEvent, value);
        }
        /// <summary>
        ///     值改变事件
        /// </summary>
        public static readonly RoutedEvent TextChangedEvent =
            EventManager.RegisterRoutedEvent("TextChanged", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(LayAutoCompleteTextBox));
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(LayAutoCompleteTextBox));
        /// <summary>
        /// 水印
        /// </summary>
        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Watermark.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.Register("Watermark", typeof(string), typeof(LayAutoCompleteTextBox));


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
            DependencyProperty.Register("IsDropDownOpen", typeof(bool), typeof(LayAutoCompleteTextBox));
        protected virtual void OnTextChanged(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_Popup = GetTemplateChild("PART_Popup") as Popup;
            PART_TextBox = GetTemplateChild("PART_TextBox") as TextBox;
            if (PART_TextBox != null && PART_Popup != null)
            {
                PART_TextBox.TextChanged -= PART_TextBox_TextChanged;
                PART_TextBox.TextChanged += PART_TextBox_TextChanged;
                PART_TextBox.SelectionChanged -= PART_TextBox_SelectionChanged;
                PART_TextBox.SelectionChanged += PART_TextBox_SelectionChanged;
                PART_TextBox.LostFocus -= PART_TextBox_LostFocus;
                PART_TextBox.LostFocus += PART_TextBox_LostFocus;
            }
            var window = Window.GetWindow(this);
            if (window != null)
            {
                window.LocationChanged -= Window_LocationChanged;
                window.LocationChanged += Window_LocationChanged;
                window.Deactivated -= Window_Deactivated;
                window.Deactivated += Window_Deactivated;
                window.MouseLeftButtonUp -= Windiw_MouseLeftButtonUp;
                window.MouseLeftButtonUp += Windiw_MouseLeftButtonUp;
            }
        }

        private void PART_TextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded) return; 
            IsDropDownOpen = true;
        }

        /// <summary>
        /// 抓取指定项控件并返回定制项控件
        /// </summary>
        /// <returns></returns>
        protected override DependencyObject GetContainerForItemOverride()
        {
            var item = new ListBoxItem();
            item.Focusable = false;
            if (item is FrameworkElement view)
            {
                view.PreviewMouseLeftButtonDown -= InvengoSelectTextBox_MouseLeftButtonDown;
                view.PreviewMouseLeftButtonDown += InvengoSelectTextBox_MouseLeftButtonDown;
            }
            return item;
        }

        private async void InvengoSelectTextBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsLoaded) return;
            PART_TextBox.Focus();
            PART_TextBox.SelectAll();
            await Task.Delay(100);
            IsDropDownOpen = false;
        }

        private void Windiw_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!IsLoaded) return;
            IsDropDownOpen = false;
        }
        private void Window_Deactivated(object sender, EventArgs e)
        {
            if (!IsLoaded) return;
            IsDropDownOpen = false;
        }

        private void PART_TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded) return;
            IsDropDownOpen = false;
        }

        private void PART_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.OnTextChanged(new RoutedEventArgs(TextChangedEvent, this));
        }
        private void Window_LocationChanged(object sender, EventArgs e)
        {
            if (!IsLoaded) return;
            IsDropDownOpen = false;
        }
    }
}
