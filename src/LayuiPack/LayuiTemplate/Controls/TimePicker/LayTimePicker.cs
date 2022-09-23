using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LayuiTemplate.Controls
{
    /// <summary>
    ///  LayTimePicker
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-06-23 上午 9:40:35</para>
    /// </summary>
    public class LayTimePicker : System.Windows.Controls.Control
    {

        /// <summary>
        /// 文本
        /// </summary>
        private TextBox PART_TextBox;
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
        /// 内容
        /// </summary>
        [Bindable(true)]
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(LayTimePicker));
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
            PART_TextBox = GetTemplateChild("PART_TextBox") as TextBox;
            if (PART_TextBox != null)
            {

            }
        }
    }
}
