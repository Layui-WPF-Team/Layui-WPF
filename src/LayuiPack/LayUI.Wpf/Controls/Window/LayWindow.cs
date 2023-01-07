using LayUI.Wpf.Enum;
using LayUI.Wpf.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LayUI.Wpf.Controls
{
    public class LayWindow : Window
    {
        /// <summary>
        /// 设置重写默认样式
        /// </summary>
        static LayWindow()
        {
            StyleProperty.OverrideMetadata(typeof(LayWindow), new FrameworkPropertyMetadata(LayResourceHelper.GetStyle(nameof(LayWindow)+"Style")));
        }
        /// <summary>
        /// 动画类型
        /// </summary>
        [Bindable(true)]
        public AnimationType Type
        {
            get { return (AnimationType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Type.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(AnimationType), typeof(LayWindow), new PropertyMetadata(AnimationType.Default));
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
        }
    }
}
