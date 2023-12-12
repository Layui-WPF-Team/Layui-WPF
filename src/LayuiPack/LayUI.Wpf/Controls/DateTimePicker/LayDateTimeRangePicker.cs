using LayUI.Wpf.Event;
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
    /// <summary>
    /// 时间区间选择器
    /// </summary>
    [TemplatePart(Name = nameof(PART_StartDate), Type = typeof(Calendar))]
    [TemplatePart(Name = nameof(PART_EndDate), Type = typeof(Calendar))]
    [TemplatePart(Name = nameof(PART_SubmitTimeBtn), Type = typeof(Button))]
    [TemplatePart(Name = nameof(PART_ResetTimeBtn), Type = typeof(Button))]
    [TemplatePart(Name = nameof(PART_Popup), Type = typeof(Popup))]
    [TemplatePart(Name = nameof(PART_StartTimeByHour), Type = typeof(ListBox))]
    [TemplatePart(Name = nameof(PART_StartTimeByMinute), Type = typeof(ListBox))]
    [TemplatePart(Name = nameof(PART_StartTimeBySecond), Type = typeof(ListBox))]
    [TemplatePart(Name = nameof(PART_EndTimeByHour), Type = typeof(ListBox))]
    [TemplatePart(Name = nameof(PART_EndTimeByMinute), Type = typeof(ListBox))]
    [TemplatePart(Name = nameof(PART_EndTimeBySecond), Type = typeof(ListBox))]
    public class LayDateTimeRangePicker : Control, ILayControl
    {
        private Calendar PART_StartDate;
        private Calendar PART_EndDate;
        private Button PART_SubmitTimeBtn;
        private Button PART_ResetTimeBtn;
        private Popup PART_Popup;
        private ListBox PART_StartTimeByHour;
        private ListBox PART_StartTimeByMinute;
        private ListBox PART_StartTimeBySecond;
        private ListBox PART_EndTimeByHour;
        private ListBox PART_EndTimeByMinute;
        private ListBox PART_EndTimeBySecond;
        /// <summary>
        /// 分钟和小时数据
        /// </summary>
        private List<string> TimesByHour { get; set; } = CreateTime(24);
        /// <summary>
        /// 秒钟数据
        /// </summary>
        private List<string> TimesByMinuteAndSecond { get; set; } = CreateTime(60);
        /// <summary>
        /// 创建时间集合
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private static List<string> CreateTime(int number)
        {
            var times = new List<string>();
            for (int i = 0; i < number; i++)
            {
                var num = i < 10 ? "0" + i : i.ToString();
                times.Add($"{num}");
            }
            return times;
        }

        internal string EndTimeBySecond
        {
            get { return (string)GetValue(EndTimeBySecondProperty); }
            set { SetValue(EndTimeBySecondProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EndTimeBySecond.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty EndTimeBySecondProperty =
            DependencyProperty.Register("EndTimeBySecond", typeof(string), typeof(LayDateTimeRangePicker));

        internal string EndTimeByMinute
        {
            get { return (string)GetValue(EndTimeByMinuteProperty); }
            set { SetValue(EndTimeByMinuteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EndTimeByMinute.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty EndTimeByMinuteProperty =
            DependencyProperty.Register("EndTimeByMinute", typeof(string), typeof(LayDateTimeRangePicker));

        internal string EndTimeByHour
        {
            get { return (string)GetValue(EndTimeByHourProperty); }
            set { SetValue(EndTimeByHourProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EndTimeByHour.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty EndTimeByHourProperty =
            DependencyProperty.Register("EndTimeByHour", typeof(string), typeof(LayDateTimeRangePicker));

        internal string StartTimeBySecond
        {
            get { return (string)GetValue(StartTimeBySecondProperty); }
            set { SetValue(StartTimeBySecondProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StartTimeBySecond.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty StartTimeBySecondProperty =
            DependencyProperty.Register("StartTimeBySecond", typeof(string), typeof(LayDateTimeRangePicker));


        internal string StartTimeByMinute
        {
            get { return (string)GetValue(StartTimeByMinuteProperty); }
            set { SetValue(StartTimeByMinuteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StartTimeByMinute.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty StartTimeByMinuteProperty =
            DependencyProperty.Register("StartTimeByMinute", typeof(string), typeof(LayDateTimeRangePicker));

        internal string StartTimeByHour
        {
            get { return (string)GetValue(StartTimeByHourProperty); }
            set { SetValue(StartTimeByHourProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StartTimeByHour.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty StartTimeByHourProperty =
            DependencyProperty.Register("StartTimeByHour", typeof(string), typeof(LayDateTimeRangePicker));

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
            DependencyProperty.Register("HoverBorderBrush", typeof(Brush), typeof(LayDateTimeRangePicker), new PropertyMetadata(Brushes.Transparent));


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
            DependencyProperty.Register("FocusedBorderBrush", typeof(Brush), typeof(LayDateTimeRangePicker), new PropertyMetadata(Brushes.Transparent));

        /// <summary>
        /// 起始水印
        /// </summary>
        public string StartWatermark
        {
            get { return (string)GetValue(StartWatermarkProperty); }
            set { SetValue(StartWatermarkProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StartWatermark.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartWatermarkProperty =
            DependencyProperty.Register("StartWatermark", typeof(string), typeof(LayDateTimeRangePicker), new PropertyMetadata("开始日期"));
        /// <summary>
        /// 结束水印
        /// </summary>
        public string EndWatermark
        {
            get { return (string)GetValue(EndWatermarkProperty); }
            set { SetValue(EndWatermarkProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EndWatermark.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EndWatermarkProperty =
            DependencyProperty.Register("EndWatermark", typeof(string), typeof(LayDateTimeRangePicker), new PropertyMetadata("结束日期"));

        /// <summary>
        /// 水印文字颜色
        /// </summary>
        public Brush WatermarkColor
        {
            get { return (Brush)GetValue(WatermarkColorProperty); }
            set { SetValue(WatermarkColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WatermarkColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WatermarkColorProperty =
            DependencyProperty.Register("WatermarkColor", typeof(Brush), typeof(LayDateTimeRangePicker));

        public bool IsDropDownOpen
        {
            get { return (bool)GetValue(IsDropDownOpenProperty); }
            set { SetValue(IsDropDownOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsDropDownOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDropDownOpenProperty =
            DependencyProperty.Register("IsDropDownOpen", typeof(bool), typeof(LayDateTimeRangePicker));



        public DateTime? StartDate
        {
            get { return (DateTime?)GetValue(StartDateProperty); }
            private set { SetValue(StartDateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StartDate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartDateProperty =
            DependencyProperty.Register("StartDate", typeof(DateTime?), typeof(LayDateTimeRangePicker));

        public DateTime? EndDate
        {
            get { return (DateTime?)GetValue(EndDateProperty); }
            private set { SetValue(EndDateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EndDate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EndDateProperty =
            DependencyProperty.Register("EndDate", typeof(DateTime?), typeof(LayDateTimeRangePicker));

        public IEnumerable<DateTime?> SelectedDates
        {
            get { return (IEnumerable<DateTime?>)GetValue(SelectedDatesProperty); }
            set { SetValue(SelectedDatesProperty, value); }
        }


        // Using a DependencyProperty as the backing store for SelectedDates.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedDatesProperty =
            DependencyProperty.Register("SelectedDates", typeof(IEnumerable<DateTime?>), typeof(LayDateTimeRangePicker), new PropertyMetadata(OnSelectedDatesChanged));

        private static void OnSelectedDatesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var LayDateTimeRangePicker = d as LayDateTimeRangePicker;
            IEnumerable<DateTime?> OldDate = (IEnumerable<DateTime?>)e.OldValue;
            IEnumerable<DateTime?> NewDate = (IEnumerable<DateTime?>)e.NewValue;

            LayDateTimeRangePicker.OnSelectedDatesChanged(new LaySelectionDatesChangedEventArgs(SelectedDatesChangedEvent, OldDate, NewDate));
        }

        internal bool IsOpenSelectedDateAnimation
        {
            get { return (bool)GetValue(IsOpenSelectedDateAnimationProperty); }
            set { SetValue(IsOpenSelectedDateAnimationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsOpenSelectedDateAnimation.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty IsOpenSelectedDateAnimationProperty =
            DependencyProperty.Register("IsOpenSelectedDateAnimation", typeof(bool), typeof(LayDateTimeRangePicker), new PropertyMetadata(false));


        [Bindable(true)]
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(LayDateTimeRangePicker));

        /// <summary>
        /// 时间选中事件
        /// </summary>
        public event EventHandler<LaySelectionDatesChangedEventArgs> SelectedDatesChanged
        {
            add => AddHandler(SelectedDatesChangedEvent, value);
            remove => RemoveHandler(SelectedDatesChangedEvent, value);
        }
        public static readonly RoutedEvent SelectedDatesChangedEvent =
            EventManager.RegisterRoutedEvent("SelectedDatesChanged", RoutingStrategy.Bubble, typeof(EventHandler<LaySelectionDatesChangedEventArgs>), typeof(LayDateTimeRangePicker));
        protected virtual void OnSelectedDatesChanged(LaySelectionDatesChangedEventArgs e)
        {
            if (PART_StartDate != null && PART_EndDate != null)
            {
                PART_StartDate.SelectedDate = SelectedDates?.FirstOrDefault();
                PART_EndDate.SelectedDate = SelectedDates?.LastOrDefault();
                StartDate = SelectedDates?.FirstOrDefault();
                EndDate = SelectedDates?.LastOrDefault();
            }
            RaiseEvent(e);
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_StartDate = GetTemplateChild(nameof(PART_StartDate)) as Calendar;
            PART_EndDate = GetTemplateChild(nameof(PART_EndDate)) as Calendar;
            PART_SubmitTimeBtn = GetTemplateChild(nameof(PART_SubmitTimeBtn)) as Button;
            PART_ResetTimeBtn = GetTemplateChild(nameof(PART_ResetTimeBtn)) as Button;
            PART_Popup = GetTemplateChild(nameof(PART_Popup)) as Popup;
            PART_StartTimeByHour = GetTemplateChild(nameof(PART_StartTimeByHour)) as ListBox;
            PART_StartTimeByMinute = GetTemplateChild(nameof(PART_StartTimeByMinute)) as ListBox;
            PART_StartTimeBySecond = GetTemplateChild(nameof(PART_StartTimeBySecond)) as ListBox;
            PART_EndTimeByHour = GetTemplateChild(nameof(PART_EndTimeByHour)) as ListBox;
            PART_EndTimeByMinute = GetTemplateChild(nameof(PART_EndTimeByMinute)) as ListBox;
            PART_EndTimeBySecond = GetTemplateChild(nameof(PART_EndTimeBySecond)) as ListBox;
            if (PART_StartDate != null &&
                PART_EndDate != null &&
                PART_SubmitTimeBtn != null &&
                PART_ResetTimeBtn != null &&
                PART_Popup != null &&
                PART_StartTimeByHour != null &&
                PART_StartTimeByMinute != null &&
                PART_StartTimeBySecond != null &&
                PART_EndTimeByHour != null &&
                PART_EndTimeByMinute != null &&
                PART_EndTimeBySecond != null)
            {
                PART_StartDate.PreviewMouseUp -= Calendar_PreviewMouseUp;
                PART_EndDate.PreviewMouseUp -= Calendar_PreviewMouseUp;
                PART_StartDate.PreviewMouseUp += Calendar_PreviewMouseUp;
                PART_EndDate.PreviewMouseUp += Calendar_PreviewMouseUp;
                PART_ResetTimeBtn.Click -= PART_ResetTimeBtn_Click;
                PART_SubmitTimeBtn.Click -= PART_SubmitTimeBtn_Click;
                PART_ResetTimeBtn.Click += PART_ResetTimeBtn_Click;
                PART_SubmitTimeBtn.Click += PART_SubmitTimeBtn_Click;
                PART_Popup.Opened -= PART_Popup_Opened;
                PART_Popup.Opened += PART_Popup_Opened;
                PART_Popup.Closed -= PART_Popup_Closed;
                PART_Popup.Closed += PART_Popup_Closed;
                PART_StartTimeByHour.SelectionChanged += DateTimeSelectChanged;
                PART_StartTimeByMinute.SelectionChanged += DateTimeSelectChanged;
                PART_StartTimeBySecond.SelectionChanged += DateTimeSelectChanged;
                PART_EndTimeByHour.SelectionChanged += DateTimeSelectChanged;
                PART_EndTimeByMinute.SelectionChanged += DateTimeSelectChanged;
                PART_EndTimeBySecond.SelectionChanged += DateTimeSelectChanged;
                PART_StartTimeByHour.ItemsSource = TimesByHour;
                PART_StartTimeByMinute.ItemsSource = TimesByMinuteAndSecond;
                PART_StartTimeBySecond.ItemsSource = TimesByMinuteAndSecond;
                PART_EndTimeByHour.ItemsSource = TimesByHour;
                PART_EndTimeByMinute.ItemsSource = TimesByMinuteAndSecond;
                PART_EndTimeBySecond.ItemsSource = TimesByMinuteAndSecond;
                PART_StartDate.SelectedDate = SelectedDates?.FirstOrDefault();
                PART_EndDate.SelectedDate = SelectedDates?.LastOrDefault();
                StartDate = SelectedDates?.FirstOrDefault();
                EndDate = SelectedDates?.LastOrDefault();
            }
        }

        private void PART_Popup_Opened(object sender, EventArgs e)
        {
            if (StartDate != null)
            {
                var Hour = ((DateTime)StartDate).Hour;
                StartTimeByHour = Hour < 10 ? "0" + Hour : Hour.ToString();
                var Minute = ((DateTime)StartDate).Minute;
                StartTimeByMinute = Minute < 10 ? "0" + Minute : Minute.ToString();
                var Second = ((DateTime)StartDate).Second;
                StartTimeBySecond = Second < 10 ? "0" + Second : Second.ToString();
            }
            else
            {
                StartTimeByHour = null;
                StartTimeByMinute = null;
                StartTimeBySecond = null;
            }
            if (EndDate != null)
            {
                var Hour = ((DateTime)EndDate).Hour;
                EndTimeByHour = Hour < 10 ? "0" + Hour : Hour.ToString();
                var Minute = ((DateTime)EndDate).Minute;
                EndTimeByMinute = Minute < 10 ? "0" + Minute : Minute.ToString();
                var Second = ((DateTime)EndDate).Second;
                EndTimeBySecond = Second < 10 ? "0" + Second : Minute.ToString();
            }
            else
            {
                EndTimeByHour = null;
                EndTimeByMinute = null;
                EndTimeBySecond = null;
            }
        }

        private async void DateTimeSelectChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(sender is ListBox)) return;
            ListBox listBox = (ListBox)sender;
            if (listBox == null) return;
            listBox.ScrollIntoView(listBox.SelectedItem);
            IsOpenSelectedDateAnimation = true;
            await Task.Delay(300);
            IsOpenSelectedDateAnimation = false;
        }

        private void PART_Popup_Closed(object sender, EventArgs e)
        {
            try
            {
                if (SelectedDates == null)
                {
                    StartTimeByHour = null;
                    StartTimeByMinute = null;
                    StartTimeBySecond = null;
                    EndTimeByHour = null;
                    EndTimeByMinute = null;
                    EndTimeBySecond = null;
                    StartDate = null;
                    EndDate = null;
                }
                else
                {
                    StartDate = SelectedDates.ToList()[0] == null ? null : SelectedDates.ToList()[0];
                    EndDate = SelectedDates.ToList()[1] == null ? null : SelectedDates.ToList()[1];
                    if (StartDate != null)
                    {
                        var Hour = ((DateTime)StartDate).Hour;
                        StartTimeByHour = Hour < 10 ? "0" + Hour : Hour.ToString();
                        var Minute = ((DateTime)StartDate).Minute;
                        StartTimeByMinute = Minute < 10 ? "0" + Minute : Minute.ToString();
                        var Second = ((DateTime)StartDate).Second;
                        StartTimeBySecond = Second < 10 ? "0" + Second : Second.ToString();
                    }
                    else
                    {
                        StartTimeByHour = null;
                        StartTimeByMinute = null;
                        StartTimeBySecond = null;
                    }
                    if (EndDate != null)
                    {
                        var Hour = ((DateTime)EndDate).Hour;
                        EndTimeByHour = Hour < 10 ? "0" + Hour : Hour.ToString();
                        var Minute = ((DateTime)EndDate).Minute;
                        EndTimeByMinute = Minute < 10 ? "0" + Minute : Minute.ToString();
                        var Second = ((DateTime)EndDate).Second;
                        EndTimeBySecond = Second < 10 ? "0" + Second : Minute.ToString();
                    }
                    else
                    {
                        EndTimeByHour = null;
                        EndTimeByMinute = null;
                        EndTimeBySecond = null;
                    }
                }
                PART_StartDate.SelectedDate = StartDate;
                PART_EndDate.SelectedDate = EndDate;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void PART_SubmitTimeBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PART_StartDate.SelectedDate != null)
                {
                    DateTime time = (DateTime)PART_StartDate.SelectedDate;
                    var Hour = string.IsNullOrEmpty(StartTimeByHour) ? "00" : StartTimeByHour;
                    var Minute = string.IsNullOrEmpty(StartTimeByMinute) ? "00" : StartTimeByMinute;
                    var Second = string.IsNullOrEmpty(StartTimeBySecond) ? "00" : StartTimeBySecond;
                    var Year = time.Year < 10 ? "000" : time.Year.ToString();
                    var Month = time.Month < 10 ? "0" + time.Month : time.Month.ToString();
                    var Day = time.Day < 10 ? "0" + time.Day : time.Day.ToString();
                    var dateTime = $"{Year}/{Month}/{Day} {Hour}:{Minute}:{Second}";
                    StartDate = Convert.ToDateTime(dateTime);
                }
                else StartDate = null;
                if (PART_EndDate.SelectedDate != null)
                {
                    DateTime time = (DateTime)PART_EndDate.SelectedDate;
                    var Hour = string.IsNullOrEmpty(EndTimeByHour) ? "00" : EndTimeByHour;
                    var Minute = string.IsNullOrEmpty(EndTimeByMinute) ? "00" : EndTimeByMinute;
                    var Second = string.IsNullOrEmpty(EndTimeBySecond) ? "00" : EndTimeBySecond;
                    var Year = time.Year < 10 ? "000" : time.Year.ToString();
                    var Month = time.Month < 10 ? "0" + time.Month : time.Month.ToString();
                    var Day = time.Day < 10 ? "0" + time.Day : time.Day.ToString();
                    var dateTime = $"{Year}/{Month}/{Day} {Hour}:{Minute}:{Second}";
                    EndDate = Convert.ToDateTime(dateTime);
                }
                else EndDate = null;
                var times = new DateTime?[2] { StartDate, EndDate };
                SelectedDates = times;
                IsDropDownOpen = false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void PART_ResetTimeBtn_Click(object sender, RoutedEventArgs e)
        {
            StartTimeByHour = null;
            StartTimeByMinute = null;
            StartTimeBySecond = null;
            EndTimeByHour = null;
            EndTimeByMinute = null;
            EndTimeBySecond = null;
            StartDate = null;
            EndDate = null;
            PART_StartDate.SelectedDate = StartDate;
            PART_EndDate.SelectedDate = EndDate;
            SelectedDates = null;
        }

        private async void Calendar_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            IsOpenSelectedDateAnimation = true;
            //释放日历中的按钮聚焦效果
            if (Mouse.Captured is CalendarItem) Mouse.Capture(null);
            await Task.Delay(300);
            IsOpenSelectedDateAnimation = false;
        }
    }
}
