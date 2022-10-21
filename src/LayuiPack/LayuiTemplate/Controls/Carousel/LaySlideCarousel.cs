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
    [TemplatePart(Name = "PART_LeftButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_RightButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_ItemsPanel", Type = typeof(Panel))]
    public class LaySlideCarousel : Control, IDisposable
    {
        // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        ~LaySlideCarousel() => Dispose(disposing: false);
        public LaySlideCarousel()
        {
            IsVisibleChanged += Carousel_IsVisibleChanged;
        }
        private readonly List<object> _entryDic = new List<object>();
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
        private bool disposedValue;

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

        private static void OnAutoSwitchChange(DependencyObject d, DependencyPropertyChangedEventArgs e) => ((LaySlideCarousel)d).ImageSwitch();
        // <summary>
        /// 切换开关
        /// </summary>
        private void ImageSwitch()
        {
            if (!IsLoaded) return;
            if (timer == null)
            {
                timer = new DispatcherTimer();
                timer.Tick -= Timer_Tick;
                timer.Tick += Timer_Tick;
                timer.Interval = Interval;
            }
            if (IsAutoSwitch) timer?.Start();
            else timer?.Stop();
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

        private static void OnIntervalChange(DependencyObject d, DependencyPropertyChangedEventArgs e) => ((LaySlideCarousel)d).SetInterval();


        /// <summary>
        /// 设置间隔时间
        /// </summary>
        private void SetInterval()
        {
            if (!IsLoaded) return;
            if (!IsAutoSwitch) return;
            if (timer == null)
            {
                timer = new DispatcherTimer();
                timer.Tick -= Timer_Tick;
                timer.Tick += Timer_Tick;
            }
            timer?.Stop();
            timer.Interval = Interval;
            timer?.Start();
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
            UpdateItems();
            InvalidateVisual();
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
            if (ui.ItemsSource is INotifyCollectionChanged notify)
            {
                notify.CollectionChanged -= ui.ItemsCollectionChanged;
                notify.CollectionChanged += ui.ItemsCollectionChanged;
            }
            IEnumerable oldValue = (IEnumerable)e.OldValue;
            IEnumerable newValue = (IEnumerable)e.NewValue;
            ui.OnItemsSourceChanged(oldValue, newValue);
        }
        protected virtual void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {

        }

        private void ItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnItemsChanged(e);
            InvalidateVisual();
        }
        protected virtual void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            UpdateItems();
        }
        /// <summary>
        /// 修改当前集合中的轮播图子项
        /// </summary>
        private void UpdateItems()
        {
            if (PART_ItemsPanel == null) return;
            PART_ItemsPanel.Children.Clear();
            _entryDic.Clear();
            if (ItemsSource == null) return;
            foreach (var item in ItemsSource)
            {
                _entryDic.Add(item);
            }
            foreach (var item in _entryDic)
            {
                PART_ItemsPanel.Children.Add(new ContentPresenter() { ContentTemplate = ItemTemplate, Content = item });
            }
            PART_ItemsPanel.Children.Add(new ContentPresenter() { ContentTemplate = ItemTemplate, Content = _entryDic.FirstOrDefault() });
            PART_ItemsPanel.Children.Insert(0, new ContentPresenter() { ContentTemplate = ItemTemplate, Content = _entryDic.LastOrDefault() });
        }
        /// <summary>
        /// 执行上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PART_RightButton_Click(object sender, RoutedEventArgs e)
        {
            if (ItemsSource is IList Items)
            {
                if (Items.Count < 0) SelectedIndex = 0;
                if (SelectedIndex >= (Items.Count - 1)) SelectedIndex = 0;
                else SelectedIndex++;
                timer?.Stop();
                if (IsAutoSwitch) timer?.Start();
            }
        }
        /// <summary>
        /// 执行下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PART_LeftButton_Click(object sender, RoutedEventArgs e)
        {
            if (ItemsSource is IList Items)
            {
                if (Items.Count < 0) SelectedIndex = 0;
                if (SelectedIndex <= 0) SelectedIndex = (Items.Count - 1);
                else SelectedIndex--;
                timer?.Stop();
                if (IsAutoSwitch) timer?.Start();
            }
        }
        public void Refresh()
        {

        }
        private void Carousel_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (timer == null) return;
            if (IsVisible)
            {
                timer.Tick += Timer_Tick;
                timer.Start();
            }
            else
            {
                timer.Stop();
                timer.Tick -= Timer_Tick;
            }
        }
        /// <summary>
        /// 计时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (ItemsSource is IList Items)
            {
                if (Items.Count < 0) SelectedIndex = 0;
                if (SelectedIndex >= (Items.Count - 1)) SelectedIndex = 0;
                else SelectedIndex++;
            }
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_LeftButton = GetTemplateChild("PART_LeftButton") as Button;
            PART_RightButton = GetTemplateChild("PART_RightButton") as Button;
            PART_ItemsPanel = GetTemplateChild("PART_ItemsPanel") as Panel;
            if (PART_LeftButton != null && PART_RightButton != null)
            {
                PART_LeftButton.Click -= PART_LeftButton_Click;
                PART_RightButton.Click -= PART_RightButton_Click;
                PART_LeftButton.Click += PART_LeftButton_Click;
                PART_RightButton.Click += PART_RightButton_Click; ;
            }
            UpdateItems();
            ImageSwitch();
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            try
            {
                if (PART_ItemsPanel != null)
                {
                    foreach (FrameworkElement item in PART_ItemsPanel.Children)
                    {
                        item.Width = ActualWidth;
                        item.Height = ActualHeight;
                    }
                }
            }
            catch
            {
            }
        }
        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)
                    IsVisibleChanged -= Carousel_IsVisibleChanged;
                    timer?.Stop();
                }
                // TODO: 释放未托管的资源(未托管的对象)并重写终结器
                // TODO: 将大型字段设置为 null
                disposedValue = true;
            }
        }


        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
