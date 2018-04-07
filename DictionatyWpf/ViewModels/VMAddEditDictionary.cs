using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using DictionatyWpf.Data;
using DictionatyWpf.Models;
using DictionatyWpf.Views;

namespace DictionatyWpf.ViewModels
{
    public class VMAddEditDictionary : VMBase
    {
        #region Declarations


        #endregion

        #region Properties

        public static readonly DependencyProperty IDProperty = DependencyProperty.Register(
            "ID", typeof(int), typeof(VMAddEditDictionary), new PropertyMetadata(default(int)));

        public int ID
        {
            get { return (int) GetValue(IDProperty); }
            set { SetValue(IDProperty, value); }
        }

        public static readonly DependencyProperty NameProperty = DependencyProperty.Register(
            "Name", typeof(string), typeof(VMAddEditDictionary), new PropertyMetadata(default(string)));

        public string Name
        {
            get { return (string) GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public static readonly DependencyProperty WordsProperty = DependencyProperty.Register(
            "Words", typeof(ObservableCollection<Word>), typeof(VMAddEditDictionary), new PropertyMetadata(default(ObservableCollection<Word>)));

        public ObservableCollection<Word> Words
        {
            get { return (ObservableCollection<Word>) GetValue(WordsProperty); }
            set { SetValue(WordsProperty, value); }
        }

        #endregion

        #region Constructorss

        public VMAddEditDictionary(ViewManager viewManager, DataManager dm, object param)
            : base(viewManager, dm)
        {
            InitializeParams(param);
        }

        #endregion


        #region Private Methods

        private async void InitializeParams(object param)
        {
            if (param != null && DM != null)
            {
                int id;
                if (Int32.TryParse(param.ToString(), out id))
                {
                    var dic = await DM.GetDictionaryAsync(id);
                    if (dic != null)
                    {
                        ID = id;
                        Name = dic.Name;
                        Words = new ObservableCollection<Word>(dic.Words);
                    }
                }
            }
        }

        public override bool Command_CanExecute(Command command, object param)
        {
            var res = false;
            if (command == Command.Save)
            {
                res = true;
            }
            else if(command == Command.Cancel)
            {
                res = true;
            }
            else if (command == Command.AddWordToDic)
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
                SaveDictionary();
            }
            else if(command == Command.Cancel)
            {
                OpenScreen(ScreenId.Dictionaries);
            }
            else if (command == Command.AddWordToDic)
            {
                OpenScreen(ScreenId.AddWordToDictionary, param);
            }
            else
            {
                base.Command_Executed(command, param);
            }
        }

        private async void SaveDictionary()
        {
            if (DM != null && !string.IsNullOrEmpty(Name))
            {
                var oRes = await DM.SaveDictionaryAsync(ID, Name);

                if (!oRes.HasError)
                {
                    OpenScreen(ScreenId.Dictionaries);
                }
                else
                {
                    HasError = true;
                    Message = oRes.Message;
                }
            }
        }


        #endregion

        #region Public Methods




        #endregion

        #region Events



        #endregion
    }
}