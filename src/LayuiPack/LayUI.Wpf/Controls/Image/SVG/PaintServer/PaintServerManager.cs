using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows.Media;
using System.Xml;

namespace LayUI.Wpf.Controls.SVG.PaintServer
{
    public class PaintServerManager
    {
        private static Dictionary<string, Color> m_knownColors = null;

        private Dictionary<string, PaintServer> m_servers = new Dictionary<string, PaintServer>();

        public string Create(SVG svg, XmlNode node)
        {
            if (node.Name == SVGTags.sLinearGradient)
            {
                string id = XmlUtil.AttrValue(node, "id");
                if (this.m_servers.ContainsKey(id) == false) this.m_servers[id] = new LinearGradientColorPaintServerPaintServer(this, node);
                return id;
            }
            if (node.Name == SVGTags.sRadialGradient)
            {
                string id = XmlUtil.AttrValue(node, "id");
                if (this.m_servers.ContainsKey(id) == false) this.m_servers[id] = new RadialGradientColorPaintServerPaintServer(this, node);
                return id;
            }
            if (node.Name == SVGTags.sPattern)
            {
                string id = XmlUtil.AttrValue(node, "id");
                if (this.m_servers.ContainsKey(id) == false) this.m_servers[id] = new PatternPaintServer(this, svg, node);
                return id;
            }
            return null;
        }

        public bool ContainsServer(string serverKey)
        {
            if (string.IsNullOrWhiteSpace(serverKey))
            {
                return false;
            }

            return m_servers.ContainsKey(serverKey);
        }

        public void AddServer(string key, PaintServer server)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return;
            }

            m_servers[key] = server;
        }

        public PaintServer GetServer(string serverKey)
        {
            if (string.IsNullOrWhiteSpace(serverKey))
            {
                return null;
            }

            PaintServer result;
            if (serverKey != null && m_servers.TryGetValue(serverKey, out result)) return result;
            else return null;
        }

        public Dictionary<string, PaintServer> GetServers()
        {
            return m_servers;
        }

        public void CreateServerFromBrush(string key, Brush customBrush)
        {
            if (customBrush is SolidColorBrush)
            {
                m_servers[key] = new SolidColorPaintServer(this, customBrush);
            }
            else if (customBrush is LinearGradientBrush)
            {
                m_servers[key] = new LinearGradientColorPaintServerPaintServer(this, customBrush);
            }
            else if(customBrush is RadialGradientBrush)
            {
                m_servers[key] = new RadialGradientColorPaintServerPaintServer(this, customBrush);
            }
            else if(customBrush is DrawingBrush)
            {
                m_servers[key] = new PatternPaintServer(this, customBrush);
            }
            else if(customBrush != null)
            {
                m_servers[key] = new PaintServer(this, customBrush);
            }
        }

        public string Parse(string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value)) return null;
                if (value == SVGTags.sNone) return null;
                PaintServer result = null;
                if (this.m_servers.TryGetValue(value, out result)) return value;
                if (value == SVGTags.sInherit)
                {
                    m_servers[value] = new InheritPaintServer(this);
                    return value;
                }
                if (value == SVGTags.sCurrentColor)
                {
                    m_servers[value] = new CurrentColorPaintServer(this);
                    return value;
                }
                if (value[0] == '#') return this.ParseSolidColor(value);
                
                if (value.StartsWith("url"))
                {
                    string id = ShapeUtil.ExtractBetween(value, '(', ')');
                    if (id.Length > 0 && id[0] == '#') id = id.Substring(1);
                    this.m_servers.TryGetValue(id, out result);
                    return id;
                }

                if (value.StartsWith("rgb"))
                {
                    return this.ParseSolidRgbColor(value);
                }

                return this.ParseKnownColor(value.ToLower());
            }
            catch (Exception) { }

            return null;
        }

        public static Color ParseHexColor(string value)
        {
            // format is #xxFF00FF where xx is optional (the a value)
            // if format ix #rgb then the values are replicated #rrggbb
            int start = 0;
            if (value[start] == '#') start++;

            uint u = Convert.ToUInt32(value.Substring(start), 16);
            if (value.Length <= 4)
            {
                uint newval = 0;
                newval |= (u & 0x000f00) << 12;
                newval |= (u & 0x000f00) << 8;
                newval |= (u & 0x0000f0) << 8;
                newval |= (u & 0x0000f0) << 4;
                newval |= (u & 0x00000f) << 4;
                newval |= (u & 0x00000f);
                u = newval;
            }
            byte a = (byte)((u & 0xff000000) >> 24);
            byte r = (byte)((u & 0x00ff0000) >> 16);
            byte g = (byte)((u & 0x0000ff00) >> 8);
            byte b = (byte)(u & 0x000000ff);
            if (a == 0) a = 255;
            return Color.FromArgb(a, r, g, b);
        }

        public static Color KnownColor(string value)
        {
            LoadKnownColors();
            if (m_knownColors.ContainsKey(value)) return m_knownColors[value];
            return Colors.Black;
        }

        private string ParseSolidColor(string value)
        {
            string id = "_solid" + value;
            PaintServer result;
            if (this.m_servers.TryGetValue(id, out result)) return id;
            result = new SolidColorPaintServer(this, ParseHexColor(value));
            this.m_servers[id] = result;
            return id;
        }

        private string ParseSolidRgbColor(string value)
        {
            value = value.Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace(" ", "");
            if (value.StartsWith("rgb("))
            {
                var newVal = value.Substring(4, value.Length - 5).Split(',');
                int alpha = 255;
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("#{0:X2}", alpha);
                sb.AppendFormat("{0:X2}", ParseColorNumber(newVal[0]));
                sb.AppendFormat("{0:X2}", ParseColorNumber(newVal[1]));
                sb.AppendFormat("{0:X2}", ParseColorNumber(newVal[2]));
                return ParseSolidColor(sb.ToString());
                //return ParseSolidColor("#" + ParseColorNumber(newVal[0]).ToString("x") + ParseColorNumber(newVal[1]).ToString("x") + ParseColorNumber(newVal[2]).ToString("x"));
            }
            if (value.StartsWith("rgba("))
            {
                var newVal = value.Substring(5, value.Length - 6).Split(',');
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("#{0:X2}", ParseColorNumber(newVal[0]));
                sb.AppendFormat("{0:X2}", ParseColorNumber(newVal[1]));
                sb.AppendFormat("{0:X2}", ParseColorNumber(newVal[2]));
                sb.AppendFormat("{0:X2}", ParseColorNumber(newVal[3]));
                return ParseSolidColor(sb.ToString());
                //return ParseSolidColor("#" + ParseColorNumber(newVal[0]).ToString("x") + ParseColorNumber(newVal[1]).ToString("x") + ParseColorNumber(newVal[2]).ToString("x") + ParseColorNumber(newVal[3]).ToString("x"));
            }

            return null;
        }

        private int ParseColorNumber(string value)
        {
            if (value.EndsWith("%"))
            {
                var nr = int.Parse(value.Substring(0, value.Length - 1));
                if (nr < 0)
                    nr = 255 - nr;
                return nr * 255 / 100;
            }

            return int.Parse(value);
        }

        private string ParseKnownColor(string value)
        {
            LoadKnownColors();
            PaintServer result;
            if (this.m_servers.TryGetValue(value, out result)) return value;
            Color c;
            if (m_knownColors.TryGetValue(value, out c))
            {
                result = new SolidColorPaintServer(this, c);
                this.m_servers[value] = result;
                return value;
            }
            return null;
        }

        private static void LoadKnownColors()
        {
            if (m_knownColors == null) m_knownColors = new Dictionary<string, Color>();
            if (m_knownColors.Count == 0)
            {
                PropertyInfo[] propinfos = typeof(Colors).GetProperties(BindingFlags.Public | BindingFlags.Static);
                foreach (PropertyInfo info in propinfos)
                {
                    if (info.PropertyType == typeof(Color)) m_knownColors[info.Name.ToLower()] = (Color)info.GetValue(typeof(Color), null);
                }
            }
        }
    }
}