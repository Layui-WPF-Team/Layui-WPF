using LayUI.Wpf.Enum.Badge;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LayUI.Wpf.Controls
{
    /// <summary>
    ///  LayBadge
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-08-05 上午 10:00:54</para>
    /// </summary>
    public class LayBadge : ContentControl
    {

        /// <summary>
        /// 是否是点
        /// </summary>
        [Bindable(true)]
        public bool IsDot
        {
            get { return (bool)GetValue(IsDotProperty); }
            set { SetValue(IsDotProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsDot.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDotProperty =
            DependencyProperty.Register("IsDot", typeof(bool), typeof(LayBadge), new PropertyMetadata(false));

        [Bindable(true)]
        internal string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            private set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(LayBadge));

        /// <summary>
        /// 标记文字颜色
        /// </summary>
        [Bindable(true)]
        public Brush BadgeForeground
        {
            get { return (Brush)GetValue(BadgeForegroundProperty); }
            set { SetValue(BadgeForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BadgeForegroundProperty =
            DependencyProperty.Register("BadgeForeground", typeof(Brush), typeof(LayBadge));

        /// <summary>
        /// 值
        /// </summary>
        [Bindable(true)]
        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(string), typeof(LayBadge), new PropertyMetadata(OnValueChanged));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LayBadge badge)
            {
                try
                {
                    var value = Convert.ToInt32(badge.Value);
                    if (value <= badge.MaxValue&& value>0)
                        badge.Header = badge.Value;
                    else if (value <= 0)
                        badge.Header="";
                    else
                        badge.Header = badge.MaxValue + "+";
                }
                catch
                {
                    badge.Header = badge.Value;
                }
            }
        }
        /// <summary>
        /// 最大值
        /// </summary>
        [Bindable(true)]
        public int MaxValue
        {
            get { return (int)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(int), typeof(LayBadge), new PropertyMetadata(100, OnValueChanged));


        /// <summary>
        /// 显示类型
        /// </summary>
        [Bindable(true)]
        public BadgeStyle Type
        {
            get { return (BadgeStyle)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Type.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(BadgeStyle), typeof(LayBadge), new PropertyMetadata(BadgeStyle.Default));


    }
}
