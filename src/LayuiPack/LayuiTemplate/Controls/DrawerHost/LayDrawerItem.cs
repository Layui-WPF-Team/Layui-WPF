using LayuiTemplate.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LayuiTemplate.Controls
{
    /// <summary>
    ///  LayDrawerItem
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-09-16 下午 5:31:30</para>
    /// </summary>
    [TemplatePart(Name = "PART_DrawerGrid", Type = typeof(Grid))]
    [TemplatePart(Name = "PART_RootGrid", Type = typeof(Grid))]
    [TemplatePart(Name = "PART_DockBorder", Type = typeof(Border))]
    public class LayDrawerItem:ContentControl
    {
        private Grid PART_RootGrid;
        private Grid PART_DrawerGrid;
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
            DependencyProperty.Register("DrawerOpen", typeof(bool), typeof(LayDrawerItem));

        [Bindable(true)]
        public bool IsModal
        {
            get { return (bool)GetValue(IsModalProperty); }
            set { SetValue(IsModalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsModal.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsModalProperty =
            DependencyProperty.Register("IsModal", typeof(bool), typeof(LayDrawerItem), new PropertyMetadata(true, OnIsModalChanged));

        private static void OnIsModalChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LayDrawerItem item)
            {
                if (item.IsLoaded) item.UpdateModelEvent();
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
            DependencyProperty.Register("Type", typeof(DrawerHostStyle), typeof(LayDrawerItem), new PropertyMetadata(DrawerHostStyle.Top));
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
