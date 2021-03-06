﻿using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Enigma.Autocomplete;
using Enigma.Data;
using Enigma.Entity.Entities;
using GalaSoft.MvvmLight;
using Enigma.Shell.Model;
using Enigma.Shell.Navigation;
using GalaSoft.MvvmLight.CommandWpf;
using Enigma.Translate;

namespace Enigma.Shell.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class AddEditWordViewModel : BaseViewModel
    {
        private ITranslator _Translator;

        private Word _Word;
        public Word Word
        {
            get { return _Word; }
            set
            {
                if (_Word != null)
                {
                    _Word.PropertyChanged -= Word_PropertyChanged;
                }
                _Word = value;
                _Word.PropertyChanged += Word_PropertyChanged;
                RaisePropertyChanged("Word");
            }
        }

        private ObservableCollection<Dictionary> _Dictionaries;
        public ObservableCollection<Dictionary> Dictionaries
        {
            get { return _Dictionaries; }
            set
            {
                _Dictionaries = value;
                RaisePropertyChanged("Dictionaries");
            }
        }

        private RelayCommand translateCommand;
        public RelayCommand TranslateCommand
        {
            get { return translateCommand; }
        }


        /// <summary>
        /// Initializes a new instance of the DictionariesViewModel class.
        /// </summary>
        public AddEditWordViewModel(IDataManager dataMgr, IFrameNavigationService navigationService, ITranslator translator)
            : base(dataMgr, navigationService)
        {
            translateCommand = new RelayCommand(TranslateWord);
            _Translator = translator;
        }

        private void TranslateWord()
        {
            if (_Translator != null)
            {
                Word.Translation = _Translator.Translate(Word.Name, "uk", "en");
            }
        }

        private async void Word_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var word = sender as Word;
            if (word != null && e.PropertyName == "Name")
            {
                //TranslateWord();
            }
        }

        public override async Task Save()
        {
            if (Word.DictionaryId > 0 && (!string.IsNullOrEmpty(Word.Name) || !string.IsNullOrEmpty(Word.Translation)))
            {
                await DataManager.SaveWordAsync(Word);
            }
        }

        public override async Task Navigated(object param)
        {
            await LoadDictionaries();

            if (param is Word)
            {
                Word = param as Word; 
            }
            else if (param is int)
            {
                Word = new Word();
                Word.DictionaryId = (int)param;
            }
        }

        private async Task LoadDictionaries()
        {
            var dictionaries = await DataManager.GetAllDictionariesAsync();
            if (dictionaries != null)
            {
                if (Dictionaries != null)
                {
                    Dictionaries.Clear();
                }

                Dictionaries = new ObservableCollection<Dictionary>(dictionaries);
            }
        }
    }
}