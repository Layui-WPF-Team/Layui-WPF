using System.Windows;
using System.Windows.Media;

namespace LayUI.Wpf.Controls.SVG.PaintServer
{
    public class CurrentColorPaintServer : PaintServer
    {
        public CurrentColorPaintServer(PaintServerManager owner)
            : base(owner)
        {
        }

        public override Brush GetBrush(double opacity, SVG svg, SVGRender svgRender, Rect bounds)
        {
            return null;
        }
    }
}
