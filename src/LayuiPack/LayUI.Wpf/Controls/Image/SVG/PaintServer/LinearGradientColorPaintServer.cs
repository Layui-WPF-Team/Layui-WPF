using System.Xml;

using System.Windows;
using System.Windows.Media;

namespace LayUI.Wpf.Controls.SVG.PaintServer
{
    public class LinearGradientColorPaintServerPaintServer : GradientColorPaintServer
    {
        public double X1 { get; private set; }

        public double Y1 { get; private set; }

        public double X2 { get; private set; }

        public double Y2 { get; private set; }

        public LinearGradientColorPaintServerPaintServer(PaintServerManager owner, XmlNode node)
            : base(owner, node)
        {
            System.Diagnostics.Debug.Assert(node.Name == SVGTags.sLinearGradient);
            this.X1 = XmlUtil.AttrValue(node, "x1", double.NaN);
            this.Y1 = XmlUtil.AttrValue(node, "y1", double.NaN);
            this.X2 = XmlUtil.AttrValue(node, "x2", double.NaN);
            this.Y2 = XmlUtil.AttrValue(node, "y2", double.NaN);
        }

        public LinearGradientColorPaintServerPaintServer(PaintServerManager owner, Brush newBrush) : base(owner)
        {
            Brush = newBrush;
        }

        public override Brush GetBrush(double opacity, SVG svg, SVGRender svgRender, Rect bounds)
        {
            if (this.Brush != null) return this.Brush;

            LinearGradientBrush b = new LinearGradientBrush();
            foreach (GradientStop stop in this.Stops) b.GradientStops.Add(stop);

            b.MappingMode = BrushMappingMode.RelativeToBoundingBox;
            b.StartPoint = new Point(0, 0);
            b.EndPoint = new Point(1, 0);
            
            if (this.GradientUnits == SVGTags.sUserSpaceOnUse)
            {
                Transform tr = this.Transform as Transform;
                if (tr != null)
                {
                    b.StartPoint = tr.Transform(new Point(this.X1, this.Y1));
                    b.EndPoint = tr.Transform(new Point(this.X2, this.Y2));
                }
                else
                {
                    b.StartPoint = new Point(this.X1, this.Y1);
                    b.EndPoint = new Point(this.X2, this.Y2);
                }
                this.Transform = null;
                b.MappingMode = BrushMappingMode.Absolute;
            }
            else
            {
                this.Normalize();
                if (double.IsNaN(this.X1) == false) b.StartPoint = new Point(this.X1, this.Y1);
                if (double.IsNaN(this.X2) == false) b.EndPoint = new Point(this.X2, this.Y2);
            }

            this.Brush = b;

            return b;
        }

        private void Normalize()
        {
            // This is until proper 'userspace' is supported.
            // crude normalization of the transition points.
            // gradient transition line is alwaysfrom 0 to 1
            if (double.IsNaN(this.X1) == false && double.IsNaN(this.X2) == false)
            {
                double min = this.X1;
                if (this.X2 < this.X1) min = this.X2;
                this.X1 -= min;
                this.X2 -= min;
                double scale = this.X1;
                if (this.X2 > this.X1) scale = this.X2;
                if (scale != 0)
                {
                    this.X1 /= scale;
                    this.X2 /= scale;
                }
            }
            if (double.IsNaN(this.Y1) == false && double.IsNaN(this.Y2) == false)
            {
                double min = this.Y1;
                if (this.Y2 < this.Y1) min = this.Y2;
                this.Y1 -= min;
                this.Y2 -= min;
                double scale = this.Y1;
                if (this.Y2 > this.Y1) scale = this.Y2;
                if (scale != 0)
                {
                    this.Y1 /= scale;
                    this.Y2 /= scale;
                }
            }
        }
    }
}
