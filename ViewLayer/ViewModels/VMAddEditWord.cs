using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using DataLayer;
using DataLayer.DataModels;
using ViewLayer.Models;
using ViewLayer.Views;

namespace ViewLayer.ViewModels
{
    public class VMAddEditWord : VMBase
    {
        #region Declarations

        private int ID { get; set; }
        private int DictionaryID { get; set; }

        #endregion

        #region Properties

        public static readonly DependencyProperty NameProperty = DependencyProperty.Register(
            "Name", typeof(string), typeof(VMAddEditWord), new PropertyMetadata(default(string)));

        public string Name
        {
            get { return (string) GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public static readonly DependencyProperty TranslationProperty = DependencyProperty.Register(
            "Translation", typeof(string), typeof(VMAddEditWord), new PropertyMetadata(default(string)));

        public string Translation
        {
            get { return (string) GetValue(TranslationProperty); }
            set { SetValue(TranslationProperty, value); }
        }

        public static readonly DependencyProperty IsAddToDictionaryProperty = DependencyProperty.Register(
            "IsAddToDictionary", typeof(bool), typeof(VMAddEditWord), new PropertyMetadata(default(bool)));

        public bool IsAddToDictionary
        {
            get { return (bool) GetValue(IsAddToDictionaryProperty); }
            set { SetValue(IsAddToDictionaryProperty, value); }
        }

        public static readonly DependencyProperty DictionariesProperty = DependencyProperty.Register(
            "Dictionaries", typeof(ObservableCollection<Dictionary>), typeof(VMAddEditWord), new PropertyMetadata(default(ObservableCollection<Dictionary>)));

        public ObservableCollection<Dictionary> Dictionaries
        {
            get { return (ObservableCollection<Dictionary>)GetValue(DictionariesProperty); }
            set { SetValue(DictionariesProperty, value); }
        }

        #endregion

        #region Constructorss

        public VMAddEditWord(ViewManager viewManager, DataManager dm, object param, bool isAddToDictionary = false)
            : base(viewManager, dm)
        {
            IsAddToDictionary = isAddToDictionary;

            if (!isAddToDictionary)
            {
                InitializeParams(param);
            }
            else
            {
                int id = -1;
                if (param != null)
                {
                    Int32.TryParse(param.ToString(), out id);
                }
                DictionaryID = id;
            }
        }

        #endregion


        #region Private Methods

        private async void InitializeParams(object param)
        {
            if (param != null && DM != null)
            {
                int id;
                if (Int32.TryParse(param.ToString(), out id) && id >= 0)
                {
                    var word = await DM.GetWordAsync(id);
                    if (word != null)
                    {
                        ID = id;
                        Name = word.Name;
                        Translation = word.Translation;
                        Dictionaries = new ObservableCollection<Dictionary>(word.Dictionaries);
                    }
                }
            }
        }

        public override bool Command_CanExecute(Command command, object param)
        {
            var res = false;
            if (command == Command.Save)
            {
                res = DM != null && !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Translation);
            }
            else if(command == Command.Cancel)
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
            if (command == Command.Save)
            {
                SaveWord();
                OpenNextScreen();
            }
            else if(command == Command.Cancel)
            {
                OpenNextScreen();
            }
            else
            {
                base.Command_Executed(command, param);
            }
        }

        private void SaveWord()
        {
            if (DM != null && !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Translation))
            {
                DM.SaveWordAsync(ID, Name, Translation, DictionaryID);
            }
        }

        private void OpenNextScreen()
        {
            if (!IsAddToDictionary)
            {
                OpenScreen(ScreenId.Words);
            }
            else
            {
                OpenScreen(ScreenId.AddEditDictionary, DictionaryID);
            }
        }

        #endregion

        #region Public Methods




        #endregion

        #region Events



        #endregion
    }
}