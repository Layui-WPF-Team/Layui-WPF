using LayUI.Wpf.Enum;
using LayUI.Wpf.Tools;
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
        private Storyboard _Storyboard;
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
            DependencyProperty.Register("Type", typeof(RippleStyle), typeof(LayRipple), new PropertyMetadata(OnTypeChanged));

        private static void OnTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is LayRipple ripple)) return;
            if (ripple.IsLoaded) ripple.ExecuteAnimation(ripple.Type);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_Border = GetTemplateChild(nameof(PART_Border)) as Border;
            ExecuteAnimation(Type);
        }
        /// <summary>
        /// 执行动画
        /// </summary>
        /// <param name="type"></param>
        void ExecuteAnimation(RippleStyle type)
        {
            ExecuteRemoveAnimation();
            switch (type)
            {
                case RippleStyle.Auto:
                    ExecuteAutoAnimation();
                    break;
                case RippleStyle.Default:
                default:
                    ExecuteDefaultAnimation();
                    break;
            }
        }
        /// <summary>
        /// 无背景色扩散动画
        /// </summary>
        void ExecuteDefaultAnimation()
        {
            if (Type != RippleStyle.Default) return;
            ExecuteRemoveAnimation();
        }
        /// <summary>
        /// 移除动画
        /// <para>并不算是真正的移除</para>
        /// </summary>
        void ExecuteRemoveAnimation()
        {
            if (PART_Border == null) return;
            if (_Storyboard != null)
            {
                _Storyboard.Stop();
                _Storyboard.Children.Clear();
            } 
            _Storyboard = LayAnimationHelper.CreateStoryboard(
                new AnimationTimeline[]
                {
                    LayAnimationHelper.CreateAnimation(3.0, 0.0, 0, new PropertyPath("(UIElement.Opacity)"), PART_Border)
                });
            LayAnimationHelper.ExecuteStoryboard(_Storyboard, PART_Border); 
        }
        /// <summary>
        /// 执行点击背景色扩散动画
        /// </summary>
        void ExecuteClickAnimation()
        {
            if (PART_Border == null) return;
            if (Type != RippleStyle.Click) return;
            if (_Storyboard != null)
            {
                _Storyboard.Stop();
                _Storyboard.Children.Clear();
            }
            _Storyboard = LayAnimationHelper.CreateStoryboard(
                new AnimationTimeline[]
                {
                    LayAnimationHelper.CreateAnimation(1.0, 1.4, 1, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"), PART_Border),
                    LayAnimationHelper.CreateAnimation(1.0, 1.4, 1, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)"), PART_Border),
                    LayAnimationHelper.CreateAnimation(0.3, 0.0, 1, new PropertyPath("(UIElement.Opacity)"), PART_Border)
                });
            LayAnimationHelper.ExecuteStoryboard(_Storyboard, PART_Border);
        }
        /// <summary>
        /// 执行自动背景色扩散动画
        /// </summary>
        void ExecuteAutoAnimation()
        {
            if (PART_Border == null) return;
            if (Type != RippleStyle.Auto) return;
            if (_Storyboard != null)
            {
                _Storyboard.Stop();
                _Storyboard.Children.Clear();
            } 
            _Storyboard = LayAnimationHelper.CreateStoryboard(
                new AnimationTimeline[]
                {
                    LayAnimationHelper.CreateAnimation(1.0, 1.4, 1, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"), PART_Border),
                    LayAnimationHelper.CreateAnimation(1.0, 1.4, 1, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)"), PART_Border),
                    LayAnimationHelper.CreateAnimation(0.3, 0.0, 1, new PropertyPath("(UIElement.Opacity)"), PART_Border)
                });
            _Storyboard.RepeatBehavior = RepeatBehavior.Forever;
            LayAnimationHelper.ExecuteStoryboard(_Storyboard, PART_Border); 
        }
        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);
            ExecuteClickAnimation();
        }
    }
}
