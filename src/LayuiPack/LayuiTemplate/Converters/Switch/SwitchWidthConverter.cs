using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LayuiTemplate.Converters
{
    /// <summary>
    ///  SwitchWidthConverter
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-06-28 下午 7:05:22</para>
    /// </summary>
    internal class SwitchWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value * 2;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
