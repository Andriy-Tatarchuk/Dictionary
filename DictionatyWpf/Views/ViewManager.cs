using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DictionatyWpf.Data;
using DictionatyWpf.ViewModels;

namespace DictionatyWpf.Views
{
    public class ViewManager
    {
        
        #region Declarations

        private Dictionary<ScreenId, Dictionary<object, ViewBase>> ScreenDic = new Dictionary<ScreenId, Dictionary<object, ViewBase>>();

        private DataManager DM { get; set; }

        #endregion

        #region Properties

       

        #endregion

        #region Constructorss

        public ViewManager(DataManager dm)
        {
            DM = dm;
        }

        #endregion


        #region Private Methods

        private ViewBase CreateScreen(ScreenId screenId, object param)
        {
            ViewBase res = null;
            if (screenId == ScreenId.Dictionaries)
            {
                res = new Dictionaries(){ViewModel = new VMDictionaries(DM){Header = "Dictionaries"}};
            }
            else if (screenId == ScreenId.Words)
            {
                res = new Words() { ViewModel = new VMWords(DM, param) { Header = "Words" } };
            }
            return res;
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


        #endregion

        #region Events



        #endregion
    }
}
