using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layui.Core
{
    public interface ILayLanguage
    {
        /// <summary>
        /// 查找当前字典中翻译文字
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object GetValue(string key);
    }
}
