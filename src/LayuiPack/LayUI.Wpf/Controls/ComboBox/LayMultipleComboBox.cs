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
    /// <summary>
    /// 多选Combobox
    /// </summary>
    public class LayMultipleComboBox : ListBox, ILayControl
    {


        private Popup popup;


        static LayMultipleComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LayMultipleComboBox), new FrameworkPropertyMetadata(typeof(LayMultipleComboBox)));
        }


        public LayMultipleComboBox()
        {
            // 监听全局鼠标按下事件
            EventManager.RegisterClassHandler(typeof(Window), Mouse.PreviewMouseDownEvent, new MouseButtonEventHandler(OnGlobalPreviewMouseDown), true);
        }

        private  void OnGlobalPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

            if (!IsDropDownOpen) return;

            var clickedElement = e.OriginalSource as DependencyObject;

            bool isClickInside = IsAncestorOf(clickedElement) || IsChildInPopup(clickedElement);

            if (isClickInside)
                // 内部点击：不关闭，处理事件
                return;
            else
                // 外部点击：关闭
                IsDropDownOpen = false;

        }

        private bool IsChildInPopup(DependencyObject element)
        {
            // 安全检查
            if (popup == null || popup.Child == null || element == null)
                return false;

            // 核心逻辑：直接判断点击的元素，是否是 Popup.Child 的子元素
            // 注意：Popup内部可能也有逻辑树，这里兼容视觉树和逻辑树
            var current = element;
            while (current != null)
            {
                if(current == popup.Child)
                    return true;

                current = VisualTreeHelper.GetParent(current);
            }
            return false;
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
            DependencyProperty.Register("IsDropDownOpen", typeof(bool), typeof(LayMultipleComboBox), new PropertyMetadata(false));




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


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            popup = GetTemplateChild("PART_Popup") as Popup;
        }


    }
}
