using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LayUI.Wpf.Tools
{
    public static class DoubleUtil
    {
        [StructLayout(LayoutKind.Explicit)]
        private struct NanUnion
        {
            [FieldOffset(0)]
            internal double DoubleValue;

            [FieldOffset(0)]
            internal ulong UintValue;
        }

        internal const double DBL_EPSILON = 2.2204460492503131E-16;

        internal const float FLT_MIN = 1.17549435E-38f;

        public static bool AreClose(double value1, double value2)
        {
            if (value1 == value2)
            {
                return true;
            }

            double num = (Math.Abs(value1) + Math.Abs(value2) + 10.0) * 2.2204460492503131E-16;
            double num2 = value1 - value2;
            if (0.0 - num < num2)
            {
                return num > num2;
            }

            return false;
        }

        public static bool LessThan(double value1, double value2)
        {
            if (value1 < value2)
            {
                return !AreClose(value1, value2);
            }

            return false;
        }

        public static bool GreaterThan(double value1, double value2)
        {
            if (value1 > value2)
            {
                return !AreClose(value1, value2);
            }

            return false;
        }

        public static bool LessThanOrClose(double value1, double value2)
        {
            if (!(value1 < value2))
            {
                return AreClose(value1, value2);
            }

            return true;
        }

        public static bool GreaterThanOrClose(double value1, double value2)
        {
            if (!(value1 > value2))
            {
                return AreClose(value1, value2);
            }

            return true;
        }

        public static bool IsOne(double value)
        {
            return Math.Abs(value - 1.0) < 2.2204460492503131E-15;
        }

        public static bool IsZero(double value)
        {
            return Math.Abs(value) < 2.2204460492503131E-15;
        }

        public static bool AreClose(Point point1, Point point2)
        {
            if (AreClose(point1.X, point2.X))
            {
                return AreClose(point1.Y, point2.Y);
            }

            return false;
        }

        public static bool AreClose(Size size1, Size size2)
        {
            if (AreClose(size1.Width, size2.Width))
            {
                return AreClose(size1.Height, size2.Height);
            }

            return false;
        }

        public static bool AreClose(Vector vector1, Vector vector2)
        {
            if (AreClose(vector1.X, vector2.X))
            {
                return AreClose(vector1.Y, vector2.Y);
            }

            return false;
        }

        public static bool AreClose(Rect rect1, Rect rect2)
        {
            if (rect1.IsEmpty)
            {
                return rect2.IsEmpty;
            }

            if (!rect2.IsEmpty && AreClose(rect1.X, rect2.X) && AreClose(rect1.Y, rect2.Y) && AreClose(rect1.Height, rect2.Height))
            {
                return AreClose(rect1.Width, rect2.Width);
            }

            return false;
        }

        public static bool IsBetweenZeroAndOne(double val)
        {
            if (GreaterThanOrClose(val, 0.0))
            {
                return LessThanOrClose(val, 1.0);
            }

            return false;
        }

        public static int DoubleToInt(double val)
        {
            if (!(0.0 < val))
            {
                return (int)(val - 0.5);
            }

            return (int)(val + 0.5);
        }

        public static bool RectHasNaN(Rect r)
        {
            if (IsNaN(r.X) || IsNaN(r.Y) || IsNaN(r.Height) || IsNaN(r.Width))
            {
                return true;
            }

            return false;
        }

        public static bool IsNaN(double value)
        {
            NanUnion nanUnion = default(NanUnion);
            nanUnion.DoubleValue = value;
            ulong num = nanUnion.UintValue & 0xFFF0000000000000uL;
            ulong num2 = nanUnion.UintValue & 0xFFFFFFFFFFFFFuL;
            if (num == 9218868437227405312L || num == 18442240474082181120uL)
            {
                return num2 != 0;
            }

            return false;
        }
    }
}
