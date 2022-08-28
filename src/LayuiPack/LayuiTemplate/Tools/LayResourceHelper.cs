using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace LayuiTemplate.Tools
{
    /// <summary>
    /// 资源查找帮助类
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-04-24 上午 10:11:26</para>
    /// </summary>
    public static class LayResourceHelper
    {
        /// <summary>
        /// 查找样式
        /// </summary>
        /// <param name="key">样式名称</param>
        /// <returns></returns>
        public static Style GetStyle(string key)
        {
            try
            {
                var data = Application.Current?.FindResource(key);
                if (data != null)
                    return data as Style;
                else return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取当前资源字典中的Key的值（一般用作语言翻译功能）
        /// <para>注意：未找到对应的值直接返回当前的Key当做值</para>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetResourceData(string key)
        {
            try
            {
                var data = Application.Current?.Resources[key];
                if (data != null) return data;
                else return key;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取当程序集下XANL文件
        /// </summary>
        /// <param name="uri">地址</param>
        /// <returns></returns>
        public static object GetXaml(Uri uri)
        {
            try
            {
                var Stream = Application.GetResourceStream(uri);
                if (Stream != null && Stream.Stream != null)
                {
                    XamlReader xaml = new XamlReader();
                    return xaml.LoadAsync(Stream.Stream);
                }
                else return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
