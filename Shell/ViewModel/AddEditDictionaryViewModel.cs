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
    public class AddEditDictionaryViewModel : BaseViewModel
    {
        private Dictionary _Dictionary;
        public Dictionary Dictionary
        {
            get { return _Dictionary; }
            set
            {
                _Dictionary = value;
                RaisePropertyChanged("Dictionary");
            }
        }

        /// <summary>
        /// Initializes a new instance of the DictionariesViewModel class.
        /// </summary>
        public AddEditDictionaryViewModel(DataManager dataMgr, IFrameNavigationService navigationService)
            : base(dataMgr, navigationService)
        {
           
        }

        public override void LoadData(object parameter)
        {
            var id = -1;
            if (parameter != null)
            {
                Int32.TryParse(parameter.ToString(), out id);
            }

            GetDictionary(id);
        }

        public override void Save()
        {
            SaveDictionary();
        }

        private async void SaveDictionary()
        {
            await DataManager.SaveDictionaryAsync(Dictionary);
        }

        private async void GetDictionary(int id)
        {
            IsLoading = true;
            var dictionary = await DataManager.GetDictionaryAsync(id);
            if(dictionary != null)
            {
                Dictionary = dictionary;
            }
            IsLoading = false;
        }

        public override void Navigated(object param)
        {
            var dictionary = param as Dictionary;
            if (dictionary != null)
            {
                Dictionary = dictionary;
            }
        }
    }
}