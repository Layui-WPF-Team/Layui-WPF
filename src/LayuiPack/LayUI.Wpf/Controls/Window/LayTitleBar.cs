using LayUI.Wpf.Tools;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shell;

namespace LayUI.Wpf.Controls
{
    [TemplatePart(Name = "PART_CloseWindowButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_MaxWindowButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_MinWindowButton", Type = typeof(Button))]
    public class LayTitleBar : HeaderedContentControl, ILayControl
    {
        private WindowChrome windowChrome = new WindowChrome()
        {
            CornerRadius = new CornerRadius(0),
            GlassFrameThickness = new Thickness(1),
            NonClientFrameEdges = NonClientFrameEdges.None,
            ResizeBorderThickness = new Thickness(9),
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
        /// <summary>
        /// 主窗口
        /// </summary>
        private Window _window; 
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(LayTitleBar));

        public WindowState WindowState
        {
            get { return (WindowState)GetValue(WindowStateProperty); }
            set { SetValue(WindowStateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WindowState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WindowStateProperty =
            DependencyProperty.Register("WindowState", typeof(WindowState), typeof(LayTitleBar));

        public ResizeMode ResizeMode
        {
            get { return (ResizeMode)GetValue(ResizeModeProperty); }
            set { SetValue(ResizeModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ResizeMode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ResizeModeProperty =
            DependencyProperty.Register("ResizeMode", typeof(ResizeMode), typeof(LayTitleBar));
        public double HeaderHeight
        {
            get { return (double)GetValue(HeaderHeightProperty); }
            set { SetValue(HeaderHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderHeightProperty =
            DependencyProperty.Register("HeaderHeight", typeof(double), typeof(LayTitleBar), new PropertyMetadata(30.0, OnHeaderHeightChanged));

        private static void OnHeaderHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as LayTitleBar).OnHeaderHeightChanged((double)e.NewValue);
        }
        private void OnHeaderHeightChanged(double value)
        {
            windowChrome.CaptionHeight = value;
        }

        public Brush HeaderBackground
        {
            get { return (Brush)GetValue(HeaderBackgroundProperty); }
            set { SetValue(HeaderBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderBackgroundProperty =
            DependencyProperty.Register("HeaderBackground", typeof(Brush), typeof(LayTitleBar));
          
        public Brush HeaderForeground
        {
            get { return (Brush)GetValue(HeaderForegroundProperty); }
            set { SetValue(HeaderForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderForegroundProperty =
            DependencyProperty.Register("HeaderForeground", typeof(Brush), typeof(LayTitleBar)); 

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _window = Window.GetWindow(this);
            if (_window != null)
            { 
                WindowChrome.SetWindowChrome(_window, windowChrome);
                LayBindingHelper.SetBinding(_window, Window.WindowStateProperty, nameof(WindowState), BindingMode.TwoWay, this); 
                LayBindingHelper.SetBinding(_window, Window.ResizeModeProperty, nameof(ResizeMode), BindingMode.TwoWay, this);
                _window.Closing -= Window_Closing;
                _window.Closing += Window_Closing;
                WindowState = _window.WindowState;
                ResizeMode = _window.ResizeMode; 

            }
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_window.DataContext is IWindowAware windowAware) e.Cancel = !windowAware.CanClosing(); 
        }

        private void PART_CloseWindowButton_Click(object sender, RoutedEventArgs e)
        {
            _window.Close();
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
    }
}
