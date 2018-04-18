using System.Collections.ObjectModel;
using Enigma.Data;
using Enigma.Entity.Entities;
using GalaSoft.MvvmLight;
using Enigma.Shell.Model;
using Enigma.Shell.Navigation;

namespace Enigma.Shell.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class DictionariesViewModel : BaseViewModel
    {
        
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

        private Dictionary _SelectedItem;
        public Dictionary SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                _SelectedItem = value;
                RaisePropertyChanged("SelectedItem");
                SelectedDictionaryChanged();
            }
        }


        /// <summary>
        /// Initializes a new instance of the DictionariesViewModel class.
        /// </summary>
        public DictionariesViewModel(DataManager dataMgr, IFrameNavigationService navigationService)
            : base(dataMgr, navigationService)
        {
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
            var dictionaries = await DataManager.GetAllDictionariesAsync();
            Dictionaries = new ObservableCollection<Dictionary>(dictionaries);
            IsLoading = false;
        }

        private void SelectedDictionaryChanged()
        {
            var selectedId = SelectedItem != null ? SelectedItem.Id : -1;
            NavigationService.NavigateTo(ScreenId.WordsView.ToString(), selectedId);
        }
    }
}