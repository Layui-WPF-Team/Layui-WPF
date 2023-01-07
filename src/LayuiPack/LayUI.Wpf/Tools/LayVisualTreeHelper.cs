using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LayUI.Wpf.Tools
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
        /// <summary>
        /// 查找某种类型的子控件，并返回一个List集合
        /// </summary>
        /// <example>List<Button> listButtons = GetChildObjects<Button>(parentPanel, typeof(Button))</example>
        /// <typeparam name="T">子控件类型</typeparam>
        /// <param name="obj">父控件</param>
        /// <param name="type">子控件的类</param>
        /// <returns></returns>
        public static List<T> GetChildObjects<T>(DependencyObject obj, Type type) where T : DependencyObject
        {
            DependencyObject child = null;
            List<T> childList = new List<T>();

            for (int i = 0; i <= VisualTreeHelper.GetChildrenCount(obj) - 1; i++)
            {
                child = VisualTreeHelper.GetChild(obj, i);

                if (child is T && (((T)child).GetType() == type))
                {
                    childList.Add((T)child);
                }
                childList.AddRange(GetChildObjects<T>(child, type));
            }
            return childList;
        }

        /// <summary>
        /// 通过名称查找子控件，并返回一个List集合
        /// </summary>
        /// <example>List<Button> listButtons = GetChildObjects<Button>(parentPanel, "button1")</example>
        /// <typeparam name="T">子控件类型</typeparam>
        /// <param name="obj">父控件</param>
        /// <param name="name">子控件名称，默认为空</param>
        /// <returns></returns>
        public static List<T> GetChildObjects<T>(DependencyObject obj, string name = null) where T : FrameworkElement
        {
            DependencyObject child = null;
            List<T> childList = new List<T>();

            for (int i = 0; i <= VisualTreeHelper.GetChildrenCount(obj) - 1; i++)
            {
                child = VisualTreeHelper.GetChild(obj, i);

                if (child is T && (((T)child).Name == name | string.IsNullOrEmpty(name)))
                {
                    childList.Add((T)child);
                }
                childList.AddRange(GetChildObjects<T>(child, name));
            }
            return childList;
        }

        /// <summary>
        /// 通过名称查找某类型的子控件
        /// </summary>
        /// <example>StackPanel sp = GetChildObject<StackPanel>(this.LayoutRoot, "spDemoPanel")</example>
        /// <typeparam name="T">子控件类型</typeparam>
        /// <param name="obj">父控件</param>
        /// <param name="name">子控件名称，默认为空</param>
        /// <returns></returns>
        public static T GetChildObject<T>(DependencyObject obj, string name = null) where T : FrameworkElement
        {
            DependencyObject child = null;
            T grandChild = null;

            for (int i = 0; i <= VisualTreeHelper.GetChildrenCount(obj) - 1; i++)
            {
                child = VisualTreeHelper.GetChild(obj, i);

                if (child is T && (((T)child).Name == name | string.IsNullOrEmpty(name)))
                {
                    return (T)child;
                }
                else
                {
                    grandChild = GetChildObject<T>(child, name);
                    if (grandChild != null)
                        return grandChild;
                }
            }
            return null;
        }

        /// <summary>
        /// 通过名称查找父控件
        /// </summary>
        /// <example>Grid layoutGrid = VTHelper.GetParentObject<Grid>(this.spDemoPanel, "LayoutRoot")</example>
        /// <typeparam name="T">父控件类型</typeparam>
        /// <param name="obj">子控件</param>
        /// <param name="name">父控件名称，默认为空</param>
        /// <returns></returns>
        public static T GetParentObject<T>(DependencyObject obj, string name = null) where T : FrameworkElement
        {
            DependencyObject parent = VisualTreeHelper.GetParent(obj);
            while (parent != null)
            {
                if (parent is T && (((T)parent).Name == name | string.IsNullOrEmpty(name)))
                {
                    return (T)parent;
                }
                parent = VisualTreeHelper.GetParent(parent);
            }
            return null;
        }
    }
}
