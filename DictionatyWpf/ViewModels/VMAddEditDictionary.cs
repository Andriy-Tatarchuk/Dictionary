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

        private int ID { get; set; }

        #endregion

        #region Properties

        public static readonly DependencyProperty NameProperty = DependencyProperty.Register(
            "Name", typeof(string), typeof(VMAddEditDictionary), new PropertyMetadata(default(string)));

        public string Name
        {
            get { return (string) GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }


        #endregion

        #region Constructorss

        public VMAddEditDictionary(DataManager dm, object param)
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
                    DM.GetDictionaryAsync(id, dic =>
                    {
                        if (dic != null)
                        {
                            ID = id;
                            Name = dic.Name;
                        }
                    });
                    
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
                SaveDictionary();
            }
            else if(command == Command.Cancel)
            {
                OpenScreen(ScreenId.Dictionaries);
            }
            else
            {
                base.Command_Executed(command, param);
            }
        }

        private void SaveDictionary()
        {
            if (DM != null && !string.IsNullOrEmpty(Name))
            {
                DM.SaveDictionaryAsync(ID, Name, oRes =>
                {
                    //if (!oRes.HasError)
                    //{
                        OpenScreen(ScreenId.Dictionaries);
                    //}
                    //else
                    //{
                    //    HasError = true;
                    //    Message = oRes.Message;
                    //}
                });
            }
        }


        #endregion

        #region Public Methods




        #endregion

        #region Events



        #endregion
    }
}