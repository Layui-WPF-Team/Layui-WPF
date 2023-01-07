using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace LayUI.Wpf.Converters
{
    /// <summary>
    ///  DrawerHostOffsetConverter
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-08-23 下午 5:21:19</para>
    /// </summary>
    public class DrawerHostOffsetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var d = value as double? ?? 0;
            if (double.IsInfinity(d) || double.IsNaN(d)) d = 0;

            var dock = (parameter is Dock) ? (Dock)parameter : Dock.Left;
            switch (dock)
            {
                case Dock.Top:
                    return new Thickness(0, 0 - d, 0, d);
                case Dock.Bottom:
                    return new Thickness(0, d, 0, 0 - d);
                case Dock.Right:
                    return new Thickness(d, 0, 0 - d, 0);
                case Dock.Left:
                default:
                    return new Thickness(0 - d, 0, d, 0);
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
