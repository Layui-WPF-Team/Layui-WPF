using LayuiTemplate.Enum.GroupBox;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LayuiTemplate.Control
{
    public class LayGroupBox: GroupBox
    {
        /// <summary>
        /// 类型
        /// </summary>
        [Bindable(true)]
        public GroupBoxStyle Type
        {
            get { return (GroupBoxStyle)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Type.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(GroupBoxStyle), typeof(LayGroupBox));
    }
}
