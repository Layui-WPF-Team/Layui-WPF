using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;

namespace LayUI.Wpf.Tools
{
    /// <summary>
    /// 动画帮助类
    /// </summary>
    public class LayAnimationHelper
    {
        /// <summary>
        /// 执行故事板
        /// </summary>
        /// <param name="storyboard">创建故事板</param>
        /// <param name="element">执行故事板的控件</param>
        public static void ExecuteStoryboard(Storyboard storyboard, FrameworkElement element) => ExecuteStoryboard(storyboard, element, null);
        /// <summary>
        /// 执行故事板
        /// </summary>
        /// <param name="storyboard">创建故事板</param>
        /// <param name="element">执行故事板的控件</param>
        /// <param name="callBack">回调(通常是指当前故事板执行完成后触发)</param>
        public static void ExecuteStoryboard(Storyboard storyboard, FrameworkElement element, Action<object, EventArgs> callBack)
        {
            if (element == null) return;
            storyboard.Completed += (o, e) => callBack?.Invoke(o, e);
            element.BeginStoryboard(storyboard);
        }
        /// <summary>
        /// 创建故事板
        /// </summary>
        /// <param name="timelines">动画</param>
        /// <returns></returns>
        public static Storyboard CreateStoryboard(Timeline[] timelines) => CreateStoryboard(null, timelines, null);
        /// <summary>
        /// 创建故事板
        /// </summary>
        /// <param name="beginTime">动画开始执行时间</param>
        /// <param name="timelines">动画</param>
        /// <returns></returns>
        public static Storyboard CreateStoryboard(double? beginTime, Timeline[] timelines) => CreateStoryboard(beginTime, timelines, null);
        /// <summary>
        /// 创建故事板
        /// </summary>
        /// <param name="timelines">动画</param>
        /// <param name="callBack">回调(通常是指当前动画执行完成后触发)</param>
        /// <returns></returns>
        public static Storyboard CreateStoryboard(Timeline[] timelines, Action<object, EventArgs> callBack) => CreateStoryboard(null, timelines, callBack);
        /// <summary>
        /// 创建故事板
        /// </summary>
        /// <param name="beginTime">动画开始执行时间</param>
        /// <param name="timelines">动画</param>
        /// <param name="callBack">回调(通常是指当前动画执行完成后触发)</param>
        /// <returns></returns>
        public static Storyboard CreateStoryboard(double? beginTime, Timeline[] timelines, Action<object, EventArgs> callBack)
        {
            try
            {
                var storyboard = new Storyboard();
                if (beginTime != null) storyboard.BeginTime = TimeSpan.FromSeconds((double)beginTime);
                foreach (Timeline timeline in timelines)
                {
                    storyboard.Children.Add(timeline);
                }
                storyboard.Completed += (o, e) => callBack?.Invoke(o, e);
                return storyboard;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 创建双精度动画
        /// </summary>
        /// <param name="from">起始值</param>
        /// <param name="to">结束值</param>
        /// <param name="duration">动画持续时间</param>
        /// <param name="path">被执行的动画属性</param> 
        /// <param name="ui">执行动画的控件</param>
        /// <returns></returns>
        public static AnimationTimeline CreateDoubleAnimation(object from, object to, double duration, PropertyPath path, FrameworkElement element) => CreateDoubleAnimation(from, to, null, duration, null, path, element, null);
        /// <summary>
        /// 创建双精度动画
        /// </summary>
        /// <param name="from">起始值</param>
        /// <param name="to">结束值</param>
        /// <param name="duration">动画持续时间</param>
        /// <param name="path">被执行的动画属性</param> 
        /// <param name="element">执行动画的控件</param>
        /// <param name="callBack">回调(通常是指当前动画执行完成后触发)</param>
        /// <returns></returns>
        public static AnimationTimeline CreateDoubleAnimation(object from, object to, double duration, PropertyPath path, FrameworkElement element, Action<object, EventArgs> callBack) => CreateDoubleAnimation(from, to, null, duration, null, path, element, callBack);
        /// <summary>
        /// 创建双精度动画
        /// </summary>
        /// <param name="from">起始值</param>
        /// <param name="to">结束值</param>
        /// <param name="duration">动画持续时间</param>
        /// <param name="easing">动画缓冲效果</param>
        /// <param name="path">被执行的动画属性</param> 
        /// <param name="element">执行动画的控件</param>
        /// <param name="callBack">回调(通常是指当前动画执行完成后触发)</param>
        /// <returns></returns>
        public static AnimationTimeline CreateDoubleAnimation(object from, object to, double duration, IEasingFunction easing, PropertyPath path, FrameworkElement element, Action<object, EventArgs> callBack) => CreateDoubleAnimation(from, to, null, duration, easing, path, element, callBack);
        /// <summary>
        /// 创建双精度动画
        /// </summary>
        /// <param name="from">起始值</param>
        /// <param name="to">结束值</param>
        /// <param name="duration">动画持续时间</param>
        /// <param name="easing">动画缓冲效果</param>
        /// <param name="path">被执行的动画属性</param> 
        /// <param name="element">执行动画的控件</param>
        /// <returns></returns>
        public static AnimationTimeline CreateDoubleAnimation(object from, object to, double duration, IEasingFunction easing, PropertyPath path, FrameworkElement ui) => CreateDoubleAnimation(from, to, null, duration, easing, path, ui, null);

        /// <summary>
        /// 创建双精度动画
        /// </summary>
        /// <param name="from">起始值</param>
        /// <param name="to">结束值</param>
        /// <param name="beginTime">动画开始执行时间</param>
        /// <param name="duration">动画持续时间</param>
        /// <param name="easing">动画缓冲效果</param>
        /// <param name="path">被执行的动画属性</param> 
        /// <param name="element">执行动画的控件</param>
        /// <param name="callBack">回调(通常是指当前动画执行完成后触发)</param>
        /// <returns></returns>
        public static AnimationTimeline CreateDoubleAnimation(object from, object to, double? beginTime, double duration, IEasingFunction easing, PropertyPath path, FrameworkElement element, Action<object, EventArgs> callBack)
        {
            try
            {
                if (to == null) throw new Exception($"结束值to不能为空");
                AnimationTimeline animation = null;
                if (to.GetType() == typeof(double))
                {
                    var DoubleAnimation = new DoubleAnimation
                    {
                        From = (double?)from,
                        To = (double?)to,
                        Duration = TimeSpan.FromSeconds(duration),
                    };
                    if (easing != null) DoubleAnimation.EasingFunction = easing;
                    animation = DoubleAnimation;
                }
                else if (to.GetType() == typeof(Int16))
                {
                   var Int16Animation = new Int16Animation
                    {
                        From = (short?)from,
                        To = (short?)to,
                        Duration = TimeSpan.FromSeconds(duration),
                    };
                    if (easing != null) Int16Animation.EasingFunction = easing;
                    animation = Int16Animation;
                }
                else if (to.GetType() == typeof(Int32))
                {
                    var Int32Animation = new Int32Animation
                    {
                        From = (int?)from,
                        To = (int?)to,
                        Duration = TimeSpan.FromSeconds(duration),
                    };
                    if (easing != null) Int32Animation.EasingFunction = easing;
                    animation = Int32Animation;
                }
                else if (to.GetType() == typeof(Int64))
                {
                  var Int64Animation = new Int64Animation
                    {
                        From = (long?)from,
                        To = (long?)to,
                        Duration = TimeSpan.FromSeconds(duration),
                    };
                    if (easing != null) Int64Animation.EasingFunction = easing;
                    animation = Int64Animation;
                }
                else if (to.GetType() == typeof(Thickness))
                {
                   var ThicknessAnimation = new ThicknessAnimation
                    {
                        From = (Thickness?)from,
                        To = (Thickness?)to,
                        Duration = TimeSpan.FromSeconds(duration),
                    };
                    if (easing != null) ThicknessAnimation.EasingFunction = easing;
                    animation = ThicknessAnimation;
                }
                else if (to.GetType() == typeof(Rect))
                {
                    var RectAnimation = new RectAnimation
                    {
                        From = (Rect?)from,
                        To = (Rect?)to,
                        Duration = TimeSpan.FromSeconds(duration),
                    };
                    if (easing != null) RectAnimation.EasingFunction = easing;
                    animation = RectAnimation;
                }
                else if (to.GetType() == typeof(Color))
                {
                   var ColorAnimation = new ColorAnimation
                    {
                        From = (Color?)from,
                        To = (Color?)to,
                        Duration = TimeSpan.FromSeconds(duration),
                    };
                    if (easing != null) ColorAnimation.EasingFunction = easing;
                    animation = ColorAnimation;
                }
                else if (to.GetType() == typeof(byte))
                {
                   var ByteAnimation = new ByteAnimation
                    {
                        From = (byte?)from,
                        To = (byte?)to,
                        Duration = TimeSpan.FromSeconds(duration),
                    };
                    if (easing != null) ByteAnimation.EasingFunction = easing;
                    animation = ByteAnimation;
                }
                else if (to.GetType() == typeof(decimal))
                {
                   var DecimalAnimation = new DecimalAnimation
                    {
                        From = (decimal?)from,
                        To = (decimal?)to,
                        Duration = TimeSpan.FromSeconds(duration),
                    };
                    if (easing != null) DecimalAnimation.EasingFunction = easing;
                    animation = DecimalAnimation;
                }
                else if (to.GetType() == typeof(Quaternion))
                {
                   var QuaternionAnimation = new QuaternionAnimation
                    {
                        From = (Quaternion?)from,
                        To = (Quaternion?)to,
                        Duration = TimeSpan.FromSeconds(duration),
                    };
                    if (easing != null) QuaternionAnimation.EasingFunction = easing;
                    animation = QuaternionAnimation;
                }
                else if (to.GetType() == typeof(Rotation3D))
                {
                    var Rotation3D = new Rotation3DAnimation
                    {
                        From = (Rotation3D)from,
                        To = (Rotation3D)to,
                        Duration = TimeSpan.FromSeconds(duration),
                    };
                    if (easing != null) Rotation3D.EasingFunction = easing;
                    animation = Rotation3D;
                }
                else if (to.GetType() == typeof(float))
                {
                   var SingleAnimation = new SingleAnimation
                    {
                        From = (float?)from,
                        To = (float?)to,
                        Duration = TimeSpan.FromSeconds(duration),
                    };
                    if (easing != null) SingleAnimation.EasingFunction = easing;
                    animation = SingleAnimation;
                }
                else if (to.GetType() == typeof(Vector3D))
                {
                    var Vector3DAnimation = new Vector3DAnimation
                    {
                        From = (Vector3D?)from,
                        To = (Vector3D?)to,
                        Duration = TimeSpan.FromSeconds(duration),
                    };
                    if (easing != null) Vector3DAnimation.EasingFunction = easing;
                    animation = Vector3DAnimation;
                }
                else if (to.GetType() == typeof(Vector))
                {
                  var VectorAnimation = new VectorAnimation
                    {
                        From = (Vector?)from,
                        To = (Vector?)to,
                        Duration = TimeSpan.FromSeconds(duration),
                    };
                    if (easing != null) VectorAnimation.EasingFunction = easing;
                    animation = VectorAnimation;
                }
                else if (to.GetType() == typeof(Size))
                {
                   var SizeAnimation = new SizeAnimation
                    {
                        From = (Size?)from,
                        To = (Size?)to,
                        Duration = TimeSpan.FromSeconds(duration),
                    };
                    if (easing != null) SizeAnimation.EasingFunction = easing;
                    animation = SizeAnimation;
                } 
                else throw new Exception($"结束值to不能为空");
                if (beginTime != null) animation.BeginTime = TimeSpan.FromSeconds((double)beginTime);
                Storyboard.SetTarget(animation, element);
                Storyboard.SetTargetProperty(animation, path);
                animation.Completed += (o, e) => callBack?.Invoke(o, e);
                return animation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
