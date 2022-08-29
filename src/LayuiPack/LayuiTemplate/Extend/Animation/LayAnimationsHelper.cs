using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace LayuiTemplate.Extend
{
    /// <summary>
    ///  动画命令附加属性
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-08-29 上午 10:13:11</para>
    /// </summary>
    public class LayAnimationsHelper : DependencyObject
    {
        /// <summary>
        /// 获取动画执行结束命令
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static ICommand GetCompletedCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CompletedCommandProperty);
        }
        /// <summary>
        /// 设置动画执行结束命令
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetCompletedCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CompletedCommandProperty, value);
        }

        // Using a DependencyProperty as the backing store for CompletedCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CompletedCommandProperty =
            DependencyProperty.RegisterAttached("CompletedCommand", typeof(ICommand), typeof(LayAnimationsHelper));
        /// <summary>
        /// 获取动画执行结束命令参数
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object GetCompletedCommandParameter(DependencyObject obj)
        {
            return (object)obj.GetValue(CompletedCommandParameterProperty);
        }
        /// <summary>
        /// 设置动画执行结束命令参数
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetCompletedCommandParameter(DependencyObject obj, object value)
        {
            obj.SetValue(CompletedCommandParameterProperty, value);
        }

        // Using a DependencyProperty as the backing store for CompletedCommandParameter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CompletedCommandParameterProperty =
            DependencyProperty.RegisterAttached("CompletedCommandParameter", typeof(object), typeof(LayAnimationsHelper));
        /// <summary>
        /// 获取附加
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool GetIsExtend(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsExtendProperty);
        }
        /// <summary>
        /// 设置附加
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetIsExtend(DependencyObject obj, bool value)
        {
            obj.SetValue(IsExtendProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsExtend.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsExtendProperty =
            DependencyProperty.RegisterAttached("IsExtend", typeof(bool), typeof(LayAnimationsHelper), new PropertyMetadata(OnExtendChanged));

        private static void OnExtendChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                if (d is Timeline)
                {
                    var timeline = d as Timeline;
                    if (GetIsExtend(d))
                    {
                        timeline.Completed -= Timeline_Completed;
                        timeline.Completed += Timeline_Completed;
                    }
                    else timeline.Completed -= Timeline_Completed;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        private static void Timeline_Completed(object sender, EventArgs e)
        {
            try
            {
                if (sender is Clock clockGroup)
                {
                    GetCompletedCommand(clockGroup.Timeline).Execute(GetCompletedCommandParameter(clockGroup.Timeline));
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

    }
}
