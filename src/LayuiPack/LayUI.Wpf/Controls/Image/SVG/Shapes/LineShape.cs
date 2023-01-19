using System.Windows;
using System.Xml;

namespace LayUI.Wpf.Controls.SVG.Shapes
{
    public class LineShape : Shape
    {
        public Point P1 { get; private set; }

        public Point P2 { get; private set; }

        public LineShape(SVG svg, XmlNode node) : base(svg, node)
        {
            double x1 = XmlUtil.AttrValue(node, "x1", 0, svg.Size.Width);
            double y1 = XmlUtil.AttrValue(node, "y1", 0, svg.Size.Height);
            double x2 = XmlUtil.AttrValue(node, "x2", 0, svg.Size.Width);
            double y2 = XmlUtil.AttrValue(node, "y2", 0, svg.Size.Height);
            this.P1 = new Point(x1, y1);
            this.P2 = new Point(x2, y2);
        }
    }
}
