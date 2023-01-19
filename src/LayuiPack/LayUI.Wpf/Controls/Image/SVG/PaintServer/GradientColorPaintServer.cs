using System.Collections.Generic;
using System.Windows.Media;
using System.Xml;

namespace LayUI.Wpf.Controls.SVG.PaintServer
{
    public abstract class GradientColorPaintServer : PaintServer
    {
        // http://www.w3.org/TR/SVG11/pservers.html#LinearGradients
        private List<GradientStop> m_stops = new List<GradientStop>();

        public IList<GradientStop> Stops => this.m_stops.AsReadOnly();

        public Transform Transform { get; protected set; }

        public string GradientUnits { get; private set; }

        public GradientColorPaintServer(PaintServerManager owner, XmlNode node) : base(owner)
        {
            this.GradientUnits = XmlUtil.AttrValue(node, "gradientUnits", string.Empty);
            string transform = XmlUtil.AttrValue(node, "gradientTransform", string.Empty);
            if (transform.Length > 0)
            {
                this.Transform = ShapeUtil.ParseTransform(transform.ToLower());
            }

            if (node.ChildNodes.Count == 0 && XmlUtil.AttrValue(node, "xlink:href", string.Empty).Length > 0)
            {
                string refid = XmlUtil.AttrValue(node, "xlink:href", string.Empty);
                string paintServerKey = owner.Parse(refid.Substring(1));
                GradientColorPaintServer refcol = owner.GetServer(paintServerKey) as GradientColorPaintServer;
                if (refcol == null) return;
                this.m_stops = new List<GradientStop>(refcol.m_stops);
            }
            foreach (XmlNode childnode in node.ChildNodes)
            {
                if (childnode.Name == "stop")
                {
                    var styleattr = new List<XmlUtil.StyleItem>();
                    string fullstyle = XmlUtil.AttrValue(childnode, SVGTags.sStyle, string.Empty);
                    if (fullstyle.Length > 0)
                    {
                        foreach (ShapeUtil.Attribute styleitem in XmlUtil.SplitStyle(null, fullstyle)) styleattr.Add(new XmlUtil.StyleItem(styleitem.Name, styleitem.Value));
                    }
                    foreach (var attr1 in styleattr) childnode.Attributes.Append(new TempXmlAttribute(childnode, attr1.Name, attr1.Value));

                    double offset = XmlUtil.AttrValue(childnode, "offset", (double)0);
                    string s = XmlUtil.AttrValue(childnode, "stop-color", "#0");

                    double stopopacity = XmlUtil.AttrValue(childnode, "stop-opacity", (double)1);

                    Color color;
                    if (s.StartsWith("#")) color = PaintServerManager.ParseHexColor(s);
                    else color = PaintServerManager.KnownColor(s);

                    if (stopopacity != 1) color = Color.FromArgb((byte)(stopopacity * 255), color.R, color.G, color.B);

                    if (offset > 1) offset = offset / 100;
                    this.m_stops.Add(new GradientStop(color, offset));
                }
            }
        }

        public GradientColorPaintServer(PaintServerManager owner) : base(owner)
        {

        }

        public class TempXmlAttribute : XmlAttribute
        {
            public TempXmlAttribute(XmlNode owner, string name, string value) : base(string.Empty, name, string.Empty, owner.OwnerDocument)
            {
                this.Value = value;
            }
        }
    }
}
