using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace LayUI.Wpf.Controls
{

    [TemplatePart(Name = nameof(PART_Hours), Type = typeof(LayComboBox))]
    [TemplatePart(Name = nameof(PART_Minutes), Type = typeof(LayComboBox))]
    [TemplatePart(Name = nameof(PART_Seconds), Type = typeof(LayComboBox))]
    [TemplatePart(Name = nameof(PART_Seconds), Type = typeof(LayComboBox))]
    [TemplatePart(Name = nameof(PART_Cancel), Type = typeof(LayButton))]
    [TemplatePart(Name = nameof(PART_Submit), Type = typeof(LayButton))]
    [TemplatePart(Name = nameof(PART_Current), Type = typeof(LayButton))]
    [TemplatePart(Name = nameof(PART_Calendar), Type = typeof(System.Windows.Controls.Calendar))]
    [TemplatePart(Name = nameof(PART_Popup), Type = typeof(System.Windows.Controls.Primitives.Popup))]

    public class LayDateTimePicker : Control, ILayControl
    {
        private System.Windows.Controls.Primitives.Popup PART_Popup;
        /// <summary>
        /// 日历
        /// </summary>
        private System.Windows.Controls.Calendar PART_Calendar;
        /// <summary>
        /// 取消
        /// </summary>
        private LayButton PART_Cancel;
        /// <summary>
        /// 确定
        /// </summary>
        private LayButton PART_Submit;
        /// <summary>
        /// 当前
        /// </summary>
        private LayButton PART_Current;
        /// <summary>
        /// 时
        /// </summary>
        private LayComboBox PART_Hours;
        /// <summary>
        /// 分
        /// </summary>
        private LayComboBox PART_Minutes;
        /// <summary>
        /// 秒
        /// </summary>
        private LayComboBox PART_Seconds;
        /// <summary>
        /// 时间集合
        /// </summary>
        private List<int> Hours { get; set; }
        /// <summary>
        /// 分钟集合
        /// </summary>
        private List<int> Minutes { get; set; }
        /// <summary>
        /// 秒钟集合
        /// </summary>
        private List<int> Seconds { get; set; }

        public LayDateTimePicker()
        {
            Hours = CreateCount(24);
            Minutes = CreateCount(60);
            Seconds = CreateCount(60);
        }
        [Bindable(true)]
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(LayDateTimePicker));
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
            DependencyProperty.Register("WatermarkColor", typeof(Brush), typeof(LayDateTimePicker), new PropertyMetadata(Brushes.Transparent));
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
            DependencyProperty.Register("HoverBorderBrush", typeof(Brush), typeof(LayDateTimePicker), new PropertyMetadata(Brushes.Transparent));
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
            DependencyProperty.Register("FocusedBorderBrush", typeof(Brush), typeof(LayDateTimePicker), new PropertyMetadata(Brushes.Transparent));

        [Bindable(true)]
        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsReadOnly.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(LayDateTimePicker));

        [Bindable(true)]
        public Style CalendarStyle
        {
            get { return (Style)GetValue(CalendarStyleProperty); }
            set { SetValue(CalendarStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CalendarStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CalendarStyleProperty =
            DependencyProperty.Register("CalendarStyle", typeof(Style), typeof(LayDateTimePicker));
        //定义一个内部时间存储属性

        [Bindable(true)]
        public DateTime? CurrentTime
        {
            get { return (DateTime?)GetValue(CurrentTimeProperty); }
            internal set { SetValue(CurrentTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentTime.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty CurrentTimeProperty =
            DependencyProperty.Register("CurrentTime", typeof(DateTime?), typeof(LayDateTimePicker));
        [Bindable(true)]
        public bool IsTodayHighlighted
        {
            get { return (bool)GetValue(IsTodayHighlightedProperty); }
            set { SetValue(IsTodayHighlightedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsTodayHighlighted.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsTodayHighlightedProperty =
            DependencyProperty.Register("IsTodayHighlighted", typeof(bool), typeof(LayDateTimePicker), new PropertyMetadata(false));
        [Bindable(true)]
        public bool IsDropDownOpen
        {
            get { return (bool)GetValue(IsDropDownOpenProperty); }
            set { SetValue(IsDropDownOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsDropDownOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDropDownOpenProperty =
            DependencyProperty.Register("IsDropDownOpen", typeof(bool), typeof(LayDateTimePicker), new PropertyMetadata(IsDropDownOpenChanged));

        private static void IsDropDownOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as LayDateTimePicker).IsDropDownOpenChanged((bool)e.NewValue);
        }
        private void IsDropDownOpenChanged(bool isOpen)
        {
            if (PART_Popup == null) return;
            try
            {
                PART_Popup.IsOpen = isOpen;
            }
            catch
            {
            }
        }

        public DateTime? DisplayStartTime
        {
            get { return (DateTime?)GetValue(DisplayStartTimeProperty); }
            set { SetValue(DisplayStartTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisplayStartTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayStartTimeProperty =
            DependencyProperty.Register("DisplayStartTime", typeof(DateTime?), typeof(LayDateTimePicker));



        public DateTime? DisplayEndTime
        {
            get { return (DateTime?)GetValue(DisplayEndTimeProperty); }
            set { SetValue(DisplayEndTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisplayEndTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayEndTimeProperty =
            DependencyProperty.Register("DisplayEndTime", typeof(DateTime?), typeof(LayDateTimePicker));

        public DateTime? Time
        {
            get { return (DateTime?)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Time.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("Time", typeof(DateTime?), typeof(LayDateTimePicker), new PropertyMetadata(OnTimeChanged));

        private static void OnTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as LayDateTimePicker).OnTimeChanged(e.NewValue);
        }
        private void OnTimeChanged(object time)
        {
            if (!(Time is DateTime date)) return;
            Hour = date.Hour;
            Minute = date.Minute;
            Second = date.Second;
            if (PART_Calendar != null)
            {
                PART_Calendar.SelectedDate = date;
                PART_Calendar.DisplayDate = date;
            }


        }
        internal int Hour
        {
            get { return (int)GetValue(HourProperty); }
            set { SetValue(HourProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Hour.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty HourProperty =
            DependencyProperty.Register("Hour", typeof(int), typeof(LayDateTimePicker), new PropertyMetadata(OnHourChanged));

        private static void OnHourChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as LayDateTimePicker).OnHourChanged((int)e.NewValue);

        }
        private void OnHourChanged(int hour)
        {
            if (CurrentTime == null) CurrentTime = DateTime.Today;
            CurrentTime = new DateTime(CurrentTime.Value.Year, CurrentTime.Value.Month, CurrentTime.Value.Day, hour, CurrentTime.Value.Minute, CurrentTime.Value.Second);
        }
        internal int Minute
        {
            get { return (int)GetValue(MinuteProperty); }
            set { SetValue(MinuteProperty, value); }
        }
        // Using a DependencyProperty as the backing store for Minute.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty MinuteProperty =
            DependencyProperty.Register("Minute", typeof(int), typeof(LayDateTimePicker), new PropertyMetadata(OnMinuteChanged));
        private static void OnMinuteChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as LayDateTimePicker).OnMinuteChanged((int)e.NewValue);
        }
        private void OnMinuteChanged(int minute)
        {
            if (CurrentTime == null) CurrentTime = DateTime.Today;
            CurrentTime = new DateTime(CurrentTime.Value.Year, CurrentTime.Value.Month, CurrentTime.Value.Day, CurrentTime.Value.Hour, minute, CurrentTime.Value.Second);
        }
        internal int Second
        {
            get { return (int)GetValue(SecondProperty); }
            set { SetValue(SecondProperty, value); }
        }
        // Using a DependencyProperty as the backing store for Second.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty SecondProperty =
            DependencyProperty.Register("Second", typeof(int), typeof(LayDateTimePicker), new PropertyMetadata(OnSecondChanged));
        private static void OnSecondChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as LayDateTimePicker).OnSecondChanged((int)e.NewValue);
        }
        private void OnSecondChanged(int second)
        {
            if (CurrentTime == null) CurrentTime = DateTime.Today;
            CurrentTime = new DateTime(CurrentTime.Value.Year, CurrentTime.Value.Month, CurrentTime.Value.Day, CurrentTime.Value.Hour, CurrentTime.Value.Minute, second);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_Hours = GetTemplateChild(nameof(PART_Hours)) as LayComboBox;
            PART_Minutes = GetTemplateChild(nameof(PART_Minutes)) as LayComboBox;
            PART_Seconds = GetTemplateChild(nameof(PART_Seconds)) as LayComboBox;
            PART_Popup = GetTemplateChild(nameof(PART_Popup)) as System.Windows.Controls.Primitives.Popup;
            if (PART_Popup != null)
            {
                PART_Popup.Closed -= PART_Popup_Closed;
                PART_Popup.Closed += PART_Popup_Closed;
            }
            if (PART_Hours != null) PART_Hours.ItemsSource = Hours;
            if (PART_Minutes != null) PART_Minutes.ItemsSource = Minutes;
            if (PART_Seconds != null) PART_Seconds.ItemsSource = Seconds;
            if (Time != null)
            {
                Hour = Time.Value.Hour;
                Minute = Time.Value.Minute;
                Second = Time.Value.Second;
                CurrentTime = Time;
            }
            PART_Cancel = GetTemplateChild(nameof(PART_Cancel)) as LayButton;
            if (PART_Cancel != null)
            {
                PART_Cancel.Click -= PART_Cancel_Click;
                PART_Cancel.Click += PART_Cancel_Click;
            }
            PART_Submit = GetTemplateChild(nameof(PART_Submit)) as LayButton;
            if (PART_Submit != null)
            {
                PART_Submit.Click -= PART_Submit_Click;
                PART_Submit.Click += PART_Submit_Click;
            }
            PART_Calendar = GetTemplateChild(nameof(PART_Calendar)) as System.Windows.Controls.Calendar;
            if (PART_Calendar != null)
            {
                PART_Calendar.SelectedDatesChanged -= PART_Calendar_SelectedDatesChanged;
                PART_Calendar.SelectedDatesChanged += PART_Calendar_SelectedDatesChanged;
                PART_Calendar.SelectedDate = Time;
                if (Time != null) PART_Calendar.DisplayDate = Time.Value;
            }
            PART_Current = GetTemplateChild("PART_Current") as LayButton;
            if (PART_Current != null)
            {
                PART_Current.Click -= PART_Current_Click;
                PART_Current.Click += PART_Current_Click;
            }
            IsDropDownOpenChanged(IsDropDownOpen);
        }

        private void PART_Popup_Closed(object sender, EventArgs e)
        {
            IsDropDownOpen = IsMouseOver;
        }

        private void PART_Current_Click(object sender, RoutedEventArgs e)
        {
            Time = DateTime.Now;
            IsDropDownOpen = false;
        }

        private void PART_Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PART_Calendar.SelectedDate == null) return;
            var date = PART_Calendar.SelectedDate.Value;
            if (CurrentTime == null) CurrentTime = DateTime.Today;
            CurrentTime = new DateTime(date.Year, date.Month, date.Day, CurrentTime.Value.Hour, CurrentTime.Value.Minute, CurrentTime.Value.Second);
        }

        private void PART_Submit_Click(object sender, RoutedEventArgs e)
        {
            Time = CurrentTime;
            IsDropDownOpen = false;
        }

        private void PART_Cancel_Click(object sender, RoutedEventArgs e) => IsDropDownOpen = false;

        /// <summary>
        /// 根据数量创建一个从0开始的整数列表
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        private List<int> CreateCount(int count)
        {
            var list = new List<int>();
            for (int i = 0; i < count; i++)
            {
                list.Add(i);
            }
            return list;
        }
    }
}
