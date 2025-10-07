using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace LayUI.Wpf.Controls
{
    /// <summary>
    /// 公告栏
    /// </summary>

    [TemplatePart(Name = nameof(PART_ContentPresenter), Type = typeof(ContentPresenter))]
    [TemplatePart(Name = nameof(PART_Grid), Type = typeof(Grid))]
    public class LayNoticeBar : ContentControl, ILayControl
    { 
        private DoubleAnimation animation;
        private Storyboard storyboard;
        private Grid PART_Grid;
        private ContentPresenter PART_ContentPresenter;
        public object InnerLeftContent
        {
            get { return (object)GetValue(InnerLeftContentProperty); }
            set { SetValue(InnerLeftContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InnerLeftContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InnerLeftContentProperty =
            DependencyProperty.Register("InnerLeftContent", typeof(object), typeof(LayNoticeBar));

        public double Duration
        {
            get { return (double)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Duration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(double), typeof(LayNoticeBar), new PropertyMetadata(20.0, OnDurationChanged));

        private static void OnDurationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
             var control = d as LayNoticeBar;
            control?.InitAnimation();
        } 

        public object InnerRightContent
        {
            get { return (object)GetValue(InnerRightContentProperty); }
            set { SetValue(InnerRightContentProperty, value); }
        } 
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(LayNoticeBar));


        // Using a DependencyProperty as the backing store for InnerRightContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InnerRightContentProperty =
            DependencyProperty.Register("InnerRightContent", typeof(object), typeof(LayNoticeBar));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_ContentPresenter = GetTemplateChild(nameof(PART_ContentPresenter)) as ContentPresenter;
            PART_Grid = GetTemplateChild(nameof(PART_Grid)) as Grid; 
        }
        void InitAnimation()
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                if (Content == null) return;
                if (storyboard == null) storyboard = new Storyboard() { RepeatBehavior = RepeatBehavior.Forever };
                storyboard.Stop();
                animation = new DoubleAnimation()
                {
                    From = PART_Grid.ActualWidth,
                    To = -PART_ContentPresenter.ActualWidth,
                    Duration = TimeSpan.FromSeconds(Duration),
                };
                storyboard.Children.Add(animation);
                Storyboard.SetTargetProperty(animation, new PropertyPath("(ContentPresenter.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)"));
                Storyboard.SetTarget(animation, PART_ContentPresenter);
                storyboard.Begin();
            }));
        } 
        protected override Size MeasureOverride(Size constraint)
        {
            InitAnimation();
            return base.MeasureOverride(constraint);
        }
    }
}
