using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using LayUI.Wpf.Tools;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Markup;

namespace LayUI.Wpf.Controls
{
    /// <summary>
    ///  这是一个支持圆角切割
    /// </summary>
    [ContentProperty("Child")]
    public class LayBorder : System.Windows.Controls.Control
    { 
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            nameof(CornerRadius), typeof(CornerRadius), typeof(LayBorder), new PropertyMetadata(new CornerRadius(0)));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        [Bindable(true)]
        public UIElement Child
        {
            get { return (UIElement)GetValue(ChildProperty); }
            set { SetValue(ChildProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Child.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChildProperty =
            DependencyProperty.Register("Child", typeof(UIElement), typeof(LayBorder));
    }
}
