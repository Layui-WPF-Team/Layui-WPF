using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LayuiTemplate.Controls
{
    public class LayTimelinePanel : System.Windows.Controls.ItemsControl
    {

        [Bindable(true)]
        public DataTemplate HeaderTemplate
        {
            get { return (DataTemplate)GetValue(HeaderTemplateProperty); }
            set { SetValue(HeaderTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderTemplateProperty =
            DependencyProperty.Register("HeaderTemplate", typeof(DataTemplate), typeof(LayTimelinePanel));

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            if (element is LayTimelineItem TimelineItem)
            {
                if (TimelineItem.Header == null)
                {
                    TimelineItem.Header = HeaderTemplate.LoadContent();
                }
                base.PrepareContainerForItemOverride(TimelineItem, item);
            }
        }
        /// <summary>
        /// 重写自定义指定项子控件
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is LayTimelineItem;
        }
        /// <summary>
        /// 抓取指定项控件并返回定制项控件
        /// </summary>
        /// <returns></returns>
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new LayTimelineItem();
        }
    }
}
