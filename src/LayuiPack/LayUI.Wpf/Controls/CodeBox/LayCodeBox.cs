using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LayUI.Wpf.Controls
{
    /// <summary>
    /// 代码输入框
    /// </summary>
    [TemplatePart(Name = nameof(PART_ScrollViewer), Type = typeof(System.Windows.Controls.ScrollViewer))]
    [TemplatePart(Name = nameof(PART_ContentHost), Type = typeof(System.Windows.Controls.ScrollViewer))]
    public class LayCodeBox: TextBox, ILayControl
    {
        private System.Windows.Controls.ScrollViewer PART_ScrollViewer;
        private System.Windows.Controls.ScrollViewer PART_ContentHost;
        public Brush LineNumberForeground
        {
            get { return (Brush)GetValue(LineNumberForegroundProperty); }
            set { SetValue(LineNumberForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LineNumberForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineNumberForegroundProperty =
            DependencyProperty.Register("LineNumberForeground", typeof(Brush), typeof(LayCodeBox));

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
            DependencyProperty.Register("HoverBorderBrush", typeof(Brush), typeof(LayCodeBox), new PropertyMetadata(Brushes.Transparent));


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
            DependencyProperty.Register("FocusedBorderBrush", typeof(Brush), typeof(LayCodeBox), new PropertyMetadata(Brushes.Transparent));


        /// <summary>
        /// 按钮圆角
        /// </summary>
        [Bindable(true)]
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(LayCodeBox));


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_ScrollViewer = GetTemplateChild(nameof(PART_ScrollViewer)) as System.Windows.Controls.ScrollViewer;
            PART_ContentHost = GetTemplateChild(nameof(PART_ContentHost)) as System.Windows.Controls.ScrollViewer;
            if (PART_ScrollViewer != null && PART_ContentHost != null)
            {
                PART_ContentHost.ScrollChanged -= PART_ContentHost_ScrollChanged;
                PART_ContentHost.ScrollChanged += PART_ContentHost_ScrollChanged;
            }
        }
        private void PART_ContentHost_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.VerticalChange != 0)
            {
                PART_ScrollViewer.ScrollToVerticalOffset(PART_ContentHost.VerticalOffset);
            }
            if (e.HorizontalChange != 0)
            {
                PART_ScrollViewer.ScrollToHorizontalOffset(PART_ContentHost.HorizontalOffset);
            }
        }

    }
}
