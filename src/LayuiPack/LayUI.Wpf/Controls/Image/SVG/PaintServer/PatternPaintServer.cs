using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Xml;
using LayUI.Wpf.Controls.SVG.Shapes;

namespace LayUI.Wpf.Controls.SVG.PaintServer
{
    public class PatternPaintServer : PaintServer
    {
        //x="0" y="0" width="2" height="12"
        //patternUnits
        //patternTransform

        private IList<Shape> m_elements;
        private IDictionary<string, PaintServer> m_pattternPaintServers;

        public double X { get; private set; }

        public double Y { get; private set; }

        public double Width { get; private set; }

        public double Height { get; private set; }

        public Transform PatternTransform { get; protected set; }

        public string PatternUnits { get; private set; }

        public PatternPaintServer(PaintServerManager owner, SVG svg, XmlNode node) : base(owner)
        {
            this.PatternUnits = XmlUtil.AttrValue(node, "patternUnits", string.Empty);
            string transform = XmlUtil.AttrValue(node, "patternTransform", string.Empty);
            if (transform.Length > 0)
            {
                this.PatternTransform = ShapeUtil.ParseTransform(transform.ToLower());
            }
            var tempSVG = new SVG(node, svg.ExternalFileLoader);
            m_elements = tempSVG.Elements;

            m_pattternPaintServers = tempSVG.PaintServers.GetServers();
            this.X = XmlUtil.AttrValue(node, "x", 0, svg.Size.Width);
            this.Y = XmlUtil.AttrValue(node, "y", 0, svg.Size.Height);
            this.Width = XmlUtil.AttrValue(node, "width", 1, svg.Size.Width);
            this.Height = XmlUtil.AttrValue(node, "height", 1, svg.Size.Height);
        }

        public PatternPaintServer(PaintServerManager owner, Brush newBrush) : base(owner)
        {
            Brush = newBrush;
        }

        public override Brush GetBrush(double opacity, SVG svg, SVGRender svgRender, Rect bounds)
        {
            if (Brush != null) return Brush;
            foreach(var server in m_pattternPaintServers)
            {
                svgRender.SVG.PaintServers.AddServer(server.Key, server.Value);
            }
            
            var db = new DrawingBrush() {Drawing = svgRender.LoadGroup(m_elements, null, false)};
            db.TileMode = TileMode.Tile;
            db.Transform = PatternTransform;
            db.Viewport = new Rect(X, Y, Width / bounds.Width, Height/ bounds.Height);
            db.ViewportUnits = BrushMappingMode.RelativeToBoundingBox;
            Brush = db;
            return db;
        }
    }
}
