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
    public class SearchWordsViewModel : BaseViewModel
    {
        private SearchData _SearchData;
        public SearchData SearchData
        {
            get { return _SearchData; }
            set
            {
                _SearchData = value;
                RaisePropertyChanged("SearchData");
            }
        }

      
        public RelayCommand SearchCommand { get; private set; }

        /// <summary>
        /// Initializes a new instance of the WordsViewModel class.
        /// </summary>
        public SearchWordsViewModel(DataManager dataMgr, IFrameNavigationService navigationService)
            : base(dataMgr, navigationService)
        {
            InitializeCommands();
            SearchData = new SearchData(){DictionaryId = -1};
        }

        private void InitializeCommands()
        {
            SearchCommand = new RelayCommand(ExecuteSearchCommand);
        }

        private async void ExecuteSearchCommand()
        {
            if (!string.IsNullOrEmpty(SearchData.SearchText))
            {
                NavigationService.NavigateTo(ScreenId.WordsView.ToString(), SearchData);
            }
        }
    }
}