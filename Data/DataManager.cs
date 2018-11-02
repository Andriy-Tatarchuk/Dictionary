using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Enigma.Entity;
using Enigma.Entity.Entities;

namespace Enigma.Data
{
    public class DataManager : IDataManager
    {
        #region Private Methods

        private static DataContext GetDataContext()
        {
            DataContext dataContext = null;
            try
            {
                dataContext = new DataContext();
            }
            catch (Exception e)
            {
                
            }

            return dataContext;
        }

        private async Task<DataContext> GetDataContextAsync()
        {
            return await Task<DataContext>.Factory.StartNew(() => { return GetDataContext(); });
        }

        #endregion

        #region Public Methods

        public async Task<bool> InitializeDataContextAsync()
        {
            var isDataContextInisialized = await Task<bool>.Factory.StartNew(() =>
            {
                var res = false;
                using (var dataContext = GetDataContext())
                {
                    res = dataContext != null;
                    if (res)
                    {
                        res = dataContext.Inisialize();
                    }
                }

                return res;
            });

            return isDataContextInisialized;
        }

        public async Task<Dictionary> AddDictionaryAsynk(string name)
        {
            using (var dataContext = GetDataContext())
            {
                var newDictionary = new Dictionary(name);
                dataContext.Dictionaries.Add(newDictionary);
                await dataContext.SaveChangesAsync();
                return newDictionary;
            }

            return null;
        }

        public async Task SaveDictionaryAsync(Dictionary dictionary)
        {
            using (var dataContext = GetDataContext())
            {
                dataContext.Dictionaries.AddOrUpdate(d=>d.Id, dictionary);
                await dataContext.SaveChangesAsync();
            }
        }

        public async Task<List<Dictionary>> GetAllDictionariesAsync()
        {
            using (var dataContext = GetDataContext())
            {
                return await dataContext.Dictionaries.ToListAsync();
            }
        }

        public async Task<List<Word>> SearchWordsAsync(string searchText = "", int dictionaryId = -1, bool isFullMatching = false)
        {
            using (var dataContext = GetDataContext())
            {
                var words = dataContext.Words.AsQueryable();
                if (dictionaryId >= 0)
                {
                    words = words.Where(w => w.DictionaryId == dictionaryId);
                }

                if (!string.IsNullOrEmpty(searchText))
                {
                    if (!isFullMatching)
                    {
                        words = words.Where(w => w.Name.Contains(searchText) || w.Translation.Contains(searchText));
                    }
                    else
                    {
                        words = words.Where(w => w.Name == searchText || w.Translation == searchText);
                    }
                }

                return await words.ToListAsync();
            }
        }

        public async Task<Word> GetWordAsync(int id)
        {
            using (var dataContext = GetDataContext())
            {
                return await dataContext.Words.FirstOrDefaultAsync(w => w.Id == id);
            }
        }

        public async Task<int> GetWordsCountAsync(int dictionaryId)
        {
            using (var dataContext = GetDataContext())
            {
                return await dataContext.Words.CountAsync(w => w.DictionaryId == dictionaryId);
            }
        }

        public async Task<Dictionary> GetDictionaryAsync(int id)
        {
            using (var dataContext = GetDataContext())
            {
                return await dataContext.Dictionaries.FirstOrDefaultAsync(d => d.Id == id);
            }
        }

        public async Task SaveWordAsync(Word word)
        {
            using (var dataContext = GetDataContext())
            {
                dataContext.Words.AddOrUpdate(w => w.Id, word);

                await dataContext.SaveChangesAsync();
            }
        }

        public async Task DeleteWordAsync(int id)
        {
            using (var dataContext = GetDataContext())
            {
                var word = await dataContext.Words.FirstOrDefaultAsync(w=> w.Id == id);
                if (word != null)
                {
                    dataContext.Words.Remove(word);
                    await dataContext.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteDictionaryAsync(int id)
        {
            using (var dataContext = GetDataContext())
            {
                var dictionary = await dataContext.Dictionaries.FirstOrDefaultAsync(w => w.Id == id);
                if (dictionary != null)
                {
                    dataContext.Dictionaries.Remove(dictionary);
                    await dataContext.SaveChangesAsync();
                }
            }
        }

        
        #endregion
    }
}