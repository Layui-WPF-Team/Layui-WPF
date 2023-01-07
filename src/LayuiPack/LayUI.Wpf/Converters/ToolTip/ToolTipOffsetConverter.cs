using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LayUI.Wpf.Converters
{
    public class ToolTipOffsetConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double verValue = values[0] as double? ?? 0;
            double horValue = values[1] as double? ?? 0;
            if (parameter != null && parameter.ToString() == "-")
            {
                var data = -((verValue + horValue) / 2);
                return data;
            }
            else {
                var data = (verValue + horValue) / 2;
                return data;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
