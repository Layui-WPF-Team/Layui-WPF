using System.Windows;

using LayUI.Wpf.Controls.SVG.Shapes;

namespace LayUI.Wpf.Controls.SVG
{
	public class TextStyle
	{
		public string FontFamily {get; set;}
		public double FontSize {get; set;}
		public FontWeight Fontweight {get; set;}
		public FontStyle Fontstyle {get; set;}
		public TextDecorationCollection TextDecoration {get; set;}
		public TextAlignment TextAlignment {get; set;}
		public double WordSpacing {get; set;}
		public double LetterSpacing {get; set;}
		public string BaseLineShift {get; set;}
		public void Copy(TextStyle aCopy)
		{
			if (aCopy == null)
				return;
			this.FontFamily = aCopy.FontFamily;
			this.FontSize = aCopy.FontSize;
			this.Fontweight = aCopy.Fontweight;
			this.Fontstyle = aCopy.Fontstyle;;
			this.TextAlignment = aCopy.TextAlignment;
			this.WordSpacing = aCopy.WordSpacing;
			this.LetterSpacing = aCopy.LetterSpacing;
			this.BaseLineShift = aCopy.BaseLineShift;
		}
		public TextStyle(TextStyle aCopy)
		{
			this.Copy(aCopy);
		}
		public TextStyle(SVG svg, Shape owner)
		{
			this.FontFamily = "Arial Unicode MS, Verdana";
			this.FontSize = 12;
			this.Fontweight = FontWeights.Normal;
			this.Fontstyle = FontStyles.Normal;
			this.TextAlignment = System.Windows.TextAlignment.Left;
			this.WordSpacing = 0;
			this.LetterSpacing = 0;
			this.BaseLineShift = string.Empty;
			if (owner.Parent != null)
				this.Copy(owner.Parent.TextStyle);
		}
	}
}
