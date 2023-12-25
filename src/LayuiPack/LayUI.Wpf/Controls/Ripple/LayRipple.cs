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
            _Storyboard = new Storyboard(); 
            DoubleAnimation ODoubleAnimation = new DoubleAnimation
            {
                From = 0,
                To = 0,
                Duration = TimeSpan.FromSeconds(0),
            };
            _Storyboard.Children.Add(ODoubleAnimation);
            Storyboard.SetTarget(ODoubleAnimation, PART_Border);
            Storyboard.SetTargetProperty(ODoubleAnimation, new PropertyPath("(UIElement.Opacity)"));
            Storyboard.SetTarget(_Storyboard, PART_Border);
            this.BeginStoryboard(_Storyboard);
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
            _Storyboard = new Storyboard();
            DoubleAnimation XDoubleAnimation = new DoubleAnimation
            {
                From = 1,
                To = 1.4,
                Duration = TimeSpan.FromSeconds(1),
            };
            _Storyboard.Children.Add(XDoubleAnimation);
            Storyboard.SetTarget(XDoubleAnimation, PART_Border);
            Storyboard.SetTargetProperty(XDoubleAnimation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
            DoubleAnimation YDoubleAnimation = new DoubleAnimation
            {
                From = 1,
                To = 1.4,
                Duration = TimeSpan.FromSeconds(1),
            };
            _Storyboard.Children.Add(YDoubleAnimation);
            Storyboard.SetTarget(YDoubleAnimation, PART_Border);
            Storyboard.SetTargetProperty(YDoubleAnimation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)"));
            DoubleAnimation ODoubleAnimation = new DoubleAnimation
            {
                From = 0.3,
                To = 0,
                Duration = TimeSpan.FromSeconds(1),
            };
            _Storyboard.Children.Add(ODoubleAnimation);
            Storyboard.SetTarget(ODoubleAnimation, PART_Border);
            Storyboard.SetTargetProperty(ODoubleAnimation, new PropertyPath("(UIElement.Opacity)"));
            this.BeginStoryboard(_Storyboard);
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
            _Storyboard = new Storyboard();
            _Storyboard.RepeatBehavior = RepeatBehavior.Forever;
            DoubleAnimation XDoubleAnimation = new DoubleAnimation
            {
                From = 1,
                To = 1.4,
                Duration = TimeSpan.FromSeconds(1),
            };
            _Storyboard.Children.Add(XDoubleAnimation);
            Storyboard.SetTarget(XDoubleAnimation, PART_Border);
            Storyboard.SetTargetProperty(XDoubleAnimation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
            DoubleAnimation YDoubleAnimation = new DoubleAnimation
            {
                From = 1,
                To = 1.4,
                Duration = TimeSpan.FromSeconds(1),
            };
            _Storyboard.Children.Add(YDoubleAnimation);
            Storyboard.SetTarget(YDoubleAnimation, PART_Border);
            Storyboard.SetTargetProperty(YDoubleAnimation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)"));
            DoubleAnimation ODoubleAnimation = new DoubleAnimation
            {
                From = 0.3,
                To = 0,
                Duration = TimeSpan.FromSeconds(1),
            };
            _Storyboard.Children.Add(ODoubleAnimation);
            Storyboard.SetTarget(ODoubleAnimation, PART_Border);
            Storyboard.SetTargetProperty(ODoubleAnimation, new PropertyPath("(UIElement.Opacity)"));
            this.BeginStoryboard(_Storyboard);
        }
        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);
            ExecuteClickAnimation();
        }
    }
}
