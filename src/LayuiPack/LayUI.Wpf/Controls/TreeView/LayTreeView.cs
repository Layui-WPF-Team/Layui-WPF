using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LayUI.Wpf.Controls
{
    public class LayTreeView : TreeView
    { 
        /// <summary>
        /// 显示复选框
        /// </summary>
        public bool IsShowCheckButton
        {
            get { return (bool)GetValue(IsShowCheckButtonProperty); }
            set { SetValue(IsShowCheckButtonProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsShowCheckButton.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsShowCheckButtonProperty =
            DependencyProperty.Register("IsShowCheckButton", typeof(bool), typeof(LayTreeView));


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
