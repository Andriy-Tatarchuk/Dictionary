using System;
using System.Collections.ObjectModel;
using System.Windows;
using DataLayer;
using DataLayer.DataModels;
using ViewLayer.Models;

namespace ViewLayer.ViewModels
{
    public class VMWords : VMBase
    {
        #region Declarations

        #endregion

        #region Properties

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            "ItemsSource", typeof(ObservableCollection<Word>), typeof(VMWords), new PropertyMetadata(default(ObservableCollection<Word>)));

        public ObservableCollection<Word> ItemsSource
        {
            get { return (ObservableCollection<Word>) GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        #endregion

        #region Constructorss

        public VMWords(ViewManager viewManager, DataManager dm, object param) : base(viewManager, dm)
        {
            LoadWords();
        }

        #endregion


        #region Private Methods

        private async void LoadWords()
        {
            if (DM != null)
            {
                IsLoading = true;
                var words = await DM.GetAllWordsAsync();
                ItemsSource = new ObservableCollection<Word>(words);
                IsLoading = false;
            }
        }

        #endregion

        #region Public Methods

        public override bool Command_CanExecute(Command command, object param)
        {
            var res = false;
            if (command == Command.AddEditWord)
            {
                res = true;
            }
            else if (command == Command.Delete)
            {
                res = true;
            }
            else
            {
                res = base.Command_CanExecute(command, param);
            }

            return res;
        }

        public override void Command_Executed(Command command, object param)
        {
            if (command == Command.AddEditWord)
            {
                OpenScreen(ScreenId.AddEditWord, param);
            }
            else if (command == Command.Delete)
            {
                if (MessageBox.Show("Are you ssure you want delete this word?", "Confirmation", MessageBoxButton.YesNo,
                        MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    DeleteWord(param);
                }
            }
            else
            {
                base.Command_Executed(command, param);
            }
        }

        private void DeleteWord(object param)
        {
            if (DM != null)
            {
                int id = -1;
                if (param != null)
                {
                    Int32.TryParse(param.ToString(), out id);
                }
                DM.DeleteWordAsync(id);

                LoadWords();
            }
        }


        #endregion

        #region Events



        #endregion
    }
}