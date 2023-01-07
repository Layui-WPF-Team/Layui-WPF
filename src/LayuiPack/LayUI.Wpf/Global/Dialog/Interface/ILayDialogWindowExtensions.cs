using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Wpf.Global
{
    internal static class ILayDialogWindowExtensions
    {
        internal static ILayDialogWindowAware GetDialogViewModel(this ILayDialogWindow dialog)
        {
            return (ILayDialogWindowAware)dialog.DataContext;
        }
    }
}
