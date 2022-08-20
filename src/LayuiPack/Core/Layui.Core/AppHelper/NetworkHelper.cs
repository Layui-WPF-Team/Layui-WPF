using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Layui.Core.AppHelper
{
    /// <summary>
    ///  网络帮助类型
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-08-20 下午 4:12:09</para>
    /// </summary>
    public static class NetworkHelper
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int connectionDescription, int reservedValue);
        /// <summary>
        /// 初始化监听器
        /// </summary>
        public static void Initialization()
        {
            if (IsRuning) return;
            IsRuning = true;
            Listening();
        }
        /// <summary>
        /// 结束监听
        /// </summary>
        public static void Close()
        {
            if (!IsRuning) return;
            IsRuning = false;
        }
        private static bool IsRuning;
        /// <summary>
        /// 获取本机当前网络状态
        /// </summary>
        /// <returns></returns>
        public static bool GetLocalNetworkStatus()
        {
            int i = 0;
            bool flag = InternetGetConnectedState(out i, 0);
            return flag;
        }
        public delegate void NetworkAvailabilityHandler(bool isAvailable);
        /// <summary>
        /// 监听本机网络实时可用状态
        /// </summary>
        public static event NetworkAvailabilityHandler NetworkAvailabilityChanged;
        /// <summary>
        /// 开始执行网络监听
        /// </summary>
        private static void Listening()
        {
            ThreadPool.QueueUserWorkItem(async (obj) =>
            {
                while (true)
                {
                    if (NetworkAvailabilityChanged != null)
                    {
                        NetworkAvailabilityChanged(GetLocalNetworkStatus());
                    }
                    await Task.Delay(200);
                }
            }, null);
        }
    }
}
