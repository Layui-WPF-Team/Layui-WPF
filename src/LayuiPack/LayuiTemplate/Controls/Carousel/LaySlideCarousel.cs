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
    [TemplatePart(Name = "PART_ItemsContentControl", Type = typeof(ContentControl))]
    [DefaultProperty("Items")]
    public class LaySlideCarousel : Control
    {
        private Collection<object> items = new Collection<object>();
        /// <summary>
        /// 轮播图容器集合
        /// </summary>
        private StackPanel PART_CarouselPanel = null;
        /// <summary>
        /// 轮播图容器集合
        /// </summary>
        private ContentControl PART_ItemsContentControl = null;
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


        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(LaySlideCarousel));

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
            if (e.NewItems != null && e.NewItems.Count > 0)
            {
                SetValue(HasItemsPropertyKey, true);
            }
            OnItemsChanged(sender, e);
        }
        internal static readonly DependencyPropertyKey HasItemsPropertyKey =
        DependencyProperty.RegisterReadOnly(nameof(HasItems), typeof(bool), typeof(LaySlideCarousel),
            new PropertyMetadata(false));
        public static readonly DependencyProperty HasItemsProperty = HasItemsPropertyKey.DependencyProperty;
        public bool HasItems => (bool)GetValue(HasItemsProperty);
        protected virtual void OnItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (!isloaded) return;
            Refresh();
            UpdateItems();
        }

        private void UpdateItems()
        {

        }

        private void Refresh()
        {
            if (PART_ItemsContentControl == null) return;
            PART_CarouselPanel = new StackPanel() { Orientation= Orientation.Horizontal };
            items.Clear();
            PART_CarouselPanel.Children.Clear();
            foreach (var item in Items.ToList())
            {
                items.Add(item);
            }
            items.Add(Items[0]);
            items.Insert(0, Items[Items.Count - 1]);
            if (PART_CarouselPanel.Children.Count == items.Count) return;
            for (int i = 0; i < items.Count; i++)
            {
                var ui = LayUIElementHelper.DeepCopy(items[i] as DependencyObject) as FrameworkElement;
                PART_CarouselPanel.Children.Add(ui);
            }
            PART_ItemsContentControl.Content = PART_CarouselPanel;
        }
        bool isloaded = false;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_LeftButton = GetTemplateChild("PART_LeftButton") as Button;
            PART_RightButton = GetTemplateChild("PART_RightButton") as Button;
            PART_ItemsContentControl = GetTemplateChild("PART_ItemsContentControl") as ContentControl;
            isloaded = true;
            Refresh();
        }
    }
}
