using LayuiTemplate.Enum;
using LayuiTemplate.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LayuiTemplate.Control
{
    public class LayCheckBox: CheckBox
    {
        /// <summary>
        /// 复选框样式
        /// </summary>
        public CheckBoxStyle Type
        {
            get { return (CheckBoxStyle)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Type.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(CheckBoxStyle), typeof(LayCheckBox), new PropertyMetadata(CheckBoxStyle.Primary));

        /// <summary>
        /// 鼠标移上去的边框颜色
        /// </summary>
        public Brush HoverBorderBrush
        {
            get { return (Brush)GetValue(HoverBorderBrushProperty); }
            set { SetValue(HoverBorderBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HoverBorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HoverBorderBrushProperty =
            DependencyProperty.Register("HoverBorderBrush", typeof(Brush), typeof(LayCheckBox), new PropertyMetadata(Brushes.Transparent));

        public Brush CheckedColor
        {
            get { return (Brush)GetValue(CheckedColorProperty); }
            set { SetValue(CheckedColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Color.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CheckedColorProperty =
            DependencyProperty.Register("CheckedColor", typeof(Brush), typeof(LayCheckBox));

    }
}
