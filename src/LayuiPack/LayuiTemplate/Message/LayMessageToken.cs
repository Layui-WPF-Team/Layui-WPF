using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayuiTemplate.Message
{
    /// <summary>
    ///  LayMessageToken
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-04-12 上午 10:58:00</para>
    /// </summary>
    internal static class LayMessageToken
    {
        /// <summary>
        /// 默认消息提示Tooken
        /// </summary>
        public static string RootMessageTooken = Guid.NewGuid().ToString();
    }
}
