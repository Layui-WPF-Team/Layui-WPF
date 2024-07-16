using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media.Animation;

namespace LayUI.Wpf.Controls
{
    [ContentProperty("Content")]
    [DefaultProperty("Content")]
    [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
    public class LayDecimalText : Control, ILayControl
    { 
        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(LayDecimalText)); 
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(LayDecimalText));

        private decimal _value;
        public decimal Value
        {
            get { return (decimal)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(decimal), typeof(LayDecimalText), new PropertyMetadata(default(decimal), OnValueChanged));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as LayDecimalText).OnValueChanged((decimal)e.OldValue, (decimal)e.NewValue);
        }

        public decimal AnimationValue
        {
            get { return (decimal)GetValue(AnimationValueProperty); }
            internal set { SetValue(AnimationValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AnimationValue.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty AnimationValueProperty =
            DependencyProperty.Register("AnimationValue", typeof(decimal), typeof(LayDecimalText), new PropertyMetadata(default(decimal)));
        protected void OnValueChanged(decimal oldValue, decimal newValue)
        {
            OnUpdateValueAnimation();
            RaiseEvent(new RoutedPropertyChangedEventArgs<decimal>(oldValue, newValue, ValueChangedEvent));
        }
        public TimeSpan Time
        {
            get { return (TimeSpan)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Time.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("Time", typeof(TimeSpan), typeof(LayDecimalText), new PropertyMetadata(TimeSpan.FromSeconds(0.3)));

        /// <summary>
        /// 动画执行
        /// </summary>
        private void OnUpdateValueAnimation()
        {
            BeginAnimation(AnimationValueProperty, null);
            DecimalAnimation decimalAnimation = new DecimalAnimation();
            decimalAnimation.Duration = Time;
            decimalAnimation.From = _value;
            decimalAnimation.To = Value;
            BeginAnimation(AnimationValueProperty, decimalAnimation);
            _value = Value;
        }
        /// <summary>
        /// 值改变事件
        /// </summary>
        public event RoutedPropertyChangedEventHandler<decimal> ValueChanged
        {
            add => AddHandler(ValueChangedEvent, value);
            remove => RemoveHandler(ValueChangedEvent, value);
        }
        /// <summary>
        /// 值改变事件
        /// </summary>
        public static readonly RoutedEvent ValueChangedEvent =
            EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<decimal>), typeof(LayDecimalText));
    }
}
