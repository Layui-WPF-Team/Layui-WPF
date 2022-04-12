using LayuiTemplate.Enum.Base;
using System.Windows;
using System.Windows.Interactivity;

namespace LayuiTemplate.Behavior
{
    public class LayLoadBehavior: Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
        }
        public LayAnimation Animation
        {
            get { return (LayAnimation)GetValue(AnimationProperty); }
            set { SetValue(AnimationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Animation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AnimationProperty =
            DependencyProperty.Register("Animation", typeof(LayAnimation), typeof(LayLoadBehavior), new PropertyMetadata(LayAnimation.Scale));


    }
}
