using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Wpf.Global
{
    internal static class ILayDialogUserControlWindowExtensions
    {
        internal static ILayDialogAware GetDialogViewModel(this ILayDialogUserControlWindow dialog)
        {
            return (ILayDialogAware)dialog.DataContext;
        }
        internal static ILayDialogAware GetDialogView(this ILayDialogUserControlWindow dialog)
        {
            return (ILayDialogAware)dialog.Content;
        }
    }
}
