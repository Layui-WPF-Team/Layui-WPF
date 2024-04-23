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
        private LanguageExtension _language;
        private LanguageExtension language
        {
            get
            { 
                if (_language == null) _language = new LanguageExtension();
                return _language;
            }

        }

        public object GetValue(string key) => language.GetValue(key);

        public void LoadDictionary(ResourceDictionary languageDictionary)=> LanguageExtension.LoadDictionary(languageDictionary);

        public void LoadPath(string path)=> LanguageExtension.LoadPath(path);

        public void LoadResourceKey(string key)=> LanguageExtension.LoadResourceKey(key);

        public void Refresh()=>LanguageExtension.Refresh();
    }
}
