using System.Collections.Generic;
using System.Linq;
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

        public Dictionary AddDictionary(string name)
        {
            using (var dataContext = GetDataContext())
            {
                if (!dataContext.Dictionaries.ToList().Exists(d => d.Name == name))
                {
                    var dic = new Dictionary(name);
                    dataContext.Dictionaries.Add(dic);
                    dataContext.SaveChanges();
                    return dic;
                }
            }
            return null;
        }

        public void AddWord(string name, string translation)
        {
            using (var dataContext = GetDataContext())
            {
                //if (!dataContext.Words.ToList().Exists(w => w.Name == name))
                {
                    dataContext.Words.Add(new Word(name, translation));
                    dataContext.SaveChanges();
                }
            }
        }

        public void AddWordToDictionary(int id, Word word)
        {
            using (var dataContext = GetDataContext())
            {
                if (dataContext.Dictionaries.Find(id) != null)
                {
                    dataContext.Dictionaries.Find(id).Words.Add(word);
                    dataContext.SaveChanges();
                }
            }
        }

        public List<Dictionary> GetAllDictionaries()
        {
            using (var dataContext = GetDataContext())
            {
                return dataContext.Dictionaries.ToList();
            }
        }

        public List<Word> GetAllWords()
        {
            using (var dataContext = GetDataContext())
            {
                return dataContext.Words.ToList();
            }
        }

        #endregion

        #region Events



        #endregion


    }
}