using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma.Translate
{
    public interface ITranslator
    {
        string Translate(string word, string targetLanguage, string sourceLanguage);
    }
}
