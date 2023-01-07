using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Xml;

namespace LayUI.Wpf.Tools
{
    /// <summary>
    /// 控件帮助类
    /// </summary>
    public class LayUIElementHelper
    {
        /// <summary>
        /// 元素复制（克隆）
        /// </summary>
        /// <param name="element">被复制目标元素</param>
        /// <returns></returns>
        public static DependencyObject DeepCopy(DependencyObject element)
        {
            string shapestring = XamlWriter.Save(element);
            StringReader stringReader = new StringReader(shapestring);
            XmlTextReader xmlTextReader = new XmlTextReader(stringReader);
            DependencyObject DeepCopyobject = (DependencyObject)XamlReader.Load(xmlTextReader);
            return DeepCopyobject;
        }
    }
}
