using LayuiTemplate.Dialog.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LayuiTemplate.Dialog
{
    /// <summary>
    ///  LayDialogWindow
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-04-12 下午 1:50:20</para>
    /// </summary>
    internal class LayDialogWindow:ContentControl,ILayDialogWindow
    {
        public ILayDialogResult Result { get; set; }
    }
}
