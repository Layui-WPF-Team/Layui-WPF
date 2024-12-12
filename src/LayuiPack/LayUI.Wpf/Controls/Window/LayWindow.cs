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
        /// 设置重写默认样式
        /// </summary>
        static LayWindow()
        {
            StyleProperty.OverrideMetadata(typeof(LayWindow), new FrameworkPropertyMetadata(LayResourceHelper.GetStyle(nameof(LayWindow) + "Style")));
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

    public static class WindowSizing
    {
        const int MONITOR_DEFAULTTONEAREST = 0x00000002;

        #region DLLImports
        [DllImport("shell32", CallingConvention = CallingConvention.StdCall)]
        public static extern int SHAppBarMessage(int dwMessage, ref APPBARDATA pData);

        [DllImport("user32", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32")]
        internal static extern bool GetMonitorInfo(IntPtr hMonitor, MONITORINFO lpmi);

        [DllImport("user32")]
        internal static extern IntPtr MonitorFromWindow(IntPtr handle, int flags);
        #endregion

        private static MINMAXINFO AdjustWorkingAreaForAutoHide(IntPtr monitorContainingApplication, MINMAXINFO mmi)
        {
            IntPtr hwnd = FindWindow("Shell_TrayWnd", null);

            if (hwnd == null)
            {
                return mmi;
            }

            IntPtr monitorWithTaskbarOnIt = MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);

            if (!monitorContainingApplication.Equals(monitorWithTaskbarOnIt))
            {
                return mmi;
            }

            APPBARDATA abd = new APPBARDATA();

            abd.cbSize = Marshal.SizeOf(abd);

            abd.hWnd = hwnd;

            SHAppBarMessage((int)ABMsg.ABM_GETTASKBARPOS, ref abd);

            int uEdge = GetEdge(abd.rc);

            bool autoHide = Convert.ToBoolean(SHAppBarMessage((int)ABMsg.ABM_GETSTATE, ref abd));

            if (!autoHide)
            {
                return mmi;
            }

            switch (uEdge)
            {
                case (int)ABEdge.ABE_LEFT:
                    mmi.ptMaxPosition.x += 2;
                    mmi.ptMaxTrackSize.x -= 2;
                    mmi.ptMaxSize.x -= 2;
                    break;
                case (int)ABEdge.ABE_RIGHT:
                    mmi.ptMaxSize.x -= 2;
                    mmi.ptMaxTrackSize.x -= 2;
                    break;
                case (int)ABEdge.ABE_TOP:
                    mmi.ptMaxPosition.y += 2;
                    mmi.ptMaxTrackSize.y -= 2;
                    mmi.ptMaxSize.y -= 2;
                    break;
                case (int)ABEdge.ABE_BOTTOM:
                    mmi.ptMaxSize.y -= 2;
                    mmi.ptMaxTrackSize.y -= 2;
                    break;
                default:
                    return mmi;
            }
            return mmi;
        }

        private static int GetEdge(RECT rc)
        {
            int uEdge = -1;

            if (rc.top == rc.left && rc.bottom > rc.right)
            {
                uEdge = (int)ABEdge.ABE_LEFT;
            }
            else if (rc.top == rc.left && rc.bottom < rc.right)
            {
                uEdge = (int)ABEdge.ABE_TOP;
            }
            else if (rc.top > rc.left)
            {
                uEdge = (int)ABEdge.ABE_BOTTOM;
            }
            else
            {
                uEdge = (int)ABEdge.ABE_RIGHT;
            }

            return uEdge;
        }

        public static void WindowInitialized(this Window window)
        {
            IntPtr handle = (new WindowInteropHelper(window)).Handle;
            HwndSource.FromHwnd(handle).AddHook(new HwndSourceHook(WindowProc));
        }

        private static IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case 0x0024:
                    WmGetMinMaxInfo(hwnd, lParam);
                    handled = true;
                    break;
            }

            return (IntPtr)0;
        }

        private static void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
        {
            MINMAXINFO mmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));
            IntPtr monitorContainingApplication = MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);

            if (monitorContainingApplication != System.IntPtr.Zero)
            {
                MONITORINFO monitorInfo = new MONITORINFO();
                GetMonitorInfo(monitorContainingApplication, monitorInfo);
                RECT rcWorkArea = monitorInfo.rcWork;
                RECT rcMonitorArea = monitorInfo.rcMonitor;
                mmi.ptMaxPosition.x = Math.Abs(rcWorkArea.left - rcMonitorArea.left);
                mmi.ptMaxPosition.y = Math.Abs(rcWorkArea.top - rcMonitorArea.top);
                mmi.ptMaxSize.x = Math.Abs(rcWorkArea.right - rcWorkArea.left);
                mmi.ptMaxSize.y = Math.Abs(rcWorkArea.bottom - rcWorkArea.top);
                mmi.ptMaxTrackSize.x = mmi.ptMaxSize.x;                                //maximum drag X size for the window
                mmi.ptMaxTrackSize.y = mmi.ptMaxSize.y;                                //maximum drag Y size for the window
                mmi.ptMinTrackSize.x = 200;                                            //minimum drag X size for the window
                mmi.ptMinTrackSize.y = 40;                                             //minimum drag Y size for the window
                mmi = AdjustWorkingAreaForAutoHide(monitorContainingApplication, mmi); //need to adjust sizing if taskbar is set to autohide
            }
            Marshal.StructureToPtr(mmi, lParam, true);
        }

        public enum ABEdge
        {
            ABE_LEFT = 0,
            ABE_TOP = 1,
            ABE_RIGHT = 2,
            ABE_BOTTOM = 3
        }

        public enum ABMsg
        {
            ABM_NEW = 0,
            ABM_REMOVE = 1,
            ABM_QUERYPOS = 2,
            ABM_SETPOS = 3,
            ABM_GETSTATE = 4,
            ABM_GETTASKBARPOS = 5,
            ABM_ACTIVATE = 6,
            ABM_GETAUTOHIDEBAR = 7,
            ABM_SETAUTOHIDEBAR = 8,
            ABM_WINDOWPOSCHANGED = 9,
            ABM_SETSTATE = 10
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct APPBARDATA
        {
            public int cbSize;
            public IntPtr hWnd;
            public int uCallbackMessage;
            public int uEdge;
            public RECT rc;
            public bool lParam;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        };

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class MONITORINFO
        {
            public int cbSize = Marshal.SizeOf(typeof(MONITORINFO));
            public RECT rcMonitor = new RECT();
            public RECT rcWork = new RECT();
            public int dwFlags = 0;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;

            public POINT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }
    }
}
