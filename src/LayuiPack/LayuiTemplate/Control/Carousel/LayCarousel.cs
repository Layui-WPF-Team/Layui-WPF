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
using System.Windows.Markup;

namespace LayuiTemplate.Control
{
    /// <summary>
    ///  LayCarousel
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-06-07 下午 4:12:39</para>
    /// </summary>
    [DefaultProperty("Items")]
    [ContentProperty("Items")]
    public class LayCarousel : System.Windows.Controls.Control
    {
        /// <summary>
        /// 图片容器
        /// </summary>
        private Panel ItemsHost;
        public LayCarousel()
        {
            var items = new ObservableCollection<object>();
            items.CollectionChanged += Value_CollectionChanged;
            Items = items;
            Loaded += LayCarousel_Loaded;
        }

        private void LayCarousel_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Bindable(true)]
        public Collection<object> Items { get; set; }

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(LayCarousel));


        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register("SelectedIndex", typeof(int), typeof(LayCarousel), new PropertyMetadata(0));



        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(LayCarousel));
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(LayCarousel), new PropertyMetadata(default(IEnumerable), OnItemsSourceChanged));

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LayCarousel carousel = d as LayCarousel;
            if (carousel.ItemsSource is INotifyCollectionChanged value)
            {
                value.CollectionChanged -= carousel.Value_CollectionChanged;
                value.CollectionChanged += carousel.Value_CollectionChanged;
            }
        }

        private void Value_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Refresh();
        }
        /// <summary>
        /// 刷新视图
        /// </summary>
        protected virtual void Refresh()
        {
            if (!IsLoaded) return;
            ItemsHost?.Children.Clear();
            if (ItemsHost != null)
            {
                if (ItemTemplate != null)
                {
                    if (ItemsSource != null)
                    {
                        foreach (var item in ItemsSource)
                        {
                            var content = new ContentControl()
                            {
                                Content = ItemTemplate.LoadContent(),
                                DataContext = item
                            };
                            ItemsHost.Children.Add(content);
                        }
                    }
                }
                foreach (var item in Items)
                {
                    var content = new ContentControl()
                    {
                        Content = item,
                    };
                    ItemsHost.Children.Add(content);
                }
            }
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ItemsHost = GetTemplateChild("PART_ItemsHost") as Panel;
        }
    }
}
