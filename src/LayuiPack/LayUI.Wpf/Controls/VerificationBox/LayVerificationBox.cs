using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace LayUI.Wpf.Controls
{
    [TemplatePart(Name = nameof(PART_Gesture), Type = typeof(Slider))]
    public class LayVerificationBox : Control, ILayControl
    {
        Slider PART_Gesture;
        Track PART_Track;
        public object OFFContent
        {
            get { return (object)GetValue(OFFContentProperty); }
            set { SetValue(OFFContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OFFContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OFFContentProperty =
            DependencyProperty.Register("OFFContent", typeof(object), typeof(LayVerificationBox));

        public object ONContent
        {
            get { return (object)GetValue(ONContentProperty); }
            set { SetValue(ONContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ONContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ONContentProperty =
            DependencyProperty.Register("ONContent", typeof(object), typeof(LayVerificationBox));




        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(LayVerificationBox));




        public bool IsSuccess
        {
            get { return (bool)GetValue(IsSuccessProperty); }
            set { SetValue(IsSuccessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsSuccess.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSuccessProperty =
            DependencyProperty.Register("IsSuccess", typeof(bool), typeof(LayVerificationBox), new PropertyMetadata(false, OnIsSuccessChanged));

        private static void OnIsSuccessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var box = d as LayVerificationBox;
            if (!(bool)e.NewValue) box.Value = 0;
            else
            {
                box.Value = 100;
                box.RaiseEvent(new RoutedEventArgs(SucceedEvent, box));
            }
        }

        internal double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(LayVerificationBox), new PropertyMetadata(0.0, OnValueChanged));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as LayVerificationBox).OnValueChanged((double)e.NewValue);
        }
        private void OnValueChanged(double value) => IsSuccess = value >= 100;

        public event EventHandler Succeed
        {
            add => AddHandler(SucceedEvent, value);
            remove => RemoveHandler(SucceedEvent, value);
        }
        public static readonly RoutedEvent SucceedEvent =
            EventManager.RegisterRoutedEvent("Succeed", RoutingStrategy.Bubble, typeof(EventHandler), typeof(LayVerificationBox));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_Gesture = GetTemplateChild(nameof(PART_Gesture)) as Slider;
            if (PART_Gesture != null)
            {
                RoutedEventHandler handler = null;
                handler = (o, e) =>
                {
                    PART_Track = PART_Gesture.Template.FindName("PART_Track", PART_Gesture) as Track;
                    PART_Track.Thumb.PreviewMouseLeftButtonUp -= PART_Track_MouseLeftButtonUp;
                    PART_Track.Thumb.PreviewMouseLeftButtonUp += PART_Track_MouseLeftButtonUp;
                    PART_Gesture.Loaded -= handler;
                };
                PART_Gesture.Loaded += handler;
            }
        }

        private void PART_Track_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!IsSuccess) Value = 0;
            e.Handled = true;
        }
    }
}
