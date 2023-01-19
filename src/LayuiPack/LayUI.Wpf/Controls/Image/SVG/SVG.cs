using System;
using System.IO;
using System.IO.Compression;
using System.Xml;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using System.Windows;
using System.Windows.Media;

using LayUI.Wpf.Controls.SVG.FileLoaders;
using LayUI.Wpf.Controls.SVG.PaintServer;
using LayUI.Wpf.Controls.SVG.Shapes;
using Group = LayUI.Wpf.Controls.SVG.Shapes.Group;

namespace LayUI.Wpf.Controls.SVG
{
    /// <summary>
    /// This is the class that reads and parses the XML file.
    /// </summary>
    public class SVG
    {
        internal Dictionary<string, Shape> m_shapes;
        internal Dictionary<string, List<XmlUtil.StyleItem>> m_styles;

        private List<Shape> m_elements;
        private Dictionary<string, Brush> m_customBrushes;

        public SVG()
        {
            this.Size     = new Size(300, 150);
            this.Filename = "";
            m_shapes = new Dictionary<string, Shape>();
            m_styles = new Dictionary<string, List<XmlUtil.StyleItem>>();
        }

        public SVG(IExternalFileLoader externalFileLoader)
            : this()
        {
            this.ExternalFileLoader = externalFileLoader;
        }

        public SVG(string filename) : this(filename, FileSystemLoader.Instance)
        {
        }

        public SVG(string filename, IExternalFileLoader externalFileLoader)
            : this()
        {
            this.ExternalFileLoader = externalFileLoader;
            this.Load(filename);
        }

        public SVG(Stream stream) : this(stream, FileSystemLoader.Instance)
        {
        }

        public SVG(Stream stream, IExternalFileLoader externalFileLoader)
            : this()
        {
            this.ExternalFileLoader = externalFileLoader;
            this.Load(stream);
        }

        public SVG(XmlNode svgTag, IExternalFileLoader externalFileLoader)
            : this()
        {
            this.ExternalFileLoader = externalFileLoader;
            this.Load(svgTag);
        }

        public string Filename { get; private set; }

        public Rect? ViewBox { get; set; }

        public Size Size { get; set; }

        public IExternalFileLoader ExternalFileLoader { get; set; }

        public Dictionary<string, Brush> CustomBrushes
        {
            get => m_customBrushes;
            set {
                m_customBrushes = value;
                if (m_customBrushes != null)
                {
                    foreach (var brush in m_customBrushes)
                    {
                        PaintServers.CreateServerFromBrush(brush.Key, brush.Value);
                    }
                }
            }
        }

        internal IDictionary<string, List<XmlUtil.StyleItem>> Styles
        {
            get {
                return m_styles;
            }
        }

        public void AddShape(string id, Shape shape)
        {
            //System.Diagnostics.Debug.Assert(id.Length > 0 && this.m_shapes.ContainsKey(id) == false);
            this.m_shapes[id] = shape;
        }

        public Shape GetShape(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }

            Shape shape = null;
            this.m_shapes.TryGetValue(id, out shape);
            return shape;
        }

        public PaintServerManager PaintServers { get; } = new PaintServerManager();

        public IList<Shape> Elements 
        {
            get {
                if (m_elements == null)
                {
                    return new List<Shape>();
                }

                return m_elements.AsReadOnly();
            }
        }

        public void LoadXml(string fileXml)
        {
            this.Filename = "";
            if (string.IsNullOrWhiteSpace(fileXml))
            {
                return;
            }

            XmlDocument doc = new XmlDocument();
            doc.XmlResolver = null;
            doc.LoadXml(fileXml);

            this.Load(doc);
        }

        public void Load(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath) || File.Exists(filePath) == false)
            {
                this.Filename = "";
                return;
            }
            this.Filename = filePath;

            // Provide a support for the .svgz files...
            UriBuilder fileUrl = new UriBuilder(filePath);
            if (string.Equals(fileUrl.Scheme, "file"))
            {
                string fileExt = Path.GetExtension(filePath);
                if (string.Equals(fileExt, ".svgz", StringComparison.OrdinalIgnoreCase))
                {
                    using (var fileStream = File.OpenRead(fileUrl.Uri.LocalPath))
                    {
                        using (var zipStream = new GZipStream(fileStream, CompressionMode.Decompress))
                        {
                            this.Load(zipStream);
                        }
                    }
                    return;
                }
            }

            XmlDocument doc = new XmlDocument();
            doc.XmlResolver = null;
            doc.Load(filePath);

            this.Load(doc);
        }

        public void Load(Uri fileUri)
        {
            if (fileUri == null || !fileUri.IsAbsoluteUri)
            {
                return;
            }

            var filePath = fileUri.IsFile ? fileUri.LocalPath : fileUri.AbsoluteUri;

            if (string.IsNullOrWhiteSpace(filePath))
            {
                this.Filename = "";
                return;
            }
            if (fileUri.IsFile)
            {
                if (File.Exists(filePath) == false)
                {
                    this.Filename = "";
                    return;
                }
            }

            this.Filename = filePath;
            XmlDocument doc = new XmlDocument();
            doc.XmlResolver = null;
            doc.Load(filePath);

            this.Load(doc);
        }

        public void Load(Stream stream)
        {
            if (stream == null)
            {
                return;
            }

            XmlDocument doc = new XmlDocument();
            doc.XmlResolver = null;
            doc.Load(stream);

            this.Load(doc);
        }

        public void Load(XmlReader xmlReader)
        {
            if (xmlReader == null)
            {
                return;
            }

            XmlDocument doc = new XmlDocument();
            doc.XmlResolver = null;
            doc.Load(xmlReader);

            this.Load(doc);
        }

        public void Load(TextReader txtReader)
        {
            if (txtReader == null)
            {
                return;
            }

            XmlDocument doc = new XmlDocument();
            doc.XmlResolver = null;
            doc.Load(txtReader);

            this.Load(doc);
        }

        private void Load(XmlDocument svgDocument)
        {
            if (svgDocument == null)
            {
                return;
            }
            this.LoadStyles(svgDocument);

            var svgTags = svgDocument.GetElementsByTagName("svg");
            if (svgTags == null || svgTags.Count == 0)
            {
                return;
            }

            XmlNode svgTag = svgTags[0];

            m_elements = this.Parse(svgTag);
        }

        private void Load(XmlNode svgTag)
        {
            if (svgTag == null)
            {
                return;
            }
            this.LoadStyles(svgTag);

            m_elements = this.Parse(svgTag);
        }

        private void LoadStyles(XmlNode doc)
        {
            if (ExternalFileLoader == null)
            {
                return;
            }

            var cssUrlNodes = (from XmlNode childNode in doc.ChildNodes
                where childNode.NodeType == XmlNodeType.ProcessingInstruction && childNode.Name == "xml-stylesheet"
                select (XmlProcessingInstruction) childNode).ToList();

            if (cssUrlNodes.Count != 0)
            {
                foreach (var node in cssUrlNodes)
                {
                    var url = Regex.Match(node.Data, "href=\"(?<url>.*?)\"").Groups["url"].Value;
                    var stream = ExternalFileLoader.LoadFile(url, this.Filename);
                    if (stream != null)
                    {
                        using (stream)
                        {
                            using (var reader = new StreamReader(stream, Encoding.UTF8))
                            {
                                var style = reader.ReadToEnd();
                                StyleParser.ParseStyle(this, style);
                            }
                        }
                    }
                }
            }
        }

        private List<Shape> Parse(XmlNode node)
        {
            var vb = node.Attributes.GetNamedItem("viewBox");
            if (vb != null)
            {
                var cord = vb.Value.Split(' ');
                var cult = CultureInfo.InvariantCulture;
                this.ViewBox = new Rect(double.Parse(cord[0], cult), 
                    double.Parse(cord[1], cult), double.Parse(cord[2], cult), double.Parse(cord[3], cult));
            }

            this.Size = new Size(XmlUtil.AttrValue(node, "width", 300), XmlUtil.AttrValue(node, "height", 150));
            
            var lstElements = new List<Shape>();
            if (node == null || (node.Name != SVGTags.sSvg && node.Name != SVGTags.sPattern))
                throw new FormatException("Not a valide SVG node");

            foreach (XmlNode childnode in node.ChildNodes)
                Group.AddToList(this, lstElements, childnode, null);

            return lstElements;
        }
    }
}
