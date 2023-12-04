using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LayUI.Wpf.Controls
{
    /// <summary>
    /// 文本控件
    /// </summary>
    public class LayTextBlock : TextBlock
    {
        /// <summary>
        /// 文字是否已被裁剪
        /// </summary>
        public bool IsTrimming
        {
            get { return (bool)GetValue(IsTrimmingProperty); }
            private set { SetValue(IsTrimmingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsTrimming.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsTrimmingProperty =
            DependencyProperty.Register("IsTrimming", typeof(bool), typeof(LayTextBlock));

        /// <summary>
        /// 获取当前文字是否已被裁剪
        /// </summary>
        /// <returns></returns>
        private bool GetIsTrimming()
        {
            if (TextTrimming == TextTrimming.None) return false;
            if (TextTrimming == TextTrimming.WordEllipsis)
            {
                var size = MeasureOverride(new Size(DesiredSize.Height, double.MaxValue));
                MeasureOverride(DesiredSize);
                if (size.Height > DesiredSize.Height) return true;
            }
            if (TextTrimming == TextTrimming.CharacterEllipsis)
            {
                var size = MeasureOverride(new Size(double.MaxValue, DesiredSize.Height));
                MeasureOverride(DesiredSize);
                if (size.Width > DesiredSize.Width) return true;
            }
            return false;
        }

        protected override Geometry GetLayoutClip(Size layoutSlotSize)
        {
            IsTrimming = GetIsTrimming();
            return base.GetLayoutClip(layoutSlotSize);
        }
    }
}
