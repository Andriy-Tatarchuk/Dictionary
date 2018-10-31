using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Translate.v2;
using Google.Cloud.Translation.V2;
using System.IO;
using System.Reflection;

namespace Enigma.Translate
{
    public class Translator : ITranslator
    {
        static TranslationClient _Client;
        static bool _IsInitialized;

        public void Initialize()
        {
            if (!_IsInitialized)
            {
                try
                {
                    var assembly = Assembly.GetExecutingAssembly();
                    var resourceName = "Enigma.Translate.Resources.GoogleCredential.json";

                    using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                    {
                        var credential = GoogleCredential.FromStream(stream);
                        _Client = TranslationClient.Create(credential);
                    }
                    _IsInitialized = true;
                }
                catch (Exception e)
                {

                }
            }
        }

        public string Translate(string word)
        {
            return Translate(word, LanguageCodes.Ukrainian);
        }

        public string Translate(string word, string targetLanguage)
        {
            return Translate(word, targetLanguage, LanguageCodes.English);
        }

        public string Translate(string word, string targetLanguage, string sourceLanguage)
        {
            var result = string.Empty;
            if(!_IsInitialized)
            {
                Initialize();
            }
            if (_Client != null)
            {
                var translaterResult = _Client.TranslateText(word, targetLanguage, sourceLanguage);
                result = translaterResult.TranslatedText;
            }           

            return result;
        }
    }
}
