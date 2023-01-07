using LayUI.Wpf.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LayUI.Wpf.Controls
{
    public class LayProgressBar : ProgressBar
    {
        
        private Panel TemplateRoot;
        [Bindable(true)]
        public double Radius
        {
            get { return (double)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register("Radius", typeof(double), typeof(LayProgressBar), new PropertyMetadata(OnCornerRadiusChanged));

        private static void OnCornerRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LayProgressBar layProgress)
            {
                if (layProgress.IsLoaded)
                    layProgress.InvalidateVisual();
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            TemplateRoot = GetTemplateChild("TemplateRoot") as Panel;
        }
        private void IndicatorCornerRadius()
        {
            TemplateRoot.Clip = new RectangleGeometry(new Rect(0, 0, this.ActualWidth, this.ActualHeight), Radius, Radius);
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            IndicatorCornerRadius();
        }
    }
}
