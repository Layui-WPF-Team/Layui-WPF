using LayUI.Wpf.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Layui.Core
{
    public class LayLanguage : ILayLanguage
    { 

        public object GetValue(string key) => LanguageExtension.GetValue(key);

        public void LoadDictionary(ResourceDictionary languageDictionary)=> LanguageExtension.LoadDictionary(languageDictionary);

        public void LoadPath(string path)=> LanguageExtension.LoadPath(path);

        public void LoadResourceKey(string key)=> LanguageExtension.LoadResourceKey(key);

        public void Refresh()=>LanguageExtension.Refresh();
    }
}
