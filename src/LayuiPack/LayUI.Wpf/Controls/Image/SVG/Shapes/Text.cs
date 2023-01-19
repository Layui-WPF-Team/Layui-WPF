using System;
using System.Collections.Generic;
using System.Xml;

using LayUI.Wpf.Controls.SVG.Shapes;

namespace LayUI.Wpf.Controls.SVG
{
	class TextShape : Shape
	{
		public double X { get; set; }
		public double Y { get; set; }
		public string Text { get; set; }
		public TSpan.Element TextSpan {get; private set;}
		static Fill DefaultFill = null;
		static Stroke DefaultStroke = null;
		public override Fill Fill 
		{ 
			get 
			{
				Fill f = base.Fill;
				if (f == null)
					f = DefaultFill;
				return f;
			}
		}
		public override Stroke Stroke
		{ 
			get 
			{
				Stroke f = base.Stroke;
				if (f == null)
					f = DefaultStroke;
				return f;
			}
		}
		public TextShape(SVG svg, XmlNode node, Shape parent) : base(svg, node, parent)
		{
			this.X = XmlUtil.AttrValue(node, "x", 0);
			this.Y = XmlUtil.AttrValue(node, "y", 0);
			this.Text = node.InnerText;
			this.GetTextStyle(svg);
			// check for tSpan tag
			if (node.InnerXml.IndexOf("<") >= 0)
				this.TextSpan = this.ParseTSpan(svg, node.InnerXml);
			if (DefaultFill == null)
			{
				DefaultFill = new Fill(svg);
				DefaultFill.PaintServerKey = svg.PaintServers.Parse("black");
			}
			if (DefaultStroke == null)
			{
				DefaultStroke = new Stroke(svg);
				DefaultStroke.Width = 0.1;
			}
		}

		TSpan.Element ParseTSpan(SVG svg, string tspanText)
		{
			try
			{
				return TSpan.Parse(svg, tspanText, this);
			}
			catch
			{
				return null;
			}
		}

		public class TSpan
		{
			public class Element : Shape
			{
				public enum eElementType
				{
					Tag,
					Text,
				}
				public override System.Windows.Media.Transform Transform 
				{ 
					get {return this.Parent.Transform; }
				}
				public eElementType ElementType {get; private set;}
				public List<ShapeUtil.Attribute> Attributes {get; set;}
				public List<Element> Children {get; private set;}
				public int StartIndex {get; set;}
				public string Text {get; set;}
				public Element End {get; set;}
				public Element(SVG svg, Shape parent, string text) : base(svg, (XmlNode)null, parent)
				{
					this.ElementType = eElementType.Text;
					this.Text = text;
				}
				public Element(SVG svg, Shape parent, eElementType eType, List<ShapeUtil.Attribute> attrs) : base(svg, attrs, parent)
				{
					this.ElementType = eType;
					this.Text = string.Empty;
					this.Children = new List<Element>();
				}
				public override string ToString()
				{
					return this.Text;
				}
			}

			static Element NextTag(SVG svg, Element parent, string text, ref int curPos)
			{
				int start = text.IndexOf("<", curPos);
				if (start < 0)
					return null;
				int end = text.IndexOf(">", start+1);
				if (end < 0)
					throw new Exception("Start '<' with no end '>'");

				end++;

				string tagtext = text.Substring(start, end - start);
				if (tagtext.IndexOf("<", 1) > 0)
					throw new Exception(string.Format("Start '<' within tag 'tag'"));

				List<ShapeUtil.Attribute> attrs = new List<ShapeUtil.Attribute>();
				int attrstart = tagtext.IndexOf("tspan");
				if (attrstart > 0)
				{
					attrstart += 5;
					while (attrstart < tagtext.Length-1)
						attrs.Add(ShapeUtil.ReadNextAttr(tagtext, ref attrstart));
				}
	
				Element tag = new Element(svg, parent, Element.eElementType.Tag, attrs);
				tag.StartIndex = start;
				tag.Text = text.Substring(start, end - start);
				if (tag.Text.IndexOf("<", 1) > 0)
					throw new Exception(string.Format("Start '<' within tag 'tag'"));

				curPos = end;
				return tag;
			}

			static Element Parse(SVG svg, string text, ref int curPos, Element parent, Element curTag)
			{
				Element tag = curTag;
				if (tag == null)
					tag = NextTag(svg, parent, text, ref curPos);
				while (curPos < text.Length)
				{
					int prevPos = curPos;
					Element next = NextTag(svg, tag, text, ref curPos);
					if (next == null && curPos < text.Length)
					{
						// remaining pure text 
						string s = text.Substring(curPos, text.Length - curPos);
						tag.Children.Add(new Element(svg, tag, s));
						return tag;
					}
					if (next != null && next.StartIndex-prevPos > 0)
					{
						// pure text between tspan elements
						int diff = next.StartIndex-prevPos;
						string s = text.Substring(prevPos, diff);
						tag.Children.Add(new Element(svg, tag, s));
					}
					if (next.Text.StartsWith("<tspan"))
					{
						// new nested element
						next = Parse(svg, text, ref curPos, tag, next);
						tag.Children.Add(next);
						continue;
					}
					if (next.Text.StartsWith("</tspan"))
					{
						// end of cur element
						tag.End = next;
						return tag;
					}
					if (next.Text.StartsWith("<textPath"))
					{
						continue;
					}
					if (next.Text.StartsWith("</textPath"))
					{
						continue;
					}
					throw new Exception(string.Format("unexpected tag '{0}'", next.Text));
				}
				return tag;
			}
			public static Element Parse(SVG svg, string text, TextShape owner)
			{
				int curpos = 0;
				Element root = new Element(svg, owner, Element.eElementType.Tag, null);
				root.Text = "<root>";
				root.StartIndex = 0;
				return Parse(svg, text, ref curpos, null, root);
			}
			public void Print(Element tag, string indent)
			{
				if (tag.ElementType == Element.eElementType.Text)
					Console.WriteLine("{0} '{1}'", indent, tag.Text);
				indent += "   ";
				foreach (Element c in tag.Children)
					this.Print(c, indent);
			}
		}
	}
}
