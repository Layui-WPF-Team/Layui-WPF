using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
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
        public static System.Drawing.Bitmap GetBitmap(ImageSource Source)
        {
            try
            {
                string file = null;
                if (Source.ToString().ToLower().Contains("pack"))
                {
                    file = System.AppDomain.CurrentDomain.BaseDirectory + new Uri(Source.ToString(), UriKind.RelativeOrAbsolute).LocalPath;
                }
                else
                {
                    file = Source.ToString();
                }
                if (file.Contains("https:"))
                {
                    var webC = new System.Net.WebClient();
                    System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(webC.OpenRead(file));
                    return bmp;
                }
                else
                {
                    System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(file);
                    return bmp;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static System.Drawing.Bitmap BitmapImageToBitmap(BitmapImage bitmapImage)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

                return new System.Drawing.Bitmap(bitmap);
            }
        }
    }
}
