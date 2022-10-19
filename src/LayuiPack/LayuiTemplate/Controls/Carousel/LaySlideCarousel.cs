using LayuiTemplate.Enum.Carousel;
using LayuiTemplate.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;
using System.Xml;

namespace LayuiTemplate.Controls
{
    /// <summary>
    /// 滑动轮播图
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-06-07 下午 4:12:39</para>
    /// </summary>
    [ContentProperty("Items")]
    [TemplatePart(Name = "PART_LeftButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_RightButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_ItemsPanel", Type = typeof(Panel))]
    [DefaultProperty("Items")]
    public class LaySlideCarousel : Control
    {
        /// <summary>
        /// 轮播图容器集合
        /// </summary>
        private Panel PART_ItemsPanel = null;
        /// <summary>
        /// 计时器
        /// </summary>
        private DispatcherTimer timer;
        /// <summary>
        /// 上一页
        /// </summary>
        private Button PART_LeftButton;
        /// <summary>
        /// 下一页
        /// </summary>
        private Button PART_RightButton;
        /// <summary>
        /// 切换按钮展示类型
        /// </summary>
        [Bindable(true)]
        public CarouselArrow Arrow
        {
            get { return (CarouselArrow)GetValue(ArrowProperty); }
            set { SetValue(ArrowProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Arrow.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ArrowProperty =
            DependencyProperty.Register("Arrow", typeof(CarouselArrow), typeof(LaySlideCarousel), new PropertyMetadata(CarouselArrow.Always));

        /// <summary>
        /// 是否自动切换
        /// </summary>
        [Bindable(true)]
        public bool IsAutoSwitch
        {
            get { return (bool)GetValue(IsAutoSwitchProperty); }
            set { SetValue(IsAutoSwitchProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAutoSwitch.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAutoSwitchProperty =
            DependencyProperty.Register("IsAutoSwitch", typeof(bool), typeof(LaySlideCarousel), new PropertyMetadata(false, OnAutoSwitchChange));

        private static void OnAutoSwitchChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }
        /// <summary>
        /// 间隔时间
        /// </summary>
        [Bindable(true)]
        public TimeSpan Interval
        {
            get { return (TimeSpan)GetValue(IntervalProperty); }
            set { SetValue(IntervalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Interval.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IntervalProperty =
            DependencyProperty.Register("Interval", typeof(TimeSpan), typeof(LaySlideCarousel), new PropertyMetadata(TimeSpan.FromSeconds(4), OnIntervalChange));

        private static void OnIntervalChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(LaySlideCarousel), new PropertyMetadata(OnItemTemplateChanged));

        private static void OnItemTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as LaySlideCarousel).OnItemTemplateChanged(e);
        }

        protected virtual void OnItemTemplateChanged(DependencyPropertyChangedEventArgs e)
        {
            if (IsLoaded)
            {
                UpdateItems();
                InvalidateVisual();
            }
        }

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(LaySlideCarousel));



        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register("SelectedIndex", typeof(int), typeof(LaySlideCarousel), new PropertyMetadata(0));



        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(LaySlideCarousel), new PropertyMetadata(OnItemsSourceChanged));

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ui = d as LaySlideCarousel;
            IEnumerable oldValue = (IEnumerable)e.OldValue;
            IEnumerable newValue = (IEnumerable)e.NewValue;
            ui.OnItemsSourceChanged(oldValue, newValue);
        }
        protected virtual void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {

        }
        private void Notify_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsLoaded) OnItemsChanged(e);
        }
        private Collection<object> _Items;

        public Collection<object> Items
        {
            get
            {
                if (_Items == null)
                {
                    CreateItemCollectionAndGenerator();
                }
                return _Items;
            }
        }
        private void CreateItemCollectionAndGenerator()
        {
            var list = new ObservableCollection<object>();
            list.CollectionChanged -= List_CollectionChanged;
            list.CollectionChanged += List_CollectionChanged;
            _Items = list;
        }

        private void List_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsLoaded) OnItemsChanged(e);
        }
        protected virtual void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            if (IsLoaded) UpdateItems();
        }

        private void UpdateItems()
        {
            if (Items.Count > 0 && ItemsSource != null) return;
            if (PART_ItemsPanel == null || PART_ItemsPanel.Children == null) return;
            PART_ItemsPanel.Children.Clear();
            if (ItemsSource != null)
            {
                if (ItemsSource is IList list)
                {
                    var items = list.Cast<object>().ToList();
                    if (items == null) return;
                    ContentPresenter FirstContent = new ContentPresenter() { ContentTemplate = ItemTemplate, Content = items.LastOrDefault() };
                    ContentPresenter LastContent = new ContentPresenter() { ContentTemplate = ItemTemplate, Content = items.FirstOrDefault() };
                    foreach (var item in ItemsSource)
                    {
                        PART_ItemsPanel.Children.Add(new ContentPresenter() { ContentTemplate = ItemTemplate, Content = item });
                    }
                    PART_ItemsPanel.Children.Insert(0, FirstContent);
                    PART_ItemsPanel.Children.Add(LastContent);
                }
            }
            else
            {
                if (Items == null) return;
                ContentPresenter FirstContent = new ContentPresenter() { Content = LayUIElementHelper.DeepCopy(Items.LastOrDefault() as FrameworkElement) };
                ContentPresenter LastContent = new ContentPresenter() { Content = LayUIElementHelper.DeepCopy(Items.FirstOrDefault() as FrameworkElement) };
                foreach (var item in Items)
                {
                    PART_ItemsPanel.Children.Add(new ContentPresenter() { Content = item });
                }
                PART_ItemsPanel.Children.Insert(0, FirstContent);
                PART_ItemsPanel.Children.Add(LastContent);
            }
        }

        private void Refresh(Size size)
        {
            if (PART_ItemsPanel != null && PART_ItemsPanel.Children != null && PART_ItemsPanel.IsLoaded && IsLoaded)
            {
                foreach (FrameworkElement item in PART_ItemsPanel.Children)
                {
                    item.Width = size.Width;
                    item.Height = size.Height;
                }
                PART_ItemsPanel.InvalidateVisual();
            }
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_LeftButton = GetTemplateChild("PART_LeftButton") as Button;
            PART_RightButton = GetTemplateChild("PART_RightButton") as Button;
            PART_ItemsPanel = GetTemplateChild("PART_ItemsPanel") as StackPanel;
            PART_ItemsPanel.Loaded += PART_ItemsControl_Loaded;
            UpdateItems();
        }
        private void PART_ItemsControl_Loaded(object sender, RoutedEventArgs e)
        {
            InvalidateVisual();
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (IsLoaded) Refresh(DesiredSize);
        }
    }
}
