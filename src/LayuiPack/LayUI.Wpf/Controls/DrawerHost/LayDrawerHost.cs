using LayUI.Wpf.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace LayUI.Wpf.Controls
{
    /// <summary>
    ///  抽屉
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-08-22 下午 1:06:35</para>
    /// </summary>

    [TemplatePart(Name = "PART_DrawerGrid", Type = typeof(Grid))]
    [TemplatePart(Name = "PART_RootGrid", Type = typeof(Grid))]
    [TemplatePart(Name = "PART_DockBorder", Type = typeof(Border))]
    public class LayDrawerHost : System.Windows.Controls.ContentControl,ILayControl
    {
        private Grid PART_RootGrid;
        private Grid PART_DrawerGrid;
        /// <summary>
        /// 圆角
        /// </summary>
        [Bindable(true)]
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(LayDrawerHost));

        [Bindable(true)]
        public bool IsModal
        {
            get { return (bool)GetValue(IsModalProperty); }
            set { SetValue(IsModalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsModal.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsModalProperty =
            DependencyProperty.Register("IsModal", typeof(bool), typeof(LayDrawerHost), new PropertyMetadata(true, OnIsModalChanged));

        private static void OnIsModalChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LayDrawerHost host)
            {
                if (host.IsInitialized) host.UpdateModelEvent();
            }
        }

        /// <summary>
        /// 抽屉子元素停靠位置
        /// </summary>
        [Bindable(true)]
        public DrawerHostStyle Type
        {
            get { return (DrawerHostStyle)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Type.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(DrawerHostStyle), typeof(LayDrawerHost), new PropertyMetadata(DrawerHostStyle.Top));

        /// <summary>
        /// 抽屉开关
        /// </summary>
        [Bindable(true)]
        public bool DrawerOpen
        {
            get { return (bool)GetValue(DrawerOpenProperty); }
            set { SetValue(DrawerOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DrawerOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DrawerOpenProperty =
            DependencyProperty.Register("DrawerOpen", typeof(bool), typeof(LayDrawerHost));

        /// <summary>
        /// 抽屉子元素内容
        /// </summary>
        [Bindable(true)]
        public object DrawerContent
        {
            get { return (object)GetValue(DrawerContentProperty); }
            set { SetValue(DrawerContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DrawerContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DrawerContentProperty =
            DependencyProperty.Register("DrawerContent", typeof(object), typeof(LayDrawerHost));
        /// <summary>
        /// 遮罩背景色
        /// </summary>
        [Bindable(true)]
        public Brush OverlayBackground
        {
            get { return (Brush)GetValue(OverlayBackgroundProperty); }
            set { SetValue(OverlayBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OverlayBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OverlayBackgroundProperty =
            DependencyProperty.Register("OverlayBackground", typeof(Brush), typeof(LayDrawerHost));

        private void UpdateModelEvent()
        {
            if (PART_DrawerGrid == null) return;
            if (IsModal)
            {
                PART_DrawerGrid.MouseLeftButtonDown -= PART_DrawerGrid_MouseButtonDown;
                PART_DrawerGrid.MouseLeftButtonDown += PART_DrawerGrid_MouseButtonDown;
            }
            else PART_DrawerGrid.MouseLeftButtonDown -= PART_DrawerGrid_MouseButtonDown;
        }
        private void PART_DrawerGrid_MouseButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DrawerOpen = false;
            e.Handled = true;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate(); 
             PART_DrawerGrid = GetTemplateChild("PART_DrawerGrid") as Grid;
            PART_RootGrid = GetTemplateChild("PART_RootGrid") as Grid;
            UpdateModelEvent();
        }
    }
}
