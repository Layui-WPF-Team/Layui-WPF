using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace LayUI.Wpf.Tools
{
    public class LayGeometryHelper
    {
        private static string uri = "pack://application:,,,/LayUI.Wpf;component/Style/Default/Geometries.xaml";
        private static ResourceDictionary _Geometrys;
        public static ResourceDictionary Geometrys
        {
            get
            {
                if (_Geometrys == null)
                {
                    _Geometrys = new ResourceDictionary() { Source = new Uri(uri) };
                }
                return _Geometrys;
            }
        }
        public static Rect HelperDeflateRect(Rect rt, Thickness thick)
        {
            return new Rect(rt.Left + thick.Left, rt.Top + thick.Top, Math.Max(0.0, rt.Width - thick.Left - thick.Right), Math.Max(0.0, rt.Height - thick.Top - thick.Bottom));
        }
        public static void GenerateGeometry(StreamGeometryContext ctx, Rect rect, Radii radii)
        {
            Point point = new Point(radii.LeftTop, 0.0);
            Point point2 = new Point(rect.Width - radii.RightTop, 0.0);
            Point point3 = new Point(rect.Width, radii.TopRight);
            Point point4 = new Point(rect.Width, rect.Height - radii.BottomRight);
            Point point5 = new Point(rect.Width - radii.RightBottom, rect.Height);
            Point point6 = new Point(radii.LeftBottom, rect.Height);
            Point point7 = new Point(0.0, rect.Height - radii.BottomLeft);
            Point point8 = new Point(0.0, radii.TopLeft);
            if (point.X > point2.X)
            {
                double num3 = (point2.X = (point.X = radii.LeftTop / (radii.LeftTop + radii.RightTop) * rect.Width));
            }

            if (point3.Y > point4.Y)
            {
                double num6 = (point4.Y = (point3.Y = radii.TopRight / (radii.TopRight + radii.BottomRight) * rect.Height));
            }

            if (point5.X < point6.X)
            {
                double num9 = (point6.X = (point5.X = radii.LeftBottom / (radii.LeftBottom + radii.RightBottom) * rect.Width));
            }

            if (point7.Y < point8.Y)
            {
                double num12 = (point8.Y = (point7.Y = radii.TopLeft / (radii.TopLeft + radii.BottomLeft) * rect.Height));
            }

            Vector vector = new Vector(rect.TopLeft.X, rect.TopLeft.Y);
            point += vector;
            point2 += vector;
            point3 += vector;
            point4 += vector;
            point5 += vector;
            point6 += vector;
            point7 += vector;
            point8 += vector;
            ctx.BeginFigure(point, isFilled: true, isClosed: true);
            ctx.LineTo(point2, isStroked: true, isSmoothJoin: false);
            double num13 = rect.TopRight.X - point2.X;
            double num14 = point3.Y - rect.TopRight.Y;
            if (!DoubleUtil.IsZero(num13) || !DoubleUtil.IsZero(num14))
            {
                ctx.ArcTo(point3, new Size(num13, num14), 0.0, isLargeArc: false, SweepDirection.Clockwise, isStroked: true, isSmoothJoin: false);
            }

            ctx.LineTo(point4, isStroked: true, isSmoothJoin: false);
            num13 = rect.BottomRight.X - point5.X;
            num14 = rect.BottomRight.Y - point4.Y;
            if (!DoubleUtil.IsZero(num13) || !DoubleUtil.IsZero(num14))
            {
                ctx.ArcTo(point5, new Size(num13, num14), 0.0, isLargeArc: false, SweepDirection.Clockwise, isStroked: true, isSmoothJoin: false);
            }

            ctx.LineTo(point6, isStroked: true, isSmoothJoin: false);
            num13 = point6.X - rect.BottomLeft.X;
            num14 = rect.BottomLeft.Y - point7.Y;
            if (!DoubleUtil.IsZero(num13) || !DoubleUtil.IsZero(num14))
            {
                ctx.ArcTo(point7, new Size(num13, num14), 0.0, isLargeArc: false, SweepDirection.Clockwise, isStroked: true, isSmoothJoin: false);
            }

            ctx.LineTo(point8, isStroked: true, isSmoothJoin: false);
            num13 = point.X - rect.TopLeft.X;
            num14 = point8.Y - rect.TopLeft.Y;
            if (!DoubleUtil.IsZero(num13) || !DoubleUtil.IsZero(num14))
            {
                ctx.ArcTo(point, new Size(num13, num14), 0.0, isLargeArc: false, SweepDirection.Clockwise, isStroked: true, isSmoothJoin: false);
            }
        }
    }
}
