using LayUI.Wpf.Tools;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace LayUI.Wpf.Controls
{
    [TemplatePart(Name = "PART_CloseWindowButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_MaxWindowButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_MinWindowButton", Type = typeof(Button))]
    public class LayTitleBar : HeaderedContentControl, ILayControl
    {
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
        private Window _window;

        public Style RootStyle
        {
            get { return (Style)GetValue(RootStyleProperty); }
            set { SetValue(RootStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RootStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RootStyleProperty =
            DependencyProperty.Register("RootStyle", typeof(Style), typeof(LayTitleBar));

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


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var window = Window.GetWindow(this);
            if (window != null)
            {
                _window = window;
                LayBindingHelper.SetBinding(_window, Window.WindowStateProperty, nameof(WindowState), BindingMode.TwoWay, this);
                LayBindingHelper.SetBinding(_window, Window.StyleProperty, nameof(RootStyle), BindingMode.TwoWay, this);
                LayBindingHelper.SetBinding(_window, Window.ResizeModeProperty, nameof(ResizeMode), BindingMode.TwoWay, this);
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
