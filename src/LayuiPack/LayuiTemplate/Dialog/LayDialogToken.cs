using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayuiTemplate.Dialog
{
    /// <summary>
    ///  DialogToken
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-04-12 上午 10:58:00</para>
    /// </summary>
    internal static class LayDialogToken
    {
        /// <summary>
        /// 默认窗体弹窗Tooken
        /// </summary>
        public static string RootDialogTooken = Guid.NewGuid().ToString();
    }
}
