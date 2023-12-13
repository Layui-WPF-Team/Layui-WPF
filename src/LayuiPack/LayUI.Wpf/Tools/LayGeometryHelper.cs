using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LayUI.Wpf.Tools
{
    public class LayGeometryHelper
    {
        private static string uri = "pack://application:,,,/LayUI.Wpf;component/Style/Default/Geometries.xaml";
        private static ResourceDictionary _Geometrys;
        public static ResourceDictionary Geometrys
        {
            get
            {
                if (_Geometrys == null)
                {
                    _Geometrys = new ResourceDictionary() { Source = new Uri(uri) };
                }
                return _Geometrys;
            }
        }
    }
}
