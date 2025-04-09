using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Wpf
{
    public abstract class LayBindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual bool SetProperty<T>(ref T storage, T value, Action onChanged = null, [CallerMemberName] string propertyName = null)
        {
            // 检查新值是否与当前值相等
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false; // 如果相等，返回 false
            }

            storage = value; // 更新存储的值
            onChanged?.Invoke(); // 调用可选的 onChanged 回调
            OnPropertyChanged(propertyName); // 触发属性变化通知
            return true; // 返回 true 表示值已更改
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName)); // 触发属性变化事件
        public void RaisePropertyChanged([CallerMemberName] string propertyName = null) =>
            OnPropertyChanged(propertyName);
        protected void OnPropertyChanged(PropertyChangedEventArgs args) =>
            PropertyChanged?.Invoke(this, args); // 通知所有订阅者
    }
}
