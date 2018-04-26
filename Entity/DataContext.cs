using Enigma.Entity.Entities;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Enigma.Entity
{
    public class DataContext : DbContext
    {
        // Your context has been configured to use a 'DataContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'DictionatyWpf.DataModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DataModel' 
        // connection string in the application configuration file.
        public DataContext()
            : base("name=DataContext")
        {
        }

        public bool Inisialize()
        {
            var res = true;
            try
            {
                Database.Connection.Open();
                //Database.CreateIfNotExists();
                if (!Dictionaries.Any())
                {
                    var newDictionary = new Dictionary("First Dictionary");
                    newDictionary.Words.Add(new Word("First Word", "Translation"));
                    Dictionaries.Add(newDictionary);

                    var numericDictionary = new Dictionary("Numbers");
                    numericDictionary.Words.Add(new Word("One", "Один"));
                    numericDictionary.Words.Add(new Word("Two", "Два"));
                    numericDictionary.Words.Add(new Word("Three", "Три"));
                    numericDictionary.Words.Add(new Word("Four", "Чотири"));
                    numericDictionary.Words.Add(new Word("Five", "П'ять"));
                    Dictionaries.Add(numericDictionary);

                    SaveChanges();
                }
            }
            catch
            {
                res = false;
            }

            return res;
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<Dictionary> Dictionaries { get; set; }
        public virtual DbSet<Word> Words { get; set; }
    }
}