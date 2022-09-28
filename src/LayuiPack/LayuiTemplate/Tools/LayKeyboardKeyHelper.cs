using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LayuiTemplate.Tools
{
    /// <summary>
    /// 自定义虚拟键盘帮助类
    /// </summary>
    public class LayKeyboardKeyHelper
    {
        [DllImport("User32.dll")]
        internal static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
        /// <summary>
        /// 按下
        /// </summary>
        public const int KeyDown = 0;
        /// <summary>
        /// 抬起
        /// </summary>
        public const int KeyUp = 0x2;
        /// <summary>
        /// 模拟键盘事件
        /// </summary>
        /// <param name="key">按键Key</param>
        /// <param name="keyEvent">关键事件</param>
        public static void Keyboard_Event(Key key, int keyEvent)
        {
            keybd_event((byte)KeyInterop.VirtualKeyFromKey(key), 0, keyEvent, 0);
        }
        /// <summary>
        /// 模拟键盘事件
        /// </summary>
        /// <param name="key">按键Key</param>
        public static void Keyboard_Event(Key key)
        {
            keybd_event((byte)KeyInterop.VirtualKeyFromKey(key), 0, KeyDown, 0);
            keybd_event((byte)KeyInterop.VirtualKeyFromKey(key), 0, KeyUp, 0);
        }
        /// <summary>
        /// 检查键盘大小写状态
        /// </summary>
        /// <param name="nVirtKey"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetKeyboardState")]
        internal static extern int GetKeyboardState(byte[] pbKeyState);
        /// <summary>
        /// 获取键盘大小写状态值
        /// </summary>
        public static bool CapsLockStatus
        {
            get
            {
                byte[] bs = new byte[256];
                GetKeyboardState(bs);
                return bs[0x14] == 1;
            }
        }
    }
}
