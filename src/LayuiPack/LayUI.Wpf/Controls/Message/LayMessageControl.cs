using LayUI.Wpf.Controls;
using LayUI.Wpf.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace LayUI.Wpf.Controls
{
    /// <summary>
    ///  LayMessageControl
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-05-19 下午 8:02:09</para>
    /// </summary>
    public class LayMessageControl : ContentControl, ILayControl
    {
        [Bindable(true)]
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(LayMessageControl));

        internal LayMessageControl()
        {
            Loaded += LayMessageControl_Loaded;
        }

        private async void LayMessageControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.Parent is LayMessageHost host)
            {
                await Task.Delay(TimeSpan.FromSeconds(Time));
                host.Items.Remove(this);
            }
        }

        public double Time { get; set; }

        [Bindable(true)]
        public MessageType Type
        {
            get { return (MessageType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Type.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(MessageType), typeof(LayMessageControl), new PropertyMetadata(MessageType.Success));

    }
}
