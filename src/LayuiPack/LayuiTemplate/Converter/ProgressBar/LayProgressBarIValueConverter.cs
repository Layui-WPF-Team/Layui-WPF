using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LayuiTemplate.Converter
{
    /// <summary>
    /// 进度值自转换器
    /// </summary>
    public class LayProgressBarIValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (double.TryParse(value.ToString(), out double result))
            {
                return result / 2;
            }
            else
            {
                return 0;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (double.TryParse(value.ToString(), out double result))
            {
                return result / 2;
            }
            else
            {
                return 0;
            }
        }
    }
}
