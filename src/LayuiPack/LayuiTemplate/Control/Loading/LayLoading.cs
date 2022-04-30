using LayuiTemplate.Enum;
using LayuiTemplate.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LayuiTemplate.Control
{
    public class LayLoading:ContentControl
    {
        /// <summary>
        /// 动画开关
        /// </summary>
        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsActive.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof(bool), typeof(LayLoading), new PropertyMetadata(false));

        /// <summary>
        /// 提示信息
        /// </summary>
        public object MessageContent
        {
            get { return (object)GetValue(MessageContentProperty); }
            set { SetValue(MessageContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MessageContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageContentProperty =
            DependencyProperty.Register("MessageContent", typeof(object), typeof(LayLoading));



        public LoadingStyle Type
        {
            get { return (LoadingStyle)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Type.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(LoadingStyle), typeof(LayLoading), new PropertyMetadata(LoadingStyle.Google));



    }
}
