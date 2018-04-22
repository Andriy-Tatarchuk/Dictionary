using System.Collections.ObjectModel;
using System.Linq;
using Enigma.Data;
using Enigma.Entity.Entities;
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
                var isChanged = _SelectedItem != value;
                if (isChanged && !IsLoading)
                {
                    _SelectedItem = value;
                    RaisePropertyChanged("SelectedItem");
                    SelectedDictionaryChanged();
                }
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

        public RelayCommand DeleteDictionaryCommand
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
            AddDictionaryCommand = new RelayCommand(AddDictionaryCommandExecuted);

            EditDictionaryCommand = new RelayCommand(() =>
            {
                if (SelectedItem != null)
                {
                    NavigationService.NavigateTo(ScreenId.AddEditDictionaryView.ToString(), SelectedItem);
                }
            });

            DeleteDictionaryCommand = new RelayCommand(DeleteDictionaryCommandExecuted);
        }

        private async void DeleteDictionaryCommandExecuted()
        {
            if (SelectedItem != null)
            {
                await DataManager.DeleteDictionaryAsync(SelectedItem.Id);
                GetDictionaries();
            }
        }

        private async void AddDictionaryCommandExecuted()
        {
            SelectedItem = null;
            var newDictionary = await DataManager.AddDictionaryAsynk("New dictionary");
            NavigationService.NavigateTo(ScreenId.AddEditDictionaryView.ToString(), newDictionary);
        }

        public override void LoadData(object parameter)
        {
            GetDictionaries();
        }

        public async void GetDictionaries()
        {
            var _selectedItem = SelectedItem;

            if (Dictionaries != null)
            {
                Dictionaries.Clear();
            }

            IsLoading = true;
            var dictionaries = await DataManager.GetAllDictionariesAsync();
            Dictionaries = new ObservableCollection<Dictionary>(dictionaries);
            IsLoading = false;

            if (_selectedItem != null)
            {
                SelectedItem = Dictionaries.FirstOrDefault(d => d.Id == _selectedItem.Id);
            }
        }

        private void SelectedDictionaryChanged()
        {
            var selectedId = SelectedItem != null ? SelectedItem.Id : -1;
            NavigationService.NavigateTo(ScreenId.WordsView.ToString(), selectedId);
        }
    }
}