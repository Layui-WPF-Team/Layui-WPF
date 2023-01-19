using System;
using System.Xml;

namespace LayUI.Wpf.Controls.SVG.Shapes
{
    public class CircleShape : Shape
    {
        public double CX { get; set; }

        public double CY { get; set; }

        public double R { get; set; }

        public CircleShape(SVG svg, XmlNode node) : base(svg, node)
        {
            this.CX = XmlUtil.AttrValue(node, "cx", 0, svg.ViewBox.HasValue ? svg.ViewBox.Value.Width : svg.Size.Width);
            this.CY = XmlUtil.AttrValue(node, "cy", 0, svg.ViewBox.HasValue ? svg.ViewBox.Value.Height : svg.Size.Height);
            var diagRef = Math.Sqrt(svg.Size.Width * svg.Size.Width + svg.Size.Height * svg.Size.Height) / Math.Sqrt(2); // see https://oreillymedia.github.io/Using_SVG/extras/ch05-percentages.html
            if (svg.ViewBox.HasValue)
                diagRef = Math.Sqrt(svg.ViewBox.Value.Width * svg.ViewBox.Value.Width + svg.ViewBox.Value.Height * svg.ViewBox.Value.Height) / Math.Sqrt(2);
            this.R = XmlUtil.AttrValue(node, "r", 0, diagRef);
        }
    }
}
