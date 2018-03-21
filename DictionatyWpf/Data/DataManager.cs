using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Documents;
using DictionatyWpf.Models;

namespace DictionatyWpf.Data
{
    public class DataManager
    {

        #region Declarations

        #endregion

        #region Properties

        //public DataContext DataContext { get; private set; }

        #endregion

        #region Constructorss

        /*public DataManager(DataContext dataContext)
        {
            DataContext = dataContext;
        }*/

        #endregion


        #region Private Methods

        private DataContext GetDataContext()
        {
            return new DataContext();
        }

        #endregion

        #region Public Methods

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

        //public async void SaveDictionaryAsync(int id, string name, Action<OperationResult> callback)
        //{
        //    if (callback != null)
        //    {
        //        var result = await Task<OperationResult>.Factory.StartNew(() => { return SaveDictionary(id, name); });

        //        callback(result);
        //    }
        //}

        public async Task<OperationResult> SaveDictionaryAsync(int id, string name)
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

        public List<Dictionary> GetAllDictionaries()
        {
            using (var dataContext = GetDataContext())
            {
                return dataContext.Dictionaries.ToList();
            }
        }

        public async Task<List<Dictionary>> GetAllDictionariesAsync()
        {
            using (var dataContext = GetDataContext())
            {
                return await dataContext.Dictionaries.ToListAsync();
            }
        }

        //public async void GetAllDictionariesAsync(Action<List<Dictionary>> callback)
        //{
        //    if (callback != null)
        //    {
        //        var result = await Task<List<Dictionary>>.Factory.StartNew(() => { return GetAllDictionaries(); });
        //        callback(result);
        //    }
        //}

        public List<Word> GetAllWords()
        {
            using (var dataContext = GetDataContext())
            {
                return dataContext.Words.ToList();
            }
        }

        public async void GetAllWordsAsync(Action<List<Word>> callback)
        {
            if (callback != null)
            {
                var result = await Task<List<Word>>.Factory.StartNew(() => { return GetAllWords(); });
                callback(result);
            }
        }

        public Word GetWord(int id)
        {
            using (var dataContext = GetDataContext())
            {
                return dataContext.Words.Find(id);
            }
        }

        public async void GetWordAsync(int id, Action<Word> callback)
        {
            if (callback != null)
            {
                var result = await Task<Word>.Factory.StartNew(() => { return GetWord(id); });
                callback(result);
            }
        }

        public Dictionary GetDictionary(int id)
        {
            using (var dataContext = GetDataContext())
            {
                return dataContext.Dictionaries.Find(id);
            }
        }

        public async void GetDictionaryAsync(int id, Action<Dictionary> callback)
        {
            if (callback != null)
            {
                var result = await Task<Dictionary>.Factory.StartNew(() => { return GetDictionary(id); });
                callback(result);
            }
        }

        public void SaveWord(int id, string name, string translation, int dictionaryId)
        {
            using (var dataContext = GetDataContext())
            {
                var newWord = false;
                var word = dataContext.Words.Find(id);
                if (word == null)
                {
                    word = dataContext.Words.Create();
                    newWord = true;
                }

                word.Name = name;
                word.Translation = translation;

                var dic = dataContext.Dictionaries.Find(dictionaryId);
                if (dic != null)
                {
                    word.Dictionaries.Add(dic);
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

        public void DeleteWord(int id)
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

        #endregion

        #region Events



        #endregion
        
    }
}