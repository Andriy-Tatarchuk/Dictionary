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

        public DataContext DataContext { get; private set; }

        #endregion

        #region Constructorss

        public DataManager(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        #endregion


        #region Private Methods



        #endregion

        #region Public Methods

        public MDictionary AddDictionary(string name)
        {
            var dic = new MDictionary(name);
            DataContext.Dictionaries.Add(dic);
            return dic;
        }

        public void AddWord(string name, string translation)
        {
            if (!DataContext.Words.ToList().Exists(w => w.Name == name))
            {
                DataContext.Words.Add(new MWord(name, translation));
            }
        }

        public void AddWordToDictionary(int id, MWord word)
        {
            if (DataContext.Dictionaries.Find(id) != null)
            {
                DataContext.Dictionaries.Find(id).Words.Add(word);
            }
        }

        public List<MDictionary> GetAllDictionaries()
        {
            return DataContext.Dictionaries.ToList();
        }

        public List<MWord> GetAllWords()
        {
            return DataContext.Words.ToList();
        }

        public void Save()
        {
            DataContext.SaveChanges();
        }
        #endregion

        #region Events



        #endregion


    }
}