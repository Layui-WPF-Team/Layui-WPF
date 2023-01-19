using System.Windows;
using System.Windows.Media;

using LayUI.Wpf.Controls.SVG.PaintServer;
using LayUI.Wpf.Controls.SVG.Shapes;

namespace LayUI.Wpf.Controls.SVG
{
    public class Stroke
    {
        public enum eLineCap
        {
            butt,
            round,
            square,
        }

        public enum eLineJoin
        {
            miter,
            round,
            bevel,
        }

        public string PaintServerKey { get; set; }

        public double Width { get; set; }

        public double Opacity { get; set; }

        public eLineCap LineCap { get; set; }

        public eLineJoin LineJoin { get; set; }

        public double[] StrokeArray { get; set; }

        public Stroke(SVG svg)
        {
            this.Width = 1;
            this.LineCap = eLineCap.butt;
            this.LineJoin = eLineJoin.miter;
            this.Opacity = 100;
        }

        public bool IsEmpty(SVG svg)
        {
            if (svg == null) return true;

            if (!svg.PaintServers.ContainsServer(this.PaintServerKey))
            {
                return true;
            }
            return svg.PaintServers.GetServer(this.PaintServerKey) == null;
        }

        public Brush StrokeBrush(SVG svg, SVGRender svgRender, Shape shape, double elementOpacity, Rect bounds)
        {
            var paintServer = svg.PaintServers.GetServer(PaintServerKey);
            if (paintServer != null)
            {
                if (paintServer is CurrentColorPaintServer)
                {
                    var shapePaintServer = svg.PaintServers.GetServer(shape.PaintServerKey);
                    if (shapePaintServer != null)
                    {
                        return shapePaintServer.GetBrush(this.Opacity * elementOpacity, svg, svgRender, bounds);

                    }
                }
                if (paintServer is InheritPaintServer)
                {
                    var p = shape.RealParent ?? shape.Parent;
                    while (p != null)
                    {
                        if (p.Stroke != null)
                        {
                            var checkPaintServer = svg.PaintServers.GetServer(p.Stroke.PaintServerKey);
                            if (!(checkPaintServer is InheritPaintServer))
                            {
                                return checkPaintServer.GetBrush(this.Opacity * elementOpacity, svg, svgRender, bounds);
                            }
                        }
                        p = p.RealParent ?? p.Parent;
                    }
                    return null;
                }
                return paintServer.GetBrush(this.Opacity * elementOpacity, svg, svgRender, bounds);
            }
            return null;
        }
    }
}
