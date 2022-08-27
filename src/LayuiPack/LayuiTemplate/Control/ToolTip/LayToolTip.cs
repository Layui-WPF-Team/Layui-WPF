using LayuiTemplate.Enum.TooplTip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace LayuiTemplate.Control
{
    public class LayToolTip : ContentControl
    {
        /// <summary>
        /// 文字提示
        /// </summary>
        [Bindable(true)]
        public object ToolTipContent
        {
            get { return (object)GetValue(ToolTipContentProperty); }
            set { SetValue(ToolTipContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ToolTipContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ToolTipContentProperty =
            DependencyProperty.Register("ToolTipContent", typeof(object), typeof(LayToolTip));
        [Bindable(true)]
        public PlacementStyle Placement
        {
            get { return (PlacementStyle)GetValue(PlacementProperty); }
            set { SetValue(PlacementProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Placement.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlacementProperty =
            DependencyProperty.Register("Placement", typeof(PlacementStyle), typeof(LayToolTip));



        [Bindable(true)]
        public Brush ToolTipForeground
        {
            get { return (Brush)GetValue(ToolTipForegroundProperty); }
            set { SetValue(ToolTipForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ToolTipForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ToolTipForegroundProperty =
            DependencyProperty.Register("ToolTipForeground", typeof(Brush), typeof(LayToolTip));
        private new object ToolTip
        {
            get { return (object)GetValue(ToolTipProperty); }
            set { SetValue(ToolTipProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ToolTip.  This enables animation, styling, binding, etc...
        private static new readonly DependencyProperty ToolTipProperty =
            DependencyProperty.Register("ToolTip", typeof(object), typeof(LayToolTip));


    }
}
