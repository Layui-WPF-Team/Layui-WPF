using LayuiTemplate.Control;
using LayuiTemplate.Global;
using System.Windows;

namespace LayuiApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : LayWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LaySnackbar.Show("欢迎使用Layui—WPF组件库", "RootSnackbarTooken");
        }
    }
}
