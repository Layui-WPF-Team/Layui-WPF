using System.Windows.Media.Effects;
using System.Xml;
using LayUI.Wpf.Controls.SVG.Shapes;

namespace LayUI.Wpf.Controls.SVG.Shapes.Filter
{
    public abstract class FilterBaseFe : Shape
    {
        public FilterBaseFe(global::LayUI.Wpf.Controls.SVG.SVG svg, XmlNode node, Shape parent)
            : base(svg, node, parent)
        {

        }

        public abstract BitmapEffect GetBitmapEffect();
    }
}

