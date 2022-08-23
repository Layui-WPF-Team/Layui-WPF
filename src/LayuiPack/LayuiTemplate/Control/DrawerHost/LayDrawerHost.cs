using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LayuiTemplate.Control
{
    /// <summary>
    ///  抽屉
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-08-22 下午 1:06:35</para>
    /// </summary>

    [TemplatePart(Name = "PART_DrawerGrid", Type = typeof(Grid))]
    [TemplatePart(Name = "PART_DockPanel", Type = typeof(DockPanel))]
    public class LayDrawerHost : System.Windows.Controls.ContentControl
    {
        private Grid PART_DrawerGrid;
        static LayDrawerHost()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LayDrawerHost), new FrameworkPropertyMetadata(typeof(LayDrawerHost)));
        }
        /// <summary>
        /// 圆角
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(LayDrawerHost));

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
                if (host.IsLoaded) host.UpdateModelEvent();
            }
        }


        #region 左侧抽屉

        /// <summary>
        /// 左侧抽屉开关
        /// </summary>
        public bool LeftDrawerOpen
        {
            get { return (bool)GetValue(LeftDrawerOpenProperty); }
            set { SetValue(LeftDrawerOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftDrawerOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftDrawerOpenProperty =
            DependencyProperty.Register("LeftDrawerOpen", typeof(bool), typeof(LayDrawerHost));



        /// <summary>
        /// 左侧抽屉内容
        /// </summary>
        public object LeftDrawerContent
        {
            get { return (object)GetValue(LeftDrawerContentProperty); }
            set { SetValue(LeftDrawerContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftDrawerContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftDrawerContentProperty =
            DependencyProperty.Register("LeftDrawerContent", typeof(object), typeof(LayDrawerHost));
        /// <summary>
        /// 左侧抽屉背景色
        /// </summary>
        public Brush LeftDrawerBackground
        {
            get { return (Brush)GetValue(LeftDrawerBackgroundProperty); }
            set { SetValue(LeftDrawerBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftDrawerBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftDrawerBackgroundProperty =
            DependencyProperty.Register("LeftDrawerBackground", typeof(Brush), typeof(LayDrawerHost));
        /// <summary>
        /// 左侧抽屉边框线颜色
        /// </summary>
        public Brush LeftDrawerBorderBrush
        {
            get { return (Brush)GetValue(LeftDrawerBorderBrushProperty); }
            set { SetValue(LeftDrawerBorderBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftDrawerBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftDrawerBorderBrushProperty =
            DependencyProperty.Register("LeftDrawerBorderBrush", typeof(Brush), typeof(LayDrawerHost));


        /// <summary>
        /// 左侧抽屉边框线
        /// </summary>
        public Thickness LeftDrawerThickness
        {
            get { return (Thickness)GetValue(LeftDrawerThicknessProperty); }
            set { SetValue(LeftDrawerThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftDrawerThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftDrawerThicknessProperty =
            DependencyProperty.Register("LeftDrawerThickness", typeof(Thickness), typeof(LayDrawerHost));


        /// <summary>
        /// 左侧抽屉圆角
        /// </summary>
        public CornerRadius LeftDrawerCornerRadius
        {
            get { return (CornerRadius)GetValue(LeftDrawerCornerRadiusProperty); }
            set { SetValue(LeftDrawerCornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftDrawerCornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftDrawerCornerRadiusProperty =
            DependencyProperty.Register("LeftDrawerCornerRadius", typeof(CornerRadius), typeof(LayDrawerHost));

        #endregion

        #region 顶部抽屉

        /// <summary>
        /// 顶部抽屉开关
        /// </summary>
        public bool TopDrawerOpen
        {
            get { return (bool)GetValue(TopDrawerOpenProperty); }
            set { SetValue(TopDrawerOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftDrawerOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TopDrawerOpenProperty =
            DependencyProperty.Register("TopDrawerOpen", typeof(bool), typeof(LayDrawerHost));

        /// <summary>
        /// 顶部抽屉内容
        /// </summary>
        public object TopDrawerContent
        {
            get { return (object)GetValue(TopDrawerContentProperty); }
            set { SetValue(TopDrawerContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftDrawerContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TopDrawerContentProperty =
            DependencyProperty.Register("TopDrawerContent", typeof(object), typeof(LayDrawerHost));
        /// <summary>
        /// 顶部抽屉背景色
        /// </summary>
        public Brush TopDrawerBackground
        {
            get { return (Brush)GetValue(TopDrawerBackgroundProperty); }
            set { SetValue(TopDrawerBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftDrawerBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TopDrawerBackgroundProperty =
            DependencyProperty.Register("TopDrawerBackground", typeof(Brush), typeof(LayDrawerHost));
        /// <summary>
        /// 顶部抽屉边框线颜色
        /// </summary>
        public Brush TopDrawerBorderBrush
        {
            get { return (Brush)GetValue(TopDrawerBorderBrushProperty); }
            set { SetValue(TopDrawerBorderBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftDrawerBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TopDrawerBorderBrushProperty =
            DependencyProperty.Register("TopDrawerBorderBrush", typeof(Brush), typeof(LayDrawerHost));


        /// <summary>
        /// 顶部抽屉边框线
        /// </summary>
        public Thickness TopDrawerThickness
        {
            get { return (Thickness)GetValue(TopDrawerThicknessProperty); }
            set { SetValue(TopDrawerThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftDrawerThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TopDrawerThicknessProperty =
            DependencyProperty.Register("TopDrawerThickness", typeof(Thickness), typeof(LayDrawerHost));


        /// <summary>
        /// 顶部抽屉圆角
        /// </summary>
        public CornerRadius TopDrawerCornerRadius
        {
            get { return (CornerRadius)GetValue(TopDrawerCornerRadiusProperty); }
            set { SetValue(TopDrawerCornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftDrawerCornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TopDrawerCornerRadiusProperty =
            DependencyProperty.Register("TopDrawerCornerRadius", typeof(CornerRadius), typeof(LayDrawerHost));

        #endregion

        #region 右侧抽屉
        /// <summary>
        /// 右侧抽屉开关
        /// </summary>
        public bool RightDrawerOpen
        {
            get { return (bool)GetValue(RightDrawerOpenProperty); }
            set { SetValue(RightDrawerOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftDrawerOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightDrawerOpenProperty =
            DependencyProperty.Register("RightDrawerOpen", typeof(bool), typeof(LayDrawerHost));

        /// <summary>
        /// 右侧抽屉内容
        /// </summary>
        public object RightDrawerContent
        {
            get { return (object)GetValue(RightDrawerContentProperty); }
            set { SetValue(RightDrawerContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftDrawerContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightDrawerContentProperty =
            DependencyProperty.Register("RightDrawerContent", typeof(object), typeof(LayDrawerHost));
        /// <summary>
        /// 右侧抽屉背景色
        /// </summary>
        public Brush RightDrawerBackground
        {
            get { return (Brush)GetValue(RightDrawerBackgroundProperty); }
            set { SetValue(RightDrawerBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftDrawerBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightDrawerBackgroundProperty =
            DependencyProperty.Register("RightDrawerBackground", typeof(Brush), typeof(LayDrawerHost));
        /// <summary>
        /// 右侧抽屉边框线颜色
        /// </summary>
        public Brush RightDrawerBorderBrush
        {
            get { return (Brush)GetValue(RightDrawerBorderBrushProperty); }
            set { SetValue(RightDrawerBorderBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftDrawerBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightDrawerBorderBrushProperty =
            DependencyProperty.Register("RightDrawerBorderBrush", typeof(Brush), typeof(LayDrawerHost));


        /// <summary>
        /// 右侧抽屉边框线
        /// </summary>
        public Thickness RightDrawerThickness
        {
            get { return (Thickness)GetValue(RightDrawerThicknessProperty); }
            set { SetValue(RightDrawerThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftDrawerThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightDrawerThicknessProperty =
            DependencyProperty.Register("RightDrawerThickness", typeof(Thickness), typeof(LayDrawerHost));


        /// <summary>
        /// 右侧抽屉圆角
        /// </summary>
        public CornerRadius RightDrawerCornerRadius
        {
            get { return (CornerRadius)GetValue(RightDrawerCornerRadiusProperty); }
            set { SetValue(RightDrawerCornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftDrawerCornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightDrawerCornerRadiusProperty =
            DependencyProperty.Register("RightDrawerCornerRadius", typeof(CornerRadius), typeof(LayDrawerHost));

        #endregion

        #region 底部抽屉
        /// <summary>
        /// 底部抽屉开关
        /// </summary>
        public bool BottomDrawerOpen
        {
            get { return (bool)GetValue(BottomDrawerOpenProperty); }
            set { SetValue(BottomDrawerOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftDrawerOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BottomDrawerOpenProperty =
            DependencyProperty.Register("BottomDrawerOpen", typeof(bool), typeof(LayDrawerHost));

        /// <summary>
        /// 底部抽屉内容
        /// </summary>
        public object BottomDrawerContent
        {
            get { return (object)GetValue(BottomDrawerContentProperty); }
            set { SetValue(BottomDrawerContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftDrawerContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BottomDrawerContentProperty =
            DependencyProperty.Register("BottomDrawerContent", typeof(object), typeof(LayDrawerHost));
        /// <summary>
        /// 底部抽屉背景色
        /// </summary>
        public Brush BottomDrawerBackground
        {
            get { return (Brush)GetValue(BottomDrawerBackgroundProperty); }
            set { SetValue(BottomDrawerBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftDrawerBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BottomDrawerBackgroundProperty =
            DependencyProperty.Register("BottomDrawerBackground", typeof(Brush), typeof(LayDrawerHost));
        /// <summary>
        /// 底部抽屉边框线颜色
        /// </summary>
        public Brush BottomDrawerBorderBrush
        {
            get { return (Brush)GetValue(BottomDrawerBorderBrushProperty); }
            set { SetValue(BottomDrawerBorderBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftDrawerBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BottomDrawerBorderBrushProperty =
            DependencyProperty.Register("BottomDrawerBorderBrush", typeof(Brush), typeof(LayDrawerHost));


        /// <summary>
        /// 底部抽屉边框线
        /// </summary>
        public Thickness BottomDrawerThickness
        {
            get { return (Thickness)GetValue(BottomDrawerThicknessProperty); }
            set { SetValue(BottomDrawerThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftDrawerThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BottomDrawerThicknessProperty =
            DependencyProperty.Register("BottomDrawerThickness", typeof(Thickness), typeof(LayDrawerHost));


        /// <summary>
        /// 底部抽屉圆角
        /// </summary>
        public CornerRadius BottomDrawerCornerRadius
        {
            get { return (CornerRadius)GetValue(BottomDrawerCornerRadiusProperty); }
            set { SetValue(BottomDrawerCornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftDrawerCornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BottomDrawerCornerRadiusProperty =
            DependencyProperty.Register("BottomDrawerCornerRadius", typeof(CornerRadius), typeof(LayDrawerHost));

        #endregion


        /// <summary>
        /// 遮罩背景色
        /// </summary>
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
                PART_DrawerGrid.MouseLeftButtonDown -= PART_DrawerGrid_MouseLeftButtonDown;
                PART_DrawerGrid.MouseLeftButtonDown += PART_DrawerGrid_MouseLeftButtonDown;
            }
            else PART_DrawerGrid.MouseLeftButtonDown -= PART_DrawerGrid_MouseLeftButtonDown;
        }
        private void PART_DrawerGrid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            LeftDrawerOpen = false;
            RightDrawerOpen = false;
            TopDrawerOpen = false;
            BottomDrawerOpen = false;
            e.Handled = true;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_DrawerGrid = GetTemplateChild("PART_DrawerGrid") as Grid;
            UpdateModelEvent();
        }
    }
}
