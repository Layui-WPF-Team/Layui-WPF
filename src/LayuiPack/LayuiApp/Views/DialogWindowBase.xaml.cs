using LayuiTemplate.Controls;
using LayuiTemplate.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LayuiApp.Views
{
    /// <summary>
    /// DialogWindowBase.xaml 的交互逻辑
    /// </summary>
    public partial class DialogWindowBase : LayWindow, ILayDialogWindow
    {
        public DialogWindowBase()
        {
            InitializeComponent();
        }

        public ILayDialogResult Result { get; set ; }
    }
}
