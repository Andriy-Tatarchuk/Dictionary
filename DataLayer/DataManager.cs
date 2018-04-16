using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.DataModels;
using EntityLayer.Entities;

namespace DataLayer
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

        private DataContext GetDataContext()
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
            if (!IsDataContextInisialized)
            {
                IsDataContextInisialized = await Task<bool>.Factory.StartNew(() =>
                {
                    var dataContext = GetDataContext();
                    var res = dataContext != null;
                    if (res)
                    {
                       dataContext.Words.Any();
                    }

                    return res;
                });
            }

            return IsDataContextInisialized;
        }

        //public Dictionary AddDictionary(string name)
        //{
        //    using (var dataContext = GetDataContext())
        //    {
        //        if (!dataContext.Dictionaries.ToList().Exists(d => d.Name == name))
        //        {
        //            var dic = new Dictionary(name);
        //            dataContext.Dictionaries.Add(dic);
        //            dataContext.SaveChanges();
        //            return dic;
        //        }
        //    }
        //    return null;
        //}

        private OperationResult SaveDictionary(int id, string name)
        {
            var res = new OperationResult(null, false, "");
            using (var dataContext = GetDataContext())
            {
                var newDic = false;
                var dic = dataContext.Dictionaries.Find(id);
                if (dic == null)
                {
                    if (dataContext.Dictionaries.Any(d => d.Name == name))
                    {
                        res.HasError = true;
                        res.Message = "This name already exists";
                        
                    }
                    else
                    {
                        dic = dataContext.Dictionaries.Create();
                        newDic = true;
                    }
                }

                if (!res.HasError)
                {
                    dic.Name = name;

                    if (newDic)
                    {
                        dataContext.Dictionaries.Add(dic);
                    }

                    res.HasError = dataContext.SaveChanges() < 1;
                    if (res.HasError)
                    {
                        res.Message = "Can't save dictionary";
                    }
                    else
                    {
                        res.Message = string.Empty;
                    }
                }
            }

            return res;
        }

        public async Task<OperationResult> SaveDictionaryAsync(int id, string name)
        {
            return await Task<OperationResult>.Factory.StartNew(() => { return SaveDictionary(id, name); });
        }

        public async Task<OperationResult> SaveDictionaryAsync2(int id, string name)
        {
            var res = new OperationResult(null, false, "");
            using (var dataContext = GetDataContext())
            {
                var newDic = false;
                var dic = await dataContext.Dictionaries.FindAsync(id);
                if (dic == null)
                {
                    if (await dataContext.Dictionaries.AnyAsync(d => d.Name == name))
                    {
                        res.HasError = true;
                        res.Message = "This name already exists";

                    }
                    else
                    {
                        dic = dataContext.Dictionaries.Create();
                        newDic = true;
                    }
                }

                if (!res.HasError)
                {
                    dic.Name = name;

                    if (newDic)
                    {
                        dataContext.Dictionaries.Add(dic);
                    }

                    res.HasError = await dataContext.SaveChangesAsync() < 1;
                    if (res.HasError)
                    {
                        res.Message = "Can't save dictionary";
                    }
                    else
                    {
                        res.Message = string.Empty;
                    }
                }
            }

            return res;
        }

        //public void AddWordToDictionary(int id, Word word)
        //{
        //    using (var dataContext = GetDataContext())
        //    {
        //        if (dataContext.Dictionaries.Find(id) != null)
        //        {
        //            dataContext.Dictionaries.Find(id).Words.Add(word);
        //            dataContext.SaveChanges();
        //        }
        //    }
        //}

        private List<Dictionary> GetAllDictionaries()
        {
            using (var dataContext = GetDataContext())
            {
                return dataContext.Dictionaries.ToList();
            }
        }

        public async Task<List<Dictionary>> GetAllDictionariesAsync()
        {
            return await Task<List<Dictionary>>.Factory.StartNew(() => { return GetAllDictionaries(); });
        }

        private List<Word> GetAllWords()
        {
            using (var dataContext = GetDataContext())
            {
                return dataContext.Words.ToList();
            }
        }

        public async Task<List<Word>> GetAllWordsAsync()
        {
            //return await Task<List<Word>>.Factory.StartNew(() => { return GetAllWords(); });
            using (var dataContext = GetDataContext())
            {
                return await dataContext.Words.ToListAsync();
            }
        }

        public async Task<List<Word>> GetWordsByDicAsync(int dicId)
        {
            using (var dataContext = GetDataContext())
            {
                var dic = await dataContext.Dictionaries.Include(d=>d.Words).FirstAsync(d=>d.Id == dicId);
                return dic.Words;
            }
        }

        private Word GetWord(int id)
        {
            using (var dataContext = GetDataContext())
            {
                return dataContext.Words.Where(w=>w.Id == id).Include(w=>w.Dictionaries).FirstOrDefault();
            }
        }

        public async Task<Word> GetWordAsync(int id)
        {
            return await Task<Word>.Factory.StartNew(() => { return GetWord(id); });
        }

        private Dictionary GetDictionary(int id)
        {
            using (var dataContext = GetDataContext())
            {
                return dataContext.Dictionaries.Where(d => d.Id == id).Include(d => d.Words).FirstOrDefault();
            }
        }

        public async Task<Dictionary> GetDictionaryAsync(int id)
        {
            return await Task<Dictionary>.Factory.StartNew(() => { return GetDictionary(id); });
        }

        private void SaveWord(int id, string name, string translation, int dictionaryId)
        {
            using (var dataContext = GetDataContext())
            {
                var newWord = false;
                var word = dataContext.Words.Find(id);
                if (word == null)
                {
                    word = new Word(name, translation);
                    newWord = true;
                }

                var dic = dataContext.Dictionaries.Find(dictionaryId);
                if (dic != null)
                {
                    dic.Words.Add(word);
                }

                if (newWord)
                {
                    dataContext.Words.Add(word);
                }

                dataContext.SaveChanges();
            }
        }

        public async void SaveWordAsync(int id, string name, string translation, int dictionaryId)
        {
            await Task.Factory.StartNew(() => { SaveWord(id, name, translation, dictionaryId); });
        }

        private void DeleteWord(int id)
        {
            using (var dataContext = GetDataContext())
            {
                var word = dataContext.Words.Find(id);
                if (word != null)
                {
                    dataContext.Words.Remove(word);
                    dataContext.SaveChanges();
                }
            }
        }

        public async void DeleteWordAsync(int id)
        {
            await Task.Factory.StartNew(() => { DeleteWord(id); });
        }


        #endregion

        #region Events



        #endregion
        
    }
}