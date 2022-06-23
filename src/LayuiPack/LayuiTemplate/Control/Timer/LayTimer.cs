using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LayuiTemplate.Control
{
    /// <summary>
    ///  LayTimer
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-06-23 下午 1:54:59</para>
    /// </summary>
    public class LayTimer : System.Windows.Controls.Control
    {
        /// <summary>
        /// 时
        /// </summary>
        private ListBox PART_Hour;
        /// <summary>
        /// 分
        /// </summary>
        private ListBox PART_Minute;
        /// <summary>
        /// 秒
        /// </summary>
        private ListBox PART_Second;

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Time
        {
            get { return (DateTime)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("Time", typeof(DateTime), typeof(LayTimer), new PropertyMetadata(OnTimeChanged));

        private static void OnTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view = d as LayTimer;
            
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(LayTimer));


        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(LayTimer));

        public double Line
        {
            get { return (double)GetValue(LineProperty); }
            set { SetValue(LineProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Line.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineProperty =
            DependencyProperty.Register("Line", typeof(double), typeof(LayTimer));

        /// <summary>
        /// 刷新数据源
        /// </summary>
        private void SetDate()
        {
            PART_Hour.Items.Clear();
            PART_Minute.Items.Clear();
            PART_Second.Items.Clear();
            for (int i = 0; i <= 23; i++)
            {
                ListBoxItem item = new ListBoxItem();
                item.Style = PART_Hour.ItemContainerStyle;
                if (i < 10) item.Content = $"0{i}";
                else item.Content = $"{i}";
                PART_Hour.Items.Add(item);
            }
            for (int i = 0; i <= 59; i++)
            {
                ListBoxItem item = new ListBoxItem();
                item.Style = PART_Minute.ItemContainerStyle;
                if (i < 10) item.Content = $"0{i}";
                else item.Content = $"{i}";
                PART_Minute.Items.Add(item);
            }
            for (int i = 0; i <= 59; i++)
            {
                ListBoxItem item = new ListBoxItem();
                item.Style = PART_Second.ItemContainerStyle;
                if (i < 10) item.Content = $"0{i}";
                else item.Content = $"{i}";
                PART_Second.Items.Add(item);
            }
            PART_Hour.SelectedIndex = 0;
            PART_Minute.SelectedIndex = 0;
            PART_Second.SelectedIndex = 0;
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        private void UpdateDate()
        { 
            //$"{(PART_Hour.SelectedItem as ListBoxItem).Content.ToString()}:{(PART_Minute.SelectedItem as ListBoxItem).Content.ToString()}:{(PART_Second.SelectedItem as ListBoxItem).Content.ToString()}";
            
        }
        /// <summary>
        /// 刷新选中项
        /// </summary>
        private void UpdateIsSelectedItem(ListBox list, string value)
        {
            foreach (ListBoxItem item in list.Items)
            {
                if (Convert.ToInt32(item.Content.ToString()) == Convert.ToInt32(value))
                {
                    item.IsSelected = true;
                    list.ScrollIntoView(item);
                }
                else
                {
                    item.IsSelected = false;
                }
            }
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_Hour = GetTemplateChild("PART_Hour") as ListBox;
            PART_Minute = GetTemplateChild("PART_Minute") as ListBox;
            PART_Second = GetTemplateChild("PART_Second") as ListBox;
            if (PART_Hour != null && PART_Minute != null && PART_Second != null)
            {
                SetDate();
                this.PART_Hour.AddHandler(ListBoxItem.MouseLeftButtonDownEvent, new RoutedEventHandler(Button_Click), true);
                this.PART_Minute.AddHandler(ListBoxItem.MouseLeftButtonDownEvent, new RoutedEventHandler(Button_Click), true);
                this.PART_Second.AddHandler(ListBoxItem.MouseLeftButtonDownEvent, new RoutedEventHandler(Button_Click), true);
                UpdateDate();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateDate();
        }
    }
}
