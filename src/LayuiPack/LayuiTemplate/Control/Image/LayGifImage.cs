using LayuiTemplate.Tools;
using System;
using System.Collections.Generic;
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

namespace LayuiTemplate.Control
{
    /// <summary>
    ///  LayGifImage
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-07-19 上午 9:38:52</para>
    /// </summary>
    public class LayGifImage : System.Windows.Controls.Control
    {
        static LayGifImage()
        {
            VisibilityProperty.OverrideMetadata(typeof(LayGifImage), new PropertyMetadata(default(Visibility), OnVisibilityChanged));
        }

        private static void OnVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LayGifImage layGif = d as LayGifImage;
            layGif.StopAnimate();
            if (e.NewValue != null)
            {
                var v = (Uri)e.NewValue;
                layGif.gifBitmap = LayImageHelper.GetBitmap(layGif.Source);
                layGif.bitmapSource = layGif.GetBitmapSource();
                layGif.PART_Image.Source = layGif.bitmapSource;
                layGif.StartAnimate();
            }
            else
            {
                layGif.PART_Image.Source = null;
            }
        }

        /// <summary>
        /// 图片加载容器
        /// </summary>
        private Image PART_Image;
        /// <summary>
        /// gif动画的System.Drawing.Bitmap
        /// </summary>
        private System.Drawing.Bitmap gifBitmap;
        /// <summary>
        /// 用于显示每一帧的BitmapSource
        /// </summary>
        private BitmapSource bitmapSource;
        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(ImageSource), typeof(LayGifImage), new PropertyMetadata(OnSourceChanged));

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LayGifImage layGif = d as LayGifImage;
            if (layGif.IsLoaded) {
                if(layGif!=null) layGif.StopAnimate();
                layGif.Refresh();
                layGif.StartAnimate();
            } 
        }
        public Stretch Stretch
        {
            get { return (Stretch)GetValue(StretchProperty); }
            set { SetValue(StretchProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Stretch.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StretchProperty =
            DependencyProperty.Register("Stretch", typeof(Stretch), typeof(LayGifImage), new PropertyMetadata(Stretch.Fill));

        private void Refresh()
        {
            try
            {
                if (Source != null)
                {
                    this.gifBitmap = LayImageHelper.GetBitmap(Source);
                    this.bitmapSource = this.GetBitmapSource();
                    this.PART_Image.Source = this.bitmapSource;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private BitmapSource GetBitmapSource()
        {
            IntPtr handle = IntPtr.Zero;
            try
            {
                handle = gifBitmap.GetHbitmap();
                this.bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
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
        private void OnFrameChanged(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
        {
            System.Drawing.ImageAnimator.UpdateFrames(); // 更新到下一帧
            if (PART_Image.Source != null)
            {
                PART_Image.Source.Freeze();
            }

            PART_Image.Source = this.GetBitmapSource();
            this.InvalidateVisual();
        }));
        }
        public void StartAnimate()
        {
            System.Drawing.ImageAnimator.Animate(this.gifBitmap, this.OnFrameChanged);
        }
        public void StopAnimate()
        {
            System.Drawing.ImageAnimator.StopAnimate(this.gifBitmap, this.OnFrameChanged);
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_Image = GetTemplateChild("PART_Image") as Image;
            Refresh();
            var unloaded = new RoutedEventHandler((s, e) =>
            {
                StopAnimate();
            });
            Unloaded += unloaded;
            var loaded = new RoutedEventHandler((s, e) =>
            {
                StartAnimate();
            });
            Loaded += loaded;
        }
    }
}
