using LayUI.Wpf.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
