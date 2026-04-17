using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LayUI.Wpf.Extend
{
    /// <summary>
    ///  LayDataGridHelper
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-07-04 下午 4:36:23</para>
    /// </summary>
    public class LayDataGridHelper: LayItemsControlHelper
    { 
        public static Brush GetSelectedRowBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(SelectedRowBackgroundProperty);
        }

        public static void SetSelectedRowBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(SelectedRowBackgroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectedRowBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedRowBackgroundProperty =
            DependencyProperty.RegisterAttached("SelectedRowBackground", typeof(Brush), typeof(LayDataGridHelper));


        public static Brush GetSelectedRowForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(SelectedRowForegroundProperty);
        }

        public static void SetSelectedRowForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(SelectedRowForegroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectedRowForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedRowForegroundProperty =
            DependencyProperty.RegisterAttached("SelectedRowForeground", typeof(Brush), typeof(LayDataGridHelper));



    }
}
