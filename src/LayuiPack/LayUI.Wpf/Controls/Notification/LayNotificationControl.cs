using LayUI.Wpf.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace LayUI.Wpf.Controls
{
    /// <summary>
    /// 提示内容控件
    /// </summary>
    [TemplatePart(Name = nameof(PART_CloseButton), Type = typeof(Button))]
    [TemplatePart(Name = nameof(PART_SubmitButton), Type = typeof(Button))]
    public class LayNotificationControl : ContentControl, ILayControl
    {
        /// <summary>
        /// 关闭按钮
        /// </summary>
        private Button PART_CloseButton;
        /// <summary>
        /// 提交按钮
        /// </summary>
        private Button PART_SubmitButton;
        /// <summary>
        /// 关闭通知信息计时器
        /// </summary>

        private DispatcherTimer PART_CloseTimer; 
        internal void RemoveTime()
        {
            if (PART_CloseTimer != null)
            {
                PART_CloseTimer.Stop();
                PART_CloseTimer.Tick -= PART_CloseTimer_Tick;
                PART_CloseTimer = null;
            }
        }
        private void PART_CloseTimer_Tick(object sender, EventArgs e)
        { 
            if(PART_CloseTimer!=null)  Close?.Invoke(false);
            RemoveTime();
        }

        internal Action<bool> Close;
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(LayNotificationControl));


        /// <summary>
        /// 倒计时时间
        /// </summary>
        internal double Time
        {
            get { return (double)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Time.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("Time", typeof(double), typeof(LayNotificationControl));



        /// <summary>
        /// 
        /// </summary>
        internal bool ShowClose
        {
            get { return (bool)GetValue(ShowCloseProperty); }
            set { SetValue(ShowCloseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowClose.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowCloseProperty =
            DependencyProperty.Register("ShowClose", typeof(bool), typeof(LayNotificationControl), new PropertyMetadata(true));



        internal bool ShowSubmit
        {
            get { return (bool)GetValue(ShowSubmitProperty); }
            set { SetValue(ShowSubmitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowSubmit.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty ShowSubmitProperty =
            DependencyProperty.Register("ShowSubmit", typeof(bool), typeof(LayNotificationControl), new PropertyMetadata(false));




        [Bindable(true)]
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(LayNotificationControl));


        /// <summary>
        /// 信息类型
        /// </summary>
        public NotificationType Type
        {
            get { return (NotificationType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Type.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(NotificationType), typeof(LayNotificationControl));

        /// <summary>
        /// 提交按钮
        /// </summary>
        public object SubmitContent
        {
            get { return (object)GetValue(SubmitContentProperty); }
            set { SetValue(SubmitContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SubmitContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SubmitContentProperty =
            DependencyProperty.Register("SubmitContent", typeof(object), typeof(LayNotificationControl), new PropertyMetadata("确定"));



        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_CloseButton = GetTemplateChild(nameof(PART_CloseButton)) as Button;
            PART_SubmitButton = GetTemplateChild(nameof(PART_SubmitButton)) as Button;
            if (PART_CloseButton != null && PART_SubmitButton != null)
            {
                PART_CloseButton.Click -= PART_CloseButton_Click;
                PART_CloseButton.Click += PART_CloseButton_Click;
                PART_SubmitButton.Click -= PART_SubmitButton_Click;
                PART_SubmitButton.Click += PART_SubmitButton_Click;
                if (Time > 0)
                {
                    PART_CloseTimer = new DispatcherTimer();
                    PART_CloseTimer.Interval = TimeSpan.FromSeconds(Time);
                    PART_CloseTimer.Tick += PART_CloseTimer_Tick;
                    PART_CloseTimer.Start();
                }
            }
        }

        private void PART_SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                Close?.Invoke(true);
            }));
        }

        private void PART_CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                Close?.Invoke(false);
            }));
        }
    }
}
