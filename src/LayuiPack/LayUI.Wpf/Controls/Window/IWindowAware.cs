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
        /// <summary>
        /// 初始化完成
        /// </summary>
        void Intialized();
        /// <summary>
        /// 窗体是否可以关闭
        /// </summary>
        /// <returns></returns>
        bool CanClosing();
        /// <summary>
        /// 窗体关闭
        /// </summary>
        void Closed();
        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        event Action WindowClose; 
    }
}
