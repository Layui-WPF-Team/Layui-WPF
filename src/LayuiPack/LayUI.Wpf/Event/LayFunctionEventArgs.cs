using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LayUI.Wpf.Event
{
    /// <summary>
    ///  LayFunctionEventArgs
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-08-08 下午 5:36:45</para>
    /// </summary>
    public class LayFunctionEventArgs<T> : RoutedEventArgs
    {
        /// <summary>
        /// 目标参数
        /// </summary>
        public T Info { get; set; }
        public LayFunctionEventArgs(T info)
        {
            Info = info;
        }

        public LayFunctionEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source)
        {
        }

    }
}
