using LayuiTemplate.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;

namespace LayuiTemplate.Control
{
    [ContentProperty("Content")]
    [DefaultProperty("Content")]
    [TemplatePart(Name = "PART_Popup", Type = typeof(Popup))]
    [TemplatePart(Name = "PART_ToggleButton", Type = typeof(ToggleButton))]
    [TemplatePart(Name = "PART_ContentGrid", Type = typeof(Grid))]
    public class LayPopupBox : System.Windows.Controls.Control
    {
        /// <summary>
        /// 容器
        /// </summary>
        private Grid PART_ContentGrid;
        /// <summary>
        /// 标题按钮
        /// </summary>
        private ToggleButton PART_ToggleButton;
        /// <summary>
        /// 弹窗
        /// </summary>
        private Popup PART_Popup;
        /// <summary>
        /// 垂直位置
        /// </summary>
        public double VerticalOffset
        {
            get { return (double)GetValue(VerticalOffsetProperty); }
            set { SetValue(VerticalOffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VerticalOffset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VerticalOffsetProperty =
            DependencyProperty.Register("VerticalOffset", typeof(double), typeof(LayPopupBox));
        // <summary>
        /// 水平位置
        /// </summary>
        public bool HorizontalOffset
        {
            get { return (bool)GetValue(HorizontalOffsetProperty); }
            set { SetValue(HorizontalOffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HorizontalOffset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HorizontalOffsetProperty =
            DependencyProperty.Register("HorizontalOffset", typeof(bool), typeof(LayPopupBox));
        public PopupAnimation PopupAnimation
        {
            get { return (PopupAnimation)GetValue(PopupAnimationProperty); }
            set { SetValue(PopupAnimationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PopupAnimation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PopupAnimationProperty =
            DependencyProperty.Register("PopupAnimation", typeof(PopupAnimation), typeof(LayPopupBox), new PropertyMetadata(PopupAnimation.None));

        /// <summary>
        /// 标题内容
        /// </summary>
        public object Header
        {
            get { return (object)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(object), typeof(LayPopupBox));

        /// <summary>
        /// 窗体悬停位置
        /// </summary>
        public PlacementMode Placement
        {
            get { return (PlacementMode)GetValue(PlacementProperty); }
            set { SetValue(PlacementProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Placement.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlacementProperty =
            DependencyProperty.Register("Placement", typeof(PlacementMode), typeof(LayPopupBox), new PropertyMetadata(PlacementMode.Bottom));


        /// <summary>
        /// 圆角程度
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(LayPopupBox));
        /// <summary>
        /// 内容
        /// </summary>
        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(LayPopupBox));


        /// <summary>
        /// 是否自动关闭当前弹出窗口
        /// <para>弹窗内有ButtonBase元素才能生效</para>
        /// </summary>
        public bool IsAutoClose
        {
            get { return (bool)GetValue(IsAutoCloseProperty); }
            set { SetValue(IsAutoCloseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAutoClose.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAutoCloseProperty =
            DependencyProperty.Register("IsAutoClose", typeof(bool), typeof(LayPopupBox), new PropertyMetadata(false));

        /// <summary>
        /// 修改当前按钮点击事件
        /// </summary>
        /// <param name="IsAddEvent"></param>
        private void UpdateClickEvent(bool IsAddEvent)
        {
            var btns = LayVisualTreeHelper.FindVisualChild<ButtonBase>(PART_ContentGrid);
            if (btns != null)
            {
                if (IsAddEvent)
                {
                    foreach (ButtonBase item in btns)
                    {
                        item.Click -= PART_Button_Click;
                        item.Click += PART_Button_Click;
                    }
                }
                else
                {
                    foreach (ButtonBase item in btns)
                    {
                        item.Click -= PART_Button_Click;
                    }
                }
            }
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_ContentGrid = GetTemplateChild("PART_ContentBorder") as Grid;
            PART_ToggleButton = GetTemplateChild("PART_ToggleButton") as ToggleButton;
            PART_Popup = GetTemplateChild("PART_Popup") as Popup;
            PART_Popup.Opened -= PART_Popup_Opened;
            PART_Popup.Opened += PART_Popup_Opened;
            PART_Popup.Closed -= PART_Popup_Closed;
            PART_Popup.Closed += PART_Popup_Closed;
            if (PART_ToggleButton != null)
            {
                PART_ToggleButton.Click -= PART_Button_Click;
                PART_ToggleButton.Click += PART_Button_Click;
            }
        }

        private void PART_Popup_Closed(object sender, EventArgs e)
        {
            UpdateClickEvent(false);
        }

        private void PART_Popup_Opened(object sender, EventArgs e)
        {
            UpdateClickEvent(IsAutoClose);
        }

        private void PART_Button_Click(object sender, RoutedEventArgs e)
        {
            if (e.Source is Button button)
            {
                PART_Popup.IsOpen = false;
            }
        }
    }
}
