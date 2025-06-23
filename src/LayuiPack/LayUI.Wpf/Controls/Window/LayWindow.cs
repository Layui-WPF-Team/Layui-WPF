using LayUI.Wpf.Enum;
using LayUI.Wpf.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shell;

namespace LayUI.Wpf.Controls
{
    [TemplatePart(Name = "PART_CloseWindowButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_MaxWindowButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_MinWindowButton", Type = typeof(Button))]
    public class LayWindow : Window, IWindowAware
    {
        private WindowChrome windowChrome = new WindowChrome()
        {
            CornerRadius=new CornerRadius(0),
            GlassFrameThickness = new Thickness(1),
            NonClientFrameEdges = NonClientFrameEdges.None,
            ResizeBorderThickness = new Thickness(4),
            UseAeroCaptionButtons = false
        };
        /// <summary>
        /// 关闭窗体
        /// </summary>
        private Button PART_CloseWindowButton = null;
        /// <summary>
        /// 窗体缩放
        /// </summary>
        private Button PART_MaxWindowButton = null;
        /// <summary>
        /// 最小化窗体
        /// </summary>
        private Button PART_MinWindowButton = null;
        public LayWindow()
        { 
            WindowChrome.SetWindowChrome(this, windowChrome);
        }
        /// <summary>
        /// 设置重写默认样式
        /// </summary>
        static LayWindow()
        {
            StyleProperty.OverrideMetadata(typeof(LayWindow), new FrameworkPropertyMetadata(LayResourceHelper.GetStyle(nameof(LayWindow) + "Style")));
        }
        public double HeaderHeight
        {
            get { return (double)GetValue(HeaderHeightProperty); }
            set { SetValue(HeaderHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderHeightProperty =
            DependencyProperty.Register("HeaderHeight", typeof(double), typeof(LayWindow),new PropertyMetadata(0.0,OnHeaderHeightChanged));

        private static void OnHeaderHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as LayWindow).OnHeaderHeightChanged((double)e.NewValue);
        }
        private void OnHeaderHeightChanged(double value)
        { 
            windowChrome.CaptionHeight = value;
        }
        /// <summary>
        /// 顶部内容
        /// </summary>
        [Bindable(true)]
        public object HeaderContent
        {
            get { return (object)GetValue(HeaderContentProperty); }
            set { SetValue(HeaderContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderContentProperty =
            DependencyProperty.Register("HeaderContent", typeof(object), typeof(LayWindow));
         

        public Effect HeaderEffect
        {
            get { return (Effect)GetValue(HeaderEffectProperty); }
            set { SetValue(HeaderEffectProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderEffect.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderEffectProperty =
            DependencyProperty.Register("HeaderEffect", typeof(Effect), typeof(LayWindow));

        /// <summary>
        /// 头部标题栏文字颜色
        /// </summary>
        [Bindable(true)]
        public Brush HeaderForeground
        {
            get { return (Brush)GetValue(HeaderForegroundProperty); }
            set { SetValue(HeaderForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderForegroundProperty =
            DependencyProperty.Register("HeaderForeground", typeof(Brush), typeof(LayWindow), new PropertyMetadata(Brushes.White));


        /// <summary>
        /// 头部标题栏背景色
        /// </summary>
        [Bindable(true)]
        public Brush HeaderBackground
        {
            get { return (Brush)GetValue(HeaderBackgroundProperty); }
            set { SetValue(HeaderBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderBackgroundProperty =
            DependencyProperty.Register("HeaderBackground", typeof(Brush), typeof(LayWindow));
        /// <summary>
        /// 是否显示头部标题栏
        /// </summary>
        [Bindable(true)]
        public bool IsShowHeader
        {
            get { return (bool)GetValue(IsShowHeaderProperty); }
            set { SetValue(IsShowHeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsShowHeader.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsShowHeaderProperty =
            DependencyProperty.Register("IsShowHeader", typeof(bool), typeof(LayWindow), new PropertyMetadata(false));
        /// <summary>
        /// 动画类型
        /// </summary>
        [Bindable(true)]
        public AnimationType Type
        {
            get { return (AnimationType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Type.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(AnimationType), typeof(LayWindow), new PropertyMetadata(AnimationType.Default));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_CloseWindowButton = GetTemplateChild("PART_CloseWindowButton") as Button;
            PART_MaxWindowButton = GetTemplateChild("PART_MaxWindowButton") as Button;
            PART_MinWindowButton = GetTemplateChild("PART_MinWindowButton") as Button;
            if (PART_MaxWindowButton != null && PART_MinWindowButton != null && PART_CloseWindowButton != null)
            {
                PART_MaxWindowButton.Click -= PART_MaxWindowButton_Click;
                PART_MinWindowButton.Click -= PART_MinWindowButton_Click;
                PART_CloseWindowButton.Click -= PART_CloseWindowButton_Click;
                PART_MaxWindowButton.Click += PART_MaxWindowButton_Click;
                PART_MinWindowButton.Click += PART_MinWindowButton_Click;
                PART_CloseWindowButton.Click += PART_CloseWindowButton_Click;
            }
        }

        private void PART_CloseWindowButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void PART_MinWindowButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void PART_MaxWindowButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            if (SizeToContent == SizeToContent.WidthAndHeight && WindowChrome.GetWindowChrome(this) != null)
            {
                InvalidateMeasure();
            }
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            if (DataContext is IWindowAware windowAware)
            {
                e.Cancel = !windowAware.CanClosing();
            }
            else
            {
                e.Cancel = !CanClosing();
                base.OnClosing(e);
            }
        }
        public virtual bool CanClosing() => true;
    }

}
