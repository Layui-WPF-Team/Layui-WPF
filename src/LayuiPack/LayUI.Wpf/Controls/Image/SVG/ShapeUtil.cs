using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows;
using System.Xml;

namespace LayUI.Wpf.Controls.SVG
{
    public static class ShapeUtil
    {
        public static Transform ParseTransform(string value)
        {
            //todo, increase perf. and object creation of this code (check with acid after)
            var transforms = value.Split(')');
            if (transforms.Length == 2)
                return ParseTransformInternal(value);

            var tg = new TransformGroup();
            foreach (var transform in transforms.OrderBy(x => x.StartsWith(SVGTags.sTranslate))) // to check why ordering is needed (see acid.svg)
            {
                if (!string.IsNullOrEmpty(transform))
                {
                    var transObj = ParseTransformInternal(transform + ")");
                    if (transObj != null)
                    {
                        tg.Children.Add(transObj);
                    }
                }
            }
            return tg;
        }

        private static Transform ParseTransformInternal(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }
            value = value.Trim();

            string type = ExtractUntil(value, '(').TrimStart(',');
            string v1 = ExtractBetween(value, '(', ')');

            ShapeUtil.StringSplitter split = new ShapeUtil.StringSplitter(v1);
            List<double> values = new List<double>();
            while (split.More)
                values.Add(split.ReadNextValue());
            if (type == SVGTags.sTranslate)
                if (values.Count == 1)
                    return new TranslateTransform(values[0], 0);
                else
                    return new TranslateTransform(values[0], values[1]);
            if (type == SVGTags.sMatrix)
                return Transform.Parse(v1);
            if (type == SVGTags.sScale)
                if (values.Count == 1)
                    return new ScaleTransform(values[0], values[0]);
                else
                    return new ScaleTransform(values[0], values[1]);
            if (type == SVGTags.sSkewX)
                return new SkewTransform(values[0], 0);
            if (type == SVGTags.sSkewY)
                return new SkewTransform(0, values[0]);
            if (type == SVGTags.sRotate)
            {
                if (values.Count == 1)
                    return new RotateTransform(values[0], 0, 0);
                if (values.Count == 2)
                    return new RotateTransform(values[0], values[1], 0);
                return new RotateTransform(values[0], values[1], values[2]);
            }

            return null;
        }

        public static string ExtractUntil(string value, char ch)
        {
            int index = value.IndexOf(ch);
            if (index <= 0)
                return value;
            return value.Substring(0, index);
        }
        public static string ExtractBetween(string value, char startch, char endch)
        {
            int start = value.IndexOf(startch);
            if (startch < 0)
                return value;
            start++;
            int end = value.IndexOf(endch, start);
            if (endch < 0)
                return value.Substring(start);
            return value.Substring(start, end - start);
        }

        public class StringSplitter
        {
            string m_value;
            int m_curPos = -1;
            char[] NumberChars = "0123456789.-".ToCharArray();
            public StringSplitter(string value)
            {
                this.m_value = value;
                this.m_curPos = 0;
            }
            public void SetString(string value, int startpos)
            {
                this.m_value = value;
                this.m_curPos = startpos;
            }
            public bool More 
            {
                get
                {
                    return this.m_curPos >= 0 && this.m_curPos < this.m_value.Length;
                }
            }
            public double ReadNextValue()
            {
                int startpos = this.m_curPos;
                if (startpos < 0)
                    startpos = 0;
                if (startpos >= this.m_value.Length)
                    return float.NaN;
                string numbers = "0123456789-.eE";
                // find start of a number
                while (startpos < this.m_value.Length && numbers.Contains(this.m_value[startpos]) == false)
                    startpos++;
                // end of number
                int endpos = startpos;
                while (endpos < this.m_value.Length && numbers.Contains(this.m_value[endpos]))
                {
                    // '-' if number is followed by '-' then it indicates the end of the value
                    if (endpos != startpos && this.m_value[endpos] == '-' && this.m_value[endpos - 1] != 'e' && this.m_value[endpos - 1] != 'E')
                        break;
                    endpos++;
                }
                int len = endpos - startpos;
                if (len <= 0)
                    return float.NaN;
                this.m_curPos = endpos;
                string s = this.m_value.Substring(startpos, len);
                                                      
                // find start of a next number
                startpos = endpos;
                while (startpos < this.m_value.Length && numbers.Contains(this.m_value[startpos]) == false)
                    startpos++;
                if (startpos >= this.m_value.Length)
                    endpos = startpos;
                                                      
                this.m_curPos = endpos;
                return System.Xml.XmlConvert.ToDouble(s);
            }
            public Point ReadNextPoint()
            {
                double x = this.ReadNextValue();
                double y = this.ReadNextValue();
                return new Point(x,y);
            }
        }

        public class Attribute
        {
            public string Name {get; set;}
            public string Value {get; set; }
            public Attribute(string name, string value)
            {
                this.Name = name;
                this.Value = value;
            }
            public override string ToString()
            {
                return string.Format("{0}:{1}", this.Name, this.Value);
            }

        }

        public static Attribute ReadNextAttr(string inputstring, ref int startpos)
        {
            if (inputstring[startpos] != ' ')
                throw new Exception("inputstring[startpos] must be a whitepace character");
            while (inputstring[startpos] == ' ')
                startpos++;

            int namestart = startpos;
            int nameend = inputstring.IndexOf('=', startpos);
            if (nameend < namestart)
                throw new Exception("did not find xml attribute name");

            int valuestart = inputstring.IndexOf('"', nameend);
            valuestart++;
            int valueend = inputstring.IndexOf('"', valuestart);
            if (valueend < 0 || valueend < valuestart)
                throw new Exception("did not find xml attribute value");

            // search for first occurence of x="yy"
            string attrName = inputstring.Substring(namestart, nameend-namestart).Trim();
            string attrValue = inputstring.Substring(valuestart, valueend-valuestart).Trim();
            startpos = valueend + 1;
            return new Attribute(attrName, attrValue);
        }
    }

    public partial class XmlUtil
    {
        static bool GetValueRespectingUnits(string inputstring, out double value, double percentageMaximum)
        {
            value = 0;
            var units = string.Empty;
            int index = inputstring.LastIndexOfAny(new char[] {'.','-','0','1','2','3','4','5','6','7','8','9'}) ;
            if (index >= 0)
            {
                string svalue = inputstring.Substring(0, index+1);
                if (index + 1 < inputstring.Length)
                    units = inputstring.Substring(index+1);
                try
                {
                    value = System.Xml.XmlConvert.ToDouble(svalue);

                    switch (units) // from http://www.selfsvg.info/?section=3.4
                    {
                        case "pt": value = value * 1.25; break;
                        case "mm": value = value * 3.54; break;
                        case "pc": value = value * 15; break;
                        case "cm": value = value * 35.43; break;
                        case "in": value = value * 90; break;
                        case "%": value = value * percentageMaximum / 100; break;
                    }

                    return true;
                }
                catch (FormatException)
                { }
            }
            return false;
        }

        public static double GetDoubleValue(string value, double percentageMaximum = 1)
        {
            double result = 0;
            if (GetValueRespectingUnits(value, out result, percentageMaximum))
            {
                return result;
            }
            return 0;
        }

        public static double AttrValue(ShapeUtil.Attribute attr)
        {
            double result = 0;
            GetValueRespectingUnits(attr.Value, out result, 1);
            return result;
        }

        public static double AttrValue(XmlNode node, string id, double defaultvalue, double percentageMaximum = 1)
        {
            XmlAttribute attr = node.Attributes[id];
            if (attr == null)
                return defaultvalue;

            double result = 0;
            if (attr != null && GetValueRespectingUnits(attr.Value, out result, percentageMaximum))
            {
                return result;
            }
            return defaultvalue;
        }

        public static string AttrValue(XmlNode node, string id, string defaultvalue)
        {
            if (node.Attributes == null)
                return defaultvalue;
            XmlAttribute attr = node.Attributes[id];
            if (attr != null)
                return attr.Value;
            return defaultvalue;
        }

        public static string AttrValue(XmlNode node, string id)
        {
            return AttrValue(node, id, string.Empty);
        }

        public static double ParseDouble(SVG svg, string svalue)
        {
            double value = 0d;
            if (GetValueRespectingUnits(svalue, out value, 1))
                return value;
            return 0.1;
        }

        public static List<ShapeUtil.Attribute> SplitStyle(SVG svg, string fullstyle)
        {
            List<ShapeUtil.Attribute> list = new List<ShapeUtil.Attribute>();
            if (fullstyle.Length == 0)
                return list;
            // style contains attributes in format of "attrname:value;attrname:value"
            string[] attrs = fullstyle.Split(';');
            foreach (string attr in attrs)
            {
                string[] s = attr.Split(':');
                if (s.Length != 2)
                    continue;
                list.Add(new ShapeUtil.Attribute(s[0].Trim(), s[1].Trim()));
            }
            return list;
        }
    }
}
