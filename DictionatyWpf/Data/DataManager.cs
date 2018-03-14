using System.Linq;
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

        public void AddDictionary(string name)
        {
            DataContext.Dictionaries.Add(new MDictionary(name));
        }

        public void AddWordToDictionary(int id, MWord word)
        {
            if (DataContext.Dictionaries.Find(id) != null)
            {
                DataContext.Dictionaries.Find(id).Words.Add(word);
            }
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