using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using LayUI.Wpf.Tools;

namespace LayUI.Wpf.Controls
{
    /// <summary>
    ///  这是一个支持圆角切割
    /// </summary>
    public class LayBorder : System.Windows.Controls.Border
    {
        /// <summary>
        /// 缓存背景的几何图形
        /// </summary>
        private StreamGeometry _backgroundGeometryCache;
        /// <summary>
        /// 缓存边框的几何图形
        /// </summary>
        private StreamGeometry _borderGeometryCache;

        /// <summary>
        /// 更新边框的期望大小（DesiredSize）。由父UIElement调用。这是布局的第一步。
        /// </summary>
        /// <remarks>
        /// 边框根据指定的子元素的边框、大小属性、边距和请求的大小来确定其所需的期望大小。
        /// </remarks>
        /// <param name="constraint">约束大小是返回值不应超过的“上限”。</param>
        /// <returns>装饰器所需的大小。</returns>
        protected override Size MeasureOverride(Size constraint)
        {
            var child = this.Child;
            var desiredSize = new Size();
            var borders = this.BorderThickness;

            // 计算所需的总大小
            var borderSize = borders.CollapseThickness();
            var paddingSize = this.Padding.CollapseThickness();

            // 判断边框是否有子元素
            if (child != null)
            {
                // 合并边框和填充的总装饰大小
                var combined = new Size(borderSize.Width + paddingSize.Width, borderSize.Height + paddingSize.Height);

                // 从子元素的参考大小中去除边框大小。
                var childConstraint = new Size(Math.Max(0.0, constraint.Width - combined.Width),
                                               Math.Max(0.0, constraint.Height - combined.Height));

                child.Measure(childConstraint);
                var childSize = child.DesiredSize;

                // 使用返回的大小驱动我们的大小，加回边距等。
                desiredSize.Width = childSize.Width + combined.Width;
                desiredSize.Height = childSize.Height + combined.Height;
            }
            else
            {
                // 由于没有子元素，边框仅需要占用边框厚度和填充的大小
                desiredSize = new Size(borderSize.Width + paddingSize.Width, borderSize.Height + paddingSize.Height);
            }

            return desiredSize;
        }

        /// <summary>
        /// 边框计算其单个子元素的位置并应用子元素的对齐方式。
        /// </summary>
        /// <param name="finalSize">父元素为此元素预留的大小。</param>
        /// <returns>元素的实际绘制区域，通常与finalSize相同。</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            var borders = this.BorderThickness;
            var boundRect = new Rect(finalSize);
            var innerRect = boundRect.Deflate(borders);
            var corners = this.CornerRadius;
            var padding = this.Padding;
            var childRect = innerRect.Deflate(padding);

            // 计算边框的渲染几何图形
            if (!boundRect.Width.IsZero() && !boundRect.Height.IsZero())
            {
                var outerBorderInfo = new BorderInfo(corners, borders, new Thickness(), true);
                var borderGeometry = new StreamGeometry();

                using (var ctx = borderGeometry.Open())
                {
                    GenerateGeometry(ctx, boundRect, outerBorderInfo);
                }

                // 冻结几何图形以提高性能
                borderGeometry.Freeze();
                this._borderGeometryCache = borderGeometry;
            }
            else
            {
                this._borderGeometryCache = null;
            }

            // 计算背景的渲染几何图形
            if (!innerRect.Width.IsZero() && !innerRect.Height.IsZero())
            {
                var innerBorderInfo = new BorderInfo(corners, borders, new Thickness(), false);
                var backgroundGeometry = new StreamGeometry();

                using (var ctx = backgroundGeometry.Open())
                {
                    GenerateGeometry(ctx, innerRect, innerBorderInfo);
                }

                // 冻结几何图形以提高性能
                backgroundGeometry.Freeze();
                this._backgroundGeometryCache = backgroundGeometry;
            }
            else
            {
                this._backgroundGeometryCache = null;
            }

            // 布置子元素并设置其剪辑区域
            var child = this.Child;
            if (child != null)
            {
                child.Arrange(childRect);
                // 计算剪辑几何图形
                var clipGeometry = new StreamGeometry();
                var childBorderInfo = new BorderInfo(corners, borders, padding, false);
                using (var ctx = clipGeometry.Open())
                {
                    GenerateGeometry(ctx, new Rect(0, 0, childRect.Width, childRect.Height), childBorderInfo);
                }

                // 冻结几何图形以提高性能
                clipGeometry.Freeze();
                // 将剪辑应用于子元素
                child.Clip = clipGeometry;
            }

            return finalSize;
        }

        /// <summary>
        /// 在此处渲染边框的子元素、边框和背景。
        /// </summary>
        /// <param name="dc">绘图上下文</param>
        protected override void OnRender(DrawingContext dc)
        {
            var borders = this.BorderThickness;
            var borderBrush = this.BorderBrush;
            var bgBrush = this.Background;
            var borderGeometry = this._borderGeometryCache;
            var backgroundGeometry = this._backgroundGeometryCache;
            // 如果边框和背景都有效
            if ((borderBrush != null) && (!borders.IsZero()) && (bgBrush != null))
            {
                // 如果背景画刷和边框画刷相同，只需绘制填充的边框几何图形
                if (borderBrush.IsEqualTo(bgBrush))
                {
                    dc.DrawGeometry(borderBrush, null, borderGeometry);
                }
                // 如果它们都是不透明的纯色画刷，先用边框画刷绘制边框几何图形，
                // 再用背景画刷绘制背景几何图形
                else if (borderBrush.IsOpaqueSolidColorBrush() && bgBrush.IsOpaqueSolidColorBrush())
                {
                    dc.DrawGeometry(borderBrush, null, borderGeometry);
                    dc.DrawGeometry(bgBrush, null, backgroundGeometry);
                }
                // 如果只有边框是不透明的，先用背景画刷填充边框几何图形，
                // 然后只绘制边框轮廓几何图形（通过从边框几何图形中排除背景几何图形获得）用边框画刷。
                // 这将防止在渲染时边框和背景之间出现空隙。
                else if (borderBrush.IsOpaqueSolidColorBrush())
                {
                    if ((borderGeometry == null) || (backgroundGeometry == null))
                    {
                        return;
                    }

                    var borderOutlinePath = borderGeometry.GetOutlinedPathGeometry();
                    var backgroundOutlinePath = backgroundGeometry.GetOutlinedPathGeometry();
                    var borderOutlineGeometry = Geometry.Combine(borderOutlinePath, backgroundOutlinePath,
                                                                 GeometryCombineMode.Exclude, null);

                    dc.DrawGeometry(bgBrush, null, borderGeometry);
                    dc.DrawGeometry(borderBrush, null, borderOutlineGeometry);
                }
                // 如果不符合上述任何情况，则表示边框和背景必须分别绘制。
                // 这可能会导致边框和背景之间出现小缝隙。
                // 分别用各自的画刷绘制边框轮廓几何图形和背景几何图形。
                else
                {
                    if ((borderGeometry == null) || (backgroundGeometry == null))
                    {
                        return;
                    }

                    var borderOutlinePath = borderGeometry.GetOutlinedPathGeometry();
                    var backgroundOutlinePath = backgroundGeometry.GetOutlinedPathGeometry();
                    var borderOutlineGeometry = Geometry.Combine(borderOutlinePath, backgroundOutlinePath,
                                                                 GeometryCombineMode.Exclude, null);

                    dc.DrawGeometry(borderBrush, null, borderOutlineGeometry);
                    dc.DrawGeometry(bgBrush, null, backgroundGeometry);
                }

                return;
            }

            // 仅边框有效
            if ((borderBrush != null) && (!borders.IsZero()))
            {
                if ((borderGeometry != null) && (backgroundGeometry != null))
                {
                    var borderOutlinePath = borderGeometry.GetOutlinedPathGeometry();
                    var backgroundOutlinePath = backgroundGeometry.GetOutlinedPathGeometry();
                    var borderOutlineGeometry = Geometry.Combine(borderOutlinePath, backgroundOutlinePath,
                                                                 GeometryCombineMode.Exclude, null);

                    dc.DrawGeometry(borderBrush, null, borderOutlineGeometry);
                }
                else
                {
                    dc.DrawGeometry(borderBrush, null, borderGeometry);
                }
            }

            // 仅背景有效
            if (bgBrush != null)
            {
                dc.DrawGeometry(bgBrush, null, backgroundGeometry);
            }
        }


        /// <summary>
        /// 生成一个StreamGeometry。
        /// </summary>
        /// <param name="ctx">一个已经打开的StreamGeometryContext。</param>
        /// <param name="rect">用于几何转换的矩形。</param>
        /// <param name="borderInfo">需要用于创建几何图形的边界核心点。</param>
        /// <returns>生成的几何图形。</returns>
        private static void GenerateGeometry(StreamGeometryContext ctx, Rect rect, BorderInfo borderInfo)
        {
            // 计算关键点的坐标
            var leftTop = new Point(borderInfo.LeftTop, 0);
            var rightTop = new Point(rect.Width - borderInfo.RightTop, 0);
            var topRight = new Point(rect.Width, borderInfo.TopRight);
            var bottomRight = new Point(rect.Width, rect.Height - borderInfo.BottomRight);
            var rightBottom = new Point(rect.Width - borderInfo.RightBottom, rect.Height);
            var leftBottom = new Point(borderInfo.LeftBottom, rect.Height);
            var bottomLeft = new Point(0, rect.Height - borderInfo.BottomLeft);
            var topLeft = new Point(0, borderInfo.TopLeft);

            // 检查关键点是否重叠，并通过根据每个角的比例划分解决重叠问题。

            // 顶边
            if (leftTop.X > rightTop.X)
            {
                var v = (borderInfo.LeftTop) / (borderInfo.LeftTop + borderInfo.RightTop) * rect.Width;
                leftTop.X = v;
                rightTop.X = v;
            }

            // 右边
            if (topRight.Y > bottomRight.Y)
            {
                var v = (borderInfo.TopRight) / (borderInfo.TopRight + borderInfo.BottomRight) * rect.Height;
                topRight.Y = v;
                bottomRight.Y = v;
            }

            // 底边
            if (leftBottom.X > rightBottom.X)
            {
                var v = (borderInfo.LeftBottom) / (borderInfo.LeftBottom + borderInfo.RightBottom) * rect.Width;
                rightBottom.X = v;
                leftBottom.X = v;
            }

            // 左边
            if (topLeft.Y > bottomLeft.Y)
            {
                var v = (borderInfo.TopLeft) / (borderInfo.TopLeft + borderInfo.BottomLeft) * rect.Height;
                bottomLeft.Y = v;
                topLeft.Y = v;
            }

            // 应用偏移量
            var offsetX = rect.TopLeft.X;
            var offsetY = rect.TopLeft.Y;
            var offset = new Vector(offsetX, offsetY);
            leftTop += offset;
            rightTop += offset;
            topRight += offset;
            bottomRight += offset;
            rightBottom += offset;
            leftBottom += offset;
            bottomLeft += offset;
            topLeft += offset;

            // 创建边界几何图形
            ctx.BeginFigure(leftTop, true /* is filled */, true /* is closed */);

            // 顶边线
            ctx.LineTo(rightTop, true /* is stroked */, false /* is smooth join */);

            // 右上角
            var radiusX = rect.TopRight.X - rightTop.X;
            var radiusY = topRight.Y - rect.TopRight.Y;
            if (!radiusX.IsZero() || !radiusY.IsZero())
            {
                ctx.ArcTo(topRight, new Size(radiusX, radiusY), 0, false, SweepDirection.Clockwise, true, false);
            }

            // 右边线
            ctx.LineTo(bottomRight, true /* is stroked */, false /* is smooth join */);

            // 右下角
            radiusX = rect.BottomRight.X - rightBottom.X;
            radiusY = rect.BottomRight.Y - bottomRight.Y;
            if (!radiusX.IsZero() || !radiusY.IsZero())
            {
                ctx.ArcTo(rightBottom, new Size(radiusX, radiusY), 0, false, SweepDirection.Clockwise, true, false);
            }

            // 底边线
            ctx.LineTo(leftBottom, true /* is stroked */, false /* is smooth join */);

            // 左下角
            radiusX = leftBottom.X - rect.BottomLeft.X;
            radiusY = rect.BottomLeft.Y - bottomLeft.Y;
            if (!radiusX.IsZero() || !radiusY.IsZero())
            {
                ctx.ArcTo(bottomLeft, new Size(radiusX, radiusY), 0, false, SweepDirection.Clockwise, true, false);
            }

            // 左边线
            ctx.LineTo(topLeft, true /* is stroked */, false /* is smooth join */);

            // 左上角
            radiusX = leftTop.X - rect.TopLeft.X;
            radiusY = topLeft.Y - rect.TopLeft.Y;
            if (!radiusX.IsZero() || !radiusY.IsZero())
            {
                ctx.ArcTo(leftTop, new Size(radiusX, radiusY), 0, false, SweepDirection.Clockwise, true, false);
            }
        }


        private class BorderInfo
        {
            /// <summary>
            /// 边界的左上角点坐标
            /// </summary>
            internal readonly double LeftTop;
            /// <summary>
            /// 边界的上左角点坐标
            /// </summary>
            internal readonly double TopLeft;
            /// <summary>
            /// 边界的上右角点坐标
            /// </summary>
            internal readonly double TopRight;
            /// <summary>
            /// 边界的右上角点坐标
            /// </summary>
            internal readonly double RightTop;
            /// <summary>
            /// 边界的右下角点坐标
            /// </summary>
            internal readonly double RightBottom;
            /// <summary>
            /// 边界的下右角点坐标
            /// </summary>
            internal readonly double BottomRight;
            /// <summary>
            /// 边界的下左角点坐标
            /// </summary>
            internal readonly double BottomLeft;
            /// <summary>
            /// 边界的左下角点坐标
            /// </summary>
            internal readonly double LeftBottom;

            /// <summary>
            /// 封装边界每个核心点的详细信息，基于给定的圆角半径（CornerRadius）、边框厚度（BorderThickness）、填充（Padding），
            /// 并通过一个标志来指示计算的是内边界还是外边界。
            /// </summary>
            /// <param name="corners">圆角半径，表示每个角的圆弧半径</param>
            /// <param name="borders">边框厚度，表示每边的边框宽度</param>
            /// <param name="padding">填充，表示内容与边框之间的距离</param>
            /// <param name="isOuterBorder">标志，指示需要计算的是外边界还是内边界</param>
            internal BorderInfo(CornerRadius corners, Thickness borders, Thickness padding, bool isOuterBorder)
            {
                // 计算每边的中点位置，加上填充
                var left = 0.5 * borders.Left + padding.Left;
                var top = 0.5 * borders.Top + padding.Top;
                var right = 0.5 * borders.Right + padding.Right;
                var bottom = 0.5 * borders.Bottom + padding.Bottom;

                if (isOuterBorder)
                {
                    // 计算外边界的核心点
                    if (corners.TopLeft.IsZero())
                    {
                        this.LeftTop = this.TopLeft = 0.0; // 如果左上角的圆角半径为零，则边界点为零
                    }
                    else
                    {
                        this.LeftTop = corners.TopLeft + left; // 左上角的左边界点坐标
                        this.TopLeft = corners.TopLeft + top;  // 左上角的上边界点坐标
                    }

                    if (corners.TopRight.IsZero())
                    {
                        this.TopRight = this.RightTop = 0.0; // 如果右上角的圆角半径为零，则边界点为零
                    }
                    else
                    {
                        this.TopRight = corners.TopRight + top; // 右上角的上边界点坐标
                        this.RightTop = corners.TopRight + right; // 右上角的右边界点坐标
                    }

                    if (corners.BottomRight.IsZero())
                    {
                        this.RightBottom = this.BottomRight = 0.0; // 如果右下角的圆角半径为零，则边界点为零
                    }
                    else
                    {
                        this.RightBottom = corners.BottomRight + right; // 右下角的右边界点坐标
                        this.BottomRight = corners.BottomRight + bottom; // 右下角的下边界点坐标
                    }

                    if (corners.BottomLeft.IsZero())
                    {
                        this.BottomLeft = this.LeftBottom = 0.0; // 如果左下角的圆角半径为零，则边界点为零
                    }
                    else
                    {
                        this.BottomLeft = corners.BottomLeft + bottom; // 左下角的下边界点坐标
                        this.LeftBottom = corners.BottomLeft + left; // 左下角的左边界点坐标
                    }
                }
                else
                {
                    // 计算内边界的核心点
                    this.LeftTop = Math.Max(0.0, corners.TopLeft - left); // 左上角的左边界点坐标，最小值为0
                    this.TopLeft = Math.Max(0.0, corners.TopLeft - top);  // 左上角的上边界点坐标，最小值为0
                    this.TopRight = Math.Max(0.0, corners.TopRight - top); // 右上角的上边界点坐标，最小值为0
                    this.RightTop = Math.Max(0.0, corners.TopRight - right); // 右上角的右边界点坐标，最小值为0
                    this.RightBottom = Math.Max(0.0, corners.BottomRight - right); // 右下角的右边界点坐标，最小值为0
                    this.BottomRight = Math.Max(0.0, corners.BottomRight - bottom); // 右下角的下边界点坐标，最小值为0
                    this.BottomLeft = Math.Max(0.0, corners.BottomLeft - bottom); // 左下角的下边界点坐标，最小值为0
                    this.LeftBottom = Math.Max(0.0, corners.BottomLeft - left); // 左下角的左边界点坐标，最小值为0
                }
            }
        }


    }
}
