using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;

namespace LayUI.Wpf.Tools
{
    /// <summary>
    /// 控件帮助类
    /// </summary>
    public static  class LayUIElementHelper
    {
        /// <summary>
        /// 元素复制（克隆）
        /// </summary>
        /// <param name="element">被复制目标元素</param>
        /// <returns></returns>
        public static DependencyObject DeepCopy(DependencyObject element)
        {
            string shapestring = XamlWriter.Save(element);
            StringReader stringReader = new StringReader(shapestring);
            XmlTextReader xmlTextReader = new XmlTextReader(stringReader);
            DependencyObject DeepCopyobject = (DependencyObject)XamlReader.Load(xmlTextReader);
            return DeepCopyobject;
        }
        #region Double

        // 常量值
        // 最小的双精度值，使得 1.0+DBL_EPSILON != 1.0
        internal const double DBL_EPSILON = 2.2204460492503131e-016;
        // 接近于零的数值，其中 float.MinValue 是 -float.MaxValue
        internal const float FLT_MIN = 1.175494351e-38F;

        /// <summary>
        /// 返回两个双精度值是否“接近”。
        /// </summary>
        /// <param name="value1"> 第一个比较的双精度值。 </param>
        /// <param name="value2"> 第二个比较的双精度值。 </param>
        /// <returns>
        /// bool - AreClose 比较的结果。
        /// </returns>
        public static bool IsCloseTo(this double value1, double value2)
        {
            //如果它们是无穷大（则 epsilon 检查不起作用）
            if (value1 == value2) return true;
            // 计算 (|value1-value2| / (|value1| + |value2| + 10.0)) < DBL_EPSILON
            var eps = (Math.Abs(value1) + Math.Abs(value2) + 10.0) * DBL_EPSILON;
            var delta = value1 - value2;
            return (-eps < delta) && (eps > delta);
        }

        /// <summary>
        /// 返回第一个双精度值是否小于第二个双精度值。
        /// </summary>
        /// <param name="value1"> 第一个比较的双精度值。 </param>
        /// <param name="value2"> 第二个比较的双精度值。 </param>
        /// <returns>
        /// bool - LessThan 比较的结果。
        /// </returns>
        public static bool IsLessThan(double value1, double value2)
        {
            return (value1 < value2) && !value1.IsCloseTo(value2);
        }

        /// <summary>
        /// 返回第一个双精度值是否大于第二个双精度值。
        /// </summary>
        /// <param name="value1"> 第一个比较的双精度值。 </param>
        /// <param name="value2"> 第二个比较的双精度值。 </param>
        /// <returns>
        /// bool - GreaterThan 比较的结果。
        /// </returns>
        public static bool IsGreaterThan(this double value1, double value2)
        {
            return (value1 > value2) && !value1.IsCloseTo(value2);
        }

        /// <summary>
        /// 返回双精度值是否“接近”1。与 AreClose(double, 1) 相同，
        /// 但这更快。
        /// </summary>
        /// <param name="value"> 与 1 比较的双精度值。 </param>
        /// <returns>
        /// bool - AreClose 比较的结果。
        /// </returns>
        public static bool IsOne(this double value)
        {
            return Math.Abs(value - 1.0) < 10.0 * DBL_EPSILON;
        }

        /// <summary>
        /// IsZero - 返回双精度值是否“接近”0。与 AreClose(double, 0) 相同，
        /// 但这更快。
        /// </summary>
        /// <param name="value"> 与 0 比较的双精度值。 </param>
        /// <returns>
        /// bool - AreClose 比较的结果。
        /// </returns>
        public static bool IsZero(this double value)
        {
            return Math.Abs(value) < 10.0 * DBL_EPSILON;
        }

        /// <summary>
        /// 模糊比较两个点。此函数
        /// 帮助补偿操作时双精度值会出现的误差
        /// </summary>
        /// <param name='point1'>第一个比较的点</param>
        /// <param name='point2'>第二个比较的点</param>
        /// <returns>两个点是否相等</returns>
        public static bool IsCloseTo(this Point point1, Point point2)
        {
            return point1.X.IsCloseTo(point2.X) && point1.Y.IsCloseTo(point2.Y);
        }

        /// <summary>
        /// 模糊比较两个 Size 实例。此函数
        /// 帮助补偿操作时双精度值会出现的误差
        /// </summary>
        /// <param name='size1'>第一个比较的大小</param>
        /// <param name='size2'>第二个比较的大小</param>
        /// <returns>两个 Size 实例是否相等</returns>
        public static bool IsCloseTo(this Size size1, Size size2)
        {
            return size1.Width.IsCloseTo(size2.Width) && size1.Height.IsCloseTo(size2.Height);
        }

        /// <summary>
        /// 模糊比较两个 Vector 实例。此函数
        /// 帮助补偿操作时双精度值会出现的误差
        /// </summary>
        /// <param name='vector1'>第一个比较的向量</param>
        /// <param name='vector2'>第二个比较的向量</param>
        /// <returns>两个 Vector 实例是否相等</returns>
        public static bool IsCloseTo(this Vector vector1, Vector vector2)
        {
            return vector1.X.IsCloseTo(vector2.X) && vector1.Y.IsCloseTo(vector2.Y);
        }

        /// <summary>
        /// 模糊比较两个矩形。此函数
        /// 帮助补偿操作时双精度值会出现的误差
        /// </summary>
        /// <param name='rect1'>第一个比较的矩形</param>
        /// <param name='rect2'>第二个比较的矩形</param>
        /// <returns>两个矩形是否相等</returns>
        public static bool IsCloseTo(this Rect rect1, Rect rect2)
        {
            // 如果它们都是空的，不要进行双精度逻辑判断。
            if (rect1.IsEmpty)
            {
                return rect2.IsEmpty;
            }

            // 此时，rect1 不是空的，所以我们首先测试的是
            // rect2.IsEmpty，然后是逐属性比较。

            return (!rect2.IsEmpty)
                   && rect1.X.IsCloseTo(rect2.X) && rect1.Y.IsCloseTo(rect2.Y)
                   && rect1.Height.IsCloseTo(rect2.Height) && rect1.Width.IsCloseTo(rect2.Width);
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct NanUnion
        {
            [FieldOffset(0)]
            internal double DoubleValue;
            [FieldOffset(0)]
            internal UInt64 UintValue;
        }

        /// <summary>
        /// 检查是否为 NaN（比 double.IsNaN() 更快）
        /// IEEE 754：如果参数是范围 0x7ff0000000000001L 到 0x7fffffffffffffffL 内的任意值
        /// 或者范围 0xfff0000000000001L 到 0xffffffffffffffffL 内的任意值，结果将是 NaN。
        /// </summary>
        /// <param name="value">要检查的值</param>
        /// <returns></returns>
        public static bool IsNaN(double value)
        {
            var t = new NanUnion
            {
                DoubleValue = value
            };

            var exp = t.UintValue & 0xfff0000000000000;
            var man = t.UintValue & 0x000fffffffffffff;

            return (exp == 0x7ff0000000000000 || exp == 0xfff0000000000000) && (man != 0);
        }

        /// <summary>
        /// 根据 DPI 缩放值对给定值进行四舍五入
        /// </summary>
        /// <param name="value">要四舍五入的值</param>
        /// <param name="dpiScale">DPI 缩放值</param>
        /// <returns></returns>
        public static double RoundLayoutValue(double value, double dpiScale)
        {
            double newValue;

            // 如果 DPI == 1，则不使用基于 DPI 的四舍五入。
            if (!dpiScale.IsCloseTo(1.0))
            {
                newValue = Math.Round(value * dpiScale) / dpiScale;
                // 如果四舍五入产生的值对布局不可接受（NaN、无穷大或最大值），使用原始值。
                if (IsNaN(newValue) ||
                    Double.IsInfinity(newValue) ||
                    newValue.IsCloseTo(Double.MaxValue))
                {
                    newValue = value;
                }
            }
            else
            {
                newValue = Math.Round(value);
            }

            return newValue;
        }
        #endregion

        #region Thickness

        /// <summary>
        /// 验证此 Thickness 是否只包含有效值
        /// 有效性检查集作为参数传递。
        /// </summary>
        /// <param name='thick'>Thickness 值</param>
        /// <param name='allowNegative'>允许负值</param>
        /// <param name='allowNaN'>允许 Double.NaN</param>
        /// <param name='allowPositiveInfinity'>允许 Double.PositiveInfinity</param>
        /// <param name='allowNegativeInfinity'>允许 Double.NegativeInfinity</param>
        /// <returns>Thickness 是否符合指定范围</returns>
        public static bool IsValid(this Thickness thick, bool allowNegative, bool allowNaN, bool allowPositiveInfinity, bool allowNegativeInfinity)
        {
            if (!allowNegative)
            {
                if (thick.Left < 0d || thick.Right < 0d || thick.Top < 0d || thick.Bottom < 0d)
                    return false;
            }

            if (!allowNaN)
            {
                if (IsNaN(thick.Left) || IsNaN(thick.Right)
                    || IsNaN(thick.Top) || IsNaN(thick.Bottom))
                    return false;
            }

            if (!allowPositiveInfinity)
            {
                if (Double.IsPositiveInfinity(thick.Left) || Double.IsPositiveInfinity(thick.Right)
                    || Double.IsPositiveInfinity(thick.Top) || Double.IsPositiveInfinity(thick.Bottom))
                {
                    return false;
                }
            }

            if (!allowNegativeInfinity)
            {
                if (Double.IsNegativeInfinity(thick.Left) || Double.IsNegativeInfinity(thick.Right)
                    || Double.IsNegativeInfinity(thick.Top) || Double.IsNegativeInfinity(thick.Bottom))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 方法将左边和右边的尺寸相加为宽度，以及顶部和底部的尺寸相加为高度
        /// </summary>
        /// <param name="thick">Thickness</param>
        /// <returns>Size</returns>
        public static Size CollapseThickness(this Thickness thick)
        {
            return new Size(thick.Left + thick.Right, thick.Top + thick.Bottom);
        }

        /// <summary>
        /// 验证 Thickness 是否只包含零值
        /// </summary>
        /// <param name="thick">Thickness</param>
        /// <returns>Size</returns>
        public static bool IsZero(this Thickness thick)
        {
            return thick.Left.IsZero()
                    && thick.Top.IsZero()
                    && thick.Right.IsZero()
                    && thick.Bottom.IsZero();
        }

        /// <summary>
        /// 验证 Thickness 中的所有值是否相同
        /// </summary>
        /// <param name="thick">Thickness</param>
        /// <returns>如果相同返回 true，否则返回 false</returns>
        public static bool IsUniform(this Thickness thick)
        {
            return thick.Left.IsCloseTo(thick.Top)
                    && thick.Left.IsCloseTo(thick.Right)
                    && thick.Left.IsCloseTo(thick.Bottom);
        }

        #region CornerRadius

        /// <summary>
        /// 验证此 CornerRadius 是否只包含有效值
        /// 有效性检查集作为参数传递。
        /// </summary>
        /// <param name='corner'>CornerRadius 值</param>
        /// <param name='allowNegative'>允许负值</param>
        /// <param name='allowNaN'>允许 Double.NaN</param>
        /// <param name='allowPositiveInfinity'>允许 Double.PositiveInfinity</param>
        /// <param name='allowNegativeInfinity'>允许 Double.NegativeInfinity</param>
        /// <returns>CornerRadius 是否符合指定范围</returns>
        public static bool IsValid(this CornerRadius corner, bool allowNegative, bool allowNaN, bool allowPositiveInfinity, bool allowNegativeInfinity)
        {
            if (!allowNegative)
            {
                if (corner.TopLeft < 0d || corner.TopRight < 0d || corner.BottomLeft < 0d || corner.BottomRight < 0d)
                {
                    return (false);
                }
            }

            if (!allowNaN)
            {
                if (IsNaN(corner.TopLeft) || IsNaN(corner.TopRight) ||
                    IsNaN(corner.BottomLeft) || IsNaN(corner.BottomRight))
                {
                    return (false);
                }
            }

            if (!allowPositiveInfinity)
            {
                if (Double.IsPositiveInfinity(corner.TopLeft) || Double.IsPositiveInfinity(corner.TopRight) ||
                    Double.IsPositiveInfinity(corner.BottomLeft) || Double.IsPositiveInfinity(corner.BottomRight))
                {
                    return (false);
                }
            }

            if (!allowNegativeInfinity)
            {
                if (Double.IsNegativeInfinity(corner.TopLeft) || Double.IsNegativeInfinity(corner.TopRight) ||
                    Double.IsNegativeInfinity(corner.BottomLeft) || Double.IsNegativeInfinity(corner.BottomRight))
                {
                    return (false);
                }
            }

            return true;
        }

        /// <summary>
        /// 验证 CornerRadius 是否只包含零值
        /// </summary>
        /// <param name="corner">CornerRadius</param>
        /// <returns>Size</returns>
        public static bool IsZero(this CornerRadius corner)
        {
            return corner.TopLeft.IsZero()
                   && corner.TopRight.IsZero()
                   && corner.BottomRight.IsZero()
                   && corner.BottomLeft.IsZero();
        }

        /// <summary>
        /// 验证 CornerRadius 是否包含相同的值
        /// </summary>
        /// <param name="corner">CornerRadius</param>
        /// <returns>如果相同返回 true，否则返回 false</returns>
        public static bool IsUniform(this CornerRadius corner)
        {
            var topLeft = corner.TopLeft;
            return topLeft.IsCloseTo(corner.TopRight) &&
                   topLeft.IsCloseTo(corner.BottomLeft) &&
                   topLeft.IsCloseTo(corner.BottomRight);
        }

        #endregion
        #endregion

        #region Rect

        /// <summary>
        /// 按指定厚度缩小矩形
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="thick">厚度</param>
        /// <returns>缩小后的矩形</returns>
        public static Rect Deflate(this Rect rect, Thickness thick)
        {
            return new Rect(rect.Left + thick.Left,
                rect.Top + thick.Top,
                Math.Max(0.0, rect.Width - thick.Left - thick.Right),
                Math.Max(0.0, rect.Height - thick.Top - thick.Bottom));
        }

        /// <summary>
        /// 按指定厚度放大矩形
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="thick">厚度</param>
        /// <returns>放大后的矩形</returns>
        public static Rect Inflate(this Rect rect, Thickness thick)
        {
            return new Rect(rect.Left - thick.Left,
                rect.Top - thick.Top,
                Math.Max(0.0, rect.Width + thick.Left + thick.Right),
                Math.Max(0.0, rect.Height + thick.Top + thick.Bottom));
        }

        #endregion

        #region Brush

        /// <summary>
        /// 验证给定的画笔是否为 SolidColorBrush 并且
        /// 其颜色不包含透明度。
        /// </summary>
        /// <param name="brush">画笔</param>
        /// <returns>如果是返回 true，否则返回 false</returns>
        public static bool IsOpaqueSolidColorBrush(this Brush brush)
        {
            return (brush as SolidColorBrush)?.Color.A == 0xff;
        }

        /// <summary>
        /// 验证给定的画笔是否与另一个画笔相同。
        /// </summary>
        /// <param name="brush">画笔</param>
        /// <param name="otherBrush">另一个画笔</param>
        /// <returns>如果是返回 true，否则返回 false</returns>
        /// <summary> 
        public static bool IsEqualTo(this Brush brush, Brush otherBrush)
        {
            if (brush.GetType() != otherBrush.GetType())
                return false;

            if (ReferenceEquals(brush, otherBrush))
                return true;

            // Are both instances of SolidColorBrush
            var solidBrushA = brush as SolidColorBrush;
            var solidBrushB = otherBrush as SolidColorBrush;
            if ((solidBrushA != null) && (solidBrushB != null))
            {
                return (solidBrushA.Color == solidBrushB.Color)
                       && solidBrushA.Opacity.IsCloseTo(solidBrushB.Opacity);
            }

            // Are both instances of LinearGradientBrush
            var linGradBrushA = brush as LinearGradientBrush;
            var linGradBrushB = otherBrush as LinearGradientBrush;

            if ((linGradBrushA != null) && (linGradBrushB != null))
            {
                var result = (linGradBrushA.ColorInterpolationMode == linGradBrushB.ColorInterpolationMode)
                               && (linGradBrushA.EndPoint == linGradBrushB.EndPoint)
                               && (linGradBrushA.MappingMode == linGradBrushB.MappingMode)
                               && linGradBrushA.Opacity.IsCloseTo(linGradBrushB.Opacity)
                               && (linGradBrushA.StartPoint == linGradBrushB.StartPoint)
                               && (linGradBrushA.SpreadMethod == linGradBrushB.SpreadMethod)
                               && (linGradBrushA.GradientStops.Count == linGradBrushB.GradientStops.Count);
                if (!result)
                    return false;

                for (var i = 0; i < linGradBrushA.GradientStops.Count; i++)
                {
                    result = (linGradBrushA.GradientStops[i].Color == linGradBrushB.GradientStops[i].Color)
                             && linGradBrushA.GradientStops[i].Offset.IsCloseTo(linGradBrushB.GradientStops[i].Offset);

                    if (!result)
                        break;
                }

                return result;
            }

            // Are both instances of RadialGradientBrush
            var radGradBrushA = brush as RadialGradientBrush;
            var radGradBrushB = otherBrush as RadialGradientBrush;

            if ((radGradBrushA != null) && (radGradBrushB != null))
            {
                var result = (radGradBrushA.ColorInterpolationMode == radGradBrushB.ColorInterpolationMode)
                             && (radGradBrushA.GradientOrigin == radGradBrushB.GradientOrigin)
                             && (radGradBrushA.MappingMode == radGradBrushB.MappingMode)
                             && radGradBrushA.Opacity.IsCloseTo(radGradBrushB.Opacity)
                             && radGradBrushA.RadiusX.IsCloseTo(radGradBrushB.RadiusX)
                             && radGradBrushA.RadiusY.IsCloseTo(radGradBrushB.RadiusY)
                             && (radGradBrushA.SpreadMethod == radGradBrushB.SpreadMethod)
                             && (radGradBrushA.GradientStops.Count == radGradBrushB.GradientStops.Count);

                if (!result)
                    return false;

                for (var i = 0; i < radGradBrushA.GradientStops.Count; i++)
                {
                    result = (radGradBrushA.GradientStops[i].Color == radGradBrushB.GradientStops[i].Color)
                             && radGradBrushA.GradientStops[i].Offset.IsCloseTo(radGradBrushB.GradientStops[i].Offset);

                    if (!result)
                        break;
                }

                return result;
            }

            // Are both instances of ImageBrush
            var imgBrushA = brush as ImageBrush;
            var imgBrushB = otherBrush as ImageBrush;
            if ((imgBrushA != null) && (imgBrushB != null))
            {
                var result = (imgBrushA.AlignmentX == imgBrushB.AlignmentX)
                              && (imgBrushA.AlignmentY == imgBrushB.AlignmentY)
                              && imgBrushA.Opacity.IsCloseTo(imgBrushB.Opacity)
                              && (imgBrushA.Stretch == imgBrushB.Stretch)
                              && (imgBrushA.TileMode == imgBrushB.TileMode)
                              && (imgBrushA.Viewbox == imgBrushB.Viewbox)
                              && (imgBrushA.ViewboxUnits == imgBrushB.ViewboxUnits)
                              && (imgBrushA.Viewport == imgBrushB.Viewport)
                              && (imgBrushA.ViewportUnits == imgBrushB.ViewportUnits)
                              && (imgBrushA.ImageSource == imgBrushB.ImageSource);

                return result;
            }

            return false;
        }

        #endregion
    }
}
