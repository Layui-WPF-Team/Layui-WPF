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
    [TemplatePart(Name = "PART_FirstContentControl", Type = typeof(ContentControl))]
    [TemplatePart(Name = "PART_ContentControl", Type = typeof(ContentControl))]
    [TemplatePart(Name = "PART_LastContentControl", Type = typeof(ContentControl))]
    [DefaultProperty("Items")]
    public class LaySlideCarousel : Control
    {
        /// <summary>
        /// 首项
        /// </summary>
        private ContentControl PART_FirstContentControl = null;
        /// <summary>
        /// 轮播图容器集合
        /// </summary>
        private ContentControl PART_ContentControl = null;
        /// <summary>
        /// 末尾
        /// </summary>
        private ContentControl PART_LastContentControl = null;
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
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(LaySlideCarousel));

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
            OnItemsChanged(sender, e);
        }
        protected virtual void OnItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (!IsLoaded) return;
            Refresh();
            UpdateItems();
        }

        private void UpdateItems()
        {

        }

        private void Refresh()
        {
            if (PART_FirstContentControl == null || PART_ContentControl == null || PART_LastContentControl == null) return;
            if (Items.Count < 1) return;
            //if (SelectedIndex < 0) SelectedIndex = 0;
            //if (SelectedIndex > Items.Count-1) SelectedIndex = Items.Count - 1;
            //PART_FirstContentControl.Content = Items[SelectedIndex+1];
            PART_ContentControl.Content = Items[0];
            //PART_LastContentControl.Content = SelectedIndex--<0? Items[Items.Count-1]: Items[SelectedIndex-1<0? Items.Count - 1:];
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_LeftButton = GetTemplateChild("PART_LeftButton") as Button;
            PART_RightButton = GetTemplateChild("PART_RightButton") as Button;
            PART_FirstContentControl = GetTemplateChild("PART_FirstContentControl") as ContentControl;
            PART_ContentControl = GetTemplateChild("PART_ContentControl") as ContentControl;
            PART_LastContentControl = GetTemplateChild("PART_LastContentControl") as ContentControl;
            Refresh();
        }
    }
}
