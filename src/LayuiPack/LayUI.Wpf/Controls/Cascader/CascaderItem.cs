using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace LayUI.Wpf.Controls
{
    public class LayCascaderItem : ItemsControl
    {

        internal LayCascaderItem ParentLayCascaderItem => ParentItemsControl as LayCascaderItem;

        internal ItemsControl ParentItemsControl => ItemsControl.ItemsControlFromItemContainer(this); 

        private Button PART_Button;
        /// <summary>
        /// 内置Popup
        /// </summary>
        private Popup PART_Popup;
        /// <summary>
        ///  父级内置Popup
        /// </summary>
        private Popup PART_Parent_Popup;

        internal double MaxDropDownHeight
        {
            get { return (double)GetValue(MaxDropDownHeightProperty); }
            set { SetValue(MaxDropDownHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxDropDownHeight.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty MaxDropDownHeightProperty =
            DependencyProperty.Register("MaxDropDownHeight", typeof(double), typeof(LayCascaderItem));

        internal bool IsDropDown
        {
            get { return (bool)GetValue(IsDropDownProperty); }
            set { SetValue(IsDropDownProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsDropDown.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty IsDropDownProperty =
            DependencyProperty.Register("IsDropDown", typeof(bool), typeof(LayCascaderItem));
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new LayCascaderItem();
        }
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is LayCascaderItem;
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_Button = GetTemplateChild(nameof(PART_Button)) as Button;
            PART_Popup = GetTemplateChild(nameof(PART_Popup)) as Popup; 
            if (ParentLayCascaderItem!=null)
            {
                PART_Parent_Popup = ParentLayCascaderItem.Template.FindName(nameof(PART_Popup), ParentLayCascaderItem) as Popup;
                if (PART_Parent_Popup != null)
                {
                    PART_Parent_Popup.Closed -= PART_Popup_Closed;
                    PART_Parent_Popup.Closed += PART_Popup_Closed;
                }
            }
            if (PART_Button != null && PART_Popup != null)
            {
                PART_Button.Click -= PART_Button_Click;
                PART_Button.Click += PART_Button_Click;
                PART_Popup.Opened -= PART_Popup_Opened;
                PART_Popup.Opened += PART_Popup_Opened;
                PART_Popup.Closed -= PART_Popup_Closed;
                PART_Popup.Closed += PART_Popup_Closed;
            }
        }

        private void PART_Popup_Closed(object sender, EventArgs e)
        {
            IsDropDown = false;
        }

        private void PART_Popup_Opened(object sender, EventArgs e)
        {

        }

        private void PART_Button_Click(object sender, RoutedEventArgs e)
        {
            IsDropDown = !IsDropDown;
        }
    }
}
