using System;
using System.Xml;
using LayUI.Wpf.Controls.SVG;
using LayUI.Wpf.Controls.SVG.Shapes;

namespace LayUI.Wpf.Controls.SVG.Animation
{
    public class AnimationBase : Shape
    {
        //https://www.mediaevent.de/tutorial/svg-animate-attribute.html
        public TimeSpan Duration { get; set; }

        public AnimationBase(global::LayUI.Wpf.Controls.SVG.SVG svg, XmlNode node, Shape parent)
            : base(svg, node, parent)
        {
            var d = XmlUtil.AttrValue(node, "dur", "");
            if (d.EndsWith("ms"))
                Duration = TimeSpan.FromMilliseconds(double.Parse(d.Substring(0, d.Length - 2)));
            else if (d.EndsWith("s"))
                Duration = TimeSpan.FromSeconds(double.Parse(d.Substring(0, d.Length - 1)));
            else
                Duration = TimeSpan.FromSeconds(double.Parse(d));
        }
    }
}
