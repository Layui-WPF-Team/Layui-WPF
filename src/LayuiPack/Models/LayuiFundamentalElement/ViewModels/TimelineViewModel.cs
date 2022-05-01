using Layui.Core.Base;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LayuiFundamentalElement.ViewModels
{
    public class TimelineViewModel : LayuiViewModelBase
    {
        /// <summary>
        /// 数据测试
        /// </summary>
        private List<object> _List=new List<object>();
        public List<object> List
        {
            get { return _List; }
            set { SetProperty(ref _List, value); }
        }
        public TimelineViewModel(IContainerExtension container) : base(container)
        {
            for (int i = 0; i < 10; i++)
            {
                List.Add(new {Title=$"{i}",Content= "layui 2.0 的一切准备工作似乎都已到位。发布之弦，一触即发。不枉近百个日日夜夜与之为伴。因小而大，因弱而强。无论它能走多远，抑或如何支撑？至少我曾倾注全心，无怨无悔 " });
            }
            List.Add(new { Title = $"结尾", Content =""});
        }
    }
}
