using LayuiTemplate.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LayuiTemplate.Control
{
    /// <summary>
    ///  分页插件
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-08-08 下午 4:49:46</para>
    /// </summary>
    public class LayPagination : System.Windows.Controls.Control
    {

        /// <summary>
        /// 刷新按钮内容
        /// </summary>
        public object RefreshBtnContent
        {
            get { return (object)GetValue(RefreshBtnContentProperty); }
            set { SetValue(RefreshBtnContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RefreshBtnContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RefreshBtnContentProperty =
            DependencyProperty.Register("RefreshBtnContent", typeof(object), typeof(LayPagination));


        /// <summary>
        /// 上一页按钮内容
        /// </summary>
        public object PrevBtnContent
        {
            get { return (object)GetValue(PrevBtnContentProperty); }
            set { SetValue(PrevBtnContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PrevContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PrevBtnContentProperty =
            DependencyProperty.Register("PrevBtnContent", typeof(object), typeof(LayPagination));
        /// <summary>
        /// 下一页按钮内容
        /// </summary>
        public object NextBtnContent
        {
            get { return (object)GetValue(NextBtnContentProperty); }
            set { SetValue(NextBtnContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NextContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NextBtnContentProperty =
            DependencyProperty.Register("NextBtnContent", typeof(object), typeof(LayPagination));


        /// <summary>
        /// 确认按钮修饰内容
        /// </summary>
        public object ConfirmBtnContent
        {
            get { return (object)GetValue(ConfirmBtnContentProperty); }
            set { SetValue(ConfirmBtnContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ConfirmContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConfirmBtnContentProperty =
            DependencyProperty.Register("ConfirmBtnContent", typeof(object), typeof(LayPagination));

        /// <summary>
        /// 数据总数修饰内容
        /// </summary>
        public object PageCountContent
        {
            get { return (object)GetValue(PageCountContentProperty); }
            set { SetValue(PageCountContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PageCountContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageCountContentProperty =
            DependencyProperty.Register("PageCountContent", typeof(object), typeof(LayPagination));
        /// <summary>
        /// 分页数据总数
        /// </summary>
        public int PageCount
        {
            get { return (int)GetValue(PageCountProperty); }
            set { SetValue(PageCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PageCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageCountProperty =
            DependencyProperty.Register("PageCount", typeof(int), typeof(LayPagination));
        /// <summary>
        /// 跳转修饰内容
        /// </summary>
        public object SkipContent
        {
            get { return (object)GetValue(SkipContentProperty); }
            set { SetValue(SkipContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SkipContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SkipContentProperty =
            DependencyProperty.Register("SkipContent", typeof(object), typeof(LayPagination));
        /// <summary>
        /// 跳转至目标页数
        /// </summary>
        public int Skip
        {
            get { return (int)GetValue(SkipProperty); }
            set { SetValue(SkipProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Skip.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SkipProperty =
            DependencyProperty.Register("Skip", typeof(int), typeof(LayPagination));

        /// <summary>
        /// 当前分页展示数量
        /// </summary>
        public int Limit
        {
            get { return (int)GetValue(LimitProperty); }
            set { SetValue(LimitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Limits.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LimitProperty =
            DependencyProperty.Register("Limit", typeof(int), typeof(LayPagination));

        /// <summary>
        /// 当前分页展示数量修饰内容
        /// </summary>
        public object LimitContent
        {
            get { return (object)GetValue(LimitContentProperty); }
            set { SetValue(LimitContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LimitContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LimitContentProperty =
            DependencyProperty.Register("LimitContent", typeof(object), typeof(LayPagination));


        /// <summary>
        /// 页面更新事件
        /// </summary>
        public event EventHandler<LayFunctionEventArgs<int>> PageUpdated
        {
            add => AddHandler(PageUpdatedEvent, value);
            remove => RemoveHandler(PageUpdatedEvent, value);
        }
        /// <summary>
        /// 页面更新事件
        /// </summary>
        public static readonly RoutedEvent PageUpdatedEvent =
            EventManager.RegisterRoutedEvent("PageUpdated", RoutingStrategy.Bubble, typeof(EventHandler<LayFunctionEventArgs<int>>), typeof(LayPagination));
    }
}
