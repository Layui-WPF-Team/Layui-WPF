using LayuiTemplate.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayuiTemplate.Control
{
    /// <summary>
    ///  LayMessageHost
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-05-19 下午 8:01:14</para>
    /// </summary>
    public class LayMessageHost : System.Windows.Controls.ItemsControl
    {
        public LayMessageHost()
        {
            Unloaded -= LayMessageHost_Unloaded;
            Unloaded += LayMessageHost_Unloaded;
        }
        /// <summary>
        /// 控件销毁移除字典中的缓存控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LayMessageHost_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var tooken = LayMessage.GetTooken(this);
            if (LayMessage.MessageHosts.ContainsKey(tooken)) LayMessage.MessageHosts.Remove(tooken);
        }

    }
}
