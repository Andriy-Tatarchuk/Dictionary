using System.Collections.ObjectModel;
using DataLayer;
using DataLayer.DataModels;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace MvvmLight.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class WordsViewModel : ViewModelBase
    {
        private DataManager _DataManager;

        public RelayCommand ReadAllCommand { get; set; }

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
        /// <summary>
        /// Initializes a new instance of the WordsViewModel class.
        /// </summary>
        public WordsViewModel(DataManager dataMgr)
        {
            _DataManager = dataMgr;
            ReadAllCommand = new RelayCommand(GetWords);
        }

        public async void GetWords()
        {
            if (Words != null)
            {
                Words.Clear();
            }

            var words = await _DataManager.GetAllWordsAsync();
            Words = new ObservableCollection<Word>(words);
        }
    }
}