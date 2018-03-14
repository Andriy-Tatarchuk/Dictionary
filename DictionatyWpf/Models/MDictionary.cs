using System;
using System.Collections.Generic;

namespace DictionatyWpf.Models
{
    public class MDictionary
    {

        #region Declarations

        #endregion

        #region Properties

        public int Id { get; set; }

        public string Name { get; set; }

        public List<MWord> Words { get; private set; }

        #endregion

        #region Constructorss

        public MDictionary() : this(String.Empty)
        {
        }

        public MDictionary(string name)
        {
            Words = new List<MWord>();
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