using System;
using System.Linq;
using System.Threading.Tasks;
using CommonServiceLocator;
using Enigma.Data;
using Enigma.Entity.Entities;
using Enigma.Shell.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
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

        private Answer _Answer1;
        public Answer Answer1
        {
            get { return _Answer1; }
            set
            {
                _Answer1 = value;
                RaisePropertyChanged("Answer1");
            }
        }

        private Answer _Answer2;
        public Answer Answer2
        {
            get { return _Answer2; }
            set
            {
                _Answer2 = value;
                RaisePropertyChanged("Answer2");
            }
        }

        private Answer _Answer3;
        public Answer Answer3
        {
            get { return _Answer3; }
            set
            {
                _Answer3 = value;
                RaisePropertyChanged("Answer3");
            }
        }

        private Answer _Answer4;
        public Answer Answer4
        {
            get { return _Answer4; }
            set
            {
                _Answer4 = value;
                RaisePropertyChanged("Answer4");
            }
        }

        private List<Answer> Answers { get; set; }

        public RelayCommand NextWordCommand { get; private set; }
        public RelayCommand<string> ShowAnswerCommand { get; private set; }

        /// <summary>
        /// Initializes a new instance of the DictionariesViewModel class.
        /// </summary>
        public ExamDictionaryViewModel(IDataManager dataMgr, IFrameNavigationService navigationService)
            : base(dataMgr, navigationService)
        {
            NextWordCommand = new RelayCommand(NextWord);
            ShowAnswerCommand = new RelayCommand<string>(ShowAnswer);
            Answers = new List<Answer>();
        }

        private void ShowAnswer(string tag)
        {
            var index = 0;
            if (Int32.TryParse(tag, out index))
            {
                if (!Answers[index].IsCorrect)
                {
                    Answers[index].Background = Brushes.Red;
                }

                Answers.FirstOrDefault(a => a.IsCorrect).Background = Brushes.Green;
            }
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
            Answers.Clear();
            Answer1 = Answer2 = Answer3 = Answer4 = null;

            NextWord();
        }

        private void NextWord()
        {
            if (CurrentWordIndex < Words.Count && Words.Count >= 4)
            {
                CurrentWord = Words[CurrentWordIndex];
                var rnd = new Random();
                Answers.Clear();
                for (int i = 0; i < 4; )
                {
                    var index = rnd.Next(0, Words.Count);
                    if (!Answers.Any(a=> a.Text == Words[index].Translation))
                    {
                        Answers.Add(new Answer(Words[index].Translation));
                        i++;
                    }
                }
                if (!Answers.Any(a=> a.Text == Words[CurrentWordIndex].Translation))
                {
                    Answers[rnd.Next(0, 4)] = new Answer(Words[CurrentWordIndex].Translation){IsCorrect = true};
                }
                else
                {
                    Answers.FirstOrDefault(a => a.Text == Words[CurrentWordIndex].Translation).IsCorrect = true;
                }
                Answer1 = Answers[0];
                Answer2 = Answers[1];
                Answer3 = Answers[2];
                Answer4 = Answers[3];
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

    public class Answer : INotifyPropertyChanged
    {
        public bool IsCorrect { get; set; }
        public string Text { get; set; }
        private Brush _Background;

        public Brush Background
        {
            get { return _Background; }
            set
            {
                _Background = value;
                OnPropertyChanged();
            }
        }

        public Answer()
        {
            IsCorrect = false;
        }

        public Answer(string text)
        {
            Text = text;
            IsCorrect = false;
            Background = Brushes.Gray;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}