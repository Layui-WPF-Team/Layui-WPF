using System;
using System.IO;
using System.Xml;
using System.Diagnostics;

using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LayUI.Wpf.Controls.SVG.Shapes
{
    public class ImageShape : Shape
    {
        public double X { get; set; }

        public double Y { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public ImageSource ImageSource { get; }

        public ImageShape(SVG svg, XmlNode node) : base(svg, node)
        {
            this.X      = XmlUtil.AttrValue(node, "x", 0, svg.Size.Width);
            this.Y      = XmlUtil.AttrValue(node, "y", 0, svg.Size.Height);
            this.Width  = XmlUtil.AttrValue(node, "width", 0, svg.Size.Width);
            this.Height = XmlUtil.AttrValue(node, "height", 0, svg.Size.Height);
            string hRef = XmlUtil.AttrValue(node, "xlink:href", string.Empty);
            if (hRef.Length > 0)
            {
                try
                {
                    Stream imageStream = null;
                    if (hRef.StartsWith("data:image/png;base64", StringComparison.OrdinalIgnoreCase))
                    {
                        var embeddedImage = hRef.Substring("data:image/png;base64,".Length);
                        if (!string.IsNullOrWhiteSpace(embeddedImage))
                        {
                            imageStream = new MemoryStream(Convert.FromBase64String(embeddedImage));
                        }
                    }
                    else
                    {
                        if (svg.ExternalFileLoader != null)
                        {
                            imageStream = svg.ExternalFileLoader.LoadFile(hRef, svg.Filename);
                        }
                    }
                    if (imageStream != null)
                    {
                        BitmapImage b = new BitmapImage();
                        b.BeginInit();
                        b.StreamSource = imageStream;
                        b.EndInit();

                        this.ImageSource = b;
                    }
                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.ToString());
                }
            }
        }
    }
}
