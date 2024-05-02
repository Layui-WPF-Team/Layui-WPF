using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;

namespace LayUI.Wpf.Controls
{
    public class LayVRImage : Control
    {
        private Image PART_Image;

        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(ImageSource), typeof(LayVRImage), new PropertyMetadata(OnSourceChanged));

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => (d as LayVRImage).OnSourceChanged();

        private void OnSourceChanged()
        {

        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_Image = GetTemplateChild(nameof(PART_Image)) as Image;
            if (PART_Image != null) OnSourceChanged();
        }
    }
}
