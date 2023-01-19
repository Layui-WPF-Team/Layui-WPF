using System.Xml;
using LayUI.Wpf.Controls.SVG;
using LayUI.Wpf.Controls.SVG.Shapes;

namespace LayUI.Wpf.Controls.SVG.Animation
{
    public class Animate : AnimationBase
    {
        public string AttributeName { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string RepeatType { get; set; }

        public string Values { get; set; }

        public string hRef { get; set; }

        public Animate(global::LayUI.Wpf.Controls.SVG.SVG svg, XmlNode node, Shape parent)
            : base(svg, node, parent)
        {
            this.From = XmlUtil.AttrValue(node, "from", null);
            this.To = XmlUtil.AttrValue(node, "to", null);
            this.AttributeName = XmlUtil.AttrValue(node, "attributeName", null);
            this.RepeatType = XmlUtil.AttrValue(node, "repeatCount", "indefinite");
            this.Values = XmlUtil.AttrValue(node, "values", null);

            this.hRef = XmlUtil.AttrValue(node, "xlink:href", string.Empty);
            if (this.hRef.StartsWith("#")) this.hRef = this.hRef.Substring(1);
        }
    }
}
