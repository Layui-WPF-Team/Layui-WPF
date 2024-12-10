using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using LayUI.Wpf.Extend;

namespace LayUI.Wpf.Controls
{
    [TemplatePart(Name = nameof(PART_Image), Type = typeof(System.Windows.Controls.Image))]
    public class LayImage : Control
    {
        private System.Windows.Controls.Image PART_Image;
        public BitmapImage Bitmap
        {
            get { return (BitmapImage)GetValue(BitmapProperty); }
            set { SetValue(BitmapProperty, value); }
        }

        // Bitmap 依赖属性
        public static readonly DependencyProperty BitmapProperty =
            DependencyProperty.Register("Bitmap", typeof(BitmapImage), typeof(LayImage));
        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            private set { SetValue(IsLoadingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsLoading.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty IsLoadingProperty =
            DependencyProperty.Register("IsLoading", typeof(bool), typeof(LayImage), new PropertyMetadata(true));

        // Source 依赖属性
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register(nameof(Source), typeof(string), typeof(LayImage),
                new PropertyMetadata(null, OnSourceChanged));
        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        // Source 属性改变事件处理
        private static async void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (LayImage)d;
            await control.LoadImageAsync((string)e.NewValue);
        }

        // Stretch 依赖属性
        public Stretch Stretch
        {
            get { return (Stretch)GetValue(StretchProperty); }
            set { SetValue(StretchProperty, value); }
        }

        public static readonly DependencyProperty StretchProperty =
            DependencyProperty.Register(nameof(Stretch), typeof(Stretch), typeof(LayImage), new PropertyMetadata(Stretch.Uniform));

        // 获取模板部件
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_Image = GetTemplateChild(nameof(PART_Image)) as System.Windows.Controls.Image;
        }
        private async Task LoadImageAsync(string imagePath)
        {
            IsLoading = true; // 设置加载状态为正在加载
            try
            {
                // 检查输入路径是否为空或仅包含空白字符
                if (string.IsNullOrWhiteSpace(imagePath))
                {
                    ClearBitmap(); // 清除现有位图
                    return; // 结束方法
                }

                // 尝试加载网络图片
                if (Uri.TryCreate(imagePath, UriKind.Absolute, out Uri uriResult) &&
                    (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
                {
                    // 异步加载网络图片
                    Bitmap = await LayImageHelper.LoadFromUrlAsync(imagePath);
                }
                // 尝试加载本地图片
                else if (File.Exists(imagePath))
                {
                    // 异步加载本地图片
                    Bitmap = await LayImageHelper.LoadFromFileAsync(imagePath);
                }
                // 尝试加载嵌入资源图片
                else
                {
                    // 异步加载嵌入资源图片
                    Bitmap = await LayImageHelper.LoadFromResourceAsync(imagePath);
                }
            }
            catch
            {
                ClearBitmap(); // 出现错误时清除位图
            }
            finally
            {
                IsLoading = false; // 重置加载状态
            }
        }
         
        // 清理Bitmap
        private void ClearBitmap()
        {
            if (Bitmap != null) Bitmap = null;
        }
    }
}
