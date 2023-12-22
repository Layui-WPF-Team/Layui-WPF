using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LayUI.Wpf.Controls
{
    /// <summary>
    /// 文件上传
    /// </summary>
    public class LayUpload:Control
    { 

        public UIElement ClickTarget
        {
            get { return (UIElement)GetValue(ClickTargetProperty); }
            set { SetValue(ClickTargetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ClickTarget.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClickTargetProperty =
            DependencyProperty.Register("ClickTarget", typeof(UIElement), typeof(LayUpload)); 



    }
}
