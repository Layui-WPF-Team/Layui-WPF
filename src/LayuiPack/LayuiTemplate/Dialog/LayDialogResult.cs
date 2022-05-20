using LayuiTemplate.Dialog.Interface;
using LayuiTemplate.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayuiTemplate.Dialog
{
    public class LayDialogResult : ILayDialogResult
    {
        public ButtonResult Result { get; set; } = ButtonResult.None;

        public LayDialogParameter Parameters { get; set; }
    }
}
