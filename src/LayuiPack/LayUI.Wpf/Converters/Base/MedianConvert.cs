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
    ///  返回值取中间值
    ///  <para>-、+ 代表需要返回正负值</para>
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-08-22 上午 10:03:25</para>
    /// </summary>
    public class MedianConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (double.TryParse(value.ToString(), out double result))
            {
                if (parameter!=null&&parameter.ToString() == "-") return -(result / 2);
                else return result / 2;
            }
            else
            {
                //输入的不可以表示成Double类型
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
