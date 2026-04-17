
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace LayUI.Wpf.Controls
{
    /// <summary>
    /// 密码框
    /// </summary>
    [TemplatePart(Name = "PART_PasswordBox", Type = typeof(PasswordBox))]
    [TemplatePart(Name = "PART_ToggleButton", Type = typeof(ToggleButton))]
    public class LayPasswordBox : LayTextBox
    {
        private ToggleButton PART_ToggleButton;
        private PasswordBox PART_PasswordBox;//用于存储模板中抓取的密码框
        // 防止 Text 和 Password 相互赋值时重复触发同步。
        private bool _isSynchronizing;
        /// <summary>
        ///密码框字符串暗码
        /// </summary>
        [Bindable(true)]
        public char PasswordChar
        {
            get { return (char)GetValue(PasswordCharProperty); }
            set { SetValue(PasswordCharProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PasswordChar.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordCharProperty =
            DependencyProperty.Register("PasswordChar", typeof(char), typeof(LayPasswordBox));


        [Bindable(true)]
        public bool IsShowPasswrod
        {
            get { return (bool)GetValue(IsShowPasswrodProperty); }
            set { SetValue(IsShowPasswrodProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsShowPasswrod.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsShowPasswrodProperty =
            DependencyProperty.Register("IsShowPasswrod", typeof(bool), typeof(LayPasswordBox), new PropertyMetadata(false));


        /// <summary>
        /// 初始化模板
        /// </summary>
        public override void OnApplyTemplate()
        {
            // 模板可能会被重复应用，先解绑旧模板上的事件。
            UnhookTemplateEvents();
            base.OnApplyTemplate();

            // 从模板中获取密码框和显隐切换按钮。
            PART_PasswordBox = GetTemplateChild("PART_PasswordBox") as PasswordBox;
            PART_ToggleButton = GetTemplateChild("PART_ToggleButton") as ToggleButton;

            // 重新绑定新模板上的事件，并同步当前 Text 值。
            HookTemplateEvents();
            SyncPasswordBoxFromText();
            UpdateEditorFocus();
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);

            // 如果这次变化本身就来自 PasswordBox，同步到这里即可，避免死循环。
            if (_isSynchronizing)
            {
                return;
            }

            // 外部通过绑定或代码修改 Text 时，需要同步到真实的 PasswordBox。
            SyncPasswordBoxFromText();
        }

        private void HookTemplateEvents()
        {
            if (PART_PasswordBox != null)
            {
                // 监听真实密码框输入，用来回写 Text。
                PART_PasswordBox.PasswordChanged += OnPasswordBoxPasswordChanged;
            }

            if (PART_ToggleButton != null)
            {
                // 监听显隐切换，切换后顺手调整输入焦点。
                PART_ToggleButton.Checked += OnToggleButtonCheckedChanged;
                PART_ToggleButton.Unchecked += OnToggleButtonCheckedChanged;
            }
        }

        private void UnhookTemplateEvents()
        {
            if (PART_PasswordBox != null)
            {
                PART_PasswordBox.PasswordChanged -= OnPasswordBoxPasswordChanged;
            }

            if (PART_ToggleButton != null)
            {
                PART_ToggleButton.Checked -= OnToggleButtonCheckedChanged;
                PART_ToggleButton.Unchecked -= OnToggleButtonCheckedChanged;
            }
        }

        private void OnPasswordBoxPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (_isSynchronizing || PART_PasswordBox == null)
            {
                return;
            }

            // PasswordBox 不能像 TextBox 那样直接绑定，这里手动同步到 Text。
            _isSynchronizing = true;
            try
            {
                SetCurrentValue(TextProperty, PART_PasswordBox.Password);
                CaretIndex = Text?.Length ?? 0;
            }
            finally
            {
                _isSynchronizing = false;
            }
        }

        private void OnToggleButtonCheckedChanged(object sender, RoutedEventArgs e)
        {
            UpdateEditorFocus();
        }

        private void SyncPasswordBoxFromText()
        {
            if (PART_PasswordBox == null)
            {
                return;
            }

            string text = Text ?? string.Empty;
            if (PART_PasswordBox.Password == text)
            {
                return;
            }

            // 这里处理从 Text 反向同步到内部 PasswordBox 的场景。
            _isSynchronizing = true;
            try
            {
                PART_PasswordBox.Password = text;
            }
            finally
            {
                _isSynchronizing = false;
            }
        }

        private void UpdateEditorFocus()
        {
            if (!IsKeyboardFocusWithin)
            {
                return;
            }

            if (IsShowPasswrod)
            {
                // 明文模式下，焦点落在当前继承自 TextBox 的编辑区。
                Focus();
                CaretIndex = Text?.Length ?? 0;
                Select(CaretIndex, 0);
                return;
            }

            // 密文模式下，焦点切回模板中的真实 PasswordBox。
            PART_PasswordBox?.Focus();
        }

    }
}
