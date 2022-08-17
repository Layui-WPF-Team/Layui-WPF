using log4net;
using log4net.Core;
using log4net.Repository;
using Prism.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Layui.Core.Log
{
    /// <summary>
    ///  日志
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-08-17 下午 3:44:16</para>
    /// </summary>
    public class LayLogger : ILayLogger
    {
        public void Debug(string message)
        {
            LogManager.GetLogger($"==>").DebugFormat(message);
        }

        public void Error(Exception exception)
        {
            LogManager.GetLogger($"==>").Error(exception);
        }

        public void Info(string message)
        {
            LogManager.GetLogger($"==>").InfoFormat(message);
        }

        public void Warn(string message)
        {
            LogManager.GetLogger($"==>").DebugFormat(message);
        }
    }
}
