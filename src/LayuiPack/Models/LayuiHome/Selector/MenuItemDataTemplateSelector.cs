using LayuiHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LayuiHome.Selector
{
    /// <summary>
    /// 菜单栏模板选择器
    /// </summary>
    public class MenuItemDataTemplateSelector: DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item != null)
            {
                var fe = container as FrameworkElement;
                MenuItemModel menuItem = item as MenuItemModel;
                 if (menuItem.Data != null) {
                    
                    return fe.FindResource("MenuItemsDataTemplate") as DataTemplate;
                }
                else {
                    return fe.FindResource("MenuItemDataTemplate") as DataTemplate;
                }
            }
            return null;
        }
    }
}
