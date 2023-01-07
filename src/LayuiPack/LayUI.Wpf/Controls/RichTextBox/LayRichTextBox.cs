using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace LayUI.Wpf.Controls
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
        [Bindable(true)]
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
        [Bindable(true)]
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
        [Bindable(true)]
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
        [Bindable(true)]
        public FlowDocument LayDocument
        {
            get { return (FlowDocument)GetValue(LayDocumentProperty); }
            set { SetValue(LayDocumentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Document.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LayDocumentProperty =
            DependencyProperty.Register("LayDocument", typeof(FlowDocument), typeof(LayRichTextBox), new FrameworkPropertyMetadata()
            {
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = System.Windows.Data.UpdateSourceTrigger.PropertyChanged
            });
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            LayDocument = Document;
        }
        /// <summary>
        /// 将富文本控件的内容转换成string类型
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static string GetTextByRichBox(FlowDocument document)
        {
            //创建一个流
            MemoryStream s = new MemoryStream();
            //获得富文本中的内容
            TextRange documentTextRange = new TextRange(document.ContentStart, document.ContentEnd);
            //将富文本中的内容转换成xaml的格式，并保存到指定的流中
            documentTextRange.Save(s, DataFormats.XamlPackage);
            //将流中的内容转换成字节数组，并转换成base64的等效格式
            return Convert.ToBase64String(s.ToArray());
        }
        /// <summary>
        /// 将数据库中的内容转换回richtextbox可识别的内容
        /// </summary>
        /// <param name="data">数据库取出的数据</param>
        /// <param name="box">接收的richtextbox控件名称</param>
        public static void SetTextToRichBox(string data, FlowDocument document)
        {
            MemoryStream s = new MemoryStream((Convert.FromBase64String(Convert.ToString(data))));
            TextRange TR = new TextRange(document.ContentStart, document.ContentEnd);
            TR.Load(s, DataFormats.XamlPackage);
        }
    }
}
