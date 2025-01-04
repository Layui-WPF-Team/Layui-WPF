
using LayUI.Wpf.Enum;
using LayUI.Wpf.Tools;
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
    /// 选项卡
    /// </summary>
    [TemplatePart(Name = nameof(PART_UpBtn), Type = typeof(Button))]
    [TemplatePart(Name = nameof(PART_DownBtn), Type = typeof(Button))]
    [TemplatePart(Name = "PART_ScrollViewer", Type = typeof(ScrollViewer))]
    public class LayTabControl : TabControl
    {
        private Button PART_UpBtn;
        private Button PART_DownBtn;
        private ScrollViewer PART_ScrollViewer;
        /// <summary>
        /// 选项卡类型
        /// </summary>
        [Bindable(true)]
        public TabControlStyle Type
        {
            get { return (TabControlStyle)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Type.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(TabControlStyle), typeof(LayTabControl), new PropertyMetadata(TabControlStyle.Simplicity));


        /// <summary>
        /// 禁用滚动按钮
        /// </summary>
        public bool IsEnableControl
        {
            get { return (bool)GetValue(IsEnableControlProperty); }
            set { SetValue(IsEnableControlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsEnableControl.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsEnableControlProperty =
            DependencyProperty.Register("IsEnableControl", typeof(bool), typeof(LayTabControl), new PropertyMetadata(true));



        /// <summary>
        /// 重写自定义指定项子控件
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is LayTabItem;
        }
        /// <summary>
        /// 抓取指定项控件并返回定制项控件
        /// </summary>
        /// <returns></returns>
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new LayTabItem();
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_ScrollViewer = GetTemplateChild(nameof(PART_ScrollViewer)) as ScrollViewer;
            PART_UpBtn = GetTemplateChild("PART_UpBtn") as Button;
            PART_DownBtn = GetTemplateChild("PART_DownBtn") as Button;
            if (PART_ScrollViewer != null && PART_UpBtn != null && PART_DownBtn != null)
            {
                PART_UpBtn.Click -= PART_UpBtn_Click;
                PART_DownBtn.Click -= PART_DownBtn_Click;
                PART_UpBtn.Click += PART_UpBtn_Click;
                PART_DownBtn.Click += PART_DownBtn_Click;
                PART_ScrollViewer.PreviewMouseWheel -= PART_ScrollViewer_MouseWheel;
                PART_ScrollViewer.PreviewMouseWheel += PART_ScrollViewer_MouseWheel;
            }
        }
        private void PART_ScrollViewer_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (TabStripPlacement == Dock.Top || TabStripPlacement == Dock.Bottom) PART_ScrollViewer?.LineLeft();
                else PART_ScrollViewer?.LineUp();
            }
            else
            {
                if (TabStripPlacement == Dock.Top || TabStripPlacement == Dock.Bottom) PART_ScrollViewer?.LineRight();
                else PART_ScrollViewer?.LineDown();
            }
            e.Handled = true;
        }
        private void PART_UpBtn_Click(object sender, RoutedEventArgs e)
        {
            if (TabStripPlacement == Dock.Top || TabStripPlacement == Dock.Bottom) PART_ScrollViewer?.LineLeft();
            else PART_ScrollViewer?.LineUp();
        }

        private void PART_DownBtn_Click(object sender, RoutedEventArgs e)
        {
            if (TabStripPlacement == Dock.Top || TabStripPlacement == Dock.Bottom) PART_ScrollViewer?.LineRight();
            else PART_ScrollViewer?.LineDown();
        }
    }
}
