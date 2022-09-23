using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
namespace LayuiTemplate.Controls
{
    /// <summary>
    ///  LayKeyboard
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-09-23 上午 10:05:38</para>
    /// </summary>
    [TemplatePart(Name = "PART_KeysRoot")]
    public class LayKeyboard:Control
    {
        private Grid PART_KeysRoot;
        public Style KeyButtonStyle
        {
            get { return (Style)GetValue(KeyButtonStyleProperty); }
            set { SetValue(KeyButtonStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for KeyButtonStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty KeyButtonStyleProperty =
            DependencyProperty.Register("KeyButtonStyle", typeof(Style), typeof(LayKeyboard));


        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(LayKeyboard));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_KeysRoot = GetTemplateChild("PART_KeysRoot") as Grid;
            if (PART_KeysRoot != null) {
            }
        }
    }
}
