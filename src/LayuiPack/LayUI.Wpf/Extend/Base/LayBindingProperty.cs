using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LayUI.Wpf.Extend
{
    /// <summary>
    ///  用于处理可视化树上无法抓到上下文的问题
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-08-30 下午 5:03:23</para>
    /// </summary>
    public class LayBindingProperty : Freezable
    {
        /// <summary>
        /// 数据上下文
        /// </summary>
        public object DataContext
        {
            get { return (object)GetValue(DataContextProperty); }
            set { SetValue(DataContextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataContext.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataContextProperty =
            DependencyProperty.Register("DataContext", typeof(object), typeof(LayBindingProperty));
        protected override Freezable CreateInstanceCore()
        {
            return new LayBindingProperty();
        }
    }
}
