using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayuiTemplate.Dialog.Interface
{
    internal static class ILayDialogUserControlExtensions
    {
        internal static ILayDialogAware GetDialogViewModel(this ILayDialogUserControl dialogUserControl)
        {
            return (ILayDialogAware)dialogUserControl.DataContext;
        }
    }
}
