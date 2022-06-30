using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace LayuiTemplate.Control
{
    /// <summary>
    ///  LaySwitch
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-06-28 下午 5:32:33</para>
    /// </summary>
    [TemplatePart(Name = "PART_Icon")]
    public class LaySwitch:ToggleButton
    {
        private Viewbox PART_Icon;
        private Border PART_Border;
        public Brush CheckedColor
        {
            get { return (Brush)GetValue(CheckedColorProperty); }
            set { SetValue(CheckedColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Color.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CheckedColorProperty =
            DependencyProperty.Register("CheckedColor", typeof(Brush), typeof(LaySwitch));
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(LaySwitch));
        protected override void OnChecked(RoutedEventArgs e)
        {
            base.OnChecked(e);
            if (PART_Icon != null)
            {
                ThicknessAnimation thicknessAnimation = new ThicknessAnimation();
                thicknessAnimation.To = new Thickness(PART_Border.ActualWidth / 2, 0, 0, 0);
                thicknessAnimation.Duration = TimeSpan.FromMilliseconds(200);
                PART_Icon.BeginAnimation(MarginProperty, thicknessAnimation);

            }
        }
        protected override void OnUnchecked(RoutedEventArgs e)
        {
            base.OnUnchecked(e);
            if (PART_Icon != null)
            {
                ThicknessAnimation thicknessAnimation = new ThicknessAnimation();
                thicknessAnimation.To = new Thickness(0, 0, 0, 0);
                thicknessAnimation.Duration = TimeSpan.FromMilliseconds(200);
                PART_Icon.BeginAnimation(MarginProperty, thicknessAnimation);

            }
        }
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            if (PART_Icon != null)
            {
                ThicknessAnimation thicknessAnimation = new ThicknessAnimation();
                if (IsChecked == true)
                {
                    thicknessAnimation.To = new Thickness(PART_Border.ActualWidth / 2, 0, 0, 0);
                }
                if (IsChecked == false)
                {
                    thicknessAnimation.To = new Thickness(0, 0, 0, 0);
                }
                thicknessAnimation.Duration = TimeSpan.FromMilliseconds(0);
                PART_Icon.BeginAnimation(MarginProperty, thicknessAnimation);
            }
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_Icon = GetTemplateChild("PART_Icon") as Viewbox;
            PART_Border = GetTemplateChild("PART_Border") as Border;
        }
    }
}
