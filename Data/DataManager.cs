using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Enigma.Data.DataModels;
using Enigma.Entity;
using Enigma.Entity.Entities;

namespace Enigma.Data
{
    public class DataManager
    {

        #region Declarations

        #endregion

        #region Properties

        public static bool IsDataContextInisialized = false;

        #endregion

        #region Constructorss

        public DataManager()
        {
        }

        #endregion


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

        public static async Task<bool> InitializeDataContextAsync()
        {
            if (!IsDataContextInisialized)
            {
                IsDataContextInisialized = await Task<bool>.Factory.StartNew(() =>
                {
                    var res = false;
                    using (var dataContext = GetDataContext())
                    {
                        res = dataContext != null;
                        if (res)
                        {
                            try
                            {
                                dataContext.Database.Connection.Open();
                                dataContext.Database.CreateIfNotExists();
                                dataContext.Words.Any();
                            }
                            catch
                            {
                                res = false;
                            }
                        }
                    }
                    return res;
                });

                if (IsDataContextInisialized && DataContextInisialized != null)
                {
                    DataContextInisialized(null, EventArgs.Empty);
                }
            }

            return IsDataContextInisialized;
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

        public async Task<OperationResult> SaveDictionaryAsync(Dictionary dictionary)
        {
            var res = new OperationResult(null, false, "");
            using (var dataContext = GetDataContext())
            {
                dictionary.Words = null;
                dataContext.Dictionaries.AddOrUpdate(d=>d.Id, dictionary);
                await dataContext.SaveChangesAsync();
            }

            return res;
        }

        public async Task AddWordToDictionaryAsync(int dictionaryId, Word word)
        {
            using (var dataContext = GetDataContext())
            {
                var dictionary = await dataContext.Dictionaries.Include(d=>d.Words).FirstOrDefaultAsync(d => d.Id == dictionaryId);

                if (dictionary != null)
                {
                    if (!dictionary.Words.Any(w => w.Id == word.Id))
                    {
                        dictionary.Words.Add(word);
                        await dataContext.SaveChangesAsync();
                    }
                }
            }
        }

        public async Task<List<Dictionary>> GetAllDictionariesAsync()
        {
            using (var dataContext = GetDataContext())
            {
                return await dataContext.Dictionaries.ToListAsync();
            }
        }

        public async Task<List<Word>> GetAllWordsAsync()
        {
            using (var dataContext = GetDataContext())
            {
                return await dataContext.Words.ToListAsync();
            }
        }

        public async Task<List<Word>> GetWordsByDictionaryAsync(int dictionaryId)
        {
            using (var dataContext = GetDataContext())
            {
                var dictionary = await dataContext.Dictionaries.Include(d=>d.Words).FirstOrDefaultAsync(d=>d.Id == dictionaryId);
                return dictionary != null ? dictionary.Words : null;
            }
        }

        public async Task<Word> GetWordAsync(int id)
        {
            using (var dataContext = GetDataContext())
            {
                return await dataContext.Words.FirstOrDefaultAsync(w => w.Id == id);
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
                word.Dictionaries = null;
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

        public async Task<Word> GetNewWordFormDictionaryAsync(int dictionaryId)
        {
            Word word = null;
            using (var dataContext = GetDataContext())
            {
                var dictionary = dataContext.Dictionaries.Find(dictionaryId);
                if (dictionary != null)
                {
                    word = dataContext.Words.Create();
                    dictionary.Words.Add(word);
                    await dataContext.SaveChangesAsync();
                }
            }

            return word;
        }

        #endregion

        #region Events

        public static event EventHandler DataContextInisialized;

        #endregion

    }
}