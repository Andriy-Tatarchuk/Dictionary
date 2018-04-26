using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Enigma.Data;
using Enigma.Entity.Entities;
using Enigma.Shell.Model;
using GalaSoft.MvvmLight.Command;
using Enigma.Shell.Navigation;

namespace Enigma.Shell.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class WordsViewModel : BaseViewModel
    {
        ObservableCollection<Word> _Words;

        public ObservableCollection<Word> Words
        {
            get { return _Words; }
            set
            {
                _Words = value;
                RaisePropertyChanged("Words");
            }
        }

        private Word _SelectedItem;
        public Word SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                _SelectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }

        private string SearchText;
        private int DictionaryId { get; set; }

        public RelayCommand AddWordCommand { get; private set; }
        public RelayCommand EditWordCommand { get; private set; }
        public RelayCommand DeleteWordCommand { get; private set; }

        /// <summary>
        /// Initializes a new instance of the WordsViewModel class.
        /// </summary>
        public WordsViewModel(DataManager dataMgr, IFrameNavigationService navigationService)
            : base(dataMgr, navigationService)
        {
            InitializeCommands();
            DictionaryId = -1;
        }

        private void InitializeCommands()
        {
            AddWordCommand = new RelayCommand(ExecuteAddWordCommand);

            EditWordCommand = new RelayCommand(ExecuteEditWordCommand);

            DeleteWordCommand = new RelayCommand(ExecuteDeleteWordCommand);
        }

        private async void ExecuteEditWordCommand()
        {
            if (SelectedItem != null)
            {
                var word = await GetWordById(SelectedItem.Id);
                NavigationService.NavigateTo(ScreenId.AddEditWordView.ToString(), word);
            }
        }

        private async void ExecuteDeleteWordCommand()
        {
            if (SelectedItem != null)
            {
                await DataManager.DeleteWordAsync(SelectedItem.Id);
                GetWords();
            }
        }

        private async void ExecuteAddWordCommand()
        {
            NavigationService.NavigateTo(ScreenId.AddEditWordView.ToString(), DictionaryId);
        }

        private async Task<Word> GetWordById(int id)
        {
            return await DataManager.GetWordAsync(id);
        }

        public override void LoadData(object parameter)
        {
            ParseParam(parameter);
        }

        public async Task GetWords()
        {
            IsLoading = true;

            if (Words != null)
            {
                Words.Clear();
            }

            var words = await DataManager.SearchWordsAsync(SearchText, DictionaryId);

            Words = words != null ? new ObservableCollection<Word>(words) : null;

            IsLoading = false;
        }

        public override async Task Navigated(object param)
        {
            ParseParam(param);
        }

        private void ParseParam(object param)
        {
            if (param is SearchData)
            {
                var searchData = param as SearchData;
                DictionaryId = searchData.DictionaryId;
                SearchText = searchData.SearchText;
            }
            else
            {
                DictionaryId = param is int ? (int)param : -1;
                SearchText = String.Empty;
            }
            GetWords();
        }
    }
}