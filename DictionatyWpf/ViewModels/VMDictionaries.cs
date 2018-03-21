using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using DictionatyWpf.Data;
using DictionatyWpf.Models;
using DictionatyWpf.Views;

namespace DictionatyWpf.ViewModels
{
    public class VMDictionaries : VMBase
    {
        #region Declarations

        #endregion

        #region Properties

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            "ItemsSource", typeof(ObservableCollection<Dictionary>), typeof(VMDictionaries), new PropertyMetadata(default(ObservableCollection<Dictionary>)));

        public ObservableCollection<Dictionary> ItemsSource
        {
            get { return (ObservableCollection<Dictionary>) GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        #endregion

        #region Constructorss

        public VMDictionaries(DataManager dm) : base(dm)
        {
            IsLoading = true;
            LoadDictionaries();
        }

        #endregion


        #region Private Methods

        private async void LoadDictionaries()
        {
            if (DM != null)
            {
                IsLoading = true;
                var dictionaries = await DM.GetAllDictionariesAsync();
                ItemsSource = new ObservableCollection<Dictionary>(dictionaries);
                IsLoading = false;
            }
        }

        #endregion

        #region Public Methods


        public override bool Command_CanExecute(Command command, object param)
        {
            var res = false;
            if (command == Command.AddEditDic)
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
            if (command == Command.AddEditDic)
            {
                OpenScreen(ScreenId.AddEditDictionary, param);
            }
            else
            {
                base.Command_Executed(command, param);
            }
        }


        #endregion

        #region Events



        #endregion
    }
}