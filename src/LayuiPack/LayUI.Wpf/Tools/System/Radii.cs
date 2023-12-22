using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LayUI.Wpf.Tools
{
    public struct Radii
    {
        internal double LeftTop;

        internal double TopLeft;

        internal double TopRight;

        internal double RightTop;

        internal double RightBottom;

        internal double BottomRight;

        internal double BottomLeft;

        internal double LeftBottom;

        internal Radii(CornerRadius radii, Thickness borders, bool outer)
        {
            double num = 0.5 * borders.Left;
            double num2 = 0.5 * borders.Top;
            double num3 = 0.5 * borders.Right;
            double num4 = 0.5 * borders.Bottom;
            if (outer)
            {
                if (DoubleUtil.IsZero(radii.TopLeft))
                {
                    LeftTop = (TopLeft = 0.0);
                }
                else
                {
                    LeftTop = radii.TopLeft + num;
                    TopLeft = radii.TopLeft + num2;
                }

                if (DoubleUtil.IsZero(radii.TopRight))
                {
                    TopRight = (RightTop = 0.0);
                }
                else
                {
                    TopRight = radii.TopRight + num2;
                    RightTop = radii.TopRight + num3;
                }

                if (DoubleUtil.IsZero(radii.BottomRight))
                {
                    RightBottom = (BottomRight = 0.0);
                }
                else
                {
                    RightBottom = radii.BottomRight + num3;
                    BottomRight = radii.BottomRight + num4;
                }

                if (DoubleUtil.IsZero(radii.BottomLeft))
                {
                    BottomLeft = (LeftBottom = 0.0);
                    return;
                }

                BottomLeft = radii.BottomLeft + num4;
                LeftBottom = radii.BottomLeft + num;
            }
            else
            {
                LeftTop = Math.Max(0.0, radii.TopLeft - num);
                TopLeft = Math.Max(0.0, radii.TopLeft - num2);
                TopRight = Math.Max(0.0, radii.TopRight - num2);
                RightTop = Math.Max(0.0, radii.TopRight - num3);
                RightBottom = Math.Max(0.0, radii.BottomRight - num3);
                BottomRight = Math.Max(0.0, radii.BottomRight - num4);
                BottomLeft = Math.Max(0.0, radii.BottomLeft - num4);
                LeftBottom = Math.Max(0.0, radii.BottomLeft - num);
            }
        }
    }
}
