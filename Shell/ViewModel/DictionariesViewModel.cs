using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
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

        public RelayCommand ExamDictionaryCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the DictionariesViewModel class.
        /// </summary>
        public DictionariesViewModel(IDataManager dataMgr, IFrameNavigationService navigationService)
            : base(dataMgr, navigationService)
        {
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            AddDictionaryCommand = new RelayCommand(AddDictionaryCommandExecuted);

            EditDictionaryCommand = new RelayCommand(SelectedDictionaryChanged);

            DeleteDictionaryCommand = new RelayCommand(DeleteDictionaryCommandExecuted);

            ExamDictionaryCommand = new RelayCommand(ExamDictionaryCommandExecuted);
        }

        private void ExamDictionaryCommandExecuted()
        {
            var selectedId = SelectedItem != null ? SelectedItem.Id : -1;
            if (selectedId >= 0)
            {
                NavigationService.NavigateTo(ScreenId.ExamDictionaryView.ToString(), SelectedItem);
            }
        }

        private async void DeleteDictionaryCommandExecuted()
        {
            if (SelectedItem != null)
            {
                if (AskForDeleting(SelectedItem.Name))
                {
                    var currentItem = SelectedItem;
                    SelectedItem = null;
                    IncRequestCounter();
                    await DataManager.DeleteDictionaryAsync(currentItem.Id);
                    DecRequestCounter();

                    Dictionaries.Remove(currentItem);
                }
            }
        }

        private bool AskForDeleting(string dictionaryName)
        {
            return MessageBox.Show(String.Format("Are you ssure to delte dictionary {0}", dictionaryName),
                       "Deleting dictionary", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) ==
                   MessageBoxResult.Yes;
        }

        private async void AddDictionaryCommandExecuted()
        {
            //SelectedItem = null;
            var newDictionary = await DataManager.AddDictionaryAsynk("New dictionary");
            Dictionaries.Add(newDictionary);
            SelectedItem = newDictionary;
            NavigationService.NavigateTo(ScreenId.AddEditDictionaryView.ToString(), newDictionary);

        }

        public override void LoadData(object parameter)
        {
            GetDictionaries();
        }

        private async Task GetDictionaries()
        {
            var _selectedItem = SelectedItem;

            IncRequestCounter();

            if (Dictionaries != null)
            {
                Dictionaries.Clear();
            }

            var dictionaries = await DataManager.GetAllDictionariesAsync();
            if (dictionaries != null)
            {
                Dictionaries = new ObservableCollection<Dictionary>(dictionaries);
            }
            else
            {
                Dictionaries = new ObservableCollection<Dictionary>();                
            }
            Dictionaries.Insert(0, new Dictionary("All words"){Id = -1});

            if (_selectedItem != null)
            {
                SelectedItem = Dictionaries.FirstOrDefault(d => d.Id == _selectedItem.Id);
            }

            DecRequestCounter();
        }

        private void SelectedDictionaryChanged()
        {
            var selectedId = SelectedItem != null ? SelectedItem.Id : -1;
            if (selectedId >= 0)
            {
                NavigationService.NavigateTo(ScreenId.AddEditDictionaryView.ToString(), SelectedItem);
            }
            else
            {
                NavigationService.NavigateTo(ScreenId.WordsView.ToString(), selectedId);                
            }
        }
    }
}