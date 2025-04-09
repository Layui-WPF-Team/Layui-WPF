using LayUI.Wpf.Event;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LayUI.Wpf.Controls
{
    public class LayPageIndicator : LayBindableBase
    {
        private int _JumpIndex;
        public int JumpIndex
        {
            get { return _JumpIndex; }
            set { SetProperty(ref _JumpIndex, value); }
        }
        private string _DisplayText;
        public string DisplayText
        {
            get { return _DisplayText; }
            set { SetProperty(ref _DisplayText, value); }
        }
        private bool _IsCurrent;
        public bool IsCurrent
        {
            get { return _IsCurrent; }
            set { SetProperty(ref _IsCurrent, value); }
        }
        public bool IsEnabled => JumpIndex > 0;

        public LayPageIndicator(int jumpIndex, string displayText, bool isCurrent)
        {
            JumpIndex = jumpIndex;
            DisplayText = displayText;
            IsCurrent = isCurrent;
        }
    }

    /// <summary>
    ///  分页插件
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-08-08 下午 4:49:46</para>
    /// </summary>
    [TemplatePart(Name = "PART_PrevButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_NextButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_ConfirmButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_FirstButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_LastButton", Type = typeof(Button))]
    public class LayPagination : System.Windows.Controls.Control
    {
        private bool _isUpdating;
        private int _lastRaisedPage = -1;
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
        private Button PART_FirstButton;
        /// <summary>
        /// 跳转最后一条按钮
        /// </summary>
        private Button PART_LastButton;
        #region 命令
        /// <summary>
        /// 跳转到第一页命令
        /// </summary>
        public ICommand FirstPageCommand { get; }
        /// <summary>
        /// 跳转到上一页命令
        /// </summary>
        public ICommand PreviousPageCommand { get; }
        /// <summary>
        /// 跳转到下一页命令
        /// </summary>
        public ICommand NextPageCommand { get; }
        /// <summary>
        /// 跳转到最后一页命令
        /// </summary>
        public ICommand LastPageCommand { get; }
        /// <summary>
        /// 提交数据命令
        /// </summary>
        public ICommand ConfirmCommand { get; }
        /// <summary>
        /// 跳到当前页命令
        /// </summary>
        public ICommand PageChangedCommand { get; }
        #endregion

        public LayPagination()
        {
            FirstPageCommand = new LayDelegateCommand(
                () => JumpIndex = 1,
                () => JumpIndex > 1);

            PreviousPageCommand = new LayDelegateCommand(
                () => JumpIndex--,
                () => JumpIndex > 1);

            NextPageCommand = new LayDelegateCommand(
                () => JumpIndex++,
                () => JumpIndex < PageCount);

            PageChangedCommand = new LayDelegateCommand<int>(
                index => JumpIndex = index);

            LastPageCommand = new LayDelegateCommand(
                () => JumpIndex = PageCount,
                () => JumpIndex < PageCount);

            ConfirmCommand = new LayDelegateCommand(RefreshData);
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

        [Bindable(true)]
        public object PageCountContent
        {
            get { return (object)GetValue(PageCountContentProperty); }
            set { SetValue(PageCountContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PageCountContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageCountContentProperty =
            DependencyProperty.Register("PageCountContent", typeof(object), typeof(LayPagination));


        public int PageCount => (int)GetValue(PageCountProperty);
        private static readonly DependencyPropertyKey PageCountPropertyKey =
            DependencyProperty.RegisterReadOnly("PageCount", typeof(int), typeof(LayPagination), new FrameworkPropertyMetadata(0, OnPageCountChanged));

        public static readonly DependencyProperty PageCountProperty =
            PageCountPropertyKey.DependencyProperty; 
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
            DependencyProperty.Register("JumpIndex", typeof(int), typeof(LayPagination), new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnJumpIndexChanged, CoerceJumpIndex), ValidateJumpIndex);
        // 添加验证逻辑
        private static bool ValidateJumpIndex(object value)
        {
            int page = (int)value;
            return page >= 1; // 过滤非法值输入
        }
        /// <summary>
        /// 页码指示器集合
        /// </summary>
        public IEnumerable<LayPageIndicator> Pages => (IEnumerable<LayPageIndicator>)GetValue(PagesProperty);

        public static readonly DependencyProperty PagesProperty =
            DependencyProperty.Register("Pages", typeof(IEnumerable<LayPageIndicator>), typeof(LayPagination)); 
          
        
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
            DependencyProperty.Register("Limit", typeof(int), typeof(LayPagination), new FrameworkPropertyMetadata(10, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnLimitChanged, CoerceLimit));

        private static object CoerceLimit(DependencyObject d, object value) => Math.Max(1, (int)value);

        /// <summary>
        /// 总数据条数
        /// </summary>
        public int Total
        {
            get => (int)GetValue(TotalProperty);
            set => SetValue(TotalProperty, value);
        }

        public static readonly DependencyProperty TotalProperty =
           DependencyProperty.Register("Total", typeof(int), typeof(LayPagination), new FrameworkPropertyMetadata(0, OnTotalChanged, CoerceTotal));
         
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

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_PrevButton = GetTemplateChild(nameof(PART_PrevButton)) as Button;
            PART_NextButton = GetTemplateChild(nameof(PART_NextButton)) as Button;
            PART_ConfirmButton = GetTemplateChild(nameof(PART_ConfirmButton)) as Button;
            PART_LastButton = GetTemplateChild(nameof(PART_LastButton)) as Button;
            PART_FirstButton = GetTemplateChild(nameof(PART_FirstButton)) as Button;
            UpdatePageCount();
            RefreshPageIndicators();
        }
        /// <summary>
        /// 页码总数改变时触发
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnPageCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (LayPagination)d;
            control.CoerceValue(JumpIndexProperty);
            control.UpdatePaginationState();
        }
        /// <summary>
        /// 页码改变时触发
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnJumpIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (LayPagination)d;
            control.UpdatePaginationState();
        }
        /// <summary>
        /// 页码大小改变时触发
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnLimitChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (LayPagination)d;
            control.UpdatePageCount();
            control.RefreshData();
        }
        /// <summary>
        /// 总数改变时触发
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnTotalChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (LayPagination)d;
            control.UpdatePageCount();
        } 
        #region 核心逻辑
        /// <summary>
        /// 更新页码总数
        /// </summary>
        private void UpdatePageCount()
        {
            var newCount = Total == 0 ? 0 : (int)Math.Ceiling(Total / (double)Limit);
            SetValue(PageCountPropertyKey, newCount);
        }
        /// <summary>
        /// 更新分页状态
        /// </summary>
        private void UpdatePaginationState()
        {
            if (_isUpdating || !IsLoaded) return;

            _isUpdating = true;
            try
            {
                // 确保页码合法性
                int newPage = ClampValue(JumpIndex, 1, PageCount);
                if (newPage != JumpIndex)
                {
                    SetCurrentValue(JumpIndexProperty, newPage);
                    return; // 页码被校正时重新进入更新流程
                }

                RefreshPageIndicators();
                UpdateCommandStates();
                RaisePageUpdated();
            }
            finally
            {
                _isUpdating = false;
            }
        }
        /// <summary>
        /// 刷新页码指示器
        /// </summary>
        private void RefreshPageIndicators()
        {
            var indicators = new List<LayPageIndicator>();

            if (PageCount == 0)
            {
                SetValue(PagesProperty, indicators);
                return;
            }

            const int gapSize = 2;
            int start = Math.Max(2, JumpIndex - gapSize);
            int end = Math.Min(PageCount - 1, JumpIndex + gapSize);

            indicators.Add(CreateIndicator(1));

            if (start > 2)
                indicators.Add(new LayPageIndicator(0, "...", false));

            for (int i = start; i <= end; i++)
                indicators.Add(CreateIndicator(i));

            if (end < PageCount - 1)
                indicators.Add(new LayPageIndicator(0, "...", false));

            if (PageCount > 1)
                indicators.Add(CreateIndicator(PageCount));

            SetValue(PagesProperty, indicators);
        }
        /// <summary>
        /// 创建页码指示器
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        private LayPageIndicator CreateIndicator(int pageNumber) => new LayPageIndicator(pageNumber, pageNumber.ToString(), pageNumber == JumpIndex);
        #endregion

        #region 辅助方法
        /// <summary>
        /// 限制页码范围
        /// </summary>
        /// <param name="d"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static object CoerceJumpIndex(DependencyObject d, object value)
        {
            var control = (LayPagination)d;
            int tryValue = (int)value;
            int maxPage = control.PageCount;

            // 处理无效数据情况
            if (maxPage <= 0)
            {
                return 1; // 默认返回首页
            }

            return ClampValue(tryValue, 1, maxPage);
        }
        // 仅适用于无符号整型的优化方案（不推荐实际使用）
        private static int ClampValue(int value, int min, int max)
        {
            // 处理无效范围（当 min > max 时自动交换）
            if (min > max)
            {
                int temp = min;
                min = max;
                max = temp;
            }

            return value < min ? min : (value > max ? max : value);
        }
        /// <summary>
        /// 强制总数为正数
        /// </summary>
        /// <param name="d"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static object CoerceTotal(DependencyObject d, object value) => Math.Max(0, (int)value);
        /// <summary>
        /// 更新命令状态
        /// </summary>
        private void UpdateCommandStates()
        {
            ((LayDelegateCommand)FirstPageCommand).RaiseCanExecuteChanged();
            ((LayDelegateCommand)PreviousPageCommand).RaiseCanExecuteChanged();
            ((LayDelegateCommand)NextPageCommand).RaiseCanExecuteChanged();
            ((LayDelegateCommand)LastPageCommand).RaiseCanExecuteChanged();
        }
        /// <summary>
        /// 刷新数据
        /// </summary>
        private void RefreshData()
        {
            if (_isUpdating || !IsLoaded) return;
            var args = new LayFunctionEventArgs<int>(PageUpdatedEvent, this)
            {
                Info = JumpIndex
            };

            RaiseEvent(args);
        }
        /// <summary>
        /// 触发页码更新事件
        /// </summary>
        private void RaisePageUpdated()
        {
            // 如果当前页码没有变化，则不触发事件
            if (_lastRaisedPage == JumpIndex) return;

            var args = new LayFunctionEventArgs<int>(PageUpdatedEvent, this)
            {
                Info = JumpIndex
            };

            RaiseEvent(args);
            // 更新最后触发的页码
            _lastRaisedPage = JumpIndex;

            // 强制更新绑定（针对某些MVVM框架的需要）
            CommandManager.InvalidateRequerySuggested();
        }
        #endregion
         
    }
}
