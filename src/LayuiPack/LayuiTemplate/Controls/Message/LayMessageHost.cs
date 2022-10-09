
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LayuiTemplate.Controls
{
    /// <summary>
    ///  LayMessageHost
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-05-19 下午 8:01:14</para>
    /// </summary>
    public class LayMessageHost : System.Windows.Controls.ItemsControl
    {
        /// <summary>
        /// 重写自定义指定项子控件
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is LayMessageControl;
        }
        /// <summary>
        /// 抓取指定项控件并返回定制项控件
        /// </summary>
        /// <returns></returns>
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new LayMessageControl();
        }
    }
}
