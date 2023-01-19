using System.Xml;
using LayUI.Wpf.Controls.SVG.Shapes;

namespace LayUI.Wpf.Controls.SVG
{
    public class PathShape : Shape
    {
        static Fill DefaultFill = null;

        public override Fill Fill
        {
            get
            {
                Fill f = base.Fill;
                if (f == null)
                    f = DefaultFill;
                return f;
            }
        }

        public bool ClosePath { get; private set; }

        public string Data { get; private set; }

        // http://apike.ca/prog_svg_paths.html
        public PathShape(SVG svg, XmlNode node, Shape parent) : base(svg, node, parent)
        {
            if (DefaultFill == null)
            {
                DefaultFill = new Fill(svg);
                DefaultFill.PaintServerKey = svg.PaintServers.Parse("black");
            }

            this.ClosePath = false;
            string path = XmlUtil.AttrValue(node, "d", string.Empty);
            this.Data = path;
        }
    }
}
