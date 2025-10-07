using LayUI.Wpf.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
        public decimal Value
        {
            get { return (decimal)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(decimal), typeof(LayNumericUpDown), new FrameworkPropertyMetadata(OnValueChanged));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LayNumericUpDown layNumericUp = d as LayNumericUpDown;
            if (!layNumericUp.IsInitialized) return;
            layNumericUp.OnValueChanged();
        }
        private void OnValueChanged()
        {
            Refresh();
            if (IsLoaded) OnValueChanged(new RoutedEventArgs(ValueChangedEvent, this));
        }
        /// <summary>
        /// 刷新
        /// </summary>
        private void Refresh()
        {
            if (PART_LowerBtn == null || PART_AddBtn == null || PART_ValueHost == null) return;
            PART_LowerBtn.IsEnabled = true;
            PART_AddBtn.IsEnabled = true;
            if (Value <= MinValue)
            {
                PART_LowerBtn.IsEnabled = false;
            }
            if (Value >= MaxValue)
            {
                PART_AddBtn.IsEnabled = false;
            }
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
        public decimal MinValue
        {
            get { return (decimal)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof(decimal), typeof(LayNumericUpDown), new PropertyMetadata(OnMinValueChanged));

        private static void OnMinValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LayNumericUpDown numericUpDown && numericUpDown.IsInitialized) numericUpDown.OnMinValueChanged((decimal)e.NewValue);
        }

        private void OnMinValueChanged(decimal value)
        {
            PART_LowerBtn.IsEnabled = true;
            if (Value <= value)
            {
                Value = value;
                PART_LowerBtn.IsEnabled = false;
            }
        }
        /// <summary>
        /// 最大值
        /// </summary>
        [Bindable(true)]
        public decimal MaxValue
        {
            get { return (decimal)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(decimal), typeof(LayNumericUpDown), new PropertyMetadata(decimal.Parse("100.0"), OnMaxValueChanged));

        private static void OnMaxValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LayNumericUpDown numericUpDown && numericUpDown.IsInitialized) numericUpDown.OnMaxValueChanged((decimal)e.NewValue);
        }

        private void OnMaxValueChanged(decimal value)
        {
            PART_AddBtn.IsEnabled = true;
            if (Value >= value)
            {
                PART_AddBtn.IsEnabled = false;
                Value = value;
            }
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
        public decimal Increment
        {
            get { return (decimal)GetValue(IncrementProperty); }
            set { SetValue(IncrementProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Increment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IncrementProperty =
            DependencyProperty.Register("Increment", typeof(decimal), typeof(LayNumericUpDown), new PropertyMetadata(decimal.Parse("1")));
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
            DependencyProperty.Register("ValueFormat", typeof(string), typeof(LayNumericUpDown));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_ValueHost = GetTemplateChild("PART_ValueHost") as TextBox;
            PART_AddBtn = GetTemplateChild("PART_AddBtn") as Button;
            PART_LowerBtn = GetTemplateChild("PART_LowerBtn") as Button;
            if (PART_ValueHost != null && PART_AddBtn != null && PART_LowerBtn != null)
            {
                MultiBinding binding = new MultiBinding();
                binding.Bindings.Add(new Binding()
                {
                    Mode = BindingMode.OneWay,
                    Source = this,
                    Path = new PropertyPath(ValueProperty.Name)
                });
                binding.Bindings.Add(new Binding()
                {
                    Mode = BindingMode.OneWay,
                    Source = this,
                    Path = new PropertyPath(ValueFormatProperty.Name)
                });
                binding.Converter = new DecimalToStringFormatConverter();
                PART_ValueHost.SetBinding(TextBox.TextProperty, binding);
                PART_ValueHost.TextChanged -= PART_ValueHost_TextChanged;
                PART_ValueHost.TextChanged += PART_ValueHost_TextChanged;
                PART_ValueHost.PreviewKeyDown -= PART_ValueHost_PreviewKeyDown;
                PART_ValueHost.PreviewKeyDown += PART_ValueHost_PreviewKeyDown;
                PART_ValueHost.LostFocus -= PART_ValueHost_LostFocus;
                PART_ValueHost.LostFocus += PART_ValueHost_LostFocus;
                PART_AddBtn.Click -= PART_AddBtn_Click;
                PART_LowerBtn.Click -= PART_LowerBtn_Click;
                PART_AddBtn.Click += PART_AddBtn_Click;
                PART_LowerBtn.Click += PART_LowerBtn_Click;
                OnMinValueChanged(MinValue);
                OnMaxValueChanged(MaxValue);
            }
        }

        /// <summary>
        /// 输入前验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PART_ValueHost_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var textBox = (TextBox)sender;
            string currentText = textBox.Text;

            // 允许的按键：主键盘数字、小键盘数字、退格、小数点、左右方向键、负号
            bool isMainDigit = e.Key >= Key.D0 && e.Key <= Key.D9;
            bool isNumpadDigit = e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9;
            bool isBackspace = e.Key == Key.Back;
            bool isDecimal = e.Key == Key.Decimal || e.Key == Key.OemPeriod;
            bool isOemMinus = e.Key == Key.OemMinus;
            bool isRight = e.Key == Key.Right;
            bool isLeft = e.Key == Key.Left;

            // 禁止输入多个小数点
            if (isDecimal && currentText.Contains('.'))
            {
                e.Handled = true;
                return;
            }
            //禁止输入多个减号
            if (isOemMinus && currentText.Contains('-'))
            {
                e.Handled = true;
                return;
            }

            // 允许数字、退格、小数点输入
            if (!(isMainDigit || isNumpadDigit || isBackspace || isDecimal || isOemMinus || isRight || isLeft))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 鼠标滚轮控制加减数字
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            if (PART_ValueHost != null)
            {
                if (PART_ValueHost.IsFocused && !IsReadOnly)
                {
                    if (Value < MaxValue && e.Delta > 0)
                    {
                        var value = Value + Increment;
                        if (value < MaxValue) Value = value;
                        else Value = MaxValue;
                    }
                    if (Value > MinValue && e.Delta < 0)
                    {
                        var value = Value - Increment;
                        if (value > MinValue) Value = value;
                        else Value = MinValue;
                    }
                    PART_ValueHost?.SelectAll();
                    e.Handled = true;
                }
            }
        }
        private int index = 0;
        private void PART_ValueHost_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var textBox = e.OriginalSource as TextBox;
                decimal value = 0;
                bool isDouble = decimal.TryParse(textBox.Text, out value);
                if (Value == value) return;
                index = textBox.CaretIndex;
                if (isDouble) Value = value;
                textBox.CaretIndex = index;
            }
            catch
            {

            }
        }

        private void PART_ValueHost_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (decimal.TryParse(textBox.Text, out decimal value))
                {
                    if (value < MinValue)
                    {
                        Value = MinValue;
                    }
                    else if (value > MaxValue)
                    {
                        Value = MaxValue;
                    }
                }
            }
        }

        private void PART_AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Value += Increment;
            PART_ValueHost?.Focus();
            PART_ValueHost?.SelectAll();
        }

        private void PART_LowerBtn_Click(object sender, RoutedEventArgs e)
        {
            Value -= Increment;
            PART_ValueHost?.Focus();
            PART_ValueHost?.SelectAll();
        }
    }
}
