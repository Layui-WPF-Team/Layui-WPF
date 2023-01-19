using System.Xml;

namespace LayUI.Wpf.Controls.SVG.Shapes
{
    public class UseShape : Shape
    {
        public double X { get; set; }

        public double Y { get; set; }

        public string hRef { get; set; }

        public UseShape(SVG svg, XmlNode node) : base(svg, node)
        { }

        protected override void Parse(SVG svg, string name, string value)
        {
            if (name.Contains(":"))
                name = name.Split(':')[1];

            if (name == SVGTags.sHref)
            {
                this.hRef = value;
                if (this.hRef.StartsWith("#"))
                    this.hRef = this.hRef.Substring(1);
                return;
            }
            if (name == "x")
            {
                this.X = XmlUtil.GetDoubleValue(value, svg.Size.Width);
                return;
            }
            if (name == "y")
            {
                this.Y = XmlUtil.GetDoubleValue(value, svg.Size.Height);
                return;
            }

            base.Parse(svg, name, value);
        }
    }
}
