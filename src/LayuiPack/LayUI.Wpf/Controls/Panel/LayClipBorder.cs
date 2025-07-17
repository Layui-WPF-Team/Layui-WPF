using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace LayUI.Wpf.Controls
{
    /// <summary>
    /// 轻量级别Border
    /// <para>支持裁剪超过可视化范围后的Border</para>
    /// </summary>
    public class LayClipBorder : Decorator
    {
        #region 依赖属性
        public static readonly DependencyProperty BorderThicknessProperty =
            DependencyProperty.Register("BorderThickness", typeof(Thickness), typeof(LayClipBorder),
                new FrameworkPropertyMetadata(new Thickness(),
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(LayClipBorder),
                new FrameworkPropertyMetadata(new CornerRadius(),
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
        [Bindable(true)]
        public Thickness BorderThickness
        {
            get => (Thickness)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }
        [Bindable(true)]
        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }
        #endregion

        private CornerRadius CalculateInnerRadii()
        {
            return new CornerRadius(
                Math.Max(CornerRadius.TopLeft - (BorderThickness.Left + BorderThickness.Top) / 2, 0),
                Math.Max(CornerRadius.TopRight - (BorderThickness.Right + BorderThickness.Top) / 2, 0),
                Math.Max(CornerRadius.BottomRight - (BorderThickness.Right + BorderThickness.Bottom) / 2, 0),
                Math.Max(CornerRadius.BottomLeft - (BorderThickness.Left + BorderThickness.Bottom) / 2, 0)
            );
        }

        protected override Size MeasureOverride(Size constraint)
        {
            // 保持原有测量逻辑不变
            Thickness border = BorderThickness;
            double combinedWidth = border.Left + border.Right;
            double combinedHeight = border.Top + border.Bottom;

            Size availableSize = new Size(
                Math.Max(constraint.Width - combinedWidth, 0),
                Math.Max(constraint.Height - combinedHeight, 0)
            );

            if (Child != null)
            {
                Child.Measure(availableSize);
                return new Size(
                    Child.DesiredSize.Width + combinedWidth,
                    Child.DesiredSize.Height + combinedHeight
                );
            }

            return new Size(combinedWidth, combinedHeight);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            // 保持原有布局逻辑不变
            Thickness border = BorderThickness;
            double contentWidth = Math.Max(finalSize.Width - border.Left - border.Right, 0);
            double contentHeight = Math.Max(finalSize.Height - border.Top - border.Bottom, 0);

            if (Child != null)
            {
                Child.Arrange(new Rect(
                    border.Left,
                    border.Top,
                    contentWidth,
                    contentHeight
                ));

                CornerRadius innerRadii = CalculateInnerRadii();
                Child.Clip = CreateClipGeometry(new Size(contentWidth, contentHeight), innerRadii);
            }

            return finalSize;
        }

        private Geometry CreateClipGeometry(Size contentSize, CornerRadius radii)
        {
            // 保持原有裁剪逻辑不变
            var rect = new Rect(contentSize);
            var geometry = new StreamGeometry();

            using (var ctx = geometry.Open())
            {
                double topLeft = Math.Min(radii.TopLeft, Math.Min(rect.Width, rect.Height) / 2);
                double topRight = Math.Min(radii.TopRight, Math.Min(rect.Width, rect.Height) / 2);
                double bottomRight = Math.Min(radii.BottomRight, Math.Min(rect.Width, rect.Height) / 2);
                double bottomLeft = Math.Min(radii.BottomLeft, Math.Min(rect.Width, rect.Height) / 2);

                ctx.BeginFigure(new Point(topLeft, 0), true, true);
                ctx.LineTo(new Point(rect.Width - topRight, 0), true, false);
                ctx.ArcTo(new Point(rect.Width, topRight), new Size(topRight, topRight), 0, false, SweepDirection.Clockwise, true, false);
                ctx.LineTo(new Point(rect.Width, rect.Height - bottomRight), true, false);
                ctx.ArcTo(new Point(rect.Width - bottomRight, rect.Height), new Size(bottomRight, bottomRight), 0, false, SweepDirection.Clockwise, true, false);
                ctx.LineTo(new Point(bottomLeft, rect.Height), true, false);
                ctx.ArcTo(new Point(0, rect.Height - bottomLeft), new Size(bottomLeft, bottomLeft), 0, false, SweepDirection.Clockwise, true, false);
                ctx.LineTo(new Point(0, topLeft), true, false);
                ctx.ArcTo(new Point(topLeft, 0), new Size(topLeft, topLeft), 0, false, SweepDirection.Clockwise, true, false);
            }

            geometry.Freeze();
            return geometry;
        }
    }
}
