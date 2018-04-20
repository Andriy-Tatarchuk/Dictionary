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

        private int DictionaryId { get; set; }
        private Dictionary Dictionary { get; set; }

        public RelayCommand AddWordCommand { get; private set; }
        public RelayCommand EditWordCommand { get; private set; }

        /// <summary>
        /// Initializes a new instance of the WordsViewModel class.
        /// </summary>
        public WordsViewModel(DataManager dataMgr, IFrameNavigationService navigationService)
            : base(dataMgr, navigationService)
        {
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            AddWordCommand = new RelayCommand(() =>
            {
                //var word = new Word();
                //if (Dictionary != null)
                //{
                //    word.Dictionaries.Add(Dictionary);
                //}

                var word = DataManager.GetNewWordFormDictionary(DictionaryId);
                NavigationService.NavigateTo(ScreenId.AddEditWordView.ToString(), word);
            });

            EditWordCommand = new RelayCommand(() =>
            {
                var word = GetWordById(SelectedItem.Id);
                NavigationService.NavigateTo(ScreenId.AddEditWordView.ToString(), word);
            });
        }

        private async Task<Word> GetWordById(int id)
        {
            return await DataManager.GetWordAsync(id);
        }

        public override void LoadData(object parameter)
        {
            var id = -1;
            if (parameter != null)
            {
                Int32.TryParse(parameter.ToString(), out id);
            }

            GetWords(id);
        }

        public async void GetWords(int dictionaryId = -1)
        {
            DictionaryId = dictionaryId;
            if (dictionaryId >= 0)
            {
                Dictionary = await DataManager.GetDictionaryAsync(dictionaryId);
            }
            if (Words != null)
            {
                Words.Clear();
            }

            IsLoading = true;
            var words = dictionaryId >= 0 ? await DataManager.GetWordsByDicAsync(dictionaryId) : await DataManager.GetAllWordsAsync();
            Words = new ObservableCollection<Word>(words);
            IsLoading = false;
        }

        public override void Navigated(object param)
        {
            var dictionaryId = param is int ? (int) param : -1;
            GetWords(dictionaryId);
        }
    }
}