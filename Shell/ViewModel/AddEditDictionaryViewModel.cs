using System;
using System.Threading.Tasks;
using System.Windows;
using CommonServiceLocator;
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
    public class AddEditDictionaryViewModel : BaseViewModel
    {
        private Dictionary _Dictionary;
        public Dictionary Dictionary
        {
            get { return _Dictionary; }
            set
            {
                _Dictionary = value;
                RaisePropertyChanged("Dictionary");
                CurrentDictionaryId = _Dictionary != null ? _Dictionary.Id : -1;
            }
        }

        private int _CurrentDictionaryId;

        public int CurrentDictionaryId
        {
            get { return _CurrentDictionaryId;}
            set
            {
                _CurrentDictionaryId = value;
                RaisePropertyChanged("CurrentDictionaryId");
            }
        }

        public RelayCommand ExamDictionaryCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the DictionariesViewModel class.
        /// </summary>
        public AddEditDictionaryViewModel(IDataManager dataMgr, IFrameNavigationService navigationService)
            : base(dataMgr, navigationService)
        {
            ExamDictionaryCommand = new RelayCommand(ExamDictionaryCommandExecuted);
        }

        private async void ExamDictionaryCommandExecuted()
        {
            var wordsCount = await DataManager.GetWordsCountAsync(CurrentDictionaryId);
            if (wordsCount >= 4)
            {
                NavigationService.NavigateTo(ScreenId.ExamDictionaryView.ToString(), Dictionary);
            }
            else
            {
                MessageBox.Show("You need four or more words in the dictionary to start exam",
                           "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        public override void LoadData(object parameter)
        {
            var id = -1;
            if (parameter != null)
            {
                Int32.TryParse(parameter.ToString(), out id);
            }

            GetDictionary(id);
        }

        public override async Task Save()
        {
            await DataManager.SaveDictionaryAsync(Dictionary);
            //ServiceLocator.Current.GetInstance<DictionariesViewModel>().LoadData(null);
        }

        private async void GetDictionary(int id)
        {
            IncRequestCounter();
            var dictionary = await DataManager.GetDictionaryAsync(id);
            if(dictionary != null)
            {
                Dictionary = dictionary;
            }
            DecRequestCounter();
        }

        public override async Task Navigated(object param)
        {
            var dictionary = param as Dictionary;
            if (dictionary != null)
            {
                Dictionary = dictionary;
            }
        }
    }
}