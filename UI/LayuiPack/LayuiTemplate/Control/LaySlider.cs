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
    /// 滑块
    /// </summary>
    public class LaySlider: Slider
    {

        /// <summary>
        /// 是否显示文本提示
        /// </summary>
        public bool ShowTips
        {
            get { return (bool)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register(nameof(ShowTips), typeof(bool), typeof(LaySlider));

    }
}