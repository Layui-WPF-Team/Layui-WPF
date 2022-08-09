using LayuiTemplate.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LayuiTemplate.Control
{
    /// <summary>
    ///  分页插件
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-08-08 下午 4:49:46</para>
    /// </summary>
    [TemplatePart(Name = "PART_PrevButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_ItemPanel", Type = typeof(Panel))]
    [TemplatePart(Name = "PART_MoreRight", Type = typeof(Border))]
    [TemplatePart(Name = "PART_MoreLeft", Type = typeof(Border))]
    [TemplatePart(Name = "PART_NextButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_RefreshButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_ConfirmButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_FirstButton", Type = typeof(RadioButton))]
    [TemplatePart(Name = "PART_LastButton", Type = typeof(RadioButton))]
    public class LayPagination : System.Windows.Controls.Control
    {
        /// <summary>
        /// 上一页按钮
        /// </summary>
        private Button PART_PrevButton;
        /// <summary>
        /// 下一页按钮
        /// </summary>
        private Button PART_NextButton;
        /// <summary>
        /// 刷新当前页按钮
        /// </summary>
        private Button PART_RefreshButton;
        /// <summary>
        /// 跳转目标页按钮
        /// </summary>
        private Button PART_ConfirmButton;
        /// <summary>
        /// 跳转第一条按钮
        /// </summary>
        private RadioButton PART_FirstButton;
        /// <summary>
        /// 跳转最后一条按钮
        /// </summary>
        private RadioButton PART_LastButton;
        /// <summary>
        /// 可视化目标页按钮容器
        /// </summary>
        private Panel PART_ItemPanel;
        /// <summary>
        /// 左边省略号
        /// </summary>
        private Border PART_MoreLeft;
        /// <summary>
        /// 右边省略号
        /// </summary>
        private Border PART_MoreRight;
        /// <summary>
        /// 用于记录每个单选按钮唯一选择性
        /// </summary>
        private string GroupName = Guid.NewGuid().ToString();
        /// <summary>
        /// 编辑模板添加状态
        /// </summary>
        private bool AppliedTemplate;
        /// <summary>
        /// 刷新按钮内容
        /// </summary>
        public object RefreshButtonContent
        {
            get { return (object)GetValue(RefreshButtonContentProperty); }
            set { SetValue(RefreshButtonContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RefreshButtonContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RefreshButtonContentProperty =
            DependencyProperty.Register("RefreshButtonContent", typeof(object), typeof(LayPagination));
        /// <summary>
        /// 总共页数
        /// </summary>
        public int PageNum
        {
            get { return (int)GetValue(PageNumProperty); }
            private set { SetValue(PageNumProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PageNum.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageNumProperty =
            DependencyProperty.Register("PageNum", typeof(int), typeof(LayPagination), new PropertyMetadata(Int32.Parse("1"), OnPageNumChanged, CoerceMaxPageCount));
        private static object CoerceMaxPageCount(DependencyObject d, object basevalue)
        {
            var intValue = (int)basevalue;
            return intValue < 1 ? 1 : intValue;
        }
        private static void OnPageNumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LayPagination pagination)
            {
                if (pagination.PageIndex > pagination.PageNum)
                {
                    pagination.PageIndex = pagination.PageNum;
                }

                pagination.CoerceValue(PageIndexProperty);
                pagination.Refresh();
            }
        }

        /// <summary>
        /// 上一页按钮内容
        /// </summary>
        public object PrevButtonContent
        {
            get { return (object)GetValue(PrevButtonContentProperty); }
            set { SetValue(PrevButtonContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PrevContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PrevButtonContentProperty =
            DependencyProperty.Register("PrevButtonContent", typeof(object), typeof(LayPagination));
        /// <summary>
        /// 下一页按钮内容
        /// </summary>
        public object NextButtonContent
        {
            get { return (object)GetValue(NextButtonContentProperty); }
            set { SetValue(NextButtonContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NextContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NextButtonContentProperty =
            DependencyProperty.Register("NextButtonContent", typeof(object), typeof(LayPagination));


        /// <summary>
        /// 确认按钮修饰内容
        /// </summary>
        public object ConfirmButtonContent
        {
            get { return (object)GetValue(ConfirmButtonContentProperty); }
            set { SetValue(ConfirmButtonContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ConfirmContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConfirmButtonContentProperty =
            DependencyProperty.Register("ConfirmButtonContent", typeof(object), typeof(LayPagination));

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
            DependencyProperty.Register("PageCount", typeof(int), typeof(LayPagination), new PropertyMetadata(Refresh));
        /// <summary>
        /// 当前页数修饰内容
        /// </summary>
        public object PageIndexContent
        {
            get { return (object)GetValue(PageIndexContentProperty); }
            set { SetValue(PageIndexContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PageIndexContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageIndexContentProperty =
            DependencyProperty.Register("PageIndexContent", typeof(object), typeof(LayPagination));
        /// <summary>
        /// 当前页数
        /// </summary>
        public int PageIndex
        {
            get { return (int)GetValue(PageIndexProperty); }
            set { SetValue(PageIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PageIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageIndexProperty =
            DependencyProperty.Register("PageIndex", typeof(int), typeof(LayPagination), new PropertyMetadata(Refresh));
        /// <summary>
        /// 分页间隔
        /// </summary>
        public int MaxPageInterval
        {
            get { return (int)GetValue(MaxPageIntervalProperty); }
            set { SetValue(MaxPageIntervalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxPageInterval.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxPageIntervalProperty =
            DependencyProperty.Register("MaxPageInterval", typeof(int), typeof(LayPagination), new PropertyMetadata(Refresh));


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
            DependencyProperty.Register("Limit", typeof(int), typeof(LayPagination), new PropertyMetadata(Refresh));

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
        /// 刷新样式
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void Refresh(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LayPagination pagination)
            {
                pagination.Refresh();
            }
        }
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
        /// <summary>
        /// 刷新需要显示分页的条数
        /// </summary>
        private void Refresh()
        {
            if (!AppliedTemplate) return;
            if (MaxPageInterval == 0)
            {
                PART_FirstButton.Visibility = Visibility.Collapsed;
                PART_LastButton.Visibility = Visibility.Collapsed;
                PART_MoreLeft.Visibility = Visibility.Collapsed;
                PART_MoreRight.Visibility = Visibility.Collapsed;
                PART_ItemPanel.Children.Clear();
                var selectButton = CreateButton(PageIndex);
                PART_ItemPanel.Children.Add(selectButton);
                selectButton.IsChecked = true;
                return;
            }
            else
            {
                PART_FirstButton.Visibility = Visibility.Visible;
                PART_LastButton.Visibility = Visibility.Visible;
                PART_MoreLeft.Visibility = Visibility.Visible;
                PART_MoreRight.Visibility = Visibility.Visible;
            }
            //更新最后一页
            if (PageNum == 1)
            {
                PART_LastButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                PART_LastButton.Visibility = Visibility.Visible;
                PART_LastButton.Content = PageNum.ToString();
            }
            //更新省略号
            var right = PageNum - PageIndex;
            var left = PageIndex - 1;
            PART_MoreLeft.Visibility = right > MaxPageInterval ? Visibility.Visible : Visibility.Collapsed;
            PART_MoreRight.Visibility= left > MaxPageInterval ? Visibility.Visible : Visibility.Collapsed;
            PART_FirstButton.GroupName = GroupName;
            PART_LastButton.GroupName = GroupName;
        }
        /// <summary>
        /// 创建目标跳转页按钮
        /// </summary>
        /// <param name="page">当前页</param>
        /// <returns></returns>
        private RadioButton CreateButton(int page)
        {
            RadioButton radioButton = new RadioButton()
            {
                Content = page.ToString(),
                GroupName = GroupName
            };
            radioButton.Click -= RadioButton_Click;
            radioButton.Click += RadioButton_Click;
            return radioButton;
        }

        public override void OnApplyTemplate()
        {
            AppliedTemplate = false;
            base.OnApplyTemplate();
            PART_PrevButton = GetTemplateChild("PART_PrevButton") as Button;
            PART_NextButton = GetTemplateChild("PART_NextButton") as Button;
            PART_RefreshButton = GetTemplateChild("PART_RefreshButton") as Button;
            PART_ConfirmButton = GetTemplateChild("PART_ConfirmButton") as Button;
            PART_FirstButton = GetTemplateChild("PART_FirstButton") as RadioButton;
            PART_LastButton = GetTemplateChild("PART_LastButton") as RadioButton;
            PART_ItemPanel = GetTemplateChild("PART_ItemPanel") as Panel;
            PART_MoreLeft = GetTemplateChild("PART_MoreLeft") as Border;
            PART_MoreRight = GetTemplateChild("PART_MoreRight") as Border;
            if (PART_PrevButton == null || PART_NextButton == null || PART_RefreshButton == null ||
            PART_ConfirmButton == null || PART_ItemPanel == null || PART_FirstButton == null
            || PART_LastButton == null || PART_MoreLeft == null || PART_MoreRight == null) throw new Exception();
            else
            {
                PART_PrevButton.Click -= PART_PrevButton_Click;
                PART_PrevButton.Click += PART_PrevButton_Click;
                PART_NextButton.Click -= PART_NextButton_Click;
                PART_NextButton.Click += PART_NextButton_Click;
                PART_FirstButton.Click -= RadioButton_Click;
                PART_FirstButton.Click += RadioButton_Click;
                PART_LastButton.Click -= RadioButton_Click;
                PART_LastButton.Click += RadioButton_Click;
            }
            AppliedTemplate = true;
            Refresh();
        }


        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is RadioButton button)
            {
                if (button.IsChecked == false) return;
                PageIndex = int.Parse(button.Content.ToString());
            }
        }

        private void PART_NextButton_Click(object sender, RoutedEventArgs e)
        {
            PageIndex++;
        }

        private void PART_PrevButton_Click(object sender, RoutedEventArgs e)
        {
            PageIndex--;
        }
    }
}
