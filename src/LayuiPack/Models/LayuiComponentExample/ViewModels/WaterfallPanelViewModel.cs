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
    public class WaterfallPanelViewModel : LayuiViewModelBase
    {
        private ObservableCollection<string> _Images;
        public ObservableCollection<string> Images
        {
            get { return _Images; }
            set { SetProperty(ref _Images, value); }
        }
        public WaterfallPanelViewModel(IContainerExtension container) : base(container)
        {
            Images = new ObservableCollection<string>()
            {
                "https://img2.baidu.com/it/u=288050482,1982365669&fm=253&fmt=auto&app=138&f=JPEG?w=650&h=413",
                "https://img2.baidu.com/it/u=3449402524,1741841315&fm=253&fmt=auto&app=138&f=JPEG?w=600&h=375", 
                "https://img1.baidu.com/it/u=1891908198,4061736086&fm=253&fmt=auto&app=120&f=JPEG?w=800&h=1068",
                "https://img1.baidu.com/it/u=2828841796,3433654732&fm=253&fmt=auto&app=120&f=JPEG?w=500&h=750",
                "https://img0.baidu.com/it/u=1284274573,1149794074&fm=253&fmt=auto&app=120&f=JPEG?w=814&h=500",
                "https://img0.baidu.com/it/u=4140009090,308227834&fm=253&fmt=auto&app=120&f=JPEG?w=816&h=459",
                "https://img1.baidu.com/it/u=3735059139,4156269988&fm=253&fmt=auto&app=138&f=JPEG?w=889&h=500",
                "https://img0.baidu.com/it/u=2540269236,312822239&fm=253&fmt=auto&app=120&f=JPEG?w=867&h=500",
                "https://img1.baidu.com/it/u=3108925124,703469481&fm=253&fmt=auto&app=120&f=JPEG?w=500&h=889",
                "https://img2.baidu.com/it/u=747971050,2621429413&fm=253&fmt=auto&app=120&f=JPEG?w=800&h=500",
                "https://img2.baidu.com/it/u=1011731999,1407707806&fm=253&fmt=auto&app=120&f=JPEG?w=800&h=500",
                "https://img2.baidu.com/it/u=2137750043,12910019&fm=253&fmt=auto&app=138&f=JPEG?w=800&h=1062",
                "https://img1.baidu.com/it/u=230867667,25422101&fm=253&fmt=auto&app=138&f=JPEG?w=1214&h=722",
                "https://img2.baidu.com/it/u=1957330745,2385228575&fm=253&fmt=auto?w=640&h=960",
                "https://img0.baidu.com/it/u=1911227375,3248982718&fm=253&fmt=auto&app=120&f=JPEG?w=1422&h=800",
                "https://img2.baidu.com/it/u=1881596134,2100951294&fm=253&fmt=auto&app=120&f=JPEG?w=889&h=500",
                "https://img2.baidu.com/it/u=2334301773,2106589567&fm=253&fmt=auto&app=120&f=JPEG?w=667&h=500",
                "https://img1.baidu.com/it/u=2641111134,3102874880&fm=253&fmt=auto&app=120&f=JPEG?w=654&h=368",
            };
        } 
    }
}
