using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace LayUI.Wpf.Controls
{
    public class LayTreeViewItem : TreeViewItem
    {
        /// <summary>
        /// 复选框按钮
        /// </summary>
        private ToggleButton checkButton;
        /// <summary>
        /// 父类复选框事件
        /// </summary>
        private Action parentCheckAction;
        /// <summary>
        /// 子类复选框事件
        /// </summary>
        private Action childrenlCheckAction;
        /// <summary>
        /// 选中
        /// </summary>

        [Bindable(true)]
        public bool? IsChecked
        {
            get { return (bool?)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsChecked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register("IsChecked", typeof(bool?), typeof(LayTreeViewItem), new PropertyMetadata(false));

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
            try
            {
                var layTreeViewItem = new LayTreeViewItem();
                Action parentCheckEvent = () =>
                {
                    CheckedParentlItem(layTreeViewItem, this);
                };
                layTreeViewItem.parentCheckAction += parentCheckEvent;
                Action childrenlCheck = () =>
                {
                    CheckedChildrenlItem(this, layTreeViewItem);
                };
                childrenlCheckAction += childrenlCheck;
                return layTreeViewItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        void CheckedParentlItem(LayTreeViewItem treeViewItem, LayTreeViewItem parentTreeViewItem)
        {
            try
            {
                var items = VisualTreeHelper.GetParent(treeViewItem) as Panel;
                if (items == null) return;
                var isCheckedNum = items.Children.Cast<LayTreeViewItem>().Where(o => o.IsChecked == true).Count();
                if (isCheckedNum == 0) parentTreeViewItem.IsChecked = false;
                var isNullCheckedNum = items.Children.Cast<LayTreeViewItem>().Where(o => o.IsChecked == null).Count();
                if (isCheckedNum > 0 || isNullCheckedNum > 0) parentTreeViewItem.IsChecked = null;
                if (isCheckedNum == items.Children.Count) parentTreeViewItem.IsChecked = true;
                Dispatcher.Invoke(() =>
                {
                    parentTreeViewItem.parentCheckAction?.Invoke();
                });
            }
            catch (Exception ex)
            { 
                throw ex;
            }

        }
        void CheckedChildrenlItem(LayTreeViewItem treeViewItem, LayTreeViewItem childrenlTreeViewItem)
        {
            try
            {
                if (treeViewItem.Items.Count > 0)
                {
                    if (childrenlTreeViewItem == null) return;
                    var items = VisualTreeHelper.GetParent(childrenlTreeViewItem) as Panel;
                    if (items == null) return;
                    if (treeViewItem.IsChecked == false)
                    {
                        foreach (LayTreeViewItem item in items.Children)
                        {
                            item.IsChecked = false;
                        }
                    }
                    if (treeViewItem.IsChecked == true)
                    {
                        foreach (LayTreeViewItem item in items.Children)
                        {
                            item.IsChecked = true;
                        }
                    }
                    Dispatcher.Invoke(() =>
                    {
                        childrenlTreeViewItem.childrenlCheckAction?.Invoke();
                    });
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            checkButton = GetTemplateChild("PART_CheckBox") as ToggleButton;
            if (checkButton != null)
            {
                checkButton.Click -= CheckButton_Click;
                checkButton.Click += CheckButton_Click; 
            }
        }
        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            parentCheckAction?.Invoke();
            childrenlCheckAction?.Invoke();
        }
    }
}
