using System;
using System.Threading.Tasks;
using CommonServiceLocator;
using Enigma.Data;
using Enigma.Entity.Entities;
using Enigma.Shell.Navigation;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Command;

namespace Enigma.Shell.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ExamDictionaryViewModel : BaseViewModel
    {
        private List<Word> Words;
        private int CurrentWordIndex = 0;

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

        private Word _CurrentWord;
        public Word CurrentWord
        {
            get { return _CurrentWord; }
            set
            {
                _CurrentWord = value;
                RaisePropertyChanged("CurrentWord");
            }
        }

        private string _TestValue1;
        public string TestValue1
        {   
            get { return _TestValue1; }
            set 
            {
                _TestValue1 = value;
                RaisePropertyChanged("TestValue1");
            }
        }

        private string _TestValue2;
        public string TestValue2
        {
            get { return _TestValue2; }
            set 
            {
                _TestValue2 = value;
                RaisePropertyChanged("TestValue2");
            }
        }

        private string _TestValue3;
        public string TestValue3
        {
            get { return _TestValue3; }
            set 
            {
                _TestValue3 = value;
                RaisePropertyChanged("TestValue3");
            }
        }

        private string _TestValue4;
        public string TestValue4
        {
            get { return _TestValue4; }
            set 
            {
                _TestValue4 = value;
                RaisePropertyChanged("TestValue4");
            }
        }

        public RelayCommand NextWordCommand { get; private set; }

        /// <summary>
        /// Initializes a new instance of the DictionariesViewModel class.
        /// </summary>
        public ExamDictionaryViewModel(IDataManager dataMgr, IFrameNavigationService navigationService)
            : base(dataMgr, navigationService)
        {
            NextWordCommand = new RelayCommand(NextWord);
        }

        public override async Task Navigated(object param)
        {
            var dictionary = param as Dictionary;
            if (dictionary != null)
            {
                Dictionary = dictionary;
                Header = "Exam for dictionary \"" + Dictionary.Name + "\"";
                await GetWords();

                StartExam();
            }
        }

        private async Task GetWords()
        {
            IncRequestCounter();

            if (Words != null)
            {
                Words.Clear();
            }

            Words = await DataManager.SearchWordsAsync("", CurrentDictionaryId);

            DecRequestCounter();
        }

        private void StartExam()
        {
            Words.Shuffle();
            CurrentWordIndex = 0;
            CurrentWord = null;
            TestValue1 = TestValue2 = TestValue3 = TestValue4 = String.Empty;

            NextWord();
        }

        private void NextWord()
        {
            if (CurrentWordIndex < Words.Count && Words.Count >= 4)
            {
                CurrentWord = Words[CurrentWordIndex];
                var rnd = new Random();
                var indexes = new List<int>();
                for (int i = 0; i < 4; )
                {
                    var index = rnd.Next(0, Words.Count);
                    if (!indexes.Contains(index))
                    {
                        indexes.Add(index);
                        i++;
                    }
                }
                if (!indexes.Contains(CurrentWordIndex))
                {
                    indexes[rnd.Next(0, 4)] = CurrentWordIndex;
                }
                TestValue1 = Words[indexes[0]].Translation;
                TestValue2 = Words[indexes[1]].Translation;
                TestValue3 = Words[indexes[2]].Translation;
                TestValue4 = Words[indexes[3]].Translation;
                CurrentWordIndex++;
            }
        }

    }

    public static class IListExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            Random rnd = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}