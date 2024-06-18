using Layui.Core.Base;
using LayUI.Wpf.Controls;
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

        private double _Value=1.1;
        public double Value
        {
            get { return _Value; }
            set { SetProperty(ref _Value, value); }
        }

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
        private  string GetIdCode()
        {
            System.Random rnd;
            string[] _crabodistrict = new string[] { "350201", "350202", "350203", "350204", "350205", "350206", "350211", "350205", "350213" };
            rnd = new Random(System.DateTime.Now.Millisecond);
            //PIN = District + Year(50-92) + Month(01-12) + Date(01-30) + Seq(001-600)
            string _pinCode = string.Format("{0}19{1}{2:00}{3:00}{4:000}", _crabodistrict[rnd.Next(0, 8)], rnd.Next(50, 92), rnd.Next(1, 12), rnd.Next(1, 30), rnd.Next(1, 600));

            char[] _chrPinCode = _pinCode.ToCharArray();
            //校验码字符值
            char[] _chrVerify = new char[] { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };
            //i----表示号码字符从由至左包括校验码在内的位置序号；
            //ai----表示第i位置上的号码字符值；
            //Wi----示第i位置上的加权因子，其数值依据公式intWeight=2（n-1）(mod 11)计算得出。
            int[] _intWeight = new int[] { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2, 1 };
            int _craboWeight = 0;
            for (int i = 0; i < 17; i++)//从1 到 17 位,18为要生成的验证码
            {
                _craboWeight = _craboWeight + Convert.ToUInt16(_chrPinCode[i].ToString()) * _intWeight[i];
            }
            _craboWeight = _craboWeight % 11;
            _pinCode += _chrVerify[_craboWeight];

            return _pinCode;
        }
        public FormViewModel(IContainerExtension container) : base(container)
        {
            Password = GetIdCode();
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
