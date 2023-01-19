using System.Collections.Generic;
using System.Windows;
using System.Xml;

namespace LayUI.Wpf.Controls.SVG.Shapes
{
    public class PolygonShape : Shape
    {
        private static Fill DefaultFill = null;

        public override Fill Fill
        {
            get
            {
                Fill f = base.Fill;
                if (f == null) f = DefaultFill;
                return f;
            }
        }

        public Point[] Points { get; private set; }

        public PolygonShape(SVG svg, XmlNode node)
            : base(svg, node)
        {
            if (DefaultFill == null)
            {
                DefaultFill = new Fill(svg);
                DefaultFill.PaintServerKey = svg.PaintServers.Parse("black");
            }

            string points = XmlUtil.AttrValue(node, SVGTags.sPoints, string.Empty);
            ShapeUtil.StringSplitter split = new ShapeUtil.StringSplitter(points);
            List<Point> list = new List<Point>();
            while (split.More)
            {
                list.Add(split.ReadNextPoint());
            }
            this.Points = list.ToArray();
        }
    }
}
