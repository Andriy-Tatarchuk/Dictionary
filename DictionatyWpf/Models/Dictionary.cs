using System;
using System.Collections.Generic;

namespace DictionatyWpf.Models
{
    public class Dictionary
    {

        #region Declarations

        #endregion

        #region Properties

        public int Id { get; set; }

        public string Name { get; set; }

        public List<Word> Words { get; private set; }

        #endregion

        #region Constructorss

        public Dictionary() : this(String.Empty)
        {
        }

        public Dictionary(string name)
        {
            Words = new List<Word>();
            Name = name;
        }

        #endregion


        #region Private Methods



        #endregion

        #region Public Methods




        #endregion

        #region Events



        #endregion

        enum OrderType
        {
            DateOrder,
            Alphabetic,
            Random
        }

    }
}