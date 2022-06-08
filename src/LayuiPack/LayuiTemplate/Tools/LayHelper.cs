using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LayuiTemplate.Tools
{
    /// <summary>
    /// 资源查找帮助类
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-04-24 上午 10:11:26</para>
    /// </summary>
    public static class LayHelper
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
    }
}
