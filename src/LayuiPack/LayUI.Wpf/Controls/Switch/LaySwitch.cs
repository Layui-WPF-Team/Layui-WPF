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

namespace LayUI.Wpf.Controls
{
    /// <summary>
    ///  LaySwitch
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-06-28 下午 5:32:33</para>
    /// </summary>
    [TemplatePart(Name = "PART_Icon")]
    public class LaySwitch : LaySimpleToggleButton, ILayControl
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
            InitAnimation((bool)this.IsChecked);
        }
        protected override void OnUnchecked(RoutedEventArgs e)
        {
            base.OnUnchecked(e);
            InitAnimation((bool)this.IsChecked);
        } 
        /// <summary>
        /// 初始化选中效果
        /// </summary>
        /// <param name="isCheck"></param>
        void InitAnimation(bool isCheck)
        {
            if (PART_Icon == null) return;
            if (isCheck)
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
                thicknessAnimation.Duration = TimeSpan.FromMilliseconds(200);
                PART_Icon.BeginAnimation(MarginProperty, thicknessAnimation);
            }
            else
            {
                ThicknessAnimation thicknessAnimation = new ThicknessAnimation();
                thicknessAnimation.To = new Thickness(0, 0, 0, 0);
                thicknessAnimation.Duration = TimeSpan.FromMilliseconds(200);
                PART_Icon.BeginAnimation(MarginProperty, thicknessAnimation);
            }
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_Icon = GetTemplateChild("PART_Icon") as Viewbox;
            PART_Border = GetTemplateChild("PART_Border") as Border;
            Loaded += LaySwitch_Loaded;
        }

        private void LaySwitch_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= LaySwitch_Loaded;
            InitAnimation((bool)this.IsChecked);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            InitAnimation((bool)this.IsChecked);
        }
    }
}
