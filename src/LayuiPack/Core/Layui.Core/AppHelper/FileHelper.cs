using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Layui.Core.AppHelper
{
    /// <summary>
    ///  文件处理
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-05-10 下午 2:58:44</para>
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// 
        /// </summary>
        private WebRequest request = null;
        /// <summary>
        /// 
        /// </summary>
        private WebResponse respone = null;
        /// <summary>
        /// 
        /// </summary>
        public DownloadFileInfo fileInfo { get; private set; }
        /// <summary>
        /// 更变值委托
        /// </summary>
        /// <param name="value">进度值</param>
        /// <param name="maxValue">目标文件体积</param>
        public delegate void ValueHandler(long value, long maxValue);
        /// <summary>
        /// 下载完成委托
        /// </summary>
        /// <param name="IsDownload"></param>
        public delegate void CompletedHandler(bool IsDownload);
        /// <summary>
        /// 值回调事件
        /// </summary>
        public event ValueHandler OnValueChange;
        /// <summary>
        /// 下载完成事件
        /// </summary>
        public event CompletedHandler OnCompleted;
        public FileHelper(DownloadFileInfo details)
        {
            fileInfo = details;
        }
        /// <summary>
        /// 获取文件体积大小
        /// </summary>
        /// <returns></returns>
        public long GetContentLength()
        {
            ////防呆，WebResponse直接返回0////
            if (respone == null) return 0;
            else return respone.ContentLength;
        }
        /// <summary>
        /// 文件下载
        /// </summary>
        public void DownloadFile()
        {
            try
            {
                ////防呆////
                if (fileInfo == null) throw new Exception($"{nameof(DownloadFileInfo)}不允许为空");
                ////创建目标请求实例////
                request = WebRequest.Create(fileInfo.RequestUriString);
                ////获取目标对象////
                respone = request.GetResponse();
                ThreadPool.QueueUserWorkItem((obj) =>
                {
                    Stream netStream = respone.GetResponseStream();
                    ////创建文件保存////
                    Stream fileStream = new FileStream($"{fileInfo.SaveFileUriString}", FileMode.Create);
                    byte[] read = new byte[1024];
                    long progressBarValue = 0;
                    int realReadLen = netStream.Read(read, 0, read.Length);
                    while (realReadLen > 0)
                    {
                        fileStream.Write(read, 0, realReadLen);
                        progressBarValue += realReadLen;
                        if (OnValueChange != null)
                            OnValueChange(progressBarValue, respone.ContentLength);
                        realReadLen = netStream.Read(read, 0, read.Length);
                    }
                    ////关闭资源////
                    netStream.Close();
                    ////关闭资源////
                    fileStream.Close();
                    ////关闭回调////
                    if (OnCompleted != null)
                        OnCompleted(true);
                }, null);
            }
            catch (Exception ex)
            {
                OnCompleted(false);
                throw ex;
            }

        }
    }
}
