using System;
using System.Collections.ObjectModel;
using Enigma.Data;
using Enigma.Entity.Entities;
using GalaSoft.MvvmLight;
using Enigma.Shell.Model;
using Enigma.Shell.Navigation;
using GalaSoft.MvvmLight.Command;

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
        private Word _Word;
        public Word Word
        {
            get { return _Word; }
            set
            {
                _Word = value;
                RaisePropertyChanged("Word");
            }
        }

        /// <summary>
        /// Initializes a new instance of the DictionariesViewModel class.
        /// </summary>
        public AddEditWordViewModel(DataManager dataMgr, IFrameNavigationService navigationService)
            : base(dataMgr, navigationService)
        {
           Word = new Word();
        }

        public override void LoadData(object parameter)
        {
            var id = -1;
            if (parameter != null)
            {
                Int32.TryParse(parameter.ToString(), out id);
            }

            GetWord(id);
        }

        public override void Save()
        {
            DataManager.SaveWordAsync(Word);
        }

        private async void GetWord(int id)
        {
            IsLoading = true;
            var word = await DataManager.GetWordAsync(id);
            if (word != null)
            {
                Word = word;
            }
            IsLoading = false;
        }

        public override void Navigated(object param)
        {
            var word = param as Word;

            if (word != null)
            {
                Word = word;
            }
        }
    }
}