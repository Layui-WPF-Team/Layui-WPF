using Layui.Core.Base;
using LayuiTemplate.Controls;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;

namespace LayuiFundamentalElement.ViewModels
{
    /// <summary>
    /// 视图输入框VM
    /// </summary>
    public class FormViewModel :  LayuiViewModelBase
    {
        private FlowDocument _GetDocument;
        public FlowDocument GetDocument
        {
            get { return _GetDocument; }
            set { SetProperty(ref _GetDocument, value); }
        }
        private FlowDocument _SetDocument;
        public FlowDocument SetDocument
        {
            get { return _SetDocument; }
            set { SetProperty(ref _SetDocument, value); }
        }
        public FormViewModel(IContainerExtension container) : base(container)
        {
            Password ="123123";
        }
        public List<string> ListDatas { get; set; } = new List<string>()
        {
            "1","2","3","4","5","6","7","8","9","10","1","2","3","4","5","6","7","8","9","10","1","2","3","4","5","6","7","8","9","10","1","2","3","4","5","6","7","8","9","10","1","2","3","4","5","6","7","8","9","10","1","2","3","4","5","6","7","8","9","10","1","2","3","4","5","6","7","8","9","10"
        };
        private bool _Error=false;

        public bool Error
        {
            get { return _Error; }
            set { _Error = value; RaisePropertyChanged(); }
        }
        private string _Password;

        public string Password
        {
            get { return _Password; }
            set { _Password = value; RaisePropertyChanged(); }
        }
        public override void ExecuteLoadedCommand()
        {
            base.ExecuteLoadedCommand();

        }
        private DelegateCommand _GetDocumentCommand;
        public DelegateCommand GetDocumentCommand =>
            _GetDocumentCommand ?? (_GetDocumentCommand = new DelegateCommand(ExecuteGetDocumentCommand));

        void ExecuteGetDocumentCommand()
        {
            //获取富文本内容(用于存储数据库)
            var data = LayRichTextBox.GetTextByRichBox(GetDocument);
            //设置富文本内容
            LayRichTextBox.SetTextToRichBox(data, SetDocument);
        }
        private DelegateCommand _SaveCommand;
        public DelegateCommand SaveCommand =>
            _SaveCommand ?? (_SaveCommand = new DelegateCommand(Save));
        private void Save()
        {
            Error = true;
            MessageBox.Show($"{Password}");
        }
    }
}
