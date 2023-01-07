using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace LayUI.Wpf.Controls
{
    /// <summary>
    ///  LayTimePicker
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-06-23 上午 9:40:35</para>
    /// </summary>
    public class LayTimePicker : System.Windows.Controls.Control
    {
        /// <summary>
        /// 
        /// </summary>
        private LayTimer PART_LayTimer;
        /// <summary>
        /// 文本
        /// </summary>
        private Popup PART_Popup;
        /// <summary>
        /// 水印文字颜色
        /// </summary>
        [Bindable(true)]
        public Brush WatermarkColor
        {
            get { return (Brush)GetValue(WatermarkColorProperty); }
            set { SetValue(WatermarkColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FocusedBorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WatermarkColorProperty =
            DependencyProperty.Register("WatermarkColor", typeof(Brush), typeof(LayTimePicker), new PropertyMetadata(Brushes.Transparent));
        /// <summary>
        /// 鼠标移入边框色
        /// </summary>
        [Bindable(true)]
        public Brush HoverBorderBrush
        {
            get { return (Brush)GetValue(HoverBorderBrushProperty); }
            set { SetValue(HoverBorderBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HoverBorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HoverBorderBrushProperty =
            DependencyProperty.Register("HoverBorderBrush", typeof(Brush), typeof(LayTimePicker), new PropertyMetadata(Brushes.Transparent));


        /// <summary>
        /// 光标聚焦后的边框色
        /// </summary>
        [Bindable(true)]
        public Brush FocusedBorderBrush
        {
            get { return (Brush)GetValue(FocusedBorderBrushProperty); }
            set { SetValue(FocusedBorderBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FocusedBorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FocusedBorderBrushProperty =
            DependencyProperty.Register("FocusedBorderBrush", typeof(Brush), typeof(LayTimePicker), new PropertyMetadata(Brushes.Transparent));


        /// <summary>
        /// 时间
        /// </summary>
        [Bindable(true)]
        public DateTime? Time
        {
            get { return (DateTime?)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("Time", typeof(DateTime?), typeof(LayTimePicker));
        /// <summary>
        /// 水印
        /// </summary>
        [Bindable(true)]
        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Watermark.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.Register("Watermark", typeof(string), typeof(LayTimePicker));



        [Bindable(true)]
        public bool IsDropDownOpen
        {
            get { return (bool)GetValue(IsDropDownOpenProperty); }
            set { SetValue(IsDropDownOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsDropDownOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDropDownOpenProperty =
            DependencyProperty.Register("IsDropDownOpen", typeof(bool), typeof(LayTimePicker));



        [Bindable(true)]
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(LayTimePicker));


        [Bindable(true)]
        public string DateMessageTitle
        {
            get { return (string)GetValue(DateMessageTitleProperty); }
            set { SetValue(DateMessageTitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DateMessageTitle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DateMessageTitleProperty =
            DependencyProperty.Register("DateMessageTitle", typeof(string), typeof(LayTimePicker));

        /// <summary>
        /// 是否只读
        /// </summary>
        [Bindable(true)]
        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsReadOnly.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(LayTimePicker), new PropertyMetadata(true));


        /// <summary>
        /// 线粗细
        /// </summary>
        [Bindable(true)]
        public double Line
        {
            get { return (double)GetValue(LineProperty); }
            set { SetValue(LineProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Line.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineProperty =
            DependencyProperty.Register("Line", typeof(double), typeof(LayTimePicker));
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_Popup = GetTemplateChild("PART_Popup") as Popup;
            PART_LayTimer = GetTemplateChild("PART_LayTimer") as LayTimer;
            if (PART_Popup != null)
            {
                PART_Popup.Opened -= PART_Popup_Opened;
                PART_Popup.Opened += PART_Popup_Opened;
                PART_LayTimer.Click -= PART_LayTimer_Click;
                PART_LayTimer.Click += PART_LayTimer_Click;
            }
        }

        private void PART_LayTimer_Click(object sender, RoutedEventArgs e)
        {
            PART_Popup.IsOpen = false;
        }

        private void PART_Popup_Opened(object sender, EventArgs e)
        {
            if (PART_LayTimer != null)
            {
                PART_LayTimer.Time = Time;
                PART_LayTimer.DefaultTime = Time;
                PART_LayTimer.RefreshView();
            }
        }
    }
}
