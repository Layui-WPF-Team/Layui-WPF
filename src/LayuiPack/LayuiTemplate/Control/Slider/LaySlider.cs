using LayuiTemplate.Enum;
using LayuiTemplate.Tools;
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
    /// <summary>
    /// 滑块
    /// </summary>
    public class LaySlider: Slider
    {

        /// <summary>
        /// 气泡提示方向
        /// </summary>
        [Bindable(true)]
        public SliderTipsPosition TipsPosition
        {
            get { return (SliderTipsPosition)GetValue(TipsPositionProperty); }
            set { SetValue(TipsPositionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TipsPositionProperty =
            DependencyProperty.Register("TipsPosition", typeof(SliderTipsPosition), typeof(LaySlider), new PropertyMetadata(SliderTipsPosition.No));


    }
}