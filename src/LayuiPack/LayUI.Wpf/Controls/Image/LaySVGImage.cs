using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.ComponentModel;
using System.Collections.Generic;

using System.Windows;
using System.Windows.Media;
using System.Windows.Markup;
using System.Windows.Controls;
using System.Windows.Resources;

using LayUI.Wpf.Controls.SVG.FileLoaders;
using LayUI.Wpf.Enum;
using LayUI.Wpf.Controls.SVG;

namespace LayUI.Wpf.Controls
{
    /// <summary>
    ///这是SVG图像视图控件图像控件可以从文件<see cref="SetImage(string)"/>加载图像，也可以通过
    ///通过<see cref="Drawing"/>设置<see cref="SetImage(Drawing)"/>对象，这允许
    ///多个控件共享同一图形实例。
    ///<para>该控件借鉴 SharpVectors 开源库，详情去GitHub搜索 SharpVectors</para>
    /// </summary>
    public class LaySVGImage : Control, IUriContext
    {
        private Uri _baseUri;

        private Drawing m_drawing;
        private SVGRender _render;

        private ScaleTransform m_scaleTransform;
        private TranslateTransform m_offsetTransform;

        private Action<SVGRender> _loadImage;

        /// <summary>
        /// Identifies the <see cref="UriSource"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty UriSourceProperty =
            DependencyProperty.Register("UriSource", typeof(Uri), typeof(LaySVGImage),
                new FrameworkPropertyMetadata(null, OnUriSourceChanged));

        public static DependencyProperty SizeTypeProperty = DependencyProperty.Register("SizeType",
            typeof(SizeType), typeof(LaySVGImage), new FrameworkPropertyMetadata(SizeType.ContentToSizeNoStretch,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                new PropertyChangedCallback(OnSizeTypeChanged)));

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source",
            typeof(string), typeof(LaySVGImage), new FrameworkPropertyMetadata(null,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                new PropertyChangedCallback(OnSourceChanged)));

        public static readonly DependencyProperty FileSourceProperty = DependencyProperty.Register("FileSource",
            typeof(string), typeof(LaySVGImage), new PropertyMetadata(OnFileSourceChanged));

        public static DependencyProperty ImageSourcePoperty = DependencyProperty.Register("ImageSource",
            typeof(Drawing), typeof(LaySVGImage), new FrameworkPropertyMetadata(null,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                new PropertyChangedCallback(OnImageSourceChanged)));

        public static readonly DependencyProperty OverrideStrokeWidthProperty = DependencyProperty.Register(nameof(OverrideStrokeWidth),
               typeof(double?), typeof(LaySVGImage), new FrameworkPropertyMetadata(default,
                   FrameworkPropertyMetadataOptions.AffectsRender, OverrideStrokeWidthPropertyChanged));

        public static readonly DependencyProperty OverrideColorProperty =
            DependencyProperty.Register("OverrideColor",
                typeof(Color?),
                typeof(LaySVGImage),
                new FrameworkPropertyMetadata(default, FrameworkPropertyMetadataOptions.AffectsRender, OverrideColorPropertyChanged));

        public static readonly DependencyProperty CustomBrushesProperty = DependencyProperty.Register(nameof(CustomBrushes),
                typeof(Dictionary<string, Brush>), typeof(LaySVGImage), new FrameworkPropertyMetadata(default,
                    FrameworkPropertyMetadataOptions.AffectsRender, CustomBrushesPropertyChanged));

        public static readonly DependencyProperty UseAnimationsProperty = DependencyProperty.Register("UseAnimations",
            typeof(bool), typeof(LaySVGImage), new PropertyMetadata(true));

        public static readonly DependencyProperty ExternalFileLoaderProperty = DependencyProperty.Register("ExternalFileLoader",
            typeof(IExternalFileLoader), typeof(LaySVGImage), new PropertyMetadata(FileSystemLoader.Instance));

        static LaySVGImage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LaySVGImage), new FrameworkPropertyMetadata(typeof(LaySVGImage)));
            ClipToBoundsProperty.OverrideMetadata(typeof(LaySVGImage), new FrameworkPropertyMetadata(true));
            SnapsToDevicePixelsProperty.OverrideMetadata(typeof(LaySVGImage), new FrameworkPropertyMetadata(true));
        }

        public LaySVGImage()
        {
            this.ClipToBounds        = true;
            this.SnapsToDevicePixels = true;

            m_offsetTransform        = new TranslateTransform();
            m_scaleTransform         = new ScaleTransform();
        }

        public SVG.SVG SVG
        {
            get
            {
                return _render?.SVG;
            }
        }


        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(LaySVGImage));


        public SizeType SizeType
        {
            get { return (SizeType)this.GetValue(SizeTypeProperty); }
            set { this.SetValue(SizeTypeProperty, value); }
        }

        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public string FileSource
        {
            get { return (string)GetValue(FileSourceProperty); }
            set { SetValue(FileSourceProperty, value); }
        }

        public Drawing ImageSource
        {
            get { return (Drawing)this.GetValue(ImageSourcePoperty); }
            set { this.SetValue(ImageSourcePoperty, value); }
        }

        public bool UseAnimations
        {
            get { return (bool)GetValue(UseAnimationsProperty); }
            set { SetValue(UseAnimationsProperty, value); }
        }

        public Color? OverrideColor
        {
            get { return (Color?)GetValue(OverrideColorProperty); }
            set { SetValue(OverrideColorProperty, value); }
        }

        public double? OverrideStrokeWidth
        {
            get { return (double?)GetValue(OverrideStrokeWidthProperty); }
            set { SetValue(OverrideStrokeWidthProperty, value); }
        }

        public Dictionary<string, Brush> CustomBrushes
        {
            get { return (Dictionary<string, Brush>)GetValue(CustomBrushesProperty); }
            set { SetValue(CustomBrushesProperty, value); }
        }

        public IExternalFileLoader ExternalFileLoader
        {
            get { return (IExternalFileLoader)GetValue(ExternalFileLoaderProperty); }
            set { SetValue(ExternalFileLoaderProperty, value); }
        }

        /// <summary>
        /// 获取或设置要加载到此文件的SVG文件的路径。
        /// </summary>
        /// <value>
        /// 指定SVG源文件路径的<see cref="System.Uri"/>。
        /// 该文件可以位于计算机、网络或程序集资源上。
        /// 将其设置为<see langword="null"/>将关闭任何打开的图表。
        /// </value>
        /// <remarks>
        /// 这与<see cref="Source"/>属性相同，并添加了一致性。
        /// </remarks>
        /// <seealso cref="UriSource"/>
        /// <seealso cref="StreamSource"/>
        public Uri UriSource
        {
            get {
                return (Uri)GetValue(UriSourceProperty);
            }
            set {
                this.SetValue(UriSourceProperty, value);
            }
        }

        /// <summary>
        /// 获取或设置当前应用程序上下文的基URI。
        /// </summary>
        /// <value>
        /// 应用程序上下文的基URI。
        /// </value>
        public Uri BaseUri
        {
            get {
                return _baseUri;
            }
            set {
                _baseUri = value;
            }
        }

        public void ReRenderSvg()
        {
            if (_render != null)
            {
                this.SetImage(_render.CreateDrawing(_render.SVG));
            }
            else if (this.IsInitialized && _loadImage != null)
            {
                _render = new SVGRender();
                _render.ExternalFileLoader  = this.ExternalFileLoader;
                _render.OverrideColor       = OverrideColor;
                _render.CustomBrushes       = CustomBrushes;
                _render.OverrideStrokeWidth = OverrideStrokeWidth;
                _render.UseAnimations       = this.UseAnimations;

                _loadImage(_render);
                _loadImage = null;
            }
        }

        public void SetImage(string svgFilename)
        {
            _loadImage = (render) =>
            {
                this.SetImage(render.LoadDrawing(svgFilename));
            };

            if (this.IsInitialized || DesignerProperties.GetIsInDesignMode(this))
            {
                _render = new SVGRender();
                _render.ExternalFileLoader  = this.ExternalFileLoader;
                _render.UseAnimations       = false;
                _render.OverrideColor       = OverrideColor;
                _render.CustomBrushes       = CustomBrushes;
                _render.OverrideStrokeWidth = OverrideStrokeWidth;

                _loadImage(_render);
                _loadImage = null;
            }
        }

        public void SetImage(Stream stream)
        {
            _loadImage = (render) =>
            {
                this.SetImage(render.LoadDrawing(stream));
            };

            if (this.IsInitialized || DesignerProperties.GetIsInDesignMode(this))
            {
                _render = new SVGRender();
                _render.ExternalFileLoader  = this.ExternalFileLoader;
                _render.OverrideColor       = OverrideColor;
                _render.CustomBrushes       = CustomBrushes;
                _render.OverrideStrokeWidth = OverrideStrokeWidth;
                _render.UseAnimations       = false;

                _loadImage(_render);
                _loadImage = null;
            }
        }

        public void SetImage(Uri uriSource)
        {
            _loadImage = (render) =>
            {
                Uri svgUri = this.ResolveUri(uriSource);
                this.SetImage(this.LoadDrawing(svgUri));
            };

            if (this.IsInitialized || DesignerProperties.GetIsInDesignMode(this))
            {
                _render = new SVGRender();
                _render.ExternalFileLoader  = this.ExternalFileLoader;
                _render.OverrideColor       = OverrideColor;
                _render.CustomBrushes       = CustomBrushes;
                _render.OverrideStrokeWidth = OverrideStrokeWidth;
                _render.UseAnimations       = false;

                _loadImage(_render);
                _loadImage = null;
            }
        }

        public void SetImage(Drawing drawing)
        {
            this.m_drawing = drawing;
            this.InvalidateVisual();
            if (this.m_drawing != null && this.SizeType == SizeType.SizeToContent)
                this.InvalidateMeasure();
            this.RecalcImage();
            this.InvalidateVisual();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            if (_loadImage != null)
            {
                _render = new SVGRender();
                _render.ExternalFileLoader  = this.ExternalFileLoader;
                _render.OverrideColor       = OverrideColor;
                _render.CustomBrushes       = CustomBrushes;
                _render.OverrideStrokeWidth = OverrideStrokeWidth;
                _render.UseAnimations       = this.UseAnimations;

                _loadImage(_render);
                _loadImage = null;
                var brushesFromSVG = new Dictionary<string, Brush>();
                foreach (var server in _render.SVG.PaintServers.GetServers())
                {
                    brushesFromSVG[server.Key] = server.Value.GetBrush();
                }
                CustomBrushes = brushesFromSVG;
            }
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            this.RecalcImage();
            this.InvalidateVisual();
        }

        /// <summary>
        /// 注意TemplateBinding BackGround必须从默认模板的边框中删除（或从模板中删除边框） 
        /// 调用子渲染后，边框渲染背景
        /// http://social.msdn.microsoft.com/Forums/en-US/wpf/thread/1575d2af-8e86-4085-81b8-a8bf24268e51/?prof=required
        /// </summary>
        /// <param name="dc"></param>
        protected override void OnRender(DrawingContext dc)
        {
            //base.OnRender(dc);
            if (this.Background != null)
                dc.DrawRectangle(this.Background, null, new Rect(0, 0, this.ActualWidth, this.ActualHeight));
            if (this.m_drawing == null)
                return;
            dc.PushTransform(this.m_offsetTransform);
            dc.PushTransform(this.m_scaleTransform);
            dc.DrawDrawing(this.m_drawing);
            dc.Pop();
            dc.Pop();
        }

        protected override Size MeasureOverride(Size constraint)
        {
            Size result = base.MeasureOverride(constraint);
            if (this.SizeType == SizeType.SizeToContent)
            {
                if (this.m_drawing != null && !this.m_drawing.Bounds.Size.IsEmpty)
                    result = this.m_drawing.Bounds.Size;
            }
            if (constraint.Width > 0 && constraint.Width < result.Width)
                result.Width = constraint.Width;
            if (constraint.Height > 0 && constraint.Height < result.Height)
                result.Height = constraint.Height;

            // Check for empty size...
            if (result.IsEmpty)
            {
                if (this.m_drawing != null)
                    result = this.m_drawing.Bounds.Size;
            }

            return result;
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            Size result = base.ArrangeOverride(arrangeBounds);
            if (this.SizeType == SizeType.SizeToContent)
            {
                if (this.m_drawing != null && !this.m_drawing.Bounds.Size.IsEmpty)
                    result = this.m_drawing.Bounds.Size;
            }
            if (arrangeBounds.Width > 0 && arrangeBounds.Width < result.Width)
                result.Width = arrangeBounds.Width;
            if (arrangeBounds.Height > 0 && arrangeBounds.Height < result.Height)
                result.Height = arrangeBounds.Height;

            return result;
        }

        void RecalcImage()
        {
            if (this.m_drawing == null)
                return;

            Rect r = this.m_drawing.Bounds;
            if (this.SizeType == SizeType.None)
            {
                this.m_scaleTransform.ScaleX = 1;
                this.m_scaleTransform.ScaleY = 1;
                switch (this.HorizontalContentAlignment)
                {
                    case System.Windows.HorizontalAlignment.Center:
                        this.m_offsetTransform.X = this.ActualWidth / 2 - r.Width / 2 - r.Left;
                        break;
                    case System.Windows.HorizontalAlignment.Right:
                        this.m_offsetTransform.X = this.ActualWidth - r.Right;
                        break;
                    default:
                        this.m_offsetTransform.X = -r.Left; // 默认情况下向左移动
                        break;
                }
                switch (this.VerticalContentAlignment)
                {
                    case System.Windows.VerticalAlignment.Center:
                        this.m_offsetTransform.Y = this.ActualHeight / 2 - r.Height / 2;
                        break;
                    case System.Windows.VerticalAlignment.Bottom:
                        this.m_offsetTransform.Y = this.ActualHeight - r.Height - r.Top;
                        break;
                    default:
                        this.m_offsetTransform.Y = -r.Top; // 默认情况下移至顶部
                        break;
                }
                return;
            }
            if (this.SizeType == SizeType.ContentToSizeNoStretch)
            {
                this.SizeToContentNoStretch(this.HorizontalContentAlignment, this.VerticalContentAlignment);
                return;
            }
            if (this.SizeType == SizeType.ContentToSizeStretch)
            {
                double xscale = this.ActualWidth / r.Width;
                double yscale = this.ActualHeight / r.Height;
                this.m_scaleTransform.CenterX = r.Left;
                this.m_scaleTransform.CenterY = r.Top;
                this.m_scaleTransform.ScaleX = xscale;
                this.m_scaleTransform.ScaleY = yscale;

                this.m_offsetTransform.X = -r.Left;
                this.m_offsetTransform.Y = -r.Top;
                return;
            }
            if (this.SizeType == SizeType.SizeToContent)
            {
                if (r.Width > this.ActualWidth || r.Height > this.ActualHeight)
                    this.SizeToContentNoStretch(HorizontalAlignment.Left, VerticalAlignment.Top);
                else
                {
                    this.m_scaleTransform.CenterX = r.Left;
                    this.m_scaleTransform.CenterY = r.Top;
                    this.m_scaleTransform.ScaleX = 1;
                    this.m_scaleTransform.ScaleY = 1;

                    this.m_offsetTransform.X = -r.Left; // 默认情况下向左移动
                    this.m_offsetTransform.Y = -r.Top; // 默认情况下移至顶部
                }
                return;
            }
        }

        void SizeToContentNoStretch(HorizontalAlignment hAlignment, VerticalAlignment vAlignment)
        {
            Rect r = this.m_drawing.Bounds;
            double xscale = this.ActualWidth / r.Width;
            double yscale = this.ActualHeight / r.Height;
            double scale = xscale;
            if (scale > yscale)
                scale = yscale;

            this.m_scaleTransform.CenterX = r.Left;
            this.m_scaleTransform.CenterY = r.Top;
            this.m_scaleTransform.ScaleX = scale;
            this.m_scaleTransform.ScaleY = scale;

            this.m_offsetTransform.X = -r.Left;
            if (scale < xscale)
            {
                switch (this.HorizontalContentAlignment)
                {
                    case System.Windows.HorizontalAlignment.Center:
                        double width = r.Width * scale;
                        this.m_offsetTransform.X = this.ActualWidth / 2 - width / 2 - r.Left;
                        break;
                    case System.Windows.HorizontalAlignment.Right:
                        this.m_offsetTransform.X = this.ActualWidth - r.Right * scale;
                        break;
                }
            }
            this.m_offsetTransform.Y = -r.Top;
            if (scale < yscale)
            {
                switch (this.VerticalContentAlignment)
                {
                    case System.Windows.VerticalAlignment.Center:
                        double height = r.Height * scale;
                        this.m_offsetTransform.Y = this.ActualHeight / 2 - height / 2 - r.Top;
                        break;
                    case System.Windows.VerticalAlignment.Bottom:
                        this.m_offsetTransform.Y = this.ActualHeight - r.Height * scale - r.Top;
                        break;
                }
            }
        }

        Uri ResolveUri(Uri svgSource)
        {
            if (svgSource == null)
            {
                return null;
            }

            if (svgSource.IsAbsoluteUri)
            {
                return svgSource;
            }

            // 尝试在同一目录中获取本地文件。。。。
            string svgPath = svgSource.ToString();
            if (svgPath[0] == '\\' || svgPath[0] == '/')
            {
                svgPath = svgPath.Substring(1);
            }
            svgPath = svgPath.Replace('/', '\\');

            Assembly assembly = Assembly.GetExecutingAssembly();
            string localFile = Path.Combine(Path.GetDirectoryName(assembly.Location), svgPath);

            if (File.Exists(localFile))
            {
                return new Uri(localFile);
            }

            // 尝试将其作为资源文件获取。。。
            if (_baseUri != null)
            {
                return new Uri(_baseUri, svgSource);
            }

            string asmName = assembly.GetName().Name;
            string uriString = string.Format("pack://application:,,,/{0};component/{1}",
                asmName, svgPath);

            return new Uri(uriString);
        }

        /// <summary>
        /// 这会将Uri指定的SVG资源转换为<see cref="DrawingGroup"/>.
        /// </summary>
        /// <param name="svgSource">A <see cref="Uri"/> 指定SVG资源的源。</param>
        /// <returns>这个<see cref="DrawingGroup"/> 转换后的SVG资源。</returns>
        DrawingGroup LoadDrawing(Uri svgSource)
        {
            string scheme = null;
            // A little hack to display preview in design mode: The design mode Uri is not absolute.
            bool designTime = DesignerProperties.GetIsInDesignMode(new DependencyObject());
            if (designTime && svgSource.IsAbsoluteUri == false)
            {
                scheme = "pack";
            }
            else
            {
                scheme = svgSource.Scheme;
            }
            if (string.IsNullOrWhiteSpace(scheme))
            {
                return null;
            }

            switch (scheme)
            {
                case "file":
                //case "ftp":
                case "https":
                case "http":
                    using (FileSvgReader reader = new FileSvgReader(this.OverrideColor))
                    {
                        DrawingGroup drawGroup = reader.Read(svgSource, _render);

                        if (drawGroup != null)
                        {
                            return drawGroup;
                        }
                    }
                    break;
                case "pack":
                    StreamResourceInfo svgStreamInfo = null;
                    if (svgSource.ToString().IndexOf("siteoforigin", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        svgStreamInfo = Application.GetRemoteStream(svgSource);
                    }
                    else
                    {
                        svgStreamInfo = Application.GetResourceStream(svgSource);
                    }

                    Stream svgStream = (svgStreamInfo != null) ? svgStreamInfo.Stream : null;

                    if (svgStream != null)
                    {
                        string fileExt = Path.GetExtension(svgSource.ToString());
                        bool isCompressed = !string.IsNullOrWhiteSpace(fileExt) &&
                            string.Equals(fileExt, ".svgz", StringComparison.OrdinalIgnoreCase);

                        if (isCompressed)
                        {
                            using (svgStream)
                            {
                                using (var zipStream = new GZipStream(svgStream, CompressionMode.Decompress))
                                {
                                    using (FileSvgReader reader = new FileSvgReader(this.OverrideColor))
                                    {
                                        DrawingGroup drawGroup = reader.Read(zipStream, _render);

                                        if (drawGroup != null)
                                        {
                                            return drawGroup;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            using (svgStream)
                            {
                                using (FileSvgReader reader = new FileSvgReader(this.OverrideColor))
                                {
                                    DrawingGroup drawGroup = reader.Read(svgStream, _render);

                                    if (drawGroup != null)
                                    {
                                        return drawGroup;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case "data":
                    var sourceData = svgSource.OriginalString.Replace(" ", "");

                    int nColon = sourceData.IndexOf(":", StringComparison.OrdinalIgnoreCase);
                    int nSemiColon = sourceData.IndexOf(";", StringComparison.OrdinalIgnoreCase);
                    int nComma = sourceData.IndexOf(",", StringComparison.OrdinalIgnoreCase);

                    string sMimeType = sourceData.Substring(nColon + 1, nSemiColon - nColon - 1);
                    string sEncoding = sourceData.Substring(nSemiColon + 1, nComma - nSemiColon - 1);

                    if (string.Equals(sMimeType.Trim(), "image/svg+xml", StringComparison.OrdinalIgnoreCase)
                        && string.Equals(sEncoding.Trim(), "base64", StringComparison.OrdinalIgnoreCase))
                    {
                        string sContent = FileSvgReader.RemoveWhitespace(sourceData.Substring(nComma + 1));
                        byte[] imageBytes = Convert.FromBase64CharArray(sContent.ToCharArray(),
                            0, sContent.Length);
                        bool isGZiped = sContent.StartsWith(FileSvgReader.GZipSignature, StringComparison.Ordinal);
                        if (isGZiped)
                        {
                            using (var stream = new MemoryStream(imageBytes))
                            {
                                using (GZipStream zipStream = new GZipStream(stream, CompressionMode.Decompress))
                                {
                                    using (var reader = new FileSvgReader(this.OverrideColor))
                                    {
                                        DrawingGroup drawGroup = reader.Read(zipStream, _render);
                                        if (drawGroup != null)
                                        {
                                            return drawGroup;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            using (var stream = new MemoryStream(imageBytes))
                            {
                                using (var reader = new FileSvgReader(this.OverrideColor))
                                {
                                    DrawingGroup drawGroup = reader.Read(stream, _render);
                                    if (drawGroup != null)
                                    {
                                        return drawGroup;
                                    }
                                }
                            }
                        }
                    }
                    break;
            }

            return null;
        }

        private static void OnUriSourceChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            LaySVGImage svgImage= obj as LaySVGImage;
            if (svgImage == null)
            {
                return;
            }

            var sourceUri = (Uri)args.NewValue;
            if (sourceUri != null)
            {
                svgImage.SetImage(sourceUri);    
            }
            else
            {
                svgImage.SetImage((Drawing)null);
            }
            
        }

        static void OnSizeTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LaySVGImage ctrl = d as LaySVGImage;
            ctrl.RecalcImage();
            ctrl.InvalidateVisual();
        }

        static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            StreamResourceInfo resource = e.NewValue != null ? Application.GetResourceStream(new Uri(e.NewValue.ToString(), UriKind.Relative)) : null;
            ((LaySVGImage)d).SetImage(resource != null ? resource.Stream : null);
        }

        static void OnFileSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                ((LaySVGImage)d).SetImage(new FileStream(e.NewValue.ToString(), FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            }
            else
            {
                ((LaySVGImage)d).SetImage((Drawing)null);
            }
        }

        static void OnImageSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((LaySVGImage)d).SetImage(e.NewValue as Drawing);
        }

        private static void OverrideColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LaySVGImage svgImage && e.NewValue is Color newColor && svgImage._render != null)
            {
                svgImage._render.OverrideColor = newColor;
                svgImage.InvalidateVisual();
                svgImage.ReRenderSvg();
            }
        }

        private static void OverrideStrokeWidthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LaySVGImage svgImage && e.NewValue is double newStrokeWidth && svgImage._render != null)
            {
                svgImage._render.OverrideStrokeWidth = newStrokeWidth;
                svgImage.InvalidateVisual();
                svgImage.ReRenderSvg();
            }
        }

        private static void CustomBrushesPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LaySVGImage svgImage && e.NewValue is Dictionary<string, Brush> newBrushes)
            {
                if (svgImage._render != null)
                {
                    if (svgImage._render.CustomBrushes != null)
                    {
                        Dictionary<string, Brush> newCustomBrushes = new Dictionary<string, Brush>(svgImage._render.CustomBrushes);
                        foreach (var brush in newBrushes)
                        {
                            newCustomBrushes[brush.Key] = brush.Value;
                        }
                        svgImage._render.CustomBrushes = newCustomBrushes;
                    }
                    else
                    {
                        svgImage._render.CustomBrushes = newBrushes;
                    }
                }
                svgImage.InvalidateVisual();
                svgImage.ReRenderSvg();
            }
        }

    }
}
