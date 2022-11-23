using LayuiTemplate.Extend;
using LayuiTemplate.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Image = System.Windows.Controls.Image;

namespace LayuiTemplate.Controls
{
    /// <summary>
    ///  LayGifImage
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-07-19 上午 9:38:52</para>
    /// </summary>
    public class LayGifImage : System.Windows.Controls.Control, IDisposable
    {
        private Image PART_Image;
        /// <summary>
        /// gif动画的System.Drawing.Bitmap
        /// </summary>
        private System.Drawing.Bitmap gifBitmap;
        /// <summary>
        /// 用于显示每一帧的BitmapSource
        /// </summary>
        private BitmapSource bitmapSource;
        private bool disposedValue;

        [Bindable(true)]
        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(ImageSource), typeof(LayGifImage), new PropertyMetadata(OnSourceChanged));

        private async static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LayGifImage layGif = d as LayGifImage;
            if (!layGif.IsLoaded) return;
            if (e.NewValue == e.OldValue) return;
            await layGif.RefreshAsync();
        }


        [Bindable(true)]
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(LayGifImage));


        [Bindable(true)]
        public Stretch Stretch
        {
            get { return (Stretch)GetValue(StretchProperty); }
            set { SetValue(StretchProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Stretch.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StretchProperty =
            DependencyProperty.Register("Stretch", typeof(Stretch), typeof(LayGifImage), new PropertyMetadata(Stretch.Fill));

        private Task RefreshAsync()
        {
            return Task.Run(() =>
           {
               if (PART_Image != null)
               {
                   Dispatcher.Invoke(async () =>
                   {
                       //防呆置空
                       if (Source == null)
                       {
                           StopAnimate();
                           gifBitmap?.Dispose();
                           if(gifBitmap!=null) gifBitmap = null;
                           this.PART_Image.Source = null;
                           return;
                       }
                       if (gifBitmap != null)
                       {
                           StopAnimate();
                           gifBitmap?.Dispose();
                       }
                       this.gifBitmap = await LayImageHelper.GetBitmapAsync(Source);
                       this.bitmapSource = this.GetBitmapSource();
                       this.PART_Image.Source = this.bitmapSource;
                       StartAnimate();
                   });
               }
           });

        }
        /// <summary>
        /// 从System.Drawing.Bitmap中获得用于显示的那一帧图像的BitmapSource
        /// </summary>
        /// <returns></returns>
        private BitmapSource GetBitmapSource()
        {
            IntPtr handle = IntPtr.Zero;
            try
            {
                if (gifBitmap != null)
                {
                    handle = this.gifBitmap.GetHbitmap();
                    this.bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

                }
            }
            finally
            {
                if (handle != IntPtr.Zero)
                {
                    LayImageHelper.DeleteObject(handle);
                }
            }
            return this.bitmapSource;
        }

        /// <summary>
        /// Start
        /// </summary>
        public void StartAnimate()
        {
            ImageAnimator.Animate(this.gifBitmap, this.OnFrameChanged);
        }

        /// <summary>
        /// Stop
        /// </summary>
        public void StopAnimate()
        {
            ImageAnimator.StopAnimate(this.gifBitmap, this.OnFrameChanged);
        }

        /// <summary>
        /// 帧处理
        /// </summary>
        private void OnFrameChanged(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
            {
                ImageAnimator.UpdateFrames(); // 更新到下一帧
                if (this.bitmapSource != null)
                {
                    this.bitmapSource.Freeze();
                }
                this.bitmapSource = this.GetBitmapSource();
                this.PART_Image.Source = this.bitmapSource;
                this.InvalidateVisual();
            }));
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_Image = GetTemplateChild("PART_Image") as Image;
            Loaded -= LayGifImage_Loaded;
            Loaded += LayGifImage_Loaded;
            Unloaded -= LayGifImage_Unloaded;
            Unloaded += LayGifImage_Unloaded;
        }

        private void LayGifImage_Unloaded(object sender, RoutedEventArgs e)
        {
            StopAnimate();
        }

        private async void LayGifImage_Loaded(object sender, RoutedEventArgs e)
        {
            await RefreshAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)
                }
                StopAnimate();
                gifBitmap?.Dispose();
                // TODO: 释放未托管的资源(未托管的对象)并重写终结器
                // TODO: 将大型字段设置为 null
                disposedValue = true;
            }
        }

        // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        ~LayGifImage()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
