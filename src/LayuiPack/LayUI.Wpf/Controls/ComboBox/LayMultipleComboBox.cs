using LayUI.Wpf.Controls.SVG;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        /// <summary>
        /// 标签元素删除路由事件
        /// </summary>
        private RoutedEventHandler delItemClickHandler;

        static LayMultipleComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LayMultipleComboBox), new FrameworkPropertyMetadata(typeof(LayMultipleComboBox)));
        }


        public LayMultipleComboBox()
        {
            // 监听全局鼠标按下事件
            EventManager.RegisterClassHandler(typeof(Window), Mouse.PreviewMouseDownEvent, new MouseButtonEventHandler(OnGlobalPreviewMouseDown), true);
            ContentItems = new ObservableCollection<object>();

            this.Loaded += LayMultipleComboBox_Loaded;
            this.Unloaded += LayMultipleComboBox_Unloaded;
        }

        private void LayMultipleComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (delItemClickHandler == null)
                delItemClickHandler = new RoutedEventHandler(OnDeleteItemBtnClick);

            this.AddHandler(Button.ClickEvent, delItemClickHandler);
        }

        private void LayMultipleComboBox_Unloaded(object sender, RoutedEventArgs e)
        {
            if (delItemClickHandler != null)
            {
                this.RemoveHandler(Button.ClickEvent, delItemClickHandler);
                delItemClickHandler = null;
            }
        }

        /// <summary>
        /// 全局点击事件捕获，用于关闭Popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGlobalPreviewMouseDown(object sender, MouseButtonEventArgs e)
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

        /// <summary>
        /// 判断元素是否在Popup内
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
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
                if (current == popup.Child)
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



        public ObservableCollection<object> ContentItems
        {
            get { return (ObservableCollection<object>)GetValue(ContentItemsProperty); }
            set { SetValue(ContentItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ContentItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentItemsProperty =
            DependencyProperty.Register("ContentItems", typeof(ObservableCollection<object>), typeof(LayMultipleComboBox), new PropertyMetadata(null));

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);

            if (ContentItems == null)
                throw new NullReferenceException("ContentItems in LayMultipleComboBox is null");

            if (e.AddedItems != null)
            {
                foreach (var item in e.AddedItems)
                {
                    var disPlayContent = GetItemContent(item);
                    if (!ContentItems.Contains(disPlayContent))
                        ContentItems.Add(disPlayContent);
                }
            }

            if (e.RemovedItems != null)
            {
                foreach (var item in e.RemovedItems)
                {
                    var disPlayContent = GetItemContent(item);
                    if (ContentItems.Contains(disPlayContent))
                        ContentItems.Remove(disPlayContent);
                }
            }
        }

        private object GetItemContent(object rawItem)
        {
            if (rawItem is LayMultipleComboBoxItem multipleComboBoxItem)
                return multipleComboBoxItem.Content;
            else
                return rawItem;
        }


        public Brush ItemDelBtnForeground
        {
            get { return (Brush)GetValue(ItemDelBtnForegroundProperty); }
            set { SetValue(ItemDelBtnForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemDelBtnForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemDelBtnForegroundProperty =
            DependencyProperty.Register("ItemDelBtnForeground", typeof(Brush), typeof(LayMultipleComboBox), new PropertyMetadata());

        /// <summary>
        /// 点击删除标签事件逻辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDeleteItemBtnClick(object sender, RoutedEventArgs e)
        {
            if (ContentItems == null)
                return;
            //判断是否是标签关闭按钮
            if (e.OriginalSource is Button btn)
            {
                //拿到数据源
                var delItem = btn.DataContext;
                //简单判断是否是ContentItems集合里的元素
                if (delItem != null && ContentItems.Contains(delItem))
                {
                    object DeselectItem = null;
                    //在SelectedItems中找到对应元素，并取消选中
                    foreach (var selectedItem in this.SelectedItems)
                    {
                        if (selectedItem is LayMultipleComboBoxItem layCItem)
                        {
                            if (object.ReferenceEquals(delItem, layCItem.Content))
                            {
                                DeselectItem = layCItem;
                                break;
                            }
                        }
                        else
                        {
                            if (object.ReferenceEquals(delItem, selectedItem))
                            {
                                DeselectItem = delItem;
                                break;
                            }
                        }
                    }
                    //在SelectedItems取消选中后，ContentItems会在OnSelectionChanged事件里Remove对应元素
                    if (DeselectItem != null)
                        this.SelectedItems.Remove(DeselectItem);
                }
            }
        }


    }
}
