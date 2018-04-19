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

        public RelayCommand AddDictionaryCommand
        {
            get;
            private set;
        }

        public RelayCommand EditDictionaryCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the DictionariesViewModel class.
        /// </summary>
        public DictionariesViewModel(DataManager dataMgr, IFrameNavigationService navigationService)
            : base(dataMgr, navigationService)
        {
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            AddDictionaryCommand = new RelayCommand(() =>
            {
                NavigationService.NavigateTo(ScreenId.AddEditDictionaryView.ToString(), new Dictionary());
            });

            EditDictionaryCommand = new RelayCommand(() =>
            {
                if (SelectedItem != null)
                {
                    NavigationService.NavigateTo(ScreenId.AddEditDictionaryView.ToString(), SelectedItem);
                }
            });
        }

        public override void LoadData(object parameter)
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