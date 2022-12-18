using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Layui.Wpf.Utils.Thread
{
    /// <summary>
    /// UI线程上调度程序操作的帮助程序类。
    /// </summary>
    public static class LayuiDispatcher
    {
        /// <summary>
        /// 在UI线程上调用 <see cref="Initialize" /> 方法后，获取对UI线程调度程序的引用。
        /// </summary>
        public static Dispatcher UIDispatcher
        {
            get;
            private set;
        }

        /// <summary>
        /// 对UI线程执行操作。如果调用此方法从UI线程中，该操作将直接执行。如果
        /// 方法从另一个线程调用时，操作将被排队并异步执行。
        /// </summary>
        /// <param name="action">将在UI上执行的操作线程</param>
        public static void CheckBeginInvokeOnUI(Action action)
        {
            if (action == null)
            {
                return;
            }

            CheckDispatcher();
            if (UIDispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                UIDispatcher.BeginInvoke(action);
            }
        }

        private static void CheckDispatcher()
        {
            if (UIDispatcher == null)
            {
                var error = new StringBuilder("DispatcherHelper未初始化。");
                error.AppendLine();
                error.Append("调用DispatcherHelper。在静态App构造函数中初始化（）。");
                throw new InvalidOperationException(error.ToString());
            }
        }
        /// <summary>
        /// 在UI线程上异步调用操作。
        /// </summary>
        /// <param name="action">必须执行的操作。</param>
        /// <returns>调用BeginInvoke后立即返回的对象，当委托在事件队列中等待执行时，该对象可用于与委托交互。</returns>
        public static DispatcherOperation RunAsync(Action action)
        {
            CheckDispatcher();
            return UIDispatcher.BeginInvoke(action);
        }

        /// <summary>
        /// 此方法应在UI线程上调用一次，以确保 <see cref="UIDispatcher" /> 属性已初始化。
        /// <para>在Silverlight应用程序中，在构建MainPage之后，在application_Startup事件处理程序中调用此方法。</para>
        /// <para>在WPF中，在静态App（）构造函数上调用此方法。</para>
        /// </summary>
        public static void Initialize()
        {
            if (UIDispatcher != null && UIDispatcher.Thread.IsAlive)
            {
                return;
            }
            UIDispatcher = Dispatcher.CurrentDispatcher;
        }

        /// <summary>
        /// 通过删除 <see cref="UIDispatcher"/>
        /// </summary>
        public static void Reset()
        {
            UIDispatcher = null;
        }
    }
}
