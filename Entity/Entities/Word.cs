﻿using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Enigma.Entity.Entities
{
    public class Word : INotifyPropertyChanged
    {
        #region Properties

        private int id;
        public int Id {
            get { return id; }
            set
            {
                id = value;
                FirePropertyChanged("Id");
            } }

        private string name;
        public string Name
        {
            get { return name;}
            set
            {
                name = value;
                FirePropertyChanged("Name");
            }
        }

        private string translation;
        public string Translation
        {
            get { return translation;}
            set
            {
                translation = value;
                FirePropertyChanged("Translation");
            } }

        private DateTime addingDate;
        public DateTime AddingDate
        {
            get { return addingDate; }
            private set
            {
                addingDate = value;
                FirePropertyChanged("AddingDate");
            }
        }

        private int dictionaryId;
        public int DictionaryId
        {
            get { return dictionaryId; }
            set
            {
                if (dictionaryId != value)
                {
                    dictionaryId = value;
                    FirePropertyChanged("DictionaryId");
                }
            }
        }

        #endregion

        #region Constructorss

        public Word() : this(String.Empty, String.Empty)
        {

        }

        public Word(string name, string translation)
        {
            Name = name;
            Translation = translation;
            AddingDate = DateTime.Now;
        }

        #endregion


        private void FirePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}