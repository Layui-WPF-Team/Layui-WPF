using LayuiTemplate.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayuiTemplate.Dialog.Interface
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
