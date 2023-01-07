using LayUI.Wpf.Event;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LayUI.Wpf.Controls
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
        /// 总共页数
        /// </summary>
        [Bindable(true)]
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
        [Bindable(true)]
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
        [Bindable(true)]
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
        [Bindable(true)]
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
        [Bindable(true)]
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
        [Bindable(true)]
        public int PageCount
        {
            get { return (int)GetValue(PageCountProperty); }
            set { SetValue(PageCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PageCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageCountProperty =
            DependencyProperty.Register("PageCount", typeof(int), typeof(LayPagination), new PropertyMetadata(Refresh));
        /// <summary>
        /// 跳转修饰内容
        /// </summary>
        [Bindable(true)]
        public object JumpContent
        {
            get { return (object)GetValue(JumpContentProperty); }
            set { SetValue(JumpContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for JumpContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty JumpContentProperty =
            DependencyProperty.Register("JumpContent", typeof(object), typeof(LayPagination));
        /// <summary>
        /// 当前页数
        /// </summary>
        [Bindable(true)]
        public int PageIndex
        {
            get { return (int)GetValue(PageIndexProperty); }
            private set { SetValue(PageIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PageIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageIndexProperty =
            DependencyProperty.Register("PageIndex", typeof(int), typeof(LayPagination), new PropertyMetadata(1, OnPageIndexChanged, CoercePageIndex));

        private static void OnPageIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LayPagination pagination && e.NewValue is int value)
            {
                pagination.Refresh();
                pagination.JumpIndex = pagination.PageIndex;
            }
        }
        private static object CoercePageIndex(DependencyObject d, object baseValue)
        {
            if (d is LayPagination pagination)
            {
                var intValue = (int)baseValue;
                return intValue < 1 ? 1 : intValue > pagination.PageNum ? pagination.PageNum : intValue;
            }
            else return 1;

        }
        /// <summary>
        /// 跳转页数
        /// </summary>
        [Bindable(true)]
        public int JumpIndex
        {
            get { return (int)GetValue(JumpIndexProperty); }
            set { SetValue(JumpIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for JumpIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty JumpIndexProperty =
            DependencyProperty.Register("JumpIndex", typeof(int), typeof(LayPagination), new PropertyMetadata(1, null, OnJumpIndexChanged));

        private static object OnJumpIndexChanged(DependencyObject d, object baseValue)
        {
            if (d is LayPagination pagination)
            {
                var intValue = (int)baseValue;
                return intValue < 1 ? 1 : intValue> pagination.PageNum ? pagination.PageNum: intValue;
            }
            else return 1;
        }


        /// <summary>
        /// 是否禁用跳转
        /// </summary>
        [Bindable(true)]
        public bool IsJumpEnabled
        {
            get { return (bool)GetValue(IsJumpEnabledProperty); }
            set { SetValue(IsJumpEnabledProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsJumpEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsJumpEnabledProperty =
            DependencyProperty.Register("IsJumpEnabled", typeof(bool), typeof(LayPagination), new PropertyMetadata(false));



        /// <summary>
        /// 分页间隔
        /// </summary>
        [Bindable(true)]
        public int MaxPageInterval
        {
            get { return (int)GetValue(MaxPageIntervalProperty); }
            set { SetValue(MaxPageIntervalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxPageInterval.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxPageIntervalProperty =
            DependencyProperty.Register("MaxPageInterval", typeof(int), typeof(LayPagination), new PropertyMetadata(5, Refresh));
        /// <summary>
        /// 当前分页展示数量
        /// </summary>
        [Bindable(true)]
        public int Limit
        {
            get { return (int)GetValue(LimitProperty); }
            set { SetValue(LimitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Limits.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LimitProperty =
            DependencyProperty.Register("Limit", typeof(int), typeof(LayPagination), new PropertyMetadata(10, OnLimitChanged));

        private static void OnLimitChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LayPagination pagination && e.NewValue is int value)
            {
                pagination.Refresh();
                pagination.RaiseEvent(new LayFunctionEventArgs<int>(PageUpdatedEvent, pagination)
                {
                    Info = pagination.PageIndex
                });
            }
        }

        /// <summary>
        /// 当前分页展示数量修饰内容
        /// </summary>
        [Bindable(true)]
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
            //计算页数
            if (PageCount % Limit > 0) PageNum = PageCount / Limit + 1;
            else PageNum = PageCount / Limit;
            //设置上下一页按钮
            PART_PrevButton.IsEnabled = PageIndex > 1;
            PART_NextButton.IsEnabled = PageIndex < PageNum;
            //防呆
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
            //设置跳转页按钮效果
            PART_FirstButton.Visibility = Visibility.Visible;
            PART_LastButton.Visibility = Visibility.Visible;
            PART_MoreLeft.Visibility = Visibility.Visible;
            PART_MoreRight.Visibility = Visibility.Visible;

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
            PART_MoreRight.Visibility = right > MaxPageInterval ? Visibility.Visible : Visibility.Collapsed;
            PART_MoreLeft.Visibility = left > MaxPageInterval ? Visibility.Visible : Visibility.Collapsed;

            //更新中间部分
            PART_ItemPanel.Children.Clear();
            if (PageIndex > 1 && PageIndex < PageNum)
            {
                var selectButton = CreateButton(PageIndex);
                PART_ItemPanel.Children.Add(selectButton);
                selectButton.IsChecked = true;
            }
            else if (PageIndex == 1)
            {
                PART_FirstButton.IsChecked = true;
            }
            else
            {
                PART_LastButton.IsChecked = true;
            }

            var sub = PageIndex;
            for (var i = 0; i < MaxPageInterval - 1; i++)
            {
                if (--sub > 1)
                {
                    PART_ItemPanel.Children.Insert(0, CreateButton(sub));
                }
                else
                {
                    break;
                }
            }
            var add = PageIndex;
            for (var i = 0; i < MaxPageInterval - 1; i++)
            {
                if (++add < PageNum)
                {
                    PART_ItemPanel.Children.Add(CreateButton(add));
                }
                else
                {
                    break;
                }
            }

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
            PART_ConfirmButton = GetTemplateChild("PART_ConfirmButton") as Button;
            PART_FirstButton = GetTemplateChild("PART_FirstButton") as RadioButton;
            PART_LastButton = GetTemplateChild("PART_LastButton") as RadioButton;
            PART_ItemPanel = GetTemplateChild("PART_ItemPanel") as Panel;
            PART_MoreLeft = GetTemplateChild("PART_MoreLeft") as Border;
            PART_MoreRight = GetTemplateChild("PART_MoreRight") as Border;
            if (PART_PrevButton == null || PART_NextButton == null ||
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
                PART_ConfirmButton.Click -= PART_ConfirmButton_Click;
                PART_ConfirmButton.Click += PART_ConfirmButton_Click;
            }
            AppliedTemplate = true;
            Refresh();
        }
        /// <summary>
        /// 跳转目标页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PART_ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            PageIndex = JumpIndex;
            RaiseEvent(new LayFunctionEventArgs<int>(PageUpdatedEvent, this)
            {
                Info = JumpIndex
            });

        }
        /// <summary>
        /// 选中当前目标页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is RadioButton button)
            {
                if (button.IsChecked == false) return;
                PageIndex = int.Parse(button.Content.ToString());
                RaiseEvent(new LayFunctionEventArgs<int>(PageUpdatedEvent, this)
                {
                    Info = PageIndex
                });
            }
        }
        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PART_NextButton_Click(object sender, RoutedEventArgs e)
        {
            PageIndex++;
            JumpIndex = PageIndex;
            RaiseEvent(new LayFunctionEventArgs<int>(PageUpdatedEvent, this)
            {
                Info = PageIndex
            });
        }

        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PART_PrevButton_Click(object sender, RoutedEventArgs e)
        {
            PageIndex--;
            JumpIndex = PageIndex;
            RaiseEvent(new LayFunctionEventArgs<int>(PageUpdatedEvent, this)
            {
                Info = PageIndex
            });
        }
    }
}
