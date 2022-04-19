using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;

namespace LayuiTemplate.Control
{
    /// <summary>
    ///  LayExpander
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-04-18 下午 1:31:57</para>
    /// </summary>
    public class LayExpander : Expander
    {
        /// <summary>
        /// 折叠板开关按钮
        /// </summary>
        private ToggleButton HeaderToggleButton;
        /// <summary>
        /// 折叠板展现容器
        /// </summary>
        private StackPanel ContentStackPanel;
        /// <summary>
        /// 动画容器
        /// </summary>
        private Grid contentAnimationGrid;
        /// <summary>
        /// 显示开关
        /// </summary>
        private bool IsShow = false;
        public LayExpander()
        {
            Loaded -= LayExpander_Loaded;
            Loaded += LayExpander_Loaded;
        }
        /// <summary>
        /// 顶部高度
        /// </summary>
        public double HeaderHeight
        {
            get { return (double)GetValue(HeaderHeightProperty); }
            set { SetValue(HeaderHeightProperty, value); }
        }
        // Using a DependencyProperty as the backing store for HeaderHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderHeightProperty =
            DependencyProperty.Register("HeaderHeight", typeof(double), typeof(LayExpander));
        private void LayExpander_Loaded(object sender, RoutedEventArgs e)
        {
            HeaderToggleButton.Checked -= HeaderToggleButton_Click;
            HeaderToggleButton.Unchecked -= HeaderToggleButton_Click;
            HeaderToggleButton.Checked += HeaderToggleButton_Click;
            HeaderToggleButton.Unchecked += HeaderToggleButton_Click;
            contentAnimationGrid.SizeChanged += ContentAnimationGrid_SizeChanged;
            if (!IsExpanded) AnimationGrid(TimeSpan.FromSeconds(0));
        }
        /// <summary>
        /// 设置可视化宽度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContentAnimationGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (IsExpanded) AnimationGrid(TimeSpan.FromSeconds(0));
        }
        /// <summary>
        /// 响应当前点击状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HeaderToggleButton_Click(object sender, RoutedEventArgs e)
        {
            IsShow = (bool)(sender as ToggleButton).IsChecked;
            AnimationGrid(TimeSpan.FromSeconds(0.2));
            //RefreshIsExpanded();
        }
        private void RefreshIsExpanded()
        {
            if (Parent == null) return;
            if (Parent is LayExpanderPanel panel)
            {
                if (!panel.IsAutoFold) return;
                foreach (var item in panel.Children)
                {
                    if (item is LayExpander expander) {
                        if (expander != this) expander.IsExpanded = false; 
                    }
                }
            }
        }
        /// <summary>
        /// 顶部颜色
        /// </summary>
        public Brush HeaderBackground
        {
            get { return (Brush)GetValue(HeaderBackgroundProperty); }
            set { SetValue(HeaderBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderBackgroundProperty =
            DependencyProperty.Register("HeaderBackground", typeof(Brush), typeof(LayExpander));
        /// <summary>
        /// 刷新动画
        /// </summary>
        /// <param name="time"></param>
        private void AnimationGrid(TimeSpan time)
        {
            DoubleAnimation animation = new DoubleAnimation()
            {
                From = IsShow == true ? 0 : contentAnimationGrid.DesiredSize.Height,
                To = IsShow == true ? contentAnimationGrid.DesiredSize.Height : 0,
                Duration = time
            };
            ContentStackPanel.BeginAnimation(HeightProperty, animation);
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            HeaderToggleButton = GetTemplateChild("header") as ToggleButton;
            ContentStackPanel = GetTemplateChild("contentPanel") as StackPanel;
            contentAnimationGrid = GetTemplateChild("contentAnimationGrid") as Grid;
        }
    }
}
