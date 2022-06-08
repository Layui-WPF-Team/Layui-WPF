using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Threading;

namespace LayuiTemplate.Control
{
    /// <summary>
    ///  渐变轮播图
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-06-07 下午 4:12:39</para>
    /// </summary>
    public class LayGradientCarousel : System.Windows.Controls.ListBox
    {
        private DispatcherTimer timer;
        /// <summary>
        /// 上一页
        /// </summary>
        private Button PART_LeftButton;
        /// <summary>
        /// 下一页
        /// </summary>
        private Button PART_RightButton;
        public LayGradientCarousel()
        {
            Loaded -= LayCarousel_Loaded;
            Loaded += LayCarousel_Loaded;
        }

        private void LayCarousel_Loaded(object sender, RoutedEventArgs e)
        {
            ImageSwitch();
        }

        /// <summary>
        /// 是否自动切换
        /// </summary>
        public bool IsAutoSwitch
        {
            get { return (bool)GetValue(IsAutoSwitchProperty); }
            set { SetValue(IsAutoSwitchProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAutoSwitch.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAutoSwitchProperty =
            DependencyProperty.Register("IsAutoSwitch", typeof(bool), typeof(LayGradientCarousel), new PropertyMetadata(false, OnAutoSwitchChange));

        private static void OnAutoSwitchChange(DependencyObject d, DependencyPropertyChangedEventArgs e)=> ((LayGradientCarousel)d).ImageSwitch();
        /// <summary>
        /// 间隔时间
        /// </summary>
        public TimeSpan Interval
        {
            get { return (TimeSpan)GetValue(IntervalProperty); }
            set { SetValue(IntervalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Interval.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IntervalProperty =
            DependencyProperty.Register("Interval", typeof(TimeSpan), typeof(LayGradientCarousel), new PropertyMetadata(TimeSpan.FromSeconds(4), OnIntervalChange));

        private static void OnIntervalChange(DependencyObject d, DependencyPropertyChangedEventArgs e) => ((LayGradientCarousel)d).SetInterval();
        /// <summary>
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
        /// 设置间隔时间
        /// </summary>
        private void SetInterval()
        {
            if(!IsLoaded) return;
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
        /// <summary>
        /// 计时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            if(Items.Count<0) SelectedIndex = 0;
            if (SelectedIndex >= (Items.Count - 1)) SelectedIndex = 0;
            else SelectedIndex++;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_LeftButton = GetTemplateChild("PART_LeftButton") as Button;
            PART_RightButton = GetTemplateChild("PART_RightButton") as Button;
            if (PART_LeftButton != null && PART_RightButton != null)
            {
                PART_LeftButton.Click -= PART_LeftButton_Click;
                PART_RightButton.Click -= PART_RightButton_Click;
                PART_LeftButton.Click += PART_LeftButton_Click;
                PART_RightButton.Click += PART_RightButton_Click; ;
            }
        }
        /// <summary>
        /// 执行上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PART_RightButton_Click(object sender, RoutedEventArgs e)
        {
            if (Items.Count < 0) SelectedIndex = 0;
            if (SelectedIndex >= (Items.Count - 1)) SelectedIndex = 0;
            else SelectedIndex++;
            timer?.Stop();
            timer?.Start();
        }
        /// <summary>
        /// 执行下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PART_LeftButton_Click(object sender, RoutedEventArgs e)
        {
            if (Items.Count < 0) SelectedIndex = 0;
            if (SelectedIndex <= 0) SelectedIndex = (Items.Count - 1);
            else SelectedIndex--;
            timer?.Stop();
            timer?.Start();
        }
        /// <summary>
        /// 监听是否符合指定子项
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is LayCarouselItem;
        }
        /// <summary>
        /// 替换子项
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected override DependencyObject GetContainerForItemOverride()
        {
            var item = new LayCarouselItem();
            return item;
        }
    }
}
