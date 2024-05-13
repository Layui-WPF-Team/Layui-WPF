using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LayUI.Wpf.Controls
{

    internal class Property
    {
        /// <summary>
        /// 属性名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 属性值
        /// </summary>
        public object Value { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public object Type { get; set; }

    }

    public class LayPropertyGrid : Control
    {
        public object Element
        {
            get { return (object)GetValue(ElementProperty); }
            set { SetValue(ElementProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Element.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ElementProperty =
            DependencyProperty.Register("Element", typeof(object), typeof(LayPropertyGrid), new PropertyMetadata(OnElementChanged));

        private static void OnElementChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LayPropertyGrid propertyGrid = d as LayPropertyGrid;
            propertyGrid.OnElementChanged();
        }

        internal ObservableCollection<Property> Propertys
        {
            get { return (ObservableCollection<Property>)GetValue(PropertysProperty); }
            set { SetValue(PropertysProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Propertys.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty PropertysProperty =
            DependencyProperty.Register("Propertys", typeof(ObservableCollection<Property>), typeof(LayPropertyGrid));

        private void OnElementChanged()
        {
            Propertys = GetPropertys(Element);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
        private ObservableCollection<Property> GetPropertys(object Element)
        {
            ObservableCollection<Property> propertys;

            try
            {
                propertys = new ObservableCollection<Property>();
                if(Element==null) return propertys;
                Type type = Element.GetType();
                foreach (var property in type.GetProperties())
                {
                    Property data = new Property() { Name = property.Name, Value = property.GetValue(Element),Type = property.GetValue(Element)?.GetType()}; 
                    propertys.Add(data);
                }
            }
            catch
            {
                propertys = new ObservableCollection<Property>();
            }
            return propertys;
        }
    }
}
