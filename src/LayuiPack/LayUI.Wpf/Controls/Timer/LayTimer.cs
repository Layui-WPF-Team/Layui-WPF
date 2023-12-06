using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace LayUI.Wpf.Controls
{
    /// <summary>
    ///  LayTimer
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-06-23 下午 1:54:59</para>
    /// </summary>

    [TemplatePart(Name = "PART_Hour", Type = typeof(ListBox))]
    [TemplatePart(Name = "PART_Minute", Type = typeof(ListBox))]
    [TemplatePart(Name = "PART_Second", Type = typeof(ListBox))]
    [TemplatePart(Name = "PART_ResetTimeBtn", Type = typeof(Button))]
    [TemplatePart(Name = "PART_QueryTimeBtn", Type = typeof(Button))]
    [TemplatePart(Name = "PART_SubmitTimeBtn", Type = typeof(Button))]
    public class LayTimer : Control, ILayControl
    {
        /// <summary>
        /// 确定按钮
        /// </summary>
        private Button PART_SubmitTimeBtn;
        /// <summary>
        /// 获取当前时间按钮
        /// </summary>
        private Button PART_QueryTimeBtn;
        /// <summary>
        /// 重置按钮
        /// </summary>
        private Button PART_ResetTimeBtn;
        /// <summary>
        /// 时
        /// </summary>
        private ListBox PART_Hour;
        /// <summary>
        /// 分
        /// </summary>
        private ListBox PART_Minute;
        /// <summary>
        /// 秒
        /// </summary>
        private ListBox PART_Second;

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime? Time
        {
            get { return (DateTime?)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("Time", typeof(DateTime?), typeof(LayTimer), new PropertyMetadata(OnTimeChanged));

        /// <summary>
        /// 点击事件
        /// </summary>
        public event EventHandler<RoutedEventArgs> Click
        {
            add => AddHandler(ClickEvent, value);
            remove => RemoveHandler(ClickEvent, value);
        }
        public static readonly RoutedEvent ClickEvent =
            EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(LayTimer));
        protected virtual void OnClick(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }
        /// <summary>
        /// 确定按钮点击事件
        /// </summary>
        public event EventHandler<RoutedEventArgs> Submited
        {
            add => AddHandler(SubmitedEvent, value);
            remove => RemoveHandler(SubmitedEvent, value);
        }
        public static readonly RoutedEvent SubmitedEvent =
            EventManager.RegisterRoutedEvent("Submited", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(LayTimer));
        protected virtual void OnSubmited(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }
        /// <summary>
        /// 时间选中事件
        /// </summary>
        public event EventHandler<RoutedEventArgs> SelectedTimesChanged
        {
            add => AddHandler(SelectedTimesChangedEvent, value);
            remove => RemoveHandler(SelectedTimesChangedEvent, value);
        }
        public static readonly RoutedEvent SelectedTimesChangedEvent =
            EventManager.RegisterRoutedEvent("SelectedTimesChanged", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(LayTimer));
        protected virtual void OnSelectedTimesChanged(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }
        /// <summary>
        /// 时间重置
        /// </summary>
        public event EventHandler<RoutedEventArgs> Reseted
        {
            add => AddHandler(ResetedEvent, value);
            remove => RemoveHandler(ResetedEvent, value);
        }
        public static readonly RoutedEvent ResetedEvent =
            EventManager.RegisterRoutedEvent("Reseted", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(LayTimer));
        protected virtual void OnReseted(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }
        /// <summary>
        /// 获取点击现在按钮事件
        /// </summary>
        public event EventHandler<RoutedEventArgs> Currented
        {
            add => AddHandler(CurrentedEvent, value);
            remove => RemoveHandler(CurrentedEvent, value);
        }
        public static readonly RoutedEvent CurrentedEvent =
            EventManager.RegisterRoutedEvent("Currented", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(LayTimer));
        protected virtual void OnCurrented(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }
        internal DateTime? DefaultTime
        {
            get { return (DateTime?)GetValue(DefaultTimeProperty); }
            set { SetValue(DefaultTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DefaultTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DefaultTimeProperty =
            DependencyProperty.Register("DefaultTime", typeof(DateTime?), typeof(LayTimer));
        private static void OnTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view = d as LayTimer;
            view.RefreshView();
        }
        /// <summary>
        /// 刷新视图
        /// </summary>
        internal void RefreshView()
        {
            UpdateIsSelectedItem(PART_Hour, Time?.Hour.ToString());
            UpdateIsSelectedItem(PART_Minute, Time?.Minute.ToString());
            UpdateIsSelectedItem(PART_Second, Time?.Second.ToString());
            DefaultTime = Time;
        }

        public bool IsShowSelectedTime
        {
            get { return (bool)GetValue(IsShowSelectedTimeProperty); }
            set { SetValue(IsShowSelectedTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsShowSelectedTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsShowSelectedTimeProperty =
            DependencyProperty.Register("IsShowSelectedTime", typeof(bool), typeof(LayTimer), new PropertyMetadata(true));



        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(LayTimer));


        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(LayTimer));

        public double Line
        {
            get { return (double)GetValue(LineProperty); }
            set { SetValue(LineProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Line.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineProperty =
            DependencyProperty.Register("Line", typeof(double), typeof(LayTimer));

        /// <summary>
        /// 刷新数据源
        /// </summary>
        private void SetDate()
        {
            PART_Hour.Items.Clear();
            PART_Minute.Items.Clear();
            PART_Second.Items.Clear();
            for (int i = 0; i <= 23; i++)
            {
                ListBoxItem item = new ListBoxItem();
                item.Style = PART_Hour.ItemContainerStyle;
                if (i < 10) item.Content = $"0{i}";
                else item.Content = $"{i}";
                PART_Hour.Items.Add(item);
            }
            for (int i = 0; i <= 59; i++)
            {
                ListBoxItem item = new ListBoxItem();
                item.Style = PART_Minute.ItemContainerStyle;
                if (i < 10) item.Content = $"0{i}";
                else item.Content = $"{i}";
                PART_Minute.Items.Add(item);
            }
            for (int i = 0; i <= 59; i++)
            {
                ListBoxItem item = new ListBoxItem();
                item.Style = PART_Second.ItemContainerStyle;
                if (i < 10) item.Content = $"0{i}";
                else item.Content = $"{i}";
                PART_Second.Items.Add(item);
            }
            PART_Hour.SelectedIndex = 0;
            PART_Minute.SelectedIndex = 0;
            PART_Second.SelectedIndex = 0;
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        private void UpdateDate()
        {
            var Date = DateTime.Now;
            var Year = Date.Year;
            var Month = Date.Month;
            var Day = Date.Day;
            var Hour = GetTime(PART_Hour.SelectedItem as ListBoxItem);
            var Minute = GetTime(PART_Minute.SelectedItem as ListBoxItem);
            var Second = GetTime(PART_Second.SelectedItem as ListBoxItem);
            var time = $"{Year}/{Month}/{Day} {Hour}:{Minute}:{Second}";
            DefaultTime = DateTime.Parse(time);
        }
        string GetTime(ListBoxItem item)
        {
            return item.Content.ToString();
        }
        /// <summary>
        /// 刷新选中项
        /// </summary>
        private void UpdateIsSelectedItem(ListBox list, string value)
        {
            if (list == null) return;
            foreach (ListBoxItem item in list.Items)
            {
                if (Convert.ToInt32(item.Content.ToString()) == Convert.ToInt32(value))
                {
                    item.IsSelected = true;
                    list.ScrollIntoView(item);
                }
                else
                {
                    item.IsSelected = false;
                }
            }
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_Hour = GetTemplateChild("PART_Hour") as ListBox;
            PART_Minute = GetTemplateChild("PART_Minute") as ListBox;
            PART_Second = GetTemplateChild("PART_Second") as ListBox;
            PART_SubmitTimeBtn = GetTemplateChild("PART_SubmitTimeBtn") as Button;
            PART_QueryTimeBtn = GetTemplateChild("PART_QueryTimeBtn") as Button;
            PART_ResetTimeBtn = GetTemplateChild("PART_ResetTimeBtn") as Button;
            if (PART_Hour != null && PART_Minute != null && PART_Second != null && PART_SubmitTimeBtn != null && PART_QueryTimeBtn != null && PART_ResetTimeBtn != null)
            {
                SetDate();
                this.PART_Hour.AddHandler(ListBoxItem.MouseLeftButtonDownEvent, new RoutedEventHandler(Button_Click), true);
                this.PART_Minute.AddHandler(ListBoxItem.MouseLeftButtonDownEvent, new RoutedEventHandler(Button_Click), true);
                this.PART_Second.AddHandler(ListBoxItem.MouseLeftButtonDownEvent, new RoutedEventHandler(Button_Click), true);
                UpdateDate();
                PART_ResetTimeBtn.Click -= PART_ResetTimeBtn_Click;
                PART_QueryTimeBtn.Click -= PART_QueryTimeBtn_Click;
                PART_SubmitTimeBtn.Click -= PART_SubmitTimeBtn_Click;
                PART_ResetTimeBtn.Click += PART_ResetTimeBtn_Click;
                PART_QueryTimeBtn.Click += PART_QueryTimeBtn_Click;
                PART_SubmitTimeBtn.Click += PART_SubmitTimeBtn_Click;
            }
        }
        /// <summary>
        /// 确定时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PART_SubmitTimeBtn_Click(object sender, RoutedEventArgs e)
        {
            Time = DefaultTime;
            OnClick(new RoutedEventArgs(ClickEvent, sender));
            OnSubmited(new RoutedEventArgs(SubmitedEvent, sender));
        }
        /// <summary>
        /// 获取现在时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PART_QueryTimeBtn_Click(object sender, RoutedEventArgs e)
        {
            DateTime localTime = DateTime.Now;
            DefaultTime = localTime;
            Time = DefaultTime;
            OnClick(new RoutedEventArgs(ClickEvent, sender));
            OnCurrented(new RoutedEventArgs(CurrentedEvent, sender));
        }
        /// <summary>
        /// 重置时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PART_ResetTimeBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateIsSelectedItem(PART_Hour, (PART_Hour.Items[0] as ListBoxItem).Content.ToString());
            UpdateIsSelectedItem(PART_Minute, (PART_Minute.Items[0] as ListBoxItem).Content.ToString());
            UpdateIsSelectedItem(PART_Second, (PART_Second.Items[0] as ListBoxItem).Content.ToString());
            UpdateDate();
            Time = DefaultTime;
            OnClick(new RoutedEventArgs(ClickEvent, sender));
            OnReseted(new RoutedEventArgs(ResetedEvent, sender));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateDate();
            OnSelectedTimesChanged(new RoutedEventArgs(SelectedTimesChangedEvent, sender));
        }
    }
}
