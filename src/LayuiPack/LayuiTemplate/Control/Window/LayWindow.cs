﻿using LayuiTemplate.Enum.Transitions;
using LayuiTemplate.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LayuiTemplate.Control
{
    public class LayWindow : Window
    {
        /// <summary>
        /// 设置重写默认样式
        /// </summary>
        static LayWindow()
        {
            StyleProperty.OverrideMetadata(typeof(LayWindow), new FrameworkPropertyMetadata(LayHelper.GetStyle(nameof(LayWindow)+"Style")));
        }
        /// <summary>
        /// 动画类型
        /// </summary>
        public AnimationType Type
        {
            get { return (AnimationType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Type.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(AnimationType), typeof(LayWindow), new PropertyMetadata(AnimationType.Default));
    }
}
