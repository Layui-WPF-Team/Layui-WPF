using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LayuiTemplate.Control
{
    public class LayDateTimePicker : System.Windows.Controls.Control
    {
        /// <summary>
        /// 时
        /// </summary>
        private ListBox PART_Hour;
        private List<string> I_Hour = new List<string>();
        /// <summary>
        /// 分
        /// </summary>
        private ListBox PART_Minute;
        private List<string> I_Minute = new List<string>();
        /// <summary>
        /// 秒
        /// </summary>
        private ListBox PART_Second;
        private List<string> I_Second = new List<string>();
        /// <summary>
        /// 文本
        /// </summary>
        private TextBox PART_TextBox;

        /// <summary>
        /// 内容
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(LayDateTimePicker));
        /// <summary>
        /// 水印
        /// </summary>
        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Watermark.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.Register("Watermark", typeof(string), typeof(LayDateTimePicker));



        public bool IsDropDownOpen
        {
            get { return (bool)GetValue(IsDropDownOpenProperty); }
            set { SetValue(IsDropDownOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsDropDownOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDropDownOpenProperty =
            DependencyProperty.Register("IsDropDownOpen", typeof(bool), typeof(LayDateTimePicker));



        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(LayDateTimePicker));


        public string DateMessageTitle
        {
            get { return (string)GetValue(DateMessageTitleProperty); }
            set { SetValue(DateMessageTitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DateMessageTitle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DateMessageTitleProperty =
            DependencyProperty.Register("DateMessageTitle", typeof(string), typeof(LayDateTimePicker));



        public double Line
        {
            get { return (double)GetValue(LineProperty); }
            set { SetValue(LineProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Line.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineProperty =
            DependencyProperty.Register("Line", typeof(double), typeof(LayDateTimePicker));

        /// <summary>
        /// 刷新数据源
        /// </summary>
        private void SetDate()
        {
            I_Hour.Clear();
            I_Minute.Clear();
            I_Second.Clear();
            for (int i = 0; i <= 23; i++)
            {
                I_Hour.Add($"{i}");
            }
            for (int i = 0; i <= 59; i++)
            {
                I_Minute.Add($"{i}");
            }
            for (int i = 0; i <= 59; i++)
            {
                I_Second.Add($"{i}");
            }
            PART_Hour.ItemsSource = I_Hour;
            PART_Minute.ItemsSource = I_Minute;
            PART_Second.ItemsSource = I_Second;
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_Hour = GetTemplateChild("PART_Hour") as ListBox;
            PART_Minute = GetTemplateChild("PART_Minute") as ListBox;
            PART_Second = GetTemplateChild("PART_Second") as ListBox;
            PART_TextBox = GetTemplateChild("PART_TextBox") as TextBox;
            if (PART_Hour != null && PART_Minute != null && PART_Second != null&& PART_TextBox!=null)
            {
                SetDate();
            }
        }

    }
}
