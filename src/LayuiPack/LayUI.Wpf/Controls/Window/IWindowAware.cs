using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Wpf.Controls
{
    /// <summary>
    /// 窗体关闭控制
    /// </summary>
    public interface IWindowAware
    {
        bool CanClosing();
    }
}
