using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace LayUI.Wpf.Controls
{
    /// <summary>
    /// 瀑布流
    /// </summary>
    public class LayWaterfallPanel : Panel
    {
        static LayWaterfallPanel()
        {
            // 覆盖默认样式键，以应用特定于 WaterfallPanel 的样式
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LayWaterfallPanel), new FrameworkPropertyMetadata(typeof(LayWaterfallPanel)));
        }

        [Category("外观")]
        [Description("项宽度")]
        /// <summary>
        /// 项宽度
        /// </summary>
        public double ColumnWidth
        {
            get { return (double)GetValue(ColumnWidthProperty); }
            set { SetValue(ColumnWidthProperty, value); }
        }

        // ColumnWidth 的 DependencyProperty，允许绑定、样式等
        public static readonly DependencyProperty ColumnWidthProperty =
            DependencyProperty.Register("ColumnWidth", typeof(double), typeof(LayWaterfallPanel), new PropertyMetadata(180.0, Refresh));

        [Category("自动化")]
        [Description("自动计算列数")]
        /// <summary>
        /// 自动计算列数
        /// </summary>
        public bool AutoColumns
        {
            get { return (bool)GetValue(AutoColumnsProperty); }
            set { SetValue(AutoColumnsProperty, value); }
        }

        // AutoColumns 的 DependencyProperty，允许绑定、样式等
        public static readonly DependencyProperty AutoColumnsProperty =
            DependencyProperty.Register("AutoColumns", typeof(bool), typeof(LayWaterfallPanel), new PropertyMetadata(false, Refresh));

        [Category("布局")]
        [Description("列数")]
        /// <summary>
        /// 列数
        /// </summary>
        public int Columns
        {
            get { return (int)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        // Columns 的 DependencyProperty，允许绑定、样式等
        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.Register("Columns", typeof(int), typeof(LayWaterfallPanel), new PropertyMetadata(4, Refresh));

        [Category("布局")]
        [Description("间距")]
        /// <summary>
        /// 间距
        /// </summary>
        public double Spacing
        {
            get { return (double)GetValue(SpacingProperty); }
            set { SetValue(SpacingProperty, value); }
        }

        // Spacing 的 DependencyProperty，允许绑定、样式等
        public static readonly DependencyProperty SpacingProperty =
            DependencyProperty.Register("Spacing", typeof(double), typeof(LayWaterfallPanel), new PropertyMetadata(2.0, Refresh));

        /// <summary>
        /// 属性值变化时，使测量无效的方法
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void Refresh(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LayWaterfallPanel waterfall)
            {
                // 使测量无效以触发布局更新
                waterfall.InvalidateMeasure();
                waterfall.InvalidateArrange();
            }
        }

        /// <summary>
        /// 方法用于确定面板的期望大小
        /// </summary>
        /// <param name="availableSize"></param>
        /// <returns></returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            // 根据可用宽度自动计算列数
            if (AutoColumns) Columns = (int)Math.Floor(availableSize.Width / ColumnWidth);
            // 存储面板的期望大小
            var panelDesiredSize = new Size(0, 0);
            // 列数取值范围
            int columns = Columns <= 0 ? 1 : Columns;
            // 数组存储每列的高度
            var columnHeights = new double[columns];
            // 计算每列的宽度，考虑间距
            double d_Width = (availableSize.Width - (columns - 1) * Spacing) / columns;
            // 防止宽度为负数
            if (d_Width <= 0) d_Width = 0;
            // 子元素索引
            int index = 0;
            try
            {
                // 遍历每个子元素
                foreach (FrameworkElement child in InternalChildren.OfType<FrameworkElement>())
                {
                    // 如果子元素数量大于等于InternalChildren.Count，则跳出循环
                    if (index >= InternalChildren.Count) break;
                    // 测量子元素的大小
                    child.Measure(new Size(d_Width, availableSize.Height));
                    // 找到最小高度的列
                    var minIndex = Array.IndexOf(columnHeights, columnHeights.Min());
                    // 计算子元素的 X 和 Y 位置
                    var x = minIndex * (d_Width + Spacing);
                    var y = columnHeights[minIndex];
                    // 更新放置子元素的列的高度
                    columnHeights[minIndex] += child.DesiredSize.Height + Spacing;
                    // 更新面板的期望大小
                    panelDesiredSize.Width = Math.Max(panelDesiredSize.Width, x + d_Width);
                    panelDesiredSize.Height = Math.Max(panelDesiredSize.Height, y + child.DesiredSize.Height);
                    // 子元素索引加一
                    index++;
                }
            }
            catch { }
            // 返回面板的期望大小
            return panelDesiredSize;
        }
        protected override Size ArrangeOverride(Size finalSize)
        {
            // 根据可用宽度自动计算列数
            if (AutoColumns) Columns = (int)Math.Floor(finalSize.Width / ColumnWidth);
            // 列数取值范围
            int columns = Columns <= 0 ? 1 : Columns;
            // 数组存储每列的高度
            var columnHeights = new double[columns];
            // 计算每列的宽度，考虑间距
            double d_Width = (finalSize.Width - (columns - 1) * Spacing) / columns;
            // 防止宽度为负数
            if (d_Width <= 0) d_Width = 0;
            // 子元素索引
            int index = 0;
            try
            {
                // 遍历每个子元素
                foreach (FrameworkElement child in InternalChildren.OfType<FrameworkElement>())
                {
                    // 如果子元素数量大于等于InternalChildren.Count，则跳出循环
                    if (index >= InternalChildren.Count) break;
                    // 子元素的大小
                    var c_size = new Size(d_Width, child.DesiredSize.Height);
                    // 找到最小高度的列
                    var minIndex = Array.IndexOf(columnHeights, columnHeights.Min());
                    // 计算子元素的 X 和 Y 位置
                    var x = minIndex * (d_Width + Spacing);
                    var y = columnHeights[minIndex];
                    // 在面板中安排子元素
                    child.Arrange(new Rect(new Point(x, y), c_size));
                    // 更新放置子元素的列的高度
                    columnHeights[minIndex] += child.DesiredSize.Height + Spacing;
                    // 子元素索引加一
                    index++;
                }
            }
            catch { }
            return finalSize;
        }
    }
}
