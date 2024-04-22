using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;

namespace LayUI.Wpf.Controls.HwndHost
{
    public class LayHwndHost : Control
    { 
        private Window _window = new Window();
        public LayHwndHost()
        {
            IntPtr hwnd = new WindowInteropHelper(_window).Handle; 
        }
    }
}
