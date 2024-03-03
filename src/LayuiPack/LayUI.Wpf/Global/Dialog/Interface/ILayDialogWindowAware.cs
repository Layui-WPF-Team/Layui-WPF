using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Wpf.Global
{
    public interface ILayDialogWindowAware:ILayDialogAware
    {
        bool CanCloseDialog();
    }
}
