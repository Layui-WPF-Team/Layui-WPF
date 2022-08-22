using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LayuiTemplate.Tools
{
    /// <summary>
    ///  可视化树工具
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-08-22 下午 2:02:26</para>
    /// </summary>
    public class LayVisualTreeHelper
    {
        /// <summary>
        /// 查找当前控件下面的所有目标元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static List<T> FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            try
            {
                List<T> TList = new List<T> { };
                //转换当前视图下面ContentPresenter元素并将其元素内容抓取转换
                DependencyObject element = obj is ContentPresenter presenter ? presenter.Content as DependencyObject : obj;
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(element, i);
                    if (child != null && child is T)
                    {
                        TList.Add((T)child);
                        List<T> childOfChildren = FindVisualChild<T>(child);
                        if (childOfChildren != null)
                        {
                            TList.AddRange(childOfChildren);
                        }
                    }
                    else
                    {
                        List<T> childOfChildren = FindVisualChild<T>(child);
                        if (childOfChildren != null)
                        {
                            TList.AddRange(childOfChildren);
                        }
                    }
                }
                return TList;
            }
            catch
            {
                return null;
            }
        }
    }
}
