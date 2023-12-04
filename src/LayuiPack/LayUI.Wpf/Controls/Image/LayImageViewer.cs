using LayUI.Wpf.Extend;
using LayUI.Wpf.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LayUI.Wpf.Controls
{
    /// <summary>
    /// 可缩放图片
    /// </summary>
    public class LayImageViewer : Control
    {
        /// <summary>
        /// 可操作图片
        /// </summary>
        private Image img;
        /// <summary>
        /// 图片可视化容器
        /// </summary>
        private Grid body;
        
        /// <summary>
        /// 缩放倍数
        /// </summary>
        public double Scale
        {
            get { return (double)GetValue(ScaleProperty); }
            private set { SetValue(ScaleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Scale.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScaleProperty =
            DependencyProperty.Register("Scale", typeof(double), typeof(LayImageViewer));


        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(ImageSource), typeof(LayImageViewer));

        /// <summary>
        /// 可视化区域图片
        /// </summary> 

        public BitmapImage Intercept
        {
            get { return (BitmapImage)GetValue(InterceptProperty); }
            private set { SetValue(InterceptProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Intercept.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InterceptProperty =
            DependencyProperty.Register("Intercept", typeof(BitmapImage), typeof(LayImageViewer));

         
        public override void OnApplyTemplate()
        { 
            img = GetTemplateChild("PART_Image") as Image;
            body = GetTemplateChild("PART_Body") as Grid; 
            base.OnApplyTemplate();
        }
    }
}
