using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace LayuiTemplate.Control
{
    /// <summary>
    ///  消息提示
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-05-24 上午 10:23:07</para>
    /// </summary>
    public class LaySnackbarControl : System.Windows.Controls.Control
    {
        /// <summary>
        /// 激活控件
        /// </summary>
        internal bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsActive.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof(bool), typeof(LaySnackbarControl), new PropertyMetadata(false));

        /// <summary>
        /// 内容
        /// </summary>
        internal object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(LaySnackbarControl), new PropertyMetadata(null));


        /// <summary>
        /// 显示动画
        /// </summary>
        public void Show(double time)
        {
            if (IsActive) return;
            IsActive = true;
            /////////位移///////
            ThicknessAnimation thickness = new ThicknessAnimation();
            /////////透明度///////
            DoubleAnimation doubleAnimation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.2));
            switch (VerticalAlignment)
            {
                case VerticalAlignment.Top:
                    thickness.From = new Thickness(0, -(this.ActualHeight), 0, this.ActualHeight);
                    thickness.To = new Thickness(0, 0, 0, 0);
                    thickness.Duration = TimeSpan.FromSeconds(0.2);
                    break;
                case VerticalAlignment.Bottom:
                    thickness.From = new Thickness(0, this.ActualHeight, 0, -(this.ActualHeight));
                    thickness.To = new Thickness(0, 0, 0, 0);
                    thickness.Duration = TimeSpan.FromSeconds(time);
                    break;
                default:
                    break;
            }
            this.BeginAnimation(MarginProperty, thickness);
            this.BeginAnimation(OpacityProperty, doubleAnimation);
        }

        /// <summary>
        /// 关闭动画
        /// </summary>
        public void Closed(double time)
        {
            if (!IsActive) return;
            IsActive = false;
            /////////位移///////
            ThicknessAnimation thickness = new ThicknessAnimation();
            /////////透明度///////
            DoubleAnimation doubleAnimation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.2));
            switch (VerticalAlignment)
            {
                case VerticalAlignment.Top:
                    thickness.From = new Thickness(0, 0, 0, 0);
                    thickness.To = new Thickness(0, -(this.ActualHeight), 0, this.ActualHeight);
                    thickness.Duration = TimeSpan.FromSeconds(0.2);
                    break;
                case VerticalAlignment.Bottom:
                    thickness.From = new Thickness(0, 0, 0, 0);
                    thickness.To = new Thickness(0, this.ActualHeight, 0, -(this.ActualHeight));
                    thickness.Duration = TimeSpan.FromSeconds(time);
                    break;
                default:
                    break;
            }
            this.BeginAnimation(MarginProperty, thickness);
            this.BeginAnimation(OpacityProperty, doubleAnimation);
        }
    }
}
