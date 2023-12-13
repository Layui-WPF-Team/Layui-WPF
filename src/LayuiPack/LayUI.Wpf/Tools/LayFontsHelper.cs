using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace LayUI.Wpf.Tools
{
    public class LayFontsHelper
    {
        private static string uri = "pack://application:,,,/LayUI.Wpf;component/Style/Default/FontsStyle.xaml";
        private static ResourceDictionary _Fonts;
        private static ResourceDictionary Fonts
        {
            get
            {
                if (_Fonts == null)
                {
                    _Fonts = new ResourceDictionary() { Source = new Uri(uri) };
                }
                return _Fonts;
            }
        }
        /// <summary>
        /// 读取字体图标中的Unicode编码
        /// </summary>
        /// <param name="fontName"></param>
        /// <returns></returns>
        public static Dictionary<string,string> GetIconFontUnicode(string fontName)
        {
            var items = new Dictionary<string, string>();
            var font = Fonts[fontName] as FontFamily;
            foreach (Typeface item in font.GetTypefaces())
            {
                GlyphTypeface glyph;
                item.TryGetGlyphTypeface(out glyph);
                IDictionary<int, ushort> keys = glyph.CharacterToGlyphMap;
                foreach (var key in keys)
                {
                    string outStr = "";
                    var unicode = "&#xe" + key.Key.ToString("x4").Substring(1) + ";";
                    string[] strlist = unicode.Replace("&#", "").Replace(";", "").Split('x');
                    for (int i = 1; i < strlist.Length; i++)
                    {
                        outStr += (char)int.Parse(strlist[i], System.Globalization.NumberStyles.HexNumber);
                    }
                    if(!items.ContainsKey(outStr)) items.Add($"{outStr}", $"&#xe{key.Key.ToString("x4").Substring(1)};"); 
                }
            }
            return items;
        }
    }
}
