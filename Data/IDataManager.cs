using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enigma.Entity;
using Enigma.Entity.Entities;

namespace Enigma.Data
{
    public interface IDataManager
    {
        Task<Dictionary> AddDictionaryAsynk(string name);
        Task SaveDictionaryAsync(Dictionary dictionary);
        Task<List<Dictionary>> GetAllDictionariesAsync();
        Task<Word> GetWordAsync(int id);
        Task<Dictionary> GetDictionaryAsync(int id);
        Task SaveWordAsync(Word word);
        Task DeleteWordAsync(int id);
        Task DeleteDictionaryAsync(int id);
        Task<List<Word>> SearchWordsAsync(string searchText = "", int dictionaryId = -1, bool isFullMatching = false);
    }
}
