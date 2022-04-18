using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace LayuiTemplate.Control
{
    /// <summary>
    ///  LayExpander
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-04-18 下午 1:31:57</para>
    /// </summary>
    public class LayExpander: Expander
    {
        public LayExpander() { 

        }
        public bool IsAutoFold
        {
            get { return (bool)GetValue(IsAutoFoldProperty); }
            set { SetValue(IsAutoFoldProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAutoFold.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAutoFoldProperty =
            DependencyProperty.Register("IsAutoFold", typeof(bool), typeof(LayExpander), new PropertyMetadata(false, Fold));

        private static void Fold(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
           
        }


        public Brush HeaderBackground
        {
            get { return (Brush)GetValue(HeaderBackgroundProperty); }
            set { SetValue(HeaderBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderBackgroundProperty =
            DependencyProperty.Register("HeaderBackground", typeof(Brush), typeof(LayExpander));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

    }
}
