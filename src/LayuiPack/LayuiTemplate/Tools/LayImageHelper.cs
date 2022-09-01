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
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LayuiTemplate.Tools
{
    /// <summary>
    ///  LayImageHelper
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-07-19 下午 2:10:58</para>
    /// </summary>
    public class LayImageHelper
    {
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
                if (Source.ToString().ToLower().Contains("pack://siteoforigin:") || Source.ToString().ToLower().Contains("pack://application:"))
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
        public BitmapImage BitmapToBitmapImage(Bitmap bitmap, ImageFormat imageFormat= null)
        {
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, imageFormat?? ImageFormat.Png);
            BitmapImage bit3 = new BitmapImage();
            bit3.BeginInit();
            bit3.StreamSource = ms;
            bit3.EndInit();
            return bit3;
        }
    }
}
