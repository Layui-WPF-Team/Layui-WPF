
using LayuiTemplate.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LayuiTemplate.Control
{
    /// <summary>
    /// 选项卡
    /// </summary>
    public class LayTabControl : TabControl
    {
        /// <summary>
        /// 选项卡类型
        /// </summary>
        public TabControlStyle Type
        {
            get { return (TabControlStyle)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Type.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(TabControlStyle), typeof(LayTabControl), new PropertyMetadata(TabControlStyle.Simplicity));



    }
}
