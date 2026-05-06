using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace LayUI.Wpf.Controls
{
    public class LayMultipleComboBox: ListBox, ILayControl
    {

        private ToggleButton toggleButton;
        private Popup popup;


        static LayMultipleComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LayMultipleComboBox), new FrameworkPropertyMetadata(typeof(LayMultipleComboBox)));
        }

        /// <summary>
        /// 这是水印
        /// </summary>
        [Bindable(true)]
        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Watermark.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.Register("Watermark", typeof(string), typeof(LayMultipleComboBox));

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
            DependencyProperty.Register("WatermarkColor", typeof(Brush), typeof(LayMultipleComboBox), new PropertyMetadata(Brushes.Transparent));



        /// <summary>
        /// 开启下拉
        /// </summary>
        public bool IsDropDownOpen
        {
            get { return (bool)GetValue(IsDropDownOpenProperty); }
            set { SetValue(IsDropDownOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsDropDownOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDropDownOpenProperty =
            DependencyProperty.Register("IsDropDownOpen", typeof(bool), typeof(LayMultipleComboBox), new PropertyMetadata(false,OnIsDropDownOpenChanged));

        private static void OnIsDropDownOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var multipComboBox = (LayMultipleComboBox)d;
            if ((bool)e.NewValue)
            {
                // 🔥 效仿原生：打开下拉时，捕获鼠标（SubTree模式：只捕获本控件及子元素）
                Mouse.Capture(multipComboBox, CaptureMode.SubTree);
            }
            else
            {
                // 关闭下拉时，释放鼠标捕获
                if (Mouse.Captured == multipComboBox)
                    Mouse.Capture(null);
            }
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseDown(e);


            if (!IsDropDownOpen) return;

            // 获取点击的元素
            var clickedElement = e.OriginalSource as DependencyObject;

            // 🔥 效仿原生：判断点击是否在「控件内部 / Popup内部」
            bool isClickInside = IsAncestorOf(clickedElement) || IsClickInsidePopup(clickedElement);

            if (isClickInside)
            {
                // 点击内部：不关闭，仅标记事件已处理（防止冒泡）
                e.Handled = true;
            }
            else
            {
                // 点击外部：关闭下拉
                IsDropDownOpen = false;
                e.Handled = true;
            }
        }

        private bool IsClickInsidePopup(DependencyObject element)
        {
            if (element == null) return false;

            // 遍历视觉树，找 Popup
            while (element != null)
            {
                if (element is Popup popup && popup.PlacementTarget == this)
                    return true;

                element = VisualTreeHelper.GetParent(element);
            }
            return false;
        }
        /// <summary>
        /// 重写自定义指定项子控件
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is LayMultipleComboBoxItem;
        }
        /// <summary>
        /// 抓取指定项控件并返回定制项控件
        /// </summary>
        /// <returns></returns>
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new LayMultipleComboBoxItem();
        }



        /// <summary>
        /// 圆角
        /// </summary>
        [Bindable(true)]
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(LayMultipleComboBox));

        
        /// <summary>
        /// 分割线宽度(只有IsEditable开启才生效)
        /// </summary>
        [Bindable(true)]
        public double Line
        {
            get { return (double)GetValue(LineProperty); }
            set { SetValue(LineProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Line.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineProperty =
            DependencyProperty.Register("Line", typeof(double), typeof(LayMultipleComboBox));



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
            DependencyProperty.Register("HoverBorderBrush", typeof(Brush), typeof(LayMultipleComboBox), new PropertyMetadata(Brushes.Transparent));


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
            DependencyProperty.Register("FocusedBorderBrush", typeof(Brush), typeof(LayMultipleComboBox), new PropertyMetadata(Brushes.Transparent));

         
        /// <summary>
        /// 
        /// </summary>
        public double MaxDropDownHeight
        {
            get { return (double)GetValue(MaxDropDownHeightProperty); }
            set { SetValue(MaxDropDownHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxDropDownHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxDropDownHeightProperty =
            DependencyProperty.Register("MaxDropDownHeight", typeof(double), typeof(LayMultipleComboBox));

   


    }
}
