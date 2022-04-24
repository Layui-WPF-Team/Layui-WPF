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
    }
}
