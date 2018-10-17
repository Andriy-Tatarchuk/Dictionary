using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Enigma.Autocomplete
{
    public class Autocompleter
    {
        private static bool IsInitialized { get; set; }
        public static List<string> Words { get; private set; }

        static Autocompleter()
        {
            Words = new List<string>();
        }

        public static async Task InitializeWordsAsync()
        {
            if (!IsInitialized)
            {
                await ReadFromFileAsync();

                IsInitialized = true;
            }
        }

        private static async Task ReadFromFileAsync()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Enigma.Autocomplete.Resources.words_alpha.txt";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                //    var uri = new Uri("pack://application:,,,/Enigma.Autocomplete;component/Resources/words_alpha.txt");
                //var reader = new StreamReader(uri);

                while (!reader.EndOfStream)
                {
                    Words.Add(reader.ReadLine());
                }
            }
        }

        public static async Task<string> GetFirstWordStartedWith(string prefix)
        {
            int index = await BinarySearchStartsWith(Words, prefix, 0, Words.Count - 1);
            if (index == -1)
            {
                return null;
            }

            while (Words[index - 1].StartsWith(prefix) && Words[index - 1].Length > prefix.Length)
            {
                index--;
            }

            if (Words[index].Length == prefix.Length && Words[index + 1].StartsWith(prefix))
            {
                index++;
            }

            return Words[index];
        }

        private static async Task<int> BinarySearchStartsWith(List<string> words, string prefix, int min, int max)
        {
            while (max >= min)
            {
                try
                {
                    int mid = (min + max) / 2;
                    int comp = String.Compare(words[mid].Length <= prefix.Length ? words[mid] : words[mid].Substring(0, prefix.Length), prefix);
                    if (comp < 0)
                    {
                        min = mid + 1;
                    }
                    else if (comp > 0)
                    {
                        max = mid - 1;
                    }
                    else
                    {
                        return mid;
                    }
                }
                catch (Exception e)
                {

                }
            }
            return -1;
        }
    }
}
