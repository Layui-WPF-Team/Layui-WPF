using System.Windows.Media.Effects;
using System.Xml;
using LayUI.Wpf.Controls.SVG.Shapes;

namespace LayUI.Wpf.Controls.SVG.Shapes.Filter
{
    public class Filter : Group
    {
        public Filter(global::LayUI.Wpf.Controls.SVG.SVG svg, XmlNode node, Shape parent)
            : base(svg, node, parent)
        {
           
        }

        public BitmapEffect GetBitmapEffect()
        {
            var beg = new BitmapEffectGroup();
            foreach (FilterBaseFe element in this.Elements)
            {
                beg.Children.Add(element.GetBitmapEffect());
            }

            return beg;
        }
    }
}
