using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LayuiTemplate.Control
{
    public class LayTreeView : TreeView
    {
        /// <summary>
        /// 是否激活复选框
        /// </summary>
        public bool IsActiveCheckBox
        {
            get { return (bool)GetValue(IsActiveCheckBoxProperty); }
            set { SetValue(IsActiveCheckBoxProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsActiveCheckBox.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsActiveCheckBoxProperty =
            DependencyProperty.Register("IsActiveCheckBox", typeof(bool), typeof(LayTreeView));

        /// <summary>
        /// 重写自定义指定项子控件
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is LayTreeViewItem;
        }
        /// <summary>
        /// 抓取指定项控件并返回定制项控件
        /// </summary>
        /// <returns></returns>
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new LayTreeViewItem();
        }
    }
}
