using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace LayUI.Wpf.Controls
{
    /// <summary>
    /// 这是一个存粹的切换按钮
    /// </summary>
    public class LaySimpleToggleButton : ButtonBase
    {

        [Category("Behavior")]
        public event RoutedEventHandler Checked
        {
            add
            {
                AddHandler(CheckedEvent, value);
            }
            remove
            {
                RemoveHandler(CheckedEvent, value);
            }
        }

        public static readonly RoutedEvent CheckedEvent =
            EventManager.RegisterRoutedEvent("Checked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(LaySimpleToggleButton));

        [Category("Behavior")]
        public event RoutedEventHandler Unchecked
        {
            add
            {
                AddHandler(UncheckedEvent, value);
            }
            remove
            {
                RemoveHandler(UncheckedEvent, value);
            }
        }
        public static readonly RoutedEvent UncheckedEvent =
            EventManager.RegisterRoutedEvent("Unchecked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(LaySimpleToggleButton));

        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsChecked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register("IsChecked", typeof(bool), typeof(LaySimpleToggleButton), new PropertyMetadata(OnIsCheckedChanged));

        private static void OnIsCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LaySimpleToggleButton toggleButton = (LaySimpleToggleButton)d;
            bool flag = (bool)e.NewValue;
            if (flag == true) toggleButton.OnChecked(new RoutedEventArgs(CheckedEvent));
            else toggleButton.OnUnchecked(new RoutedEventArgs(UncheckedEvent));
        }
        protected virtual void OnChecked(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }

        protected virtual void OnUnchecked(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }
        protected override void OnClick()
        {
            OnToggle();
            base.OnClick();
        }

        protected virtual void OnToggle() => IsChecked = !IsChecked;
    }
}
