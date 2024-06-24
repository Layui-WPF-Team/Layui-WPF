using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace LayUI.Wpf.Controls
{
    /// <summary>
    ///  数字加减控件
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-06-14 下午 1:13:06</para>
    /// </summary>
    public class LayNumericUpDown : Control, ILayControl
    {
        private TextBox PART_ValueHost;
        private Button PART_AddBtn;
        private Button PART_LowerBtn;
        /// <summary>
        /// 当前值
        /// </summary>
        [Bindable(true)]
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(LayNumericUpDown), new FrameworkPropertyMetadata(OnValueChanged));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LayNumericUpDown layNumericUp = d as LayNumericUpDown;
            if (!layNumericUp.IsInitialized) return;
            layNumericUp.OnValueChanged();
        }
        private void OnValueChanged()
        {
            Refresh();
            if(IsLoaded)OnValueChanged(new RoutedEventArgs(ValueChangedEvent, this));
        }
        /// <summary>
        /// 刷新
        /// </summary>
        private void Refresh()
        {
            PART_LowerBtn.IsEnabled=true;
            PART_AddBtn.IsEnabled = true;
            if (Value <= MinValue)
            {
                Value = MinValue;
                PART_LowerBtn.IsEnabled = false;
            }
            if (Value >= MaxValue)
            {
                Value = MaxValue;
                PART_AddBtn.IsEnabled = false;
            }
            PART_ValueHost.Text = $"{Value}";  
        }
        /// <summary>
        /// 值改变事件
        /// </summary>
        public event EventHandler<RoutedEventArgs> ValueChanged
        {
            add => AddHandler(ValueChangedEvent, value);
            remove => RemoveHandler(ValueChangedEvent, value);
        }
        /// <summary>
        ///     值改变事件
        /// </summary>
        public static readonly RoutedEvent ValueChangedEvent =
            EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(LayNumericUpDown));


        protected virtual void OnValueChanged(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }
        /// <summary>
        /// 布局方向
        /// </summary>
        [Bindable(true)]
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Orientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(LayNumericUpDown), new PropertyMetadata(Orientation.Horizontal));


        /// <summary>
        /// 最小值
        /// </summary>
        [Bindable(true)]
        public double MinValue
        {
            get { return (double)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof(double), typeof(LayNumericUpDown), new PropertyMetadata(OnMinValueChanged));

        private static void OnMinValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LayNumericUpDown layNumericUp = d as LayNumericUpDown;
            if (!layNumericUp.IsInitialized) return;
            layNumericUp.OnValueChanged();
        }

        /// <summary>
        /// 最大值
        /// </summary>
        [Bindable(true)]
        public double MaxValue
        {
            get { return (double)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(double), typeof(LayNumericUpDown), new PropertyMetadata(100.0, OnMaxValueChanged));

        private static void OnMaxValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LayNumericUpDown layNumericUp = d as LayNumericUpDown;
            if (!layNumericUp.IsInitialized) return;
            layNumericUp.OnValueChanged();
        }

        /// <summary>
        /// 输入框圆角
        /// </summary>
        [Bindable(true)]
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(LayNumericUpDown));
        /// <summary>
        /// 鼠标移入边框色
        /// </summary>
        [Bindable(true)]
        public Brush HoverBorderBrush
        {
            get { return (Brush)GetValue(HoverBorderBrushProperty); }
            set { SetValue(HoverBorderBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HoverBorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HoverBorderBrushProperty =
            DependencyProperty.Register("HoverBorderBrush", typeof(Brush), typeof(LayNumericUpDown), new PropertyMetadata(Brushes.Transparent));


        /// <summary>
        /// 光标聚焦后的边框色
        /// </summary>
        [Bindable(true)]
        public Brush FocusedBorderBrush
        {
            get { return (Brush)GetValue(FocusedBorderBrushProperty); }
            set { SetValue(FocusedBorderBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FocusedBorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FocusedBorderBrushProperty =
            DependencyProperty.Register("FocusedBorderBrush", typeof(Brush), typeof(LayNumericUpDown), new PropertyMetadata(Brushes.Transparent));

        /// <summary>
        /// 分割线
        /// </summary>
        [Bindable(true)]
        public double Line
        {
            get { return (double)GetValue(LineProperty); }
            set { SetValue(LineProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Line.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineProperty =
            DependencyProperty.Register("Line", typeof(double), typeof(LayNumericUpDown));

        /// <summary>
        /// 每单击按钮时增加或减少的数量
        /// </summary>
        [Bindable(true)]
        public double Increment
        {
            get { return (double)GetValue(IncrementProperty); }
            set { SetValue(IncrementProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Increment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IncrementProperty =
            DependencyProperty.Register("Increment", typeof(double), typeof(LayNumericUpDown));
        /// <summary>
        /// 是否只读
        /// </summary>
        [Bindable(true)]
        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsReadOnly.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(LayNumericUpDown), new PropertyMetadata(false));
          
        /// <summary>
        /// 指示要显示的数字的格式，这将会覆盖 <see cref="DecimalPlaces"/> 属性
        /// </summary>
        public string ValueFormat
        {
            get => (string)GetValue(ValueFormatProperty);
            set => SetValue(ValueFormatProperty, value);
        }
        /// <summary>
        /// 指示要显示的数字的格式
        /// </summary>
        public static readonly DependencyProperty ValueFormatProperty =
            DependencyProperty.Register("ValueFormat", typeof(string), typeof(LayNumericUpDown), new PropertyMetadata(default(string)));
        /// <summary>
        ///  指示要显示的小数位数
        /// </summary>
        internal int? DecimalPlaces
        {
            get => (int?)GetValue(DecimalPlacesProperty);
            set => SetValue(DecimalPlacesProperty, value);
        }
        /// <summary>
        ///  指示要显示的小数位数
        /// </summary>
        internal static readonly DependencyProperty DecimalPlacesProperty =
            DependencyProperty.Register("DecimalPlaces", typeof(int?), typeof(LayNumericUpDown), new PropertyMetadata(default(int?)));
        private string CurrentText => string.IsNullOrWhiteSpace(ValueFormat) ? DecimalPlaces.HasValue ? Value.ToString($"#0.{new string('0', DecimalPlaces.Value)}") : Value.ToString() : Value.ToString(ValueFormat);
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_ValueHost = GetTemplateChild("PART_ValueHost") as TextBox;
            PART_AddBtn = GetTemplateChild("PART_AddBtn") as Button;
            PART_LowerBtn = GetTemplateChild("PART_LowerBtn") as Button;
            if (PART_ValueHost != null && PART_AddBtn != null && PART_LowerBtn != null)
            {
                PART_ValueHost.LostFocus -= PART_ValueHost_LostFocus;
                PART_ValueHost.LostFocus += PART_ValueHost_LostFocus;
                PART_ValueHost.PreviewKeyDown -= PART_ValueHost_PreviewKeyDown;
                PART_ValueHost.PreviewKeyDown += PART_ValueHost_PreviewKeyDown;
                PART_ValueHost.TextChanged -= PART_ValueHost_TextChanged;
                PART_ValueHost.TextChanged += PART_ValueHost_TextChanged;
                PART_AddBtn.Click -= PART_AddBtn_Click;
                PART_LowerBtn.Click -= PART_LowerBtn_Click;
                PART_AddBtn.Click += PART_AddBtn_Click;
                PART_LowerBtn.Click += PART_LowerBtn_Click; 
                Refresh();
            }
        }
        /// <summary>
        /// 鼠标滚轮控制加减数字
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            if (PART_ValueHost.IsFocused && !IsReadOnly)
            {
                Value += e.Delta > 0 ? Increment : -Increment;
                e.Handled = true;
            }
        }
        private void PART_ValueHost_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (IsReadOnly) return;
            if (e.Key == Key.Up)
            {
                Value += Increment;
            }
            else if (e.Key == Key.Down)
            {
                Value -= Increment;
            }
        }

        private void PART_ValueHost_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (double.TryParse(PART_ValueHost.Text, out double value))
                {
                    if (value >= MinValue && value <= MaxValue)
                    {
                        SetCurrentValue(ValueProperty, value);
                    }
                } 
            }
            catch
            {

            }
        }
        private void PART_AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Value += Increment;
            PART_ValueHost.Focus();
        }

        private void PART_LowerBtn_Click(object sender, RoutedEventArgs e)
        {
            Value -= Increment;
            PART_ValueHost.Focus();
        }

        private void PART_ValueHost_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty((sender as TextBox).Text)) (sender as TextBox).Text = "0";
            PART_ValueHost.Text = CurrentText;
        }
    }
}
