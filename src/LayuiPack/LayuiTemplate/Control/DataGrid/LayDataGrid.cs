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

namespace LayuiTemplate.Control
{
    public class LayDataGrid : DataGrid
    {

        [Bindable(true)]
        public Style ElementCheckBoxColumnStyle
        {
            get { return (Style)GetValue(ElementCheckBoxColumnStyleProperty); }
            set { SetValue(ElementCheckBoxColumnStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EditingComboBoxColumnStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ElementCheckBoxColumnStyleProperty =
            DependencyProperty.Register("ElementCheckBoxColumnStyle", typeof(Style), typeof(LayDataGrid), new PropertyMetadata(default(Style), OnCheckBoxColumnStyleChanged));

        [Bindable(true)]
        public Style EditingCheckBoxColumnStyle
        {
            get { return (Style)GetValue(EditingCheckBoxColumnStyleProperty); }
            set { SetValue(EditingCheckBoxColumnStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EditingCheckBoxColumnStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EditingCheckBoxColumnStyleProperty =
            DependencyProperty.Register("EditingCheckBoxColumnStyle", typeof(Style), typeof(LayDataGrid), new PropertyMetadata(default(Style), OnCheckBoxColumnStyleChanged));

        private static void OnCheckBoxColumnStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LayDataGrid layDataGrid)
            {
                if (e.OldValue == null && e.NewValue != null)
                {
                    layDataGrid.UpdateCheckBoxColumnStyles(layDataGrid);
                }
            }
        }
        /// <summary>
        /// 更新复选框样式
        /// </summary>
        /// <param name="layDataGrid"></param>
        private void UpdateCheckBoxColumnStyles(LayDataGrid layDataGrid)
        {
            var checkBoxColumnStyle = ElementCheckBoxColumnStyle;
            var editingCheckBoxColumnStyle = EditingCheckBoxColumnStyle;

            if (checkBoxColumnStyle != null)
            {
                foreach (var column in layDataGrid.Columns.OfType<DataGridCheckBoxColumn>())
                {
                    var elementStyle = new Style
                    {
                        BasedOn = column.ElementStyle,
                        TargetType = checkBoxColumnStyle.TargetType
                    };

                    foreach (var setter in checkBoxColumnStyle.Setters.OfType<Setter>())
                    {
                        elementStyle.Setters.Add(setter);
                    }

                    column.ElementStyle = elementStyle;
                }
            }

            if (editingCheckBoxColumnStyle != null)
            {
                foreach (var column in layDataGrid.Columns.OfType<DataGridCheckBoxColumn>())
                {
                    var editingElementStyle = new Style
                    {
                        BasedOn = column.EditingElementStyle,
                        TargetType = editingCheckBoxColumnStyle.TargetType
                    };

                    foreach (var setter in editingCheckBoxColumnStyle.Setters.OfType<Setter>())
                    {
                        editingElementStyle.Setters.Add(setter);
                    }

                    column.EditingElementStyle = editingElementStyle;
                }
            }
        }

        [Bindable(true)]
        public Style ElementComboBoxColumnStyle
        {
            get { return (Style)GetValue(ElementComboBoxColumnStyleProperty); }
            set { SetValue(ElementComboBoxColumnStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ElementComboBoxColumnStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ElementComboBoxColumnStyleProperty =
            DependencyProperty.Register("ElementComboBoxColumnStyle", typeof(Style), typeof(LayDataGrid), new PropertyMetadata(default(Style), OnComboBoxColumnStyleChanged));

        [Bindable(true)]
        public Style EditingComboBoxColumnStyle
        {
            get { return (Style)GetValue(EditingComboBoxColumnStyleProperty); }
            set { SetValue(EditingComboBoxColumnStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EditingComboBoxColumnStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EditingComboBoxColumnStyleProperty =
            DependencyProperty.Register("EditingComboBoxColumnStyle", typeof(Style), typeof(LayDataGrid), new PropertyMetadata(default(Style), OnComboBoxColumnStyleChanged));

        private static void OnComboBoxColumnStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LayDataGrid layDataGrid)
            {
                if (e.OldValue == null && e.NewValue != null)
                {
                    //layDataGrid.UpdateComboBoxColumnStyles(layDataGrid);
                }
            }
        }
        /// <summary>
        /// 更新下拉框样式
        /// </summary>
        /// <param name="layDataGrid"></param>
        private void UpdateComboBoxColumnStyles(LayDataGrid layDataGrid)
        {
            var comboBoxColumnStyle = ElementComboBoxColumnStyle;
            var editingComboBoxColumnStyle = EditingComboBoxColumnStyle;

            if (comboBoxColumnStyle != null)
            {
                foreach (var column in layDataGrid.Columns.OfType<DataGridComboBoxColumn>())
                {
                    var elementStyle = new Style
                    {
                        BasedOn = column.ElementStyle,
                        TargetType = comboBoxColumnStyle.TargetType
                    };

                    foreach (var setter in comboBoxColumnStyle.Setters.OfType<Setter>())
                    {
                        elementStyle.Setters.Add(setter);
                    }

                    column.ElementStyle = elementStyle;
                }
            }

            if (editingComboBoxColumnStyle != null)
            {
                foreach (var column in layDataGrid.Columns.OfType<DataGridComboBoxColumn>())
                {
                    var editingElementStyle = new Style
                    {
                        BasedOn = column.EditingElementStyle,
                        TargetType = editingComboBoxColumnStyle.TargetType
                    };

                    foreach (var setter in editingComboBoxColumnStyle.Setters.OfType<Setter>())
                    {
                        editingElementStyle.Setters.Add(setter);
                    }

                    column.EditingElementStyle = editingElementStyle;
                }
            }
        }
        [Bindable(true)]
        public Style ElementTextColumnStyle
        {
            get { return (Style)GetValue(ElementTextColumnStyleProperty); }
            set { SetValue(ElementTextColumnStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ElementTextStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ElementTextColumnStyleProperty =
            DependencyProperty.Register("ElementTextColumnStyle", typeof(Style), typeof(LayDataGrid), new PropertyMetadata(default(Style), OnTextColumnStyleChanged));

        [Bindable(true)]
        public Style EditingTextColumnStyle
        {
            get { return (Style)GetValue(EditingTextColumnStyleProperty); }
            set { SetValue(EditingTextColumnStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EditingTextColumnStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EditingTextColumnStyleProperty =
            DependencyProperty.Register("EditingTextColumnStyle", typeof(Style), typeof(LayDataGrid), new PropertyMetadata(default(Style), OnTextColumnStyleChanged));

        private static void OnTextColumnStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LayDataGrid layDataGrid)
            {
                if (e.OldValue == null && e.NewValue != null)
                {
                    layDataGrid.UpdateTextColumnStyles(layDataGrid);
                }
            }
        }
        /// <summary>
        /// 更新文本样式
        /// </summary>
        /// <param name="grid"></param>
        private void UpdateTextColumnStyles(LayDataGrid grid)
        {
            var textColumnStyle = ElementTextColumnStyle;
            var editingTextColumnStyle = EditingTextColumnStyle;
            if (textColumnStyle != null)
            {
                foreach (var column in grid.Columns.OfType<DataGridTextColumn>())
                {
                    var elementStyle = new Style
                    {
                        BasedOn = column.ElementStyle,
                        TargetType = textColumnStyle.TargetType
                    };

                    foreach (var setter in textColumnStyle.Setters.OfType<Setter>())
                    {
                        elementStyle.Setters.Add(setter);
                    }

                    column.ElementStyle = elementStyle;
                }
            }
            if (editingTextColumnStyle != null)
            {
                foreach (var column in grid.Columns.OfType<DataGridTextColumn>())
                {
                    var editingElementStyle = new Style
                    {
                        BasedOn = column.EditingElementStyle,
                        TargetType = editingTextColumnStyle.TargetType,
                    };

                    foreach (var setter in editingTextColumnStyle.Setters.OfType<Setter>())
                    {
                        editingElementStyle.Setters.Add(setter);
                    }

                    column.EditingElementStyle = editingElementStyle;
                }
            }
        }
        [Bindable(true)]
        public bool IsShowRowsFocusVisual
        {
            get { return (bool)GetValue(IsShowRowsFocusVisualProperty); }
            set { SetValue(IsShowRowsFocusVisualProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsShowRowsFocusVisual.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsShowRowsFocusVisualProperty =
            DependencyProperty.Register("IsShowRowsFocusVisual", typeof(bool), typeof(LayDataGrid), new PropertyMetadata(false));


        /// <summary>
        /// 无数据提示信息
        /// </summary>
        [Bindable(true)]
        public string NoDataTips
        {
            get { return (string)GetValue(NoDataTipsProperty); }
            set { SetValue(NoDataTipsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NoDataTips.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NoDataTipsProperty =
            DependencyProperty.Register("NoDataTips", typeof(string), typeof(LayDataGrid), new PropertyMetadata("暂无数据"));
        /// <summary>
        /// 动画开关
        /// </summary>
        [Bindable(true)]
        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsActive.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof(bool), typeof(LayDataGrid), new PropertyMetadata(false));
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            UpdateTextColumnStyles(this);
            UpdateComboBoxColumnStyles(this);
            UpdateCheckBoxColumnStyles(this);
        }

    }

}
