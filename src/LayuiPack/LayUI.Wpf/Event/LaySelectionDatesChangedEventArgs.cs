using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LayUI.Wpf.Event
{
    public delegate void LaySelectionDatesChangedEventHandler(object sender, LaySelectionDatesChangedEventArgs e);
    public class LaySelectionDatesChangedEventArgs : RoutedEventArgs
    { 
        /// <summary>
        /// 旧时间
        /// </summary>
        public IEnumerable<DateTime?> OldValue;
        /// <summary>
        /// 新时间
        /// </summary>
        public IEnumerable<DateTime?> NewValue;
        public LaySelectionDatesChangedEventArgs(RoutedEvent id, IEnumerable<DateTime?> oldValue, IEnumerable<DateTime?> newValue)
        {
            base.RoutedEvent = id;

            if (oldValue != null) OldValue = oldValue; 
            if (newValue != null) NewValue = newValue;
        }

        protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
        {
            EventHandler<LaySelectionDatesChangedEventArgs> eventHandler = genericHandler as EventHandler<LaySelectionDatesChangedEventArgs>;
            if (eventHandler != null)
            {
                eventHandler(genericTarget, this);
            }
            else
            {
                LaySelectionDatesChangedEventHandler selectionChangedEventHandler = (LaySelectionDatesChangedEventHandler)genericHandler;
                selectionChangedEventHandler(genericTarget, this);
            }
        }

    }
}
