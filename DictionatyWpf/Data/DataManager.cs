using System;
using System.Collections.Generic;
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

        public OperationResult SaveDictionary(int id, string name)
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

        public async void SaveDictionaryAsync(int id, string name, Action<OperationResult> callback)
        {
            if (callback != null)
            {
                var result = await Task<OperationResult>.Factory.StartNew(() => { return SaveDictionary(id, name); });

                callback(result);
            }
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

        public async void GetAllDictionariesAsync(Action<List<Dictionary>> callback)
        {
            if (callback != null)
            {
                var result = await Task<List<Dictionary>>.Factory.StartNew(() => { return GetAllDictionaries(); });
                callback(result);
            }
        }

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

        public void SaveWord(int id, string name, string translation)
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

                if (newWord)
                {
                    dataContext.Words.Add(word);
                }

                dataContext.SaveChanges();
            }
        }

        public async void SaveWordAsync(int id, string name, string translation)
        {
            await Task.Factory.StartNew(() => { SaveWord(id, name, translation); });
        }

        #endregion

        #region Events



        #endregion
        
    }
}