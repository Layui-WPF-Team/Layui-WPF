using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace LayUI.Wpf.Controls
{
    [TemplatePart(Name = nameof(PART_ItemsControl), Type = typeof(ItemsControl))]
    public class LayTagTextBox : Control, ILayControl
    {
        private ItemsControl PART_ItemsControl;
        private LayTextBox PART_TextBox;
        public LayTagTextBox()
        {
            PART_TextBox = new LayTextBox();
            ItemsSoure = new ObservableCollection<object>();
            Tags = new ObservableCollection<string>();
        }
        public Style TextBoxStyle
        {
            get { return (Style)GetValue(TextBoxStyleProperty); }
            set { SetValue(TextBoxStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBoxStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBoxStyleProperty =
            DependencyProperty.Register("TextBoxStyle", typeof(Style), typeof(LayTagTextBox), new PropertyMetadata(OnTextBoxStyleChanged));
        /// <summary>
        /// 水印
        /// </summary>
        [Bindable(true)]
        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }
        // Using a DependencyProperty as the backing store for Watermark.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.Register("Watermark", typeof(string), typeof(LayTagTextBox), new PropertyMetadata(OnWatermarkChanged));

        private static void OnWatermarkChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var tagTextBox = d as LayTagTextBox;
            tagTextBox.PART_TextBox.Watermark = e.NewValue as string;
        }
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(LayTagTextBox));


        public bool IsTextBoxFocused
        {
            get { return (bool)GetValue(IsTextBoxFocusedProperty); }
            private set { SetValue(IsTextBoxFocusedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsTextBoxFocused.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsTextBoxFocusedProperty =
            DependencyProperty.Register("IsTextBoxFocused", typeof(bool), typeof(LayTagTextBox));


        private static void OnTextBoxStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var tagTextBox = d as LayTagTextBox;
            tagTextBox.PART_TextBox.Style = e.NewValue as Style;
        }

        public IList<string> Tags
        {
            get { return (IList<string>)GetValue(TagsProperty); }
            set
            {
                SetValue(TagsProperty, value);
            }
        }
        // Using a DependencyProperty as the backing store for Tags.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TagsProperty =
            DependencyProperty.Register("Tags", typeof(IList<string>), typeof(LayTagTextBox), new PropertyMetadata(OnTagsChanged));

        private static void OnTagsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as LayTagTextBox).OnTagsChanged(e);

        }
        private void OnTagsChanged(DependencyPropertyChangedEventArgs e)
        {
            var newTags = e.NewValue as IList<string>;
            var oldTags = e.OldValue as IList<string>;
            if (!(ItemsSoure is IList list))
                return;
            for (int i = 0; i < list.Count - 1; i++)
            {
                list.RemoveAt(list.Count - 1);
            }

            for (int i = 0; i < newTags.Count; i++)
            {
                list.Add(newTags[i]);
            }

            if (oldTags is INotifyCollectionChanged inccold)
            {
                inccold.CollectionChanged -= OnCollectionChanged;
            }

            if (Tags is INotifyCollectionChanged incc)
            {
                incc.CollectionChanged += OnCollectionChanged;
            }
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var items = e.NewItems;
                int index = e.NewStartingIndex;
                foreach (var item in items)
                {
                    if (item is string s)
                    {
                        if (!(ItemsSoure is IList list)) continue;
                        list.Insert(index, s);
                        index++;
                    }
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                var items = e.OldItems;
                int index = e.OldStartingIndex;
                foreach (var item in items)
                {
                    if (item is string s)
                    {
                        if (!(ItemsSoure is IList list)) continue;
                        list.RemoveAt(index);
                    }
                }
            }
        }

        /// <summary>
        /// 允许重复
        /// </summary>
        public bool AllowDuplicates
        {
            get { return (bool)GetValue(AllowDuplicatesProperty); }
            set { SetValue(AllowDuplicatesProperty, value); }
        }
        // Using a DependencyProperty as the backing store for AllowDuplicates.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AllowDuplicatesProperty =
            DependencyProperty.Register("AllowDuplicates", typeof(bool), typeof(LayTagTextBox));
        /// <summary>
        /// 分割线
        /// </summary>
        public string Separator
        {
            get { return (string)GetValue(SeparatorProperty); }
            set { SetValue(SeparatorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Separator.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SeparatorProperty =
            DependencyProperty.Register("Separator", typeof(string), typeof(LayTagTextBox));
        public DataTemplate TagTemplate
        {
            get { return (DataTemplate)GetValue(TagTemplateProperty); }
            set { SetValue(TagTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TagTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TagTemplateProperty =
            DependencyProperty.Register("TagTemplate", typeof(DataTemplate), typeof(LayTagTextBox));
        internal IList ItemsSoure
        {
            get { return (IList)GetValue(ItemsSoureProperty); }
            private set { SetValue(ItemsSoureProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemsSoure.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty ItemsSoureProperty =
            DependencyProperty.Register("ItemsSoure", typeof(IList), typeof(LayTagTextBox));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_ItemsControl = GetTemplateChild(nameof(PART_ItemsControl)) as ItemsControl;
            if (PART_ItemsControl != null)
            {
                if (ItemsSoure is IList list) list.Add(PART_TextBox);
                PART_TextBox.GotFocus -= PART_TextBox_GotFocus;
                PART_TextBox.GotFocus += PART_TextBox_GotFocus;
                PART_TextBox.LostFocus -= PART_TextBox_LostFocus;
                PART_TextBox.LostFocus += PART_TextBox_LostFocus;
            }
        }

        private void PART_TextBox_LostFocus(object sender, RoutedEventArgs e) => IsTextBoxFocused = false;

        private void PART_TextBox_GotFocus(object sender, RoutedEventArgs e) => IsTextBoxFocused = true;

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);
            if (e.Key == Key.Enter)
            {
                if (PART_TextBox.Text?.Length > 0)
                {
                    string[] values;
                    if (!string.IsNullOrEmpty(this.Separator))
                        values = PART_TextBox.Text.Split(new string[] { this.Separator }, StringSplitOptions.RemoveEmptyEntries);
                    else
                        values = new[] { PART_TextBox.Text };
                    if (!AllowDuplicates)
                        values = values.Distinct().Except(Tags).ToArray();
                    for (int i = 0; i < values.Length; i++)
                    {
                        if (!(ItemsSoure is IList list))
                            continue;
                        int index = list.Count - 1;
                        if (Tags is IList<string> tags) tags.Insert(index, values[i]);
                    }
                    PART_TextBox.Text = "";
                }
            }
            else if (e.Key == Key.Delete || e.Key == Key.Back)
            {
                if ((PART_TextBox.Text?.Length) == 0)
                {
                    if (Tags is IList<string> tags)
                    {
                        if (tags.Count != 0)
                        {
                            if (ItemsSoure is IList list)
                            {
                                int index = list.Count - 2;
                                tags.RemoveAt(index);
                            }
                        }
                    }
                }
            }
        }
    }
}
