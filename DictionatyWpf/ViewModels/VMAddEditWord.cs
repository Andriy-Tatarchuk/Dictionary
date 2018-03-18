using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using DictionatyWpf.Data;
using DictionatyWpf.Models;
using DictionatyWpf.Views;

namespace DictionatyWpf.ViewModels
{
    public class VMAddEditWord : VMBase
    {
        #region Declarations

        private int ID { get; set; }

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


        #endregion

        #region Constructorss

        public VMAddEditWord(DataManager dm, object param)
            : base(dm)
        {
            InitializeParams(param);
        }

        #endregion


        #region Private Methods

        private void InitializeParams(object param)
        {
            ID = -1;
            if (param != null && DM != null)
            {
                int id = -1;
                if (Int32.TryParse(param.ToString(), out id) && id >= 0)
                {
                    var word = DM.GetWord(id);
                    if (word != null)
                    {
                        ID = id;
                        Name = word.Name;
                        Translation = word.Translation;
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
                OpenScreen(ScreenId.Words);
            }
            else if(command == Command.Cancel)
            {
                OpenScreen(ScreenId.Words);
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
                DM.SaveWord(ID, Name, Translation);
            }
        }


        #endregion

        #region Public Methods




        #endregion

        #region Events



        #endregion
    }
}