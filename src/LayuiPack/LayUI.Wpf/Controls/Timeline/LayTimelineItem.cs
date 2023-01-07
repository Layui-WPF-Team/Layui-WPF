using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LayUI.Wpf.Controls
{
    public class LayTimelineItem : System.Windows.Controls.ListBoxItem
    {
        /// <summary>
        /// 用于控制内容区域状态(一般用于分割线结尾控制)
        /// </summary>
        [Bindable(true)]
        public bool IsContentState
        {
            get { return (bool)GetValue(IsContentStateProperty); }
            set { SetValue(IsContentStateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsContentState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsContentStateProperty =
            DependencyProperty.Register("IsContentState", typeof(bool), typeof(LayTimelineItem), new PropertyMetadata(true));

        /// <summary>
        /// 顶部标题
        /// </summary>
        [Bindable(true)]
        public object Header
        {
            get { return (object)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(object), typeof(LayTimelineItem));
        [Bindable(true)]
        public VerticalAlignment HeaderVerticalAlignment
        {
            get { return (VerticalAlignment)GetValue(HeaderVerticalAlignmentProperty); }
            set { SetValue(HeaderVerticalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderVerticalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderVerticalAlignmentProperty =
            DependencyProperty.Register("HeaderVerticalAlignment", typeof(VerticalAlignment), typeof(LayTimelineItem));


        [Bindable(true)]
        public HorizontalAlignment HeaderHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(HeaderHorizontalAlignmentProperty); }
            set { SetValue(HeaderHorizontalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderHorizontalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderHorizontalAlignmentProperty =
            DependencyProperty.Register("HeaderHorizontalAlignment", typeof(HorizontalAlignment), typeof(LayTimelineItem));


    }
}
