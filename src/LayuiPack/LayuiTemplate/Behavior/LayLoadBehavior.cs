using LayuiTemplate.Enum.Base;
using System.Windows;
using System.Windows.Interactivity;

namespace LayuiTemplate.Behavior
{
    public class LayLoadBehavior: Behavior<FrameworkElement>
    {
        /// <summary>
        /// 动画类型
        /// </summary>
        public LayAnimation Animation { get; set; } = LayAnimation.Scale;
        protected override void OnAttached()
        {
            base.OnAttached();
        }
    }
}
