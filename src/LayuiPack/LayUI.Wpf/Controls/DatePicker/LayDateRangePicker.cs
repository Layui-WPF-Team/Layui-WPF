using LayUI.Wpf.Event;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// 日期区间选择器
    /// </summary>

    [TemplatePart(Name = nameof(PART_StartDate), Type = typeof(Calendar))]
    [TemplatePart(Name = nameof(PART_EndDate), Type = typeof(Calendar))]
    [TemplatePart(Name = nameof(PART_SubmitTimeBtn), Type = typeof(Button))]
    [TemplatePart(Name = nameof(PART_ResetTimeBtn), Type = typeof(Button))]
    [TemplatePart(Name = nameof(PART_Popup), Type = typeof(Popup))]
    public class LayDateRangePicker : Control, ILayControl
    {
        private Calendar PART_StartDate;
        private Calendar PART_EndDate;
        private Button PART_SubmitTimeBtn;
        private Button PART_ResetTimeBtn;
        private Popup PART_Popup;

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
            DependencyProperty.Register("HoverBorderBrush", typeof(Brush), typeof(LayDateRangePicker), new PropertyMetadata(Brushes.Transparent));


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
            DependencyProperty.Register("FocusedBorderBrush", typeof(Brush), typeof(LayDateRangePicker), new PropertyMetadata(Brushes.Transparent));

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
            DependencyProperty.Register("StartWatermark", typeof(string), typeof(LayDateRangePicker), new PropertyMetadata("开始日期"));
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
            DependencyProperty.Register("EndWatermark", typeof(string), typeof(LayDateRangePicker), new PropertyMetadata("结束日期"));

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
            DependencyProperty.Register("WatermarkColor", typeof(Brush), typeof(LayDateRangePicker));

        public bool IsDropDownOpen
        {
            get { return (bool)GetValue(IsDropDownOpenProperty); }
            set { SetValue(IsDropDownOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsDropDownOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDropDownOpenProperty =
            DependencyProperty.Register("IsDropDownOpen", typeof(bool), typeof(LayDateRangePicker));

        public IEnumerable<DateTime?> SelectedDates
        {
            get { return (IEnumerable<DateTime?>)GetValue(SelectedDatesProperty); }
            set { SetValue(SelectedDatesProperty, value); }
        }


        public DateTime? StartDate
        {
            get { return (DateTime?)GetValue(StartDateProperty); }
            private set { SetValue(StartDateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StartDate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartDateProperty =
            DependencyProperty.Register("StartDate", typeof(DateTime?), typeof(LayDateRangePicker));

        public DateTime? EndDate
        {
            get { return (DateTime?)GetValue(EndDateProperty); }
            private set { SetValue(EndDateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EndDate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EndDateProperty =
            DependencyProperty.Register("EndDate", typeof(DateTime?), typeof(LayDateRangePicker));



        // Using a DependencyProperty as the backing store for SelectedItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedDatesProperty =
            DependencyProperty.Register("SelectedDates", typeof(IEnumerable<DateTime?>), typeof(LayDateRangePicker), new PropertyMetadata(OnSelectedDatesChanged));

        private static void OnSelectedDatesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var layDateRangePicker = d as LayDateRangePicker;
            IEnumerable<DateTime?> OldDate = (IEnumerable<DateTime?>)e.OldValue;
            IEnumerable<DateTime?> NewDate = (IEnumerable<DateTime?>)e.NewValue;
            layDateRangePicker.OnSelectedDatesChanged(new LaySelectionDatesChangedEventArgs(SelectedDatesChangedEvent, OldDate, NewDate));
        }

        internal bool IsOpenSelectedDateAnimation
        {
            get { return (bool)GetValue(IsOpenSelectedDateAnimationProperty); }
            set { SetValue(IsOpenSelectedDateAnimationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsOpenSelectedDateAnimation.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty IsOpenSelectedDateAnimationProperty =
            DependencyProperty.Register("IsOpenSelectedDateAnimation", typeof(bool), typeof(LayDateRangePicker), new PropertyMetadata(false));


        [Bindable(true)]
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(LayDateRangePicker));

        /// <summary>
        /// 时间选中事件
        /// </summary>
        public event EventHandler<LaySelectionDatesChangedEventArgs> SelectedDatesChanged
        {
            add => AddHandler(SelectedDatesChangedEvent, value);
            remove => RemoveHandler(SelectedDatesChangedEvent, value);
        }
        public static readonly RoutedEvent SelectedDatesChangedEvent =
            EventManager.RegisterRoutedEvent("SelectedDatesChanged", RoutingStrategy.Bubble, typeof(EventHandler<LaySelectionDatesChangedEventArgs>), typeof(LayDateRangePicker));
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
            if (PART_StartDate != null && PART_EndDate != null && PART_SubmitTimeBtn != null && PART_ResetTimeBtn != null && PART_Popup != null)
            {
                PART_StartDate.PreviewMouseUp -= Calendar_PreviewMouseUp;
                PART_EndDate.PreviewMouseUp -= Calendar_PreviewMouseUp;
                PART_StartDate.PreviewMouseUp += Calendar_PreviewMouseUp;
                PART_EndDate.PreviewMouseUp += Calendar_PreviewMouseUp;
                PART_ResetTimeBtn.Click -= PART_ResetTimeBtn_Click;
                PART_SubmitTimeBtn.Click -= PART_SubmitTimeBtn_Click;
                PART_ResetTimeBtn.Click += PART_ResetTimeBtn_Click;
                PART_SubmitTimeBtn.Click += PART_SubmitTimeBtn_Click;
                PART_Popup.Closed -= PART_Popup_Closed;
                PART_Popup.Closed += PART_Popup_Closed; 
                PART_StartDate.SelectedDate = SelectedDates?.FirstOrDefault();
                PART_EndDate.SelectedDate = SelectedDates?.LastOrDefault();
                StartDate = SelectedDates?.FirstOrDefault();
                EndDate = SelectedDates?.LastOrDefault();
            } 
        }

        private void PART_Popup_Closed(object sender, EventArgs e)
        {
            if (SelectedDates == null)
            {
                StartDate = null;
                EndDate = null;
            }
            else
            {
                StartDate = SelectedDates.ToList()[0]==null?null: SelectedDates.ToList()[0];
                EndDate = SelectedDates.ToList()[1] == null ? null : SelectedDates.ToList()[1];
            }
            PART_StartDate.SelectedDate = StartDate;
            PART_EndDate.SelectedDate = EndDate;
        }

        private void PART_SubmitTimeBtn_Click(object sender, RoutedEventArgs e)
        {
            StartDate = PART_StartDate.SelectedDate;
            EndDate = PART_EndDate.SelectedDate;
            var times= new DateTime?[2] { StartDate, EndDate };
            SelectedDates = times;
            IsDropDownOpen = false;
        }

        private void PART_ResetTimeBtn_Click(object sender, RoutedEventArgs e)
        {
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
