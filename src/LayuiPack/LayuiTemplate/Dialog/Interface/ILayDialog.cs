using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayuiTemplate.Dialog.Interface
{
    public interface ILayDialog
    {
        void Show(string dialogName, ILayDialogParameter parameters);
        void Show(string dialogName, ILayDialogParameter parameters,Action<ILayDialogResult> callback);
    }
}
