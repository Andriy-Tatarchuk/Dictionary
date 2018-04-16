using System.Collections.ObjectModel;
using DataLayer;
using EntityLayer.Entities;
using GalaSoft.MvvmLight.Command;
using MvvmLight.Navigation;

namespace MvvmLight.ViewModel
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

        /// <summary>
        /// Initializes a new instance of the WordsViewModel class.
        /// </summary>
        public WordsViewModel(DataManager dataMgr, IFrameNavigationService navigationService)
            : base(dataMgr, navigationService)
        {
        }

        public override void LoadData()
        {
            GetWords();
        }

        public async void GetWords(int dicId = -1)
        {
            if (Words != null)
            {
                Words.Clear();
            }

            IsLoading = true;
            var words = dicId >= 0 ? await DataManager.GetWordsByDicAsync(dicId) : await DataManager.GetAllWordsAsync();
            Words = new ObservableCollection<Word>(words);
            IsLoading = false;
        }

        public override void Navigated(object param)
        {
            var dicId = param is int ? (int) param : -1;
            GetWords(dicId);
        }
    }
}