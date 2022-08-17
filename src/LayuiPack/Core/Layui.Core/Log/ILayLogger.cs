using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layui.Core.Log
{
    /// <summary>
    ///  ILayLogger
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-08-17 下午 3:51:01</para>
    /// </summary>
    public interface ILayLogger
    {
        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="message"></param>
        void Info(string message);
        /// <summary>
        /// 异常日志
        /// </summary>
        /// <param name="exception"></param>
        void Error(Exception exception);
        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="message"></param>
        void Warn(string message);
        /// <summary>
        /// Debug
        /// </summary>
        /// <param name="message"></param>
        void Debug(string message);
    }
}
