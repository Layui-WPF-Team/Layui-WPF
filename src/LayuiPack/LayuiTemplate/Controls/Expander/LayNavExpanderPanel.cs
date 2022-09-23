using LayuiTemplate.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LayuiTemplate.Controls
{
    /// <summary>
    ///  LayExpanderPanel
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-04-19 下午 5:56:27</para>
    /// </summary>
    public class LayNavExpanderPanel : StackPanel
    {

        [Bindable(true)]
        public bool IsAutoFold
        {
            get { return (bool)GetValue(IsAutoFoldProperty); }
            set { SetValue(IsAutoFoldProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAutoFold.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAutoFoldProperty =
            DependencyProperty.Register("IsAutoFold", typeof(bool), typeof(LayNavExpanderPanel), new PropertyMetadata(false));
    }
}
