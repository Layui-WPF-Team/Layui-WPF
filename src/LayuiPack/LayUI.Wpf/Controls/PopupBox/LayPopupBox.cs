using LayUI.Wpf.Tools;
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

namespace LayUI.Wpf.Controls
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
        [Bindable(true)]
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
        [Bindable(true)]
        public bool HorizontalOffset
        {
            get { return (bool)GetValue(HorizontalOffsetProperty); }
            set { SetValue(HorizontalOffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HorizontalOffset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HorizontalOffsetProperty =
            DependencyProperty.Register("HorizontalOffset", typeof(bool), typeof(LayPopupBox));

        [Bindable(true)]
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
        [Bindable(true)]
        public object Header
        {
            get { return (object)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(object), typeof(LayPopupBox));
        /// <summary>
        /// 是否开启弹窗
        /// </summary>
        [Bindable(true)]
        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(LayPopupBox), new PropertyMetadata(false,OnOpenChanged));

        private static void OnOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LayPopupBox layPopup)
            {
                layPopup.OnOpen(new RoutedEventArgs(OpenEvent, layPopup));
                if (layPopup.Command != null)
                {
                    layPopup.Command.Execute(layPopup.CommandParameter);
                    layPopup.IsEnabled = layPopup.Command.CanExecute(layPopup.CommandParameter);
                }
            }
        }
        /// <summary>
        /// 窗体悬停位置
        /// </summary>
        [Bindable(true)]
        public PlacementMode Placement
        {
            get { return (PlacementMode)GetValue(PlacementProperty); }
            set { SetValue(PlacementProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Placement.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlacementProperty =
            DependencyProperty.Register("Placement", typeof(PlacementMode), typeof(LayPopupBox), new PropertyMetadata(PlacementMode.Bottom));
        /// <summary>
        /// 开关事件
        /// </summary>
        public event RoutedEventHandler Open
        {
            add => AddHandler(OpenEvent, value);
            remove => RemoveHandler(OpenEvent, value);
        }
        /// <summary>
        /// 开关事件
        /// </summary>
        public static readonly RoutedEvent OpenEvent =
            EventManager.RegisterRoutedEvent("Open", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(LayPopupBox));
        /// <summary>
        /// 开关事件
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnOpen(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }
        [Bindable(true), Category("Action")]
        [Localizability(LocalizationCategory.NeverLocalize)]
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(LayPopupBox), new FrameworkPropertyMetadata((ICommand)null));
        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandParameter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(LayPopupBox), new PropertyMetadata((object)null));

        /// <summary>
        /// 圆角程度
        /// </summary>
        [Bindable(true)]
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
        [Bindable(true)]
        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(LayPopupBox));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_ContentGrid = GetTemplateChild("PART_ContentBorder") as Grid;
            PART_ToggleButton = GetTemplateChild("PART_ToggleButton") as ToggleButton;
            PART_Popup = GetTemplateChild("PART_Popup") as Popup;
        }
    }
}
