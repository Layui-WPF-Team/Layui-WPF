using LayUI.Wpf.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Wpf.Global
{
    public interface ILayDialogResult
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        ButtonResult Result { get; }
        LayDialogParameter Parameters { get; }
    }
}
