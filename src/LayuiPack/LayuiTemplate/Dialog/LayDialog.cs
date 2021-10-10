using LayuiTemplate.Control;
using LayuiTemplate.Dialog.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LayuiTemplate.Dialog
{
    public class LayDialog : ILayDialog
    {
        public static LayDialog Dialog { get; set; } = Dialog ?? new LayDialog();
        public static Dictionary<string, LayDialogUserControl> DialogViewCollection { get; set; } = DialogViewCollection ?? new Dictionary<string, LayDialogUserControl>();
        public LayDialog()
        {
        }
        /// <summary>
        /// 注入弹窗试图
        /// </summary>
        /// <typeparam name="TView">需要注入的类型</typeparam>
        /// <param name="dialogName">视图名称</param>
        public static void RegisterDialog<TView>(string dialogName)
        {
            try
            {
                foreach (var item in DialogViewCollection)
                {
                    if (item.Key == dialogName) throw new Exception($"{dialogName}弹窗视图多次注入");
                }
                var data = (UserControl)Activator.CreateInstance(typeof(TView));
                DialogViewCollection.Add(dialogName, new LayDialogUserControl()
                {
                    Content = data,
                    DataContext= data.DataContext
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Show(string dialogName, ILayDialogParameter parameters)
        {
            Alert(dialogName, parameters, null);
        }

        public void Show(string dialogName, ILayDialogParameter parameters, Action<ILayDialogResult> callback)
        {
            Alert(dialogName, parameters, callback);
        }
        private void Alert(string dialogName, ILayDialogParameter parameters, Action<ILayDialogResult> callback)
        {
            try
            {
                LayDialogHost layDialog = Application.Current.MainWindow.Template.FindName("WindowDialogHostBody", Application.Current.MainWindow) as LayDialogHost;
                if (layDialog == null|| layDialog.Content!=null) return;
                LayDialogUserControl DialogView = null;
                foreach (var item in DialogViewCollection)
                {
                    if (item.Key == dialogName)
                    {
                        if (layDialog.IsOpen) return;
                        DialogView = item.Value;
                        DialogView.DataContext = item.Value.DataContext;
                        layDialog.Content = DialogView;
                        layDialog.DataContext = DialogView.DataContext;
                        layDialog.IsOpen = true;
                    }
                }
                if (DialogView == null) return;
                ILayDialogUserControl dialog = DialogView as ILayDialogUserControl;
                Action<ILayDialogResult> requestCloseHandler = null;
                requestCloseHandler = (o) =>
                {
                    dialog.Result = o;
                    layDialog.IsOpen = false;
                    layDialog.Dispatcher.BeginInvoke(new Action(async() =>
                    {
                        layDialog.IsOpen = false;
                        await Task.Delay(200);
                        DialogView = null;
                        layDialog.Content = null;
                        layDialog.DataContext = null;
                    }));
                };
                RoutedEventHandler LoadedHandler = null;
                LoadedHandler = (o, e) =>
                {
                    dialog.Loaded -= LoadedHandler;
                    dialog.GetDialogViewModel().RequestClose += requestCloseHandler;
                };
                dialog.Loaded += LoadedHandler;
                RoutedEventHandler UnloadedHandler = null;
                UnloadedHandler = (o, e) =>
                {
                    dialog.Unloaded -= UnloadedHandler;
                    dialog.GetDialogViewModel().RequestClose -= requestCloseHandler;
                    callback?.Invoke(dialog.Result);
                };
                dialog.Unloaded += UnloadedHandler;
                foreach (var item in DialogViewCollection)
                {
                    if (item.Key == dialogName)
                    {
                        if (!(item.Value.DataContext is ILayDialogAware viewModel))
                            throw new NullReferenceException("对话框的 ViewModel 必须实现 IDialogAware 接口 ");
                        ViewAndViewModelAction<ILayDialogAware>(viewModel, d => d.OnDialogOpened(parameters));
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private static void ViewAndViewModelAction<T>(object view, Action<T> action)
        {
            if (view is T viewAsT)
                action(viewAsT);

            if (view is FrameworkElement element && element.DataContext is T viewModelAsT)
            {
                action(viewModelAsT);
            }
        }
    }
}
