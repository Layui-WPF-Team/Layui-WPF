using System.Windows;
using System.Windows.Media;

namespace LayUI.Wpf.Controls.SVG.PaintServer
{
    public class SolidColorPaintServer : PaintServer
    {
        public Color Color { get; set; }

        public SolidColorPaintServer(PaintServerManager owner, Color c)
            : base(owner)
        {
            this.Color = c;
        }

        public SolidColorPaintServer(PaintServerManager owner, Brush newBrush) : base(owner)
        {
            Brush = newBrush;
        }

        public override Brush GetBrush(double opacity, SVG svg, SVGRender svgRender, Rect bounds)
        {
            if (Brush != null) return Brush;
            byte a = (byte)(255 * opacity / 100);
            Color c = this.Color;
            Color newcol = Color.FromArgb(a, c.R, c.G, c.B);
            Brush = new SolidColorBrush(newcol);
            return Brush;
        }
    }
}
