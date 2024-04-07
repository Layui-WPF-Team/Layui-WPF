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
    public class LayCascader:ItemsControl
    { 
        private Popup PART_Popup;
        public double MaxDropDownHeight
        {
            get { return (double)GetValue(MaxDropDownHeightProperty); }
            set { SetValue(MaxDropDownHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxDropDownHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxDropDownHeightProperty =
            DependencyProperty.Register("MaxDropDownHeight", typeof(double), typeof(LayCascader));
         
        public bool IsDropDown
        {
            get { return (bool)GetValue(IsDropDownProperty); }
            set { SetValue(IsDropDownProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsDropDown.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDropDownProperty =
            DependencyProperty.Register("IsDropDown", typeof(bool), typeof(LayCascader)); 

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
            PART_Popup = GetTemplateChild(nameof(PART_Popup)) as Popup;
            if (PART_Popup != null)
            { 
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
    }
}
