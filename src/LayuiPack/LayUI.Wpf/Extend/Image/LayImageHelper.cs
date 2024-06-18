using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace LayUI.Wpf.Extend
{
    /// <summary>
    ///  LayImageHelper
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-09-13 下午 1:24:37</para>
    /// </summary>
    public class LayImageHelper
    {
        public static bool GetAllowDrop(DependencyObject obj)
        {
            return (bool)obj.GetValue(AllowDropProperty);
        }

        public static void SetAllowDrop(DependencyObject obj, bool value)
        {
            obj.SetValue(AllowDropProperty, value);
        }

        // Using a DependencyProperty as the backing store for AllowDrop.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AllowDropProperty =
            DependencyProperty.RegisterAttached("AllowDrop", typeof(bool), typeof(LayImageHelper), new PropertyMetadata(false, OnAllowDropChanged));

        private static void OnAllowDropChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is System.Windows.Controls.Image image)
            {
                var element = GetAllowDropPanel(image);
                if (element is Panel panel)
                {
                    panel.Tag = image;
                    if (panel.Background == null) panel.Background = System.Windows.Media.Brushes.Transparent;
                    if (GetAllowDrop(image) && panel.AllowDrop)
                    {
                        panel.Drop -= Image_Drop;
                        panel.Drop += Image_Drop;
                    }
                    else
                    {
                        panel.Drop -= Image_Drop;
                    }
                }
                if (element is Border border)
                {
                    border.Tag = image;
                    if (border.Background == null) border.Background = System.Windows.Media.Brushes.Transparent;
                    if (GetAllowDrop(image) && border.AllowDrop)
                    {
                        border.Drop -= Image_Drop;
                        border.Drop += Image_Drop;
                    }
                    else
                    {
                        border.Drop -= Image_Drop;
                    }
                }
            }
        }
        private static FrameworkElement GetAllowDropPanel(FrameworkElement element)
        {
            if (element.Parent == null) return null;
            if (element.Parent is Panel panel)
            {
                if (panel.AllowDrop) return panel;
                else
                {
                    return GetAllowDropPanel(panel);
                }
            }else return GetAllowDropPanel(element.Parent as FrameworkElement);
        }
        private static void Image_Drop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data is IDataObject data)
                {
                    if (!data.GetDataPresent(DataFormats.FileDrop))
                    {
                        return;
                    }
                    string[] files = (string[])data.GetData(DataFormats.FileDrop);
                    if (files.Length < 0) return;
                    ((sender as FrameworkElement).Tag as System.Windows.Controls.Image).Source = GetBitmapImage(new Uri(files[0]));
                }
            }
            catch
            {

            }
        }
        
        /// <summary>
        /// 图片转化
        /// </summary>
        /// <param name="imagePath">图片路径</param>
        /// <returns></returns>
        public static BitmapImage GetBitmapImage(Uri imagePath)
        {
            try
            {
                if (imagePath == null || imagePath.ToString().Trim() == "") return null;
                BitmapImage bitmap = new BitmapImage();
                if (File.Exists(imagePath.LocalPath))
                {
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    using (Stream ms = new MemoryStream(File.ReadAllBytes(imagePath.LocalPath)))
                    {
                        bitmap.StreamSource = ms;
                        bitmap.EndInit();
                        bitmap.Freeze();
                    }
                }
                return bitmap;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 删除本地 bitmap resource
        /// </summary>
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject(IntPtr hObject);
        /// <summary>
        /// ImageSource转Bitmap
        /// </summary>
        /// <param name="Source"></param>
        /// <returns></returns>
        public static async Task<Bitmap> GetBitmapAsync(ImageSource Source)
        {
            try
            {
                if (Source.ToString().ToLower().Contains("pack://siteoforigin:"))
                {
                    Uri uri = new Uri(Source.ToString());
                    var localUri = Environment.CurrentDirectory + uri.LocalPath;
                    return new Bitmap(localUri);
                }
                if (Source.ToString().ToLower().Contains("pack://application:"))
                {
                    Uri uri = new Uri(Source.ToString());
                    return new Bitmap(Application.GetResourceStream(uri).Stream);
                }
                if (Source.ToString().ToLower().Contains("https:") || Source.ToString().ToLower().Contains("http:"))
                {
                    string https = Source.ToString();
                    Stream Stream = (await WebRequest.Create(https).GetResponseAsync()).GetResponseStream();
                    return new Bitmap(Stream);
                }
                string file = Source.ToString(); 
                return new Bitmap(file);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// BitmapImage转Bitmap
        /// </summary>
        /// <param name="bitmapImage"></param>
        /// <returns></returns>
        public static System.Drawing.Bitmap BitmapImageToBitmap(BitmapImage bitmapImage)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                BitmapFrame bitmapFrame = BitmapFrame.Create(bitmapImage);
                enc.Frames.Add(bitmapFrame);
                enc.Save(outStream);
                Bitmap bitmap = new Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }
        /// <summary>
        /// Bitmap转BitmapImage
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="imageFormat">图片格式</param>
        /// <returns></returns>
        public static BitmapImage BitmapToBitmapImage(Bitmap bitmap, ImageFormat imageFormat = null)
        {
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, imageFormat ?? ImageFormat.Png);
            BitmapImage bit3 = new BitmapImage();
            bit3.BeginInit();
            bit3.StreamSource = ms;
            bit3.EndInit();
            return bit3;
        }


        /// <summary>
        /// 截图转换成bitmap
        /// </summary>
        /// <param name="element"></param>
        /// <param name="width">默认控件宽度</param>
        /// <param name="height">默认控件高度</param>
        /// <param name="x">默认0</param>
        /// <param name="y">默认0</param>
        /// <returns></returns>
        public static Bitmap ToBitmap(FrameworkElement element, int width = 0, int height = 0, int x = 0, int y = 0)
        {
            if (width == 0) width = (int)element.ActualWidth;
            if (height == 0) height = (int)element.ActualHeight;

            var rtb = new RenderTargetBitmap(width, height, x, y, System.Windows.Media.PixelFormats.Default);
            rtb.Render(element);
            var bit = BitmapSourceToBitmap(rtb);

            //测试代码
            DirectoryInfo d = new DirectoryInfo(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Cache"));
            if (!d.Exists) d.Create();
            bit.Save(System.IO.Path.Combine(d.FullName, "控件截图.png"));

            return bit;
        }

        /// <summary>
        /// BitmapSource转Bitmap
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Bitmap BitmapSourceToBitmap(BitmapSource source)
        {
            return BitmapSourceToBitmap(source, source.PixelWidth, source.PixelHeight);
        }

        /// <summary>
        /// Convert BitmapSource to Bitmap
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Bitmap BitmapSourceToBitmap(BitmapSource source, int width, int height)
        {
            Bitmap bmp = null;
            try
            {
                System.Drawing.Imaging.PixelFormat format = System.Drawing.Imaging.PixelFormat.Format24bppRgb;
                /*set the translate type according to the in param(source)*/
                switch (source.Format.ToString())
                {
                    case "Rgb24":
                    case "Bgr24": format = System.Drawing.Imaging.PixelFormat.Format24bppRgb; break;
                    case "Bgra32": format = System.Drawing.Imaging.PixelFormat.Format32bppPArgb; break;
                    case "Bgr32": format = System.Drawing.Imaging.PixelFormat.Format32bppRgb; break;
                    case "Pbgra32": format = System.Drawing.Imaging.PixelFormat.Format32bppArgb; break;
                }
                bmp = new Bitmap(width, height, format);
                BitmapData data = bmp.LockBits(new System.Drawing.Rectangle(System.Drawing.Point.Empty, bmp.Size),
                    ImageLockMode.WriteOnly,
                    format);
                source.CopyPixels(Int32Rect.Empty, data.Scan0, data.Height * data.Stride, data.Stride);
                bmp.UnlockBits(data);
            }
            catch
            {
                if (bmp != null)
                {
                    bmp.Dispose();
                    bmp = null;
                }
            }

            return bmp;
        }
    }
}
