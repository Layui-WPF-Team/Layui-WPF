using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Xml;
using System.Windows;

namespace LayUI.Wpf.Controls.SVG.Shapes
{
    public class Shape : ClipArtElement
    {
        protected Fill m_fill;

        protected Stroke m_stroke;

        protected TextStyle m_textstyle;

        protected string m_localStyle;

        internal Clip m_clip = null;

        internal Clip Clip
        {
            get
            {
                return this.m_clip;
            }
        }

        internal Geometry geometryElement;

        public virtual string PaintServerKey { get; set; }

        public string RequiredExtensions { get; set; }

        public string RequiredFeatures { get; set; }

        public Visibility Visibility { get; set; }

        public bool Display = true;

        public virtual Stroke Stroke
        {
            get
            {
                if (this.m_stroke != null) return this.m_stroke;
                var parent = this.Parent;
                while (parent != null)
                {
                    if (this.Parent.Stroke != null) return parent.Stroke;
                    parent = parent.Parent;
                }
                return null;
            }
            set
            {
                m_stroke = value;
            }
        }

        public virtual Fill Fill
        {
            get
            {
                if (this.m_fill != null) return this.m_fill;
                var parent = this.Parent;
                while (parent != null)
                {
                    if (parent.Fill != null) return parent.Fill;
                    parent = parent.Parent;
                }
                return null;
            }
            set
            {
                m_fill = value;
            }
        }

        public virtual TextStyle TextStyle
        {
            get
            {
                if (this.m_textstyle != null) return this.m_textstyle;
                var parent = this.Parent;
                while (parent != null)
                {
                    if (parent.m_textstyle != null) return parent.m_textstyle;
                    parent = parent.Parent;
                }
                return null;
            }
        }

        public double Opacity
        {
            get => Visibility == Visibility.Visible ? m_opacity : 0;
            set => m_opacity = value;
        }

        public virtual Transform Transform { get; private set; }

        internal virtual Filter.Filter Filter { get; private set; }

        //Used during render
        internal Shape RealParent;
        private double m_opacity;

        public Shape Parent { get; set; }

        public Shape(SVG svg, XmlNode node) : this(svg, node, null)
        {
        }

        public Shape(SVG svg, XmlNode node, Shape parent) : base(node)
        {
            this.Opacity = 1;
            this.Parent = parent;
            this.ParseAtStart(svg, node);
            if (node != null)
            {
                foreach (XmlAttribute attr in node.Attributes)
                    this.Parse(svg, attr.Name, attr.Value);
            }
            ParseLocalStyle(svg);
        }

        public Shape(SVG svg, List<ShapeUtil.Attribute> attrs, Shape parent) : base(null)
        {
            this.Opacity = 1;
            this.Parent = parent;
            if (attrs != null)
            {
                foreach (ShapeUtil.Attribute attr in attrs)
                    this.Parse(svg, attr.Name, attr.Value);
            }
            ParseLocalStyle(svg);
        }

        protected virtual void ParseAtStart(SVG svg, XmlNode node)
        {
            if (node != null)
            {
                var name = node.Name;
                if (name.Contains(":"))
                    name = name.Split(':')[1];

                if (svg.m_styles.TryGetValue(name, out var attributes))
                {
                    foreach (var xmlAttribute in attributes)
                    {
                        Parse(svg, xmlAttribute.Name, xmlAttribute.Value);
                    }
                }

                if (!string.IsNullOrEmpty(this.Id))
                {
                    if (svg.m_styles.TryGetValue("#" + this.Id, out attributes))
                    {
                        foreach (var xmlAttribute in attributes)
                        {
                            Parse(svg, xmlAttribute.Name, xmlAttribute.Value);
                        }
                    }
                }
            }
        }

        protected virtual void ParseLocalStyle(SVG svg)
        {
            if (!string.IsNullOrEmpty(this.m_localStyle))
                foreach (ShapeUtil.Attribute item in XmlUtil.SplitStyle(svg, this.m_localStyle))
                    this.Parse(svg, item.Name, item.Value);
        }

        protected virtual void Parse(SVG svg, string name, string value)
        {
            if (name.Contains(":"))
                name = name.Split(':')[1];

            if (name == SVGTags.sDisplay && value == "none")
            {
                this.Display = false;
            }

            if (name == SVGTags.sClass)
            {
                var classes = value.Split(' ');
                foreach (var @class in classes)
                {
                    if (svg.m_styles.TryGetValue("." + @class, out var attributes))
                    {
                        foreach (var xmlAttribute in attributes)
                        {
                            Parse(svg, xmlAttribute.Name, xmlAttribute.Value);
                        }
                    }
                }
                return;
            }
            if (name == SVGTags.sTransform)
            {
                this.Transform = ShapeUtil.ParseTransform(value.ToLower());
                return;
            }
            if (name == SVGTags.sVisibility)
            {
                this.Visibility = value == "hidden" ? Visibility.Hidden : Visibility.Visible;
            }
            if (name == SVGTags.sStroke)
            {
                this.GetStroke(svg).PaintServerKey = svg.PaintServers.Parse(value);
                return;
            }
            if (name == SVGTags.sStrokeWidth)
            {
                this.GetStroke(svg).Width = XmlUtil.ParseDouble(svg, value);
                return;
            }
            if (name == SVGTags.sStrokeOpacity)
            {
                this.GetStroke(svg).Opacity = XmlUtil.ParseDouble(svg, value) * 100;
                return;
            }
            if (name == SVGTags.sStrokeDashArray)
            {
                if (value == "none")
                {
                    this.GetStroke(svg).StrokeArray = null;
                    return;
                }
                ShapeUtil.StringSplitter sp = new ShapeUtil.StringSplitter(value);
                List<double> a = new List<double>();
                while (sp.More)
                {
                    a.Add(sp.ReadNextValue());
                }
                this.GetStroke(svg).StrokeArray = a.ToArray();
                return;
            }
            if (name == SVGTags.sRequiredFeatures)
            {
                this.RequiredFeatures = value.Trim();
                return;
            }
            if (name == SVGTags.sRequiredExtensions)
            {
                this.RequiredExtensions = value.Trim();
                return;
            }
            if (name == SVGTags.sStrokeLinecap)
            {
                this.GetStroke(svg).LineCap = (Stroke.eLineCap)System.Enum.Parse(typeof(Stroke.eLineCap), value);
                return;
            }
            if (name == SVGTags.sStrokeLinejoin)
            {
                this.GetStroke(svg).LineJoin = (Stroke.eLineJoin)System.Enum.Parse(typeof(Stroke.eLineJoin), value);
                return;
            }
            if (name == SVGTags.sFilterProperty)
            {
                if (value.StartsWith("url"))
                {
                    Shape result;
                    string id = ShapeUtil.ExtractBetween(value, '(', ')');
                    if (id.Length > 0 && id[0] == '#') id = id.Substring(1);
                    svg.m_shapes.TryGetValue(id, out result);
                    this.Filter = result as Filter.Filter;
                    return;
                }
                return;
            }
            if (name == SVGTags.sClipPathProperty)
            {
                if (value.StartsWith("url"))
                {
                    Shape result;
                    string id = ShapeUtil.ExtractBetween(value, '(', ')');
                    if (id.Length > 0 && id[0] == '#') id = id.Substring(1);
                    svg.m_shapes.TryGetValue(id, out result);
                    this.m_clip = result as Clip;
                    return;
                }
                return;
            }
            if (name == SVGTags.sFill)
            {
                this.GetFill(svg).PaintServerKey = svg.PaintServers.Parse(value);
                return;
            }
            if (name == SVGTags.sColor)
            {
                this.PaintServerKey = svg.PaintServers.Parse(value);
                return;
            }
            if (name == SVGTags.sFillOpacity)
            {
                this.GetFill(svg).Opacity = XmlUtil.ParseDouble(svg, value) * 100;
                return;
            }
            if (name == SVGTags.sFillRule)
            {
                this.GetFill(svg).FillRule = (Fill.eFillRule)System.Enum.Parse(typeof(Fill.eFillRule), value);
                return;
            }
            if (name == SVGTags.sOpacity)
            {
                this.Opacity = XmlUtil.ParseDouble(svg, value);
                return;
            }
            if (name == SVGTags.sStyle)
            {
                m_localStyle = value;
            }
            //********************** text *******************
            if (name == SVGTags.sFontFamily)
            {
                this.GetTextStyle(svg).FontFamily = value;
                return;
            }
            if (name == SVGTags.sFontSize)
            {
                this.GetTextStyle(svg).FontSize = XmlUtil.AttrValue(new ShapeUtil.Attribute(name, value));
                return;
            }
            if (name == SVGTags.sFontWeight)
            {
                this.GetTextStyle(svg).Fontweight = (FontWeight)new FontWeightConverter().ConvertFromString(value);
                return;
            }
            if (name == SVGTags.sFontStyle)
            {
                this.GetTextStyle(svg).Fontstyle = (FontStyle)new FontStyleConverter().ConvertFromString(value);
                return;
            }
            if (name == SVGTags.sTextDecoration)
            {
                TextDecoration t = new TextDecoration();
                if (value == "none") return;
                if (value == "underline") t.Location = TextDecorationLocation.Underline;
                if (value == "overline") t.Location = TextDecorationLocation.OverLine;
                if (value == "line-through") t.Location = TextDecorationLocation.Strikethrough;
                TextDecorationCollection tt = new TextDecorationCollection();
                tt.Add(t);
                this.GetTextStyle(svg).TextDecoration = tt;
                return;
            }
            if (name == SVGTags.sTextAnchor)
            {
                if (value == "start") this.GetTextStyle(svg).TextAlignment = TextAlignment.Left;
                if (value == "middle") this.GetTextStyle(svg).TextAlignment = TextAlignment.Center;
                if (value == "end") this.GetTextStyle(svg).TextAlignment = TextAlignment.Right;
                return;
            }
            if (name == "word-spacing")
            {
                this.GetTextStyle(svg).WordSpacing = XmlUtil.AttrValue(new ShapeUtil.Attribute(name, value));
                return;
            }
            if (name == "letter-spacing")
            {
                this.GetTextStyle(svg).LetterSpacing = XmlUtil.AttrValue(new ShapeUtil.Attribute(name, value));
                return;
            }
            if (name == "baseline-shift")
            {
                //GetTextStyle(svg).BaseLineShift = XmlUtil.AttrValue(new ShapeUtil.Attribute(name, value));
                this.GetTextStyle(svg).BaseLineShift = value;
                return;
            }

            //Debug.WriteLine("Unsupported: "+ name);
        }

        private Stroke GetStroke(SVG svg)
        {
            if (this.m_stroke == null) this.m_stroke = new Stroke(svg);
            return this.m_stroke;
        }

        protected Fill GetFill(SVG svg)
        {
            if (this.m_fill == null) this.m_fill = new Fill(svg);
            return this.m_fill;
        }

        protected TextStyle GetTextStyle(SVG svg)
        {
            if (this.m_textstyle == null) this.m_textstyle = new TextStyle(svg, this);
            return this.m_textstyle;
        }

        public override string ToString()
        {
            return this.GetType().Name + " (" + (Id ?? "") + ")";
        }
    }
}
