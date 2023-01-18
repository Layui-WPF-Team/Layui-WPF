using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Media3D;
using System.Windows.Media;

namespace LayUI.Wpf.Controls
{
    [DefaultProperty("Items")]
    [ContentProperty("Items")]
    [TemplatePart(Name = "PART_Popup", Type = typeof(Popup))]
    [TemplatePart(Name = "PART_ItemsHost", Type = typeof(Panel))]
    public class LayAutoCompleteTextBox : TextBox
    {
        public LayAutoCompleteTextBox()
        {
            var items = new ObservableCollection<object>();
            items.CollectionChanged += Items_CollectionChanged;
            Items = items;
        }

        private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count > 0)
            {
                SetValue(HasItemsProperty, true);
            }
            OnItemsChanged(sender, e);
        }

        private Panel PART_ItemsHost { get; set; }
        private Popup PART_Popup;
        /// <summary>
        /// 无数据提示信息
        /// </summary>
        [Bindable(true)]
        public string NoDataTips
        {
            get { return (string)GetValue(NoDataTipsProperty); }
            set { SetValue(NoDataTipsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NoDataTips.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NoDataTipsProperty =
            DependencyProperty.Register("NoDataTips", typeof(string), typeof(LayAutoCompleteTextBox));
        /// <summary>
        /// 数据集合
        /// </summary>

        public Collection<object> Items
        {
            get { return (Collection<object>)GetValue(ItemsProperty); }
            internal set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(Collection<object>), typeof(LayAutoCompleteTextBox));



        internal bool HasItems
        {
            get { return (bool)GetValue(HasItemsProperty); }
            set { SetValue(HasItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HasItems.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty HasItemsProperty =
            DependencyProperty.Register("HasItems", typeof(bool), typeof(LayAutoCompleteTextBox), new PropertyMetadata(false));

        private void OnItemCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count > 0)
            {
                SetValue(HasItemsProperty, true);
            }
            OnItemsChanged(sender, e);
        }
        /// <summary>
        /// 刷新
        /// </summary>
        protected virtual void Refresh()
        {
            if (PART_Popup == null && PART_ItemsHost == null) return;
            PART_ItemsHost.Children.Clear();
            if (ItemsSource != null)
            {
                if (ItemsSource is INotifyCollectionChanged)
                {
                    Items = (ObservableCollection<object>)ItemsSource;
                }
                else
                {
                    var items = new Collection<object>();
                    foreach (var item in ItemsSource)
                    {
                        items.Add(item);
                    }
                    Items = items;
                }

            }
            if (Items != null)
            {
                foreach (var item in Items)
                {
                    DependencyObject container;
                    if (IsItemItsOwnContainerOverride(item))
                    {
                        container = item as DependencyObject;
                    }
                    else
                    {
                        container = GetContainerForItemOverride();
                        PrepareContainerForItemOverride(container, item);
                    }

                    if (container is FrameworkElement element)
                    {
                        element.Style = ItemContainerStyle;
                        PART_ItemsHost.Children.Add(element);
                    }
                }
            }
        }
        protected virtual void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            switch (element)
            {
                case ContentControl contentControl:
                    contentControl.Content = item;
                    contentControl.ContentTemplate = ItemTemplate;
                    break;
                case ContentPresenter contentPresenter:
                    contentPresenter.Content = item;
                    contentPresenter.ContentTemplate = ItemTemplate;
                    break;
            }
        }
        protected virtual DependencyObject GetContainerForItemOverride() => new ContentPresenter();

        protected virtual bool IsItemItsOwnContainerOverride(object item) => item is ContentPresenter;

        [Bindable(true)]
        [Category("Content")]
        public Style ItemContainerStyle
        {
            get { return (Style)GetValue(ItemContainerStyleProperty); }
            set { SetValue(ItemContainerStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemContainerStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemContainerStyleProperty =
            DependencyProperty.Register("ItemContainerStyle", typeof(Style), typeof(LayAutoCompleteTextBox), new FrameworkPropertyMetadata(null, OnItemContainerStyleChanged));
        private static void OnItemContainerStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => (d as LayAutoCompleteTextBox)?.OnItemContainerStyleChanged(e);
        protected virtual void OnItemContainerStyleChanged(DependencyPropertyChangedEventArgs e) => Refresh();

        /// <summary>
        /// 修改数据集合
        /// </summary>
        private void UpdateItems()
        {

        }
        protected virtual void OnItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Refresh();
            UpdateItems();
        }


        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(LayAutoCompleteTextBox));


        [Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]


        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        // Using a DependencyProperty as the backing store for ItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(LayAutoCompleteTextBox), new FrameworkPropertyMetadata(null, OnItemsSourceChanged));

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LayAutoCompleteTextBox AutoCompleteTextBox = (LayAutoCompleteTextBox)d;
            IEnumerable oldValue = (IEnumerable)e.OldValue;
            IEnumerable enumerable = (IEnumerable)e.NewValue;
            if (e.OldValue != null)
            {
                if (oldValue is INotifyCollectionChanged notify) notify.CollectionChanged += AutoCompleteTextBox.Items_CollectionChanged;
            }
            else if (e.NewValue != null)
            {
                if (enumerable is INotifyCollectionChanged notify) notify.CollectionChanged += AutoCompleteTextBox.Items_CollectionChanged;
            }
            else
            {
                (AutoCompleteTextBox.Items as IList).Clear();
            }
            AutoCompleteTextBox.OnItemsSourceChanged(oldValue, enumerable);
        }

        protected virtual void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {

        }
        /// <summary>
        /// 集合最大高度
        /// </summary>
        [Bindable(true)]
        public double MaxDropDownHeight
        {
            get { return (double)GetValue(MaxDropDownHeightProperty); }
            set { SetValue(MaxDropDownHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxDropDownHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxDropDownHeightProperty =
            DependencyProperty.Register("MaxDropDownHeight", typeof(double), typeof(LayAutoCompleteTextBox));

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
        [Bindable(true)]
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
        [Bindable(true)]
        public bool IsDropDownOpen
        {
            get { return (bool)GetValue(IsDropDownOpenProperty); }
            set { SetValue(IsDropDownOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsDropDownOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDropDownOpenProperty =
            DependencyProperty.Register("IsDropDownOpen", typeof(bool), typeof(LayAutoCompleteTextBox));

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            if (!IsDropDownOpen) IsDropDownOpen = true;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_Popup = GetTemplateChild("PART_Popup") as Popup;
            PART_ItemsHost = GetTemplateChild("PART_ItemsHost") as Panel;
            var window = Window.GetWindow(this);
            if (PART_Popup == null && PART_ItemsHost == null && window == null) return;
            window.LocationChanged -= Window_LocationChanged;
            window.LocationChanged += Window_LocationChanged;
            LostFocus -= LayAutoCompleteTextBox_LostFocus;
            LostFocus += LayAutoCompleteTextBox_LostFocus;
            Refresh();
        }

        private void LayAutoCompleteTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            IsDropDownOpen = false;
        }
        protected override void OnGotFocus(RoutedEventArgs e)
        {
            IsDropDownOpen = true;
            base.OnGotFocus(e);
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            IsDropDownOpen = false;
        }
        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);
            IsDropDownOpen = true;
        }

    }
}
