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
    public class LayPopupBox : System.Windows.Controls.Control
    {
        private Popup PART_Popup;

        public double VerticalOffset
        {
            get { return (double)GetValue(VerticalOffsetProperty); }
            set { SetValue(VerticalOffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VerticalOffset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VerticalOffsetProperty =
            DependencyProperty.Register("VerticalOffset", typeof(double), typeof(LayPopupBox));

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


        public object Header
        {
            get { return (object)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(object), typeof(LayPopupBox));


        public PlacementMode Placement
        {
            get { return (PlacementMode)GetValue(PlacementProperty); }
            set { SetValue(PlacementProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Placement.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlacementProperty =
            DependencyProperty.Register("Placement", typeof(PlacementMode), typeof(LayPopupBox), new PropertyMetadata(PlacementMode.Bottom));



        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(LayPopupBox));

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
            var PART_ContentBorder = GetTemplateChild("PART_ContentBorder") as FrameworkElement;
            PART_Popup = GetTemplateChild("PART_Popup") as Popup;
            if (PART_ContentBorder != null)
            {
                PART_ContentBorder.PreviewMouseLeftButtonDown -= PART_ContentBorder_MouseLeftButtonDown;
                PART_ContentBorder.PreviewMouseLeftButtonDown += PART_ContentBorder_MouseLeftButtonDown;
            }
        }

        private void PART_ContentBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is Button button) {
                PART_Popup.IsOpen = false;
            }
        }
    }
}
