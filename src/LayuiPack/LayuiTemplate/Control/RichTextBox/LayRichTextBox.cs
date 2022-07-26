using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;

namespace LayuiTemplate.Control
{
    /// <summary>
    ///  LayRichTextBox
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-07-26 下午 3:27:04</para>
    /// </summary>
    public class LayRichTextBox : RichTextBox
    {
        private HashSet<Thread> _recursionProtection = new HashSet<Thread>();
        /// <summary>
        /// 输入框圆角
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(LayRichTextBox));
        /// <summary>
        /// 鼠标移入边框色
        /// </summary>
        public Brush HoverBorderBrush
        {
            get { return (Brush)GetValue(HoverBorderBrushProperty); }
            set { SetValue(HoverBorderBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HoverBorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HoverBorderBrushProperty =
            DependencyProperty.Register("HoverBorderBrush", typeof(Brush), typeof(LayRichTextBox), new PropertyMetadata(Brushes.Transparent));


        /// <summary>
        /// 光标聚焦后的边框色
        /// </summary>
        public Brush FocusedBorderBrush
        {
            get { return (Brush)GetValue(FocusedBorderBrushProperty); }
            set { SetValue(FocusedBorderBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FocusedBorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FocusedBorderBrushProperty =
            DependencyProperty.Register("FocusedBorderBrush", typeof(Brush), typeof(LayRichTextBox), new PropertyMetadata(Brushes.Transparent));

        /// <summary>
        /// 设置富文本内容
        /// </summary>
        public string DocumentXaml
        {
            get { return (string)GetValue(DocumentXamlProperty); }
            internal set
            {
                _recursionProtection.Add(Thread.CurrentThread);
                SetValue(DocumentXamlProperty, value);
                _recursionProtection.Remove(Thread.CurrentThread);
            }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DocumentXamlProperty =
            DependencyProperty.Register("DocumentXaml", typeof(string), typeof(LayRichTextBox));


        private static void OnDocumentXamlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LayRichTextBox layRichText = d as LayRichTextBox;
            if (layRichText._recursionProtection.Contains(Thread.CurrentThread)) return;
            try
            {
                var stream = new MemoryStream(Encoding.UTF8.GetBytes(layRichText.DocumentXaml));
                var doc = (FlowDocument)XamlReader.Load(stream);
                layRichText.Document = doc;
            }
            catch
            {
                layRichText.Document = new FlowDocument();
            }
        }
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            DocumentXaml = XamlWriter.Save(Document);
        }
    }
}
