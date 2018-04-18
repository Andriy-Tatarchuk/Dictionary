using System;
using System.Collections.Generic;
using System.ComponentModel;
using Enigma.Data;
using ViewLayer.Models;
using ViewLayer.ViewModels;
using ViewLayer.Views;

namespace ViewLayer
{
    public class ViewManager : INotifyPropertyChanged
    {
        
        #region Declarations

        private Dictionary<ScreenId, Dictionary<object, ViewBase>> ScreenDic = new Dictionary<ScreenId, Dictionary<object, ViewBase>>();

        private DataManager DM { get; set; }

        #endregion

        #region Properties

        private ViewBase _CurrentView;

        public ViewBase CurrentView
        {
            get { return _CurrentView; }
            set
            {
                var dif = _CurrentView != value;
                if (dif)
                {
                    _CurrentView = value;
                    FirePropertyChanged("CurrentView");
                }
            }
        }

        #endregion

        #region Constructorss

        public ViewManager()
        {
            DM = new DataManager();
        }

        #endregion


        #region Private Methods

        private ViewBase CreateScreen(ScreenId screenId, object param)
        {
            ViewBase res = null;
            if (screenId == ScreenId.Dictionaries)
            {
                res = new Dictionaries(){ViewModel = new VMDictionaries(this, DM){Header = "Dictionaries"}};
            }
            else if (screenId == ScreenId.Words)
            {
                res = new Words() { ViewModel = new VMWords(this, DM, param) { Header = "Words" } };
            }
            else if (screenId == ScreenId.AddEditWord)
            {
                res = new AddEditWord() { ViewModel = new VMAddEditWord(this, DM, param) { Header = "Word Editor" } };
            }
            else if (screenId == ScreenId.AddEditDictionary)
            {
                res = new AddEditDictionary() { ViewModel = new VMAddEditDictionary(this, DM, param) { Header = "Dictionary Editor" } };
            }
            else if (screenId == ScreenId.AddWordToDictionary)
            {
                res = new AddEditWord() { ViewModel = new VMAddEditWord(this, DM, param, true) { Header = "Add Word To Dictionary" } };
            }
            return res;
        }

        private void FirePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region Public Methods

        public ViewBase GetScreen(ScreenId screenId, object param = null)
        {
            ViewBase res = null;
            if (ScreenDic.ContainsKey(screenId) && ScreenDic[screenId].ContainsKey(param))
            {
                res = ScreenDic[screenId][param];
            }
            else
            {
                res = CreateScreen(screenId, param);
            }

            return res;
        }

        public void OpenScreen(ScreenId screenId, object param = null)
        {
            var view = GetScreen(screenId, param);
            if (view != null)
            {
                CurrentView = view;
            }
        }

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;


        #endregion

    }
}
