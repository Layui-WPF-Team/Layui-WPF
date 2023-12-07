using LayUI.Wpf.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LayUI.Wpf.Controls
{
    /// <summary>
    /// 提示内容控件
    /// </summary>
    [TemplatePart(Name =nameof(PART_CloseButton),Type =typeof(Button))]
    public class LayNotificationControl:ContentControl, ILayControl
    {
        /// <summary>
        /// 关闭按钮
        /// </summary>
        private Button PART_CloseButton;

        internal Action Close;
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(LayNotificationControl));


        [Bindable(true)]
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(LayNotificationControl));


        /// <summary>
        /// 信息类型
        /// </summary>
        public NotificationType Type
        {
            get { return (NotificationType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Type.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(NotificationType), typeof(LayNotificationControl));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_CloseButton = GetTemplateChild(nameof(PART_CloseButton)) as Button;
            if (PART_CloseButton != null)
            {
                PART_CloseButton.Click += PART_CloseButton_Click;
                PART_CloseButton.Click += PART_CloseButton_Click;
            }
        }

        private void PART_CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                Close?.Invoke();
            }));
        }
    }
}
