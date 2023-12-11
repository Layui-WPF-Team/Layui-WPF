using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace LayUI.Wpf.Controls
{
    /// <summary>
    /// 树选择控件
    /// </summary> 
    [TemplatePart(Name = nameof(PART_Popup), Type = typeof(Popup))]
    public class LayTreeSelect : ItemsControl, ILayControl
    { 
        private Popup PART_Popup;
        public bool IsDropDownOpen
        {
            get { return (bool)GetValue(IsDropDownOpenProperty); }
            set { SetValue(IsDropDownOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsDropDownOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDropDownOpenProperty =
            DependencyProperty.Register("IsDropDownOpen", typeof(bool), typeof(LayTreeSelect));

        public IEnumerable SelectedItems
        {
            get { return (IEnumerable)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register("SelectedItems", typeof(IEnumerable), typeof(LayTreeSelect));
        [Bindable(true)]
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(LayTreeSelect));

        public ItemsPanelTemplate SelectedItemsPanel
        {
            get { return (ItemsPanelTemplate)GetValue(SelectedItemsPanelProperty); }
            set { SetValue(SelectedItemsPanelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItemsPanel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemsPanelProperty =
            DependencyProperty.Register("SelectedItemsPanel", typeof(ItemsPanelTemplate), typeof(LayTreeSelect));


        public DataTemplate SelectedItemTemplate
        {
            get { return (DataTemplate)GetValue(SelectedItemTemplateProperty); }
            set { SetValue(SelectedItemTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItemTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemTemplateProperty =
            DependencyProperty.Register("SelectedItemTemplate", typeof(DataTemplate), typeof(LayTreeSelect));



        /// <summary>
        /// 重写自定义指定项子控件
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is LayTreeSelectItem;
        }
        /// <summary>
        /// 抓取指定项控件并返回定制项控件
        /// </summary>
        /// <returns></returns>
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new LayTreeSelectItem();
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate(); 
            PART_Popup = GetTemplateChild(nameof(PART_Popup)) as Popup;
        }

    }
}
