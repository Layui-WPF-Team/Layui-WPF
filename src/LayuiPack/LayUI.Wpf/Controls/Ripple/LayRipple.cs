using LayUI.Wpf.Enum;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace LayUI.Wpf.Controls
{
    /// <summary>
    /// 波纹
    /// </summary>
    public class LayRipple : ContentControl, ILayControl
    {
        private Border PART_Border;
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(LayRipple));


        public RippleStyle Type
        {
            get { return (RippleStyle)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }
        // Using a DependencyProperty as the backing store for Type.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(RippleStyle), typeof(LayRipple));


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_Border = GetTemplateChild(nameof(PART_Border)) as Border;
        }
        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);
            if (Type == RippleStyle.Click)
            {
                var storyboard = new Storyboard();
                DoubleAnimation XDoubleAnimation = new DoubleAnimation
                {
                    From = 1,
                    To = 1.4,
                    Duration = TimeSpan.FromSeconds(1),
                };
                storyboard.Children.Add(XDoubleAnimation);
                Storyboard.SetTarget(XDoubleAnimation, PART_Border);
                Storyboard.SetTargetProperty(XDoubleAnimation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
                DoubleAnimation YDoubleAnimation = new DoubleAnimation
                {
                    From = 1,
                    To = 1.4,
                    Duration = TimeSpan.FromSeconds(1),
                };
                storyboard.Children.Add(YDoubleAnimation);
                Storyboard.SetTarget(YDoubleAnimation, PART_Border);
                Storyboard.SetTargetProperty(YDoubleAnimation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)"));
                DoubleAnimation ODoubleAnimation = new DoubleAnimation
                {
                    From = 0.3,
                    To = 0,
                    Duration = TimeSpan.FromSeconds(1),
                };
                storyboard.Children.Add(ODoubleAnimation);
                Storyboard.SetTarget(ODoubleAnimation, PART_Border);
                Storyboard.SetTargetProperty(ODoubleAnimation, new PropertyPath("(UIElement.Opacity)"));
                this.BeginStoryboard(storyboard);
            }
        }
    }
}
