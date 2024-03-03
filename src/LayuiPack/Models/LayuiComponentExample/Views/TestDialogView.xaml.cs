using LayUI.Wpf.Enum;
using LayUI.Wpf.Global;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LayuiComponentExample.Views
{
    /// <summary>
    /// TestDialogView.xaml 的交互逻辑
    /// </summary>
    public partial class TestDialogView : UserControl, ILayDialogAware
    {
        public TestDialogView()
        {
            InitializeComponent();
        }

        public event Action<ILayDialogResult> RequestClose;

        public void OnDialogClosed()
        { 
        }

        public void OnDialogOpened(ILayDialogParameter parameters)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LayDialogResult dialogResult = new LayDialogResult();
            dialogResult.Result = ButtonResult.No;
            RequestClose?.Invoke(dialogResult);
        }
    }
}
