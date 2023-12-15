using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LayUI.Wpf.Controls
{
    //代码照抄Ursa控件库代码
    /// <summary>
    /// 标签输入框容器 
    /// </summary>
    public class LayTagTextBoxPanel : Panel
    { 
        private bool AreClose(double value1, double value2)
        { 
            if (value1 == value2) return true;
            double eps = (Math.Abs(value1) + Math.Abs(value2) + 10.0) * 2.2204460492503131e-016;
            double delta = value1 - value2;
            return (-eps < delta) && (eps > delta);
        }
        private bool GreaterThan(double value1, double value2)
        {
            return (value1 > value2) && !AreClose(value1, value2);
        }
        protected override Size MeasureOverride(Size availableSize)
        { 
            double currentLineX = 0;
            double currentLineHeight = 0;
            double totalHeight = 0;

            var children = Children;
            for (int i = 0; i < children.Count - 1; i++)
            {
                var child = children[i];
                child.Measure(availableSize);
                double deltaX = availableSize.Width - currentLineX; 
                if (GreaterThan(deltaX, child.DesiredSize.Width))
                {
                    currentLineX += child.DesiredSize.Width;
                    currentLineHeight = Math.Max(currentLineHeight, child.DesiredSize.Height);
                }
                else
                {
                    currentLineX = child.DesiredSize.Width;
                    totalHeight += currentLineHeight;
                    currentLineHeight = child.DesiredSize.Height;
                }
            }

            var last = children[children.Count - 1];
            last.Measure(availableSize);
            double lastDeltaX = availableSize.Width - currentLineX;
            if (lastDeltaX < 30)
            {
                totalHeight += currentLineHeight;
                totalHeight += last.DesiredSize.Height;
            }
            else
            {
                currentLineHeight = Math.Max(currentLineHeight, last.DesiredSize.Height);
                totalHeight += currentLineHeight;
            }

            return new Size(availableSize.Width, totalHeight);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double currentLineX = 0;
            double currentLineHeight = 0;
            double totalHeight = 0;
            var children = Children;
            for (int i = 0; i < children.Count - 1; i++)
            {
                var child = children[i];
                double deltaX = finalSize.Width - currentLineX; 
                if (GreaterThan(deltaX, child.DesiredSize.Width))
                {
                    child.Arrange(new Rect(currentLineX, totalHeight, child.DesiredSize.Width, Math.Max(child.DesiredSize.Height, currentLineHeight)));
                    currentLineX += child.DesiredSize.Width;
                    currentLineHeight = Math.Max(currentLineHeight, child.DesiredSize.Height);
                }
                else
                {
                    totalHeight += currentLineHeight;
                    child.Arrange(new Rect(0, totalHeight, Math.Min(child.DesiredSize.Width, finalSize.Width), child.DesiredSize.Height));
                    currentLineX = child.DesiredSize.Width;
                    currentLineHeight = child.DesiredSize.Height;
                }
            }

            var last = children[children.Count - 1];
            double lastDeltaX = finalSize.Width - currentLineX;
            if (lastDeltaX < 10)
            {
                totalHeight += currentLineHeight;
                last.Arrange(new Rect(0, totalHeight, finalSize.Width, last.DesiredSize.Height));
                totalHeight += last.DesiredSize.Height;
            }
            else
            {
                currentLineHeight = children.Count == 1 ? finalSize.Height : currentLineHeight;
                last.Arrange(new Rect(currentLineX, totalHeight, lastDeltaX,
                    Math.Max(currentLineHeight, last.DesiredSize.Height)));
                currentLineHeight = Math.Max(currentLineHeight, last.DesiredSize.Height);
                totalHeight += currentLineHeight;
            }

            return new Size(finalSize.Width, totalHeight);
        }
    }
}
