using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enigma.Data;
using Enigma.Entity.Entities;

namespace Enigma.UnitTest
{
    internal class DataManagerMockObject : IDataManager
    {
        private List<Dictionary> DictionaryList { get; set; }

        #region Constructor
        internal DataManagerMockObject()
        {
            DictionaryList = new List<Dictionary>();
        }
        #endregion

        #region Public Methods

        public async Task<Dictionary> AddDictionaryAsynk(string name)
        {
            var dictionary = new Dictionary(name);
            DictionaryList.Add(dictionary);
            return dictionary;
        }

        public async Task DeleteDictionaryAsync(int id)
        {
            var dictionary = DictionaryList.FirstOrDefault(d => d.Id == id);
            if (dictionary != null)
            {
                DictionaryList.Remove(dictionary);
            }
        }

        public async Task DeleteWordAsync(int id)
        {
            foreach (Dictionary dictionary in DictionaryList)
            {
                var word = dictionary.Words.FirstOrDefault(w => w.Id == id);
                if (word != null)
                {
                    dictionary.Words.Remove(word);
                    break;
                }
            }
        }

        public async Task<List<Dictionary>> GetAllDictionariesAsync()
        {
            return DictionaryList;
        }

        public async Task<Dictionary> GetDictionaryAsync(int id)
        {
            return new Dictionary("test"){Id = id};
        }

        public async Task<Word> GetWordAsync(int id)
        {
            return new Word(){Id = id};
        }

        public async Task<bool> InitializeDataContextAsync()
        {
            return true;
        }

        public async Task SaveDictionaryAsync(Dictionary dictionaryToSave)
        {
            
        }

        public async Task SaveWordAsync(Word word)
        {
            
        }

        public async Task<List<Word>> SearchWordsAsync(string searchText = "", int dictionaryId = -1, bool isFullMatching = false)
        {
            return new List<Word>(){new Word(searchText, searchText)};
        }

        #endregion
    }
}
