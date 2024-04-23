using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
        /// <summary>
        /// 根据Key在当前程序中查找翻译文件
        /// </summary>
        /// <param name="key"></param>
        void LoadResourceKey(string key);
        /// <summary>
        /// 根据路径加载翻译文件
        /// </summary>
        /// <param name="path"></param>
        void LoadPath(string path);
        /// <summary>
        /// 加载翻译文件
        /// </summary>
        /// <param name="languageDictionary"></param>
        void LoadDictionary(ResourceDictionary languageDictionary);
        /// <summary>
        /// 刷新视图翻译效果
        /// </summary>
        void Refresh();
    }
}
