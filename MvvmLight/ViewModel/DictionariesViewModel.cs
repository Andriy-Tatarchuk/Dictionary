using System.Collections.ObjectModel;
using DataLayer;
using EntityLayer.Entities;
using GalaSoft.MvvmLight;

namespace MvvmLight.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class DictionariesViewModel : BaseViewModel
    {
        private DataManager _DataManager;

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

        /// <summary>
        /// Initializes a new instance of the DictionariesViewModel class.
        /// </summary>
        public DictionariesViewModel(DataManager dataMgr)
        {
            _DataManager = dataMgr; 
        }

        public override void LoadData()
        {
            GetDictionaries();
        }

        public async void GetDictionaries()
        {
            if (Dictionaries != null)
            {
                Dictionaries.Clear();
            }

            IsLoading = true;
            var dictionaries = await _DataManager.GetAllDictionariesAsync();
            Dictionaries = new ObservableCollection<Dictionary>(dictionaries);
            IsLoading = false;
        }
    }
}