using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayuiHome.Models
{
    /// <summary>
    /// 菜单栏模型
    /// </summary>
    public class MenuItemModel: BindableBase
    {
        private bool _IsSelected;
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsSelected
        {
            get { return _IsSelected; }
            set { _IsSelected = value; RaisePropertyChanged(); }
        }
        private string _PageKey;
        /// <summary>
        /// 导航地址
        /// </summary>
        public string PageKey
        {
            get { return _PageKey; }
            set { _PageKey = value; RaisePropertyChanged(); }
        }
        private string _ItemTitle;
        /// <summary>
        /// 标题
        /// </summary>
        public string ItemTitle
        {
            get { return _ItemTitle; }
            set { _ItemTitle = value; RaisePropertyChanged(); }
        }
        private string _StringIocn;
        /// <summary>
        /// 字体图标
        /// </summary>
        public string StringIocn
        {
            get { return _StringIocn; }
            set { _StringIocn = value; RaisePropertyChanged(); }
        }
        private ObservableCollection<MenuItemModel> _Data;
        /// <summary>
        /// 多级菜单集合
        /// </summary>
        public ObservableCollection<MenuItemModel> Data
        {
            get { return _Data; }
            set { _Data = value; RaisePropertyChanged(); }
        }
        private bool _IsNew;
        public bool IsNew
        {
            get { return _IsNew; }
            set { SetProperty(ref _IsNew, value); }
        }
    }
}
