using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayuiTemplate.Global
{
    public interface ILayDialogWindowAware:ILayDialogAware
    {
        bool CanCloseDialog();
        void OnDialogClosed();
    }
}
