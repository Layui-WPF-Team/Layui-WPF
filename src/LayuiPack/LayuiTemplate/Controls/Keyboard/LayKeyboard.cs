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
using System.Windows.Input;

namespace LayuiTemplate.Controls
{
    /// <summary>
    ///  自定义虚拟键盘
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-09-23 上午 10:05:38</para>
    /// </summary>
    [TemplatePart(Name = "PART_KeysRoot")]
    public class LayKeyboard : Control
    {
        /// <summary>
        /// 存储键盘控件按钮主容器
        /// </summary>
        private Grid PART_KeysRoot;
        /// <summary>
        /// 默认键盘按钮样式
        /// </summary>
        [Bindable(true)]
        public Style DefaultButtonStyle
        {
            get { return (Style)GetValue(DefaultButtonStyleProperty); }
            set { SetValue(DefaultButtonStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for KeyButtonStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DefaultButtonStyleProperty =
            DependencyProperty.Register("DefaultButtonStyle", typeof(Style), typeof(LayKeyboard));

        /// <summary>
        /// 大小写键盘按钮样式
        /// </summary>
        public Style CapsButtonStyle
        {
            get { return (Style)GetValue(CapsButtonStyleProperty); }
            set { SetValue(CapsButtonStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CapsButtonStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CapsButtonStyleProperty =
            DependencyProperty.Register("CapsButtonStyle", typeof(Style), typeof(LayKeyboard));

        /// <summary>
        /// 圆角
        /// </summary>
        [Bindable(true)]
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(LayKeyboard));


        /// <summary>
        /// 大写锁定
        /// </summary>
        public bool IsCapsLock
        {
            get { return (bool)GetValue(IsCapsLockProperty); }
            set { SetValue(IsCapsLockProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsCapsLock.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCapsLockProperty =
            DependencyProperty.Register("IsCapsLock", typeof(bool), typeof(LayKeyboard), new PropertyMetadata(false));


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_KeysRoot = GetTemplateChild("PART_KeysRoot") as Grid;
            Loaded -= LayKeyboard_Loaded;
            Loaded += LayKeyboard_Loaded;
            Unloaded -= LayKeyboard_Unloaded;
            Unloaded += LayKeyboard_Unloaded;
        }
        /// <summary>
        /// 初始化给添加按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LayKeyboard_Unloaded(object sender, RoutedEventArgs e)
        {
            Unloaded -= LayKeyboard_Unloaded;
            if (PART_KeysRoot != null)
            {
                AddOrRemoveKeyButtonEnevt(false);
            }
        }

        /// <summary>
        /// 关闭时删除按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LayKeyboard_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= LayKeyboard_Loaded;
            if (PART_KeysRoot != null)
            {
                AddOrRemoveKeyButtonEnevt(true);
            }
        }

        /// <summary>
        /// 给模拟键盘按钮新增或删除事件
        /// </summary>
        /// <param name="isAdd"></param>
        private void AddOrRemoveKeyButtonEnevt(bool isAdd)
        {
            var itemsControls = PART_KeysRoot.Children.Cast<ItemsControl>();
            if (itemsControls != null)
            {
                foreach (var itemsControl in itemsControls)
                {
                    var buttonBases = itemsControl.Items.Cast<ButtonBase>();
                    if (buttonBases != null)
                    {
                        foreach (var button in buttonBases)
                        {
                            if (isAdd)
                            {
                                button.Click -= Button_Click;
                                button.Click += Button_Click;
                            }
                            else
                            {
                                button.Click -= Button_Click;
                            }
                        }
                    }

                }
            }
        }
        /// <summary>
        /// 开始进行文本内容填充
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is ButtonBase button)
            {
                if (button.CommandParameter != null && button.CommandParameter is Key key)
                {
                    LayKeyboardKeyHelper.Keyboard_Event(key);
                }
            }
        }
    }
}
