using LayuiTemplate.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace LayuiTemplate.Control
{
    public class LayDataGrid : DataGrid
    {
        public bool IsShowRowsFocusVisual
        {
            get { return (bool)GetValue(IsShowRowsFocusVisualProperty); }
            set { SetValue(IsShowRowsFocusVisualProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsShowRowsFocusVisual.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsShowRowsFocusVisualProperty =
            DependencyProperty.Register("IsShowRowsFocusVisual", typeof(bool), typeof(LayDataGrid), new PropertyMetadata(false));


        /// <summary>
        /// 无数据提示信息
        /// </summary>
        public string NoDataTips
        {
            get { return (string)GetValue(NoDataTipsProperty); }
            set { SetValue(NoDataTipsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NoDataTips.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NoDataTipsProperty =
            DependencyProperty.Register("NoDataTips", typeof(string), typeof(LayDataGrid), new PropertyMetadata("暂无数据"));
        /// <summary>
        /// 动画开关
        /// </summary>
        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsActive.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof(bool), typeof(LayDataGrid), new PropertyMetadata(false));


    }
    
}
