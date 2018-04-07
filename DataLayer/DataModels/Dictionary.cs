using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DataLayer.DataModels
{
    public class Dictionary : INotifyPropertyChanged
    {

        #region Declarations

        #endregion

        #region Properties

        private int id;
        public int Id 
        { 
            get{ return id;}
            set
            {
                id = value;
                FirePropertyChanged("Id");
            } 
        }

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                FirePropertyChanged("Name");

            }
        }

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

        private void FirePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        enum OrderType
        {
            DateOrder,
            Alphabetic,
            Random
        }

    }
}