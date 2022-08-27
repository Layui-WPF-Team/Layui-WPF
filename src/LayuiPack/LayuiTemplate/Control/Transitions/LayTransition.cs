using LayuiTemplate.Enum.Transitions;
using LayuiTemplate.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace LayuiTemplate.Control
{
    /// <summary>
    /// 动画过渡容器
    /// </summary>
    public class LayTransition : ContentControl
    {

        [Bindable(true)]
        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsActive.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof(bool), typeof(LayTransition), new PropertyMetadata(false, AnimationChange));


        /// <summary>
        /// 动画容器
        /// </summary>
        private Grid TransitionBody = null;
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
            DependencyProperty.Register("Type", typeof(AnimationType), typeof(LayTransition), new PropertyMetadata(AnimationType.Default, AnimationChange));
        /// <summary>
        /// 监听动画类型改变
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void AnimationChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LayTransition control = d as LayTransition;
            if (!control.IsLoaded) return;
            control.RefreshAnimation();
        }
        /// <summary>
        /// 间隔时间
        /// </summary>
        [Bindable(true)]
        public TimeSpan? BeginTime
        {
            get { return (TimeSpan?)GetValue(BeginTimeProperty); }
            set { SetValue(BeginTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BeginTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BeginTimeProperty =
            DependencyProperty.Register("BeginTime", typeof(TimeSpan?), typeof(LayTransition), new PropertyMetadata(AnimationChange));


        /// <summary>
        /// 刷新动画
        /// </summary>
        private void RefreshAnimation()
        {
            if (!IsActive)
            {
                var story = new Storyboard()
                {
                    BeginTime = (BeginTime == null ? TimeSpan.FromSeconds(0) : BeginTime)
                };
                story.Children.Add(Template.Resources[nameof(AnimationType.Default) + "ScaleXDecimalAnimation"] as DoubleAnimation);
                story.Children.Add(Template.Resources[nameof(AnimationType.Default) + "ScaleYDecimalAnimation"] as DoubleAnimation);
                story.Children.Add(Template.Resources[nameof(AnimationType.Default) + "OpacityDecimalAnimation"] as DoubleAnimation);
                TransitionBody.BeginStoryboard(story);
                return;
            };
            Storyboard storyboard = new Storyboard()
            {
                BeginTime = (BeginTime == null ? TimeSpan.FromSeconds(0) : BeginTime)
            };
            switch (Type)
            {
                case AnimationType.Zoom:
                    storyboard.Children.Add(Template.Resources[nameof(AnimationType.Zoom) + "ScaleXDecimalAnimation"] as DoubleAnimation);
                    storyboard.Children.Add(Template.Resources[nameof(AnimationType.Zoom) + "ScaleYDecimalAnimation"] as DoubleAnimation);
                    storyboard.Children.Add(Template.Resources[nameof(AnimationType.Zoom) + "OpacityDecimalAnimation"] as DoubleAnimation);
                    break;
                case AnimationType.Gradient:
                    storyboard.Children.Add(Template.Resources[nameof(AnimationType.Gradient) + "ScaleXDecimalAnimation"] as DoubleAnimation);
                    storyboard.Children.Add(Template.Resources[nameof(AnimationType.Gradient) + "ScaleYDecimalAnimation"] as DoubleAnimation);
                    storyboard.Children.Add(Template.Resources[nameof(AnimationType.Gradient) + "OpacityDecimalAnimation"] as DoubleAnimation);
                    break;
                case AnimationType.Default:
                    storyboard.Children.Add(Template.Resources[nameof(AnimationType.Default) + "ScaleXDecimalAnimation"] as DoubleAnimation);
                    storyboard.Children.Add(Template.Resources[nameof(AnimationType.Default) + "ScaleYDecimalAnimation"] as DoubleAnimation);
                    storyboard.Children.Add(Template.Resources[nameof(AnimationType.Default) + "OpacityDecimalAnimation"] as DoubleAnimation);
                    break;
                case AnimationType.SlideIn:
                    storyboard.Children.Add(Template.Resources[nameof(AnimationType.Default) + "ScaleXDecimalAnimation"] as DoubleAnimation);
                    storyboard.Children.Add(Template.Resources[nameof(AnimationType.Default) + "ScaleYDecimalAnimation"] as DoubleAnimation);
                    storyboard.Children.Add(Template.Resources[nameof(AnimationType.SlideIn) + "YDecimalAnimation"] as DoubleAnimation);
                    storyboard.Children.Add(Template.Resources[nameof(AnimationType.SlideIn) + "OpacityDecimalAnimation"] as DoubleAnimation);
                    break;
            }
            TransitionBody.BeginStoryboard(storyboard);
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            TransitionBody = GetTemplateChild("TransitionBody") as Grid;
            if (TransitionBody == null) return;
            RefreshAnimation();
        }
    }
}
