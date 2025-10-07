using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace LayUI.Wpf.Tools
{
    public class LayBindingHelper
    { 
        /// <summary>
        /// 绑定目标控件属性
        /// </summary> 
        public static void SetBinding(FrameworkElement element, DependencyProperty property,string path, BindingMode mode,object source)
        {
            element.SetBinding(property, new Binding()
            {
                Path = new PropertyPath(path),
                Mode = mode,
                Source = source
            });
        }
    }
}
