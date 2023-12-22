using LayUI.Wpf.Tools;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace LayUI.Wpf.Converters
{
    public class CornerRadiusClipConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            StreamGeometry streamGeometry = null;
            if (values[0] is double Width && values[1] is double Height && values[2] is CornerRadius radius && values[3] is Thickness thickness)
            {
                Rect rect = new Rect(new Size(Width, Height));
                Rect rect2 = LayGeometryHelper.HelperDeflateRect(rect, thickness);
                Radii radii;
                var horizontal = (HorizontalAlignment)parameter;
                if (horizontal == HorizontalAlignment.Left)
                {
                    radii = new Radii(new CornerRadius() { TopLeft = radius.TopLeft, BottomLeft = radius.BottomLeft }, thickness, outer: false);
                }
                else if (horizontal == HorizontalAlignment.Right)
                {
                    radii = new Radii(new CornerRadius() { TopRight = radius.TopRight, BottomRight = radius.BottomRight }, thickness, outer: false);
                }
                else
                {
                    radii = new Radii(radius, thickness, outer: false);
                }
                streamGeometry = new StreamGeometry();
                using (StreamGeometryContext ctx = streamGeometry.Open())
                {
                    LayGeometryHelper.GenerateGeometry(ctx, rect2, radii);
                }
                return streamGeometry;
            }

            return streamGeometry;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return (object[])value;
        }

    }

}
