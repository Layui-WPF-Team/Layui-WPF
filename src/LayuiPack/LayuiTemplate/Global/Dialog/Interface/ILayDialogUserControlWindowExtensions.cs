using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayuiTemplate.Global
{
    internal static class ILayDialogUserControlWindowExtensions
    {
        internal static ILayDialogAware GetDialogViewModel(this ILayDialogUserControlWindow dialog)
        {
            return (ILayDialogAware)dialog.DataContext;
        }
    }
}
