using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LayUI.Wpf.Controls
{
    /// <summary>
    /// 控件通用属性
    /// </summary>
    internal interface ILayControl
    { 
        /// <summary>
        /// 圆角
        /// </summary>
        CornerRadius CornerRadius { get;  set ; } 
    }
}
