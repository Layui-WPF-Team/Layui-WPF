using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LayUI.Wpf.Controls
{
    /// <summary>
    /// 流加载控件
    /// </summary>

    [TemplatePart(Name = nameof(PART_ScrollViewer), Type = typeof(ScrollViewer))]
    public class LayFlowItemsControl : ItemsControl
    {
        private ScrollViewer PART_ScrollViewer;
        /// <summary>
        /// 滚动条滑动到底部触发事件
        /// </summary>
        public event RoutedEventHandler Append
        {
            add => AddHandler(AppendEvent, value);
            remove => RemoveHandler(AppendEvent, value);
        }
        public static readonly RoutedEvent AppendEvent =
            EventManager.RegisterRoutedEvent("Append", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(LayFlowItemsControl));
        /// <summary>
        /// 滚动条滑动到底部触发方法
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnAppend(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_ScrollViewer = GetTemplateChild(nameof(PART_ScrollViewer)) as ScrollViewer;
            if (PART_ScrollViewer != null)
            {
                PART_ScrollViewer.ScrollChanged -= PART_ScrollViewer_ScrollChanged;
                PART_ScrollViewer.ScrollChanged += PART_ScrollViewer_ScrollChanged; 
            }
        } 
        private void PART_ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        { 
            if ((e.ViewportHeight+ e.VerticalOffset) >= e.ExtentHeight)
            {
                OnAppend(new RoutedEventArgs(AppendEvent, this));
            }
        } 
    }
}
