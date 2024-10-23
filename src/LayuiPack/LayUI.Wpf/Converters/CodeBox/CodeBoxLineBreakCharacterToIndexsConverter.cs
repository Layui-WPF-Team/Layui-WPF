using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace LayUI.Wpf.Converters
{
    /// <summary>
    ///  换行符转化Index
    /// </summary>
    public class CodeBoxLineBreakCharacterToIndexsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return DependencyProperty.UnsetValue;
            var indexs = new List<int>();
            var values = value.ToString().Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            for (int i = 1; i <= values.Length; i++)
            {
                indexs.Add(i);
            }
            return indexs;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
