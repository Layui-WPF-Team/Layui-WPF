using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LayUI.Wpf.Converters
{
    /// <summary>
    ///  ElementAutoNumberConvert
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-09-01 下午 3:52:01</para>
    /// </summary>
    public class ElementAutoNumberConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (double.TryParse(value.ToString(), out double result))
            {
                if (parameter != null && parameter.ToString() == "-") return -result;
                else return result;
            }
            else
            {
                //输入的不可以表示成Double类型
                return 0;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
