
using LayUI.Wpf.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LayUI.Wpf.Controls
{
    [TemplatePart(Name = "PART_ItemsControl", Type = typeof(ItemsControl))]
    public class LayDialogHost : Control
    {
        public LayDialogHost()
        {
        }
        public ItemsControl DialogItems
        {
            get { return (ItemsControl)GetValue(DialogItemsProperty); }
            internal set { SetValue(DialogItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DialogItemsProperty =
            DependencyProperty.Register("DialogItems", typeof(ItemsControl), typeof(LayDialogHost));

        /// <summary>
        /// 唯一标识ID
        /// </summary>
        internal string GUID
        {
            get { return (string)GetValue(GUIDProperty); }
            set { SetValue(GUIDProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UID.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty GUIDProperty =
            DependencyProperty.Register("GUID", typeof(string), typeof(LayDialogHost));
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            DialogItems = GetTemplateChild("PART_ItemsControl") as ItemsControl;
        }
    }
}
