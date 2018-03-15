using System;
using System.Collections.Generic;

namespace DictionatyWpf.Models
{
    public class MWord
    {

        #region Declarations

        #endregion

        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Translation { get; set; }
        public DateTime AddingDate { get; private set; }

        public List<MDictionary> Dictionaries { get; set; }

        #endregion

        #region Constructorss

        public MWord() : this(String.Empty, String.Empty)
        {

        }

        public MWord(string name, string translation)
        {
            Name = name;
            Translation = translation;
            AddingDate = DateTime.Now;

            Dictionaries = new List<MDictionary>();
        }

        #endregion


        #region Private Methods



        #endregion

        #region Public Methods




        #endregion

        #region Events



        #endregion


    }
}