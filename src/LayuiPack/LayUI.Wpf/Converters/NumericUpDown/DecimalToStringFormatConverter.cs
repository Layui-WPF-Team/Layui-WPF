using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace LayUI.Wpf.Converters
{
    internal class DecimalToStringFormatConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0].Equals(DependencyProperty.UnsetValue)) return DependencyProperty.UnsetValue;
            object format = values[1];
            object value = values[0];
            if (format != null)
            {
                if (!string.IsNullOrEmpty(format.ToString()))
                {
                    format = format.ToString();
                }
            }
            if (format != null) value = string.Format($"{{0:{format}}}", (decimal)value);
            else value = string.Format($"{{0}}", (decimal)value);
            return value;
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
