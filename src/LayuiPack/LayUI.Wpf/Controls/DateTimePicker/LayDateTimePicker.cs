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
    public class LayDateTimePicker : LayDatePicker
    {
        /// <summary>
        /// 水印
        /// </summary>
        [Bindable(true)]
        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Watermark.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.Register("Watermark", typeof(string), typeof(LayDateTimePicker));

        internal DateTime? DefaultTime
        {
            get { return (DateTime?)GetValue(DefaultTimeProperty); }
            set { SetValue(DefaultTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DefaultTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DefaultTimeProperty =
            DependencyProperty.Register("DefaultTime", typeof(DateTime?), typeof(LayDateTimePicker));

        [Bindable(true)]
        public string DateMessageTitle
        {
            get { return (string)GetValue(DateMessageTitleProperty); }
            set { SetValue(DateMessageTitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DateMessageTitle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DateMessageTitleProperty =
            DependencyProperty.Register("DateMessageTitle", typeof(string), typeof(LayDateTimePicker));

        /// <summary>
        /// 是否只读
        /// </summary>
        [Bindable(true)]
        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsReadOnly.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(LayDateTimePicker), new PropertyMetadata(true));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
    }
}
