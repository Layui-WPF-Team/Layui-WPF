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
using LayuiTemplate.Tools;
using System.ComponentModel;

namespace LayuiTemplate.Controls
{
    /// <summary>
    ///  LayNavExpander
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-04-18 下午 1:31:57</para>
    /// </summary>
    public class LayNavExpander : HeaderedContentControl
    {
        
        /// <summary>
        /// 状态监听
        /// </summary>
        private ToggleButton headerToggleButton = null;
        [Bindable(true)]
        public VerticalAlignment HeaderVerticalAlignment
        {
            get { return (VerticalAlignment)GetValue(HeaderVerticalAlignmentProperty); }
            set { SetValue(HeaderVerticalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderVerticalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderVerticalAlignmentProperty =
            DependencyProperty.Register("HeaderVerticalAlignment", typeof(VerticalAlignment), typeof(LayNavExpander));


        [Bindable(true)]
        public HorizontalAlignment HeaderHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(HeaderHorizontalAlignmentProperty); }
            set { SetValue(HeaderHorizontalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderHorizontalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderHorizontalAlignmentProperty =
            DependencyProperty.Register("HeaderHorizontalAlignment", typeof(HorizontalAlignment), typeof(LayNavExpander));

        [Bindable(true)]
        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }
        // Using a DependencyProperty as the backing store for IsExpanded.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register("IsExpanded", typeof(bool), typeof(LayNavExpander));

        /// <summary>
        /// 顶部高度
        /// </summary>
        [Bindable(true)]
        public double HeaderHeight
        {
            get { return (double)GetValue(HeaderHeightProperty); }
            set { SetValue(HeaderHeightProperty, value); }
        }
        // Using a DependencyProperty as the backing store for HeaderHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderHeightProperty =
            DependencyProperty.Register("HeaderHeight", typeof(double), typeof(LayNavExpander));
        /// <summary>
        /// 顶部颜色
        /// </summary>
        [Bindable(true)]
        public Brush HeaderBackground
        {
            get { return (Brush)GetValue(HeaderBackgroundProperty); }
            set { SetValue(HeaderBackgroundProperty, value); }
        }
        // Using a DependencyProperty as the backing store for HeaderBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderBackgroundProperty =
            DependencyProperty.Register("HeaderBackground", typeof(Brush), typeof(LayNavExpander));
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            /////////防呆///////
            if (headerToggleButton != null)
            {
                headerToggleButton = GetTemplateChild("header") as ToggleButton;
                headerToggleButton.Checked -= HeaderToggleButton_Click;
                headerToggleButton.Checked += HeaderToggleButton_Click;
            }
        }
        private void HeaderToggleButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshExpanded();
        }
        /// <summary>
        /// 刷新折叠效果
        /// </summary>
        private void RefreshExpanded()
        {
            if (this.Parent == null) return;
            if (this.Parent is LayNavExpanderPanel panel)
            {
                if (!panel.IsAutoFold) return;
                foreach (var item in panel.Children)
                {
                    if (item is LayNavExpander expander && expander != this)
                    {
                        expander.IsExpanded = false;
                    }
                }
            }
        }
    }
}
