using Layui.Core.Base;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LayuiComponentExample.ViewModels
{
    public class CarouselViewModel : LayuiViewModelBase
    {
        private ObservableCollection<string> _Images;
        public ObservableCollection<string> Images
        {
            get { return _Images; }
            set { SetProperty(ref _Images, value); }
        }
        public CarouselViewModel(IContainerExtension container) : base(container)
        {
            Images = new ObservableCollection<string>() {
                "https://tse2-mm.cn.bing.net/th/id/OIP-C.18-23IyhMHV0DyhI4Rp-hAHaEK?w=331&h=186&c=7&r=0&o=5&pid=1.7",
                "https://tse2-mm.cn.bing.net/th/id/OIP-C.Vp-fmx_i9hx4yQQvVjhv3gHaEK?w=291&h=180&c=7&r=0&o=5&pid=1.7",
                "https://tse2-mm.cn.bing.net/th/id/OIP-C.x698Rwf29-ARM8Zy2YugmQHaFL?w=257&h=180&c=7&r=0&o=5&pid=1.7",
                "https://tse4-mm.cn.bing.net/th/id/OIP-C.7LAvhNHfAu1vLWSCAzqjVwHaDo?w=348&h=171&c=7&r=0&o=5&pid=1.7",
                "https://tse1-mm.cn.bing.net/th/id/OIP-C.Xf7-mF0XmUSVYF1_FmlEmwHaE7?w=296&h=197&c=7&r=0&o=5&pid=1.7"
            };
        }
    }
}
