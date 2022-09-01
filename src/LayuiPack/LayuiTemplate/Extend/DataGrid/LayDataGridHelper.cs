using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LayuiTemplate.Extend
{
    /// <summary>
    ///  LayDataGridHelper
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-07-04 下午 4:36:23</para>
    /// </summary>
    public class LayDataGridHelper
    {
        /// <summary>
        /// 获得DataGrid当前行号
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int GetRowIndex(DependencyObject obj)
        {
            return (int)obj.GetValue(RowIndexProperty);
        }
        /// <summary>
        /// 设置DataGrid当前行号
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static void SetRowIndex(DependencyObject obj, int value)
        {
            obj.SetValue(RowIndexProperty, value);
        }

        // Using a DependencyProperty as the backing store for Index.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowIndexProperty =
            DependencyProperty.RegisterAttached("RowIndex", typeof(int), typeof(LayDataGridHelper));


    }
}
