using System.Windows;
using System.Windows.Media;

namespace LayUI.Wpf.Controls.SVG.PaintServer
{
    // http://www.w3.org/TR/SVGTiny12/painting.html#PaintServers
    public class PaintServer
    {
        public PaintServerManager Owner { get; private set; }

        protected Brush Brush = null;

        public PaintServer(PaintServerManager owner)
        {
            this.Owner = owner;
        }

        public PaintServer(PaintServerManager owner, Brush newBrush)
        {
            this.Owner = owner;
            this.Brush = newBrush;
        }

        public virtual Brush GetBrush(double opacity, SVG svg, SVGRender svgRender, Rect bounds)
        {
            return Brush;
        }

        public Brush GetBrush()
        {
            return Brush;
        }
    }
}
