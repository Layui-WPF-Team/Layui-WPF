using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace LayUI.Wpf.Converters
{
    public class ItemsControlToIndexConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(values[0] is object) || !(values[1] is IList) || !(values[2] is int)) return 0;
            var item = values[0];
            if (values[1] is IList list)
            {
                var index = list.IndexOf(item) + 1;
                return index;
            }
            return 0;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
