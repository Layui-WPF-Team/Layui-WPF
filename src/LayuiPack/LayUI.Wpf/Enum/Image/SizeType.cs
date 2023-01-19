using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Wpf.Enum
{
    /// <summary>
    /// 这控制如何拉伸图像以填充控件，
    /// </summary>
    public enum SizeType
    {
        /// <summary>
        ///图像未缩放。图像位置被平移，因此左上角
        ///将图像边界框的左上角移动到图像控件的左上。
        /// </summary>
        None,
        /// <summary>
        ///缩放图像以适合控件，而不进行任何拉伸。
        ///将缩放X或Y方向以填充整个宽度或高度。
        /// </summary>
        ContentToSizeNoStretch,
        /// <summary>
        /// 图像将被拉伸以填充整个宽度和高度。
        /// </summary>
        ContentToSizeStretch,
        /// <summary>
        ///控件将调整大小以适合未缩放的图像。如果图像大于
        ///控件的最大大小，控件设置为最大大小并缩放图像。
        /// </summary>
        SizeToContent,
    }
}
