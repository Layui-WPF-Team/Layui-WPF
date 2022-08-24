using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LayuiTemplate.Control
{
    /// <summary>
    ///  LaySVGImage (尚未完成)
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-08-24 上午 11:03:52</para>
    /// </summary>
    public class LaySVGImage : System.Windows.Controls.Control
    {
        /// <summary>
        /// 图片加载容器
        /// </summary>
        private Image PART_Image;
        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(ImageSource), typeof(LaySVGImage), new PropertyMetadata(OnSourceChanged));

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LaySVGImage laySVGImage)
            {
                if (laySVGImage.IsLoaded) laySVGImage.Refresh();
            }
        }
        /// <summary>
        /// 刷新
        /// </summary>
        private void Refresh()
        {
            
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_Image = GetTemplateChild("PART_Image") as Image;
            Refresh();
        }
    }
}
