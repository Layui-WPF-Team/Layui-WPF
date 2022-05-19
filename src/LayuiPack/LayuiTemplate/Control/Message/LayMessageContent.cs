using LayuiTemplate.Control;
using LayuiTemplate.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace LayuiTemplate.Control
{
    /// <summary>
    ///  LayMessageContent
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-05-19 下午 8:02:09</para>
    /// </summary>
    public class LayMessageContent : ContentControl
    {
        public LayMessageContent()
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = 0;
            doubleAnimation.To = 1;
            doubleAnimation.Duration = TimeSpan.FromSeconds(0.2);
            this.BeginAnimation(OpacityProperty, doubleAnimation);
            Loaded += LayMessageContent_Loaded;
        }

        private async void LayMessageContent_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.Parent is LayMessageHost host)
            {
                await Task.Delay(TimeSpan.FromSeconds(Time));
                host.Items.Remove(this);
            }
        }

        public double Time { get; set; }

        public MessageType Type
        {
            get { return (MessageType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Type.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(MessageType), typeof(LayMessageContent), new PropertyMetadata(MessageType.Success));

    }
}
