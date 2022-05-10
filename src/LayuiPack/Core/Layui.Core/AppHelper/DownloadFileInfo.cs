using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layui.Core.AppHelper
{
    /// <summary>
    ///  文件下载详情
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-05-10 下午 3:20:12</para>
    /// </summary>
    public class DownloadFileInfo
    {
        /// <summary>
        /// 请求地址
        /// </summary>
        public string RequestUriString { get; set; }
        /// <summary>
        /// 保存地址(全路径，包括文件后缀名)
        /// </summary>
        public string SaveFileUriString { get; set; }
    }
}
