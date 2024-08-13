using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Data;

namespace LayUI.Wpf.Converters
{
    internal class SimpleCornerRadiusToClipConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            RectangleGeometry rectangle;
            if (values.Length != 3 || !(values[0] is double actualWidth) || !(values[1] is double actualHeight) || !(values[2] is CornerRadius radius)) rectangle = new RectangleGeometry();
            else rectangle = new RectangleGeometry(new Rect(0, 0, actualWidth, actualHeight), (double)radius.TopLeft, (double)radius.TopLeft);
            return rectangle;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
