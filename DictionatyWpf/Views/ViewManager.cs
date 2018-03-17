using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DictionatyWpf.Data;

namespace DictionatyWpf.Views
{
    public class ViewManager
    {
        
        #region Declarations

        private Dictionary<ScreenId, ViewBase> ScreenDic = new Dictionary<ScreenId, ViewBase>();

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

        private ViewBase CreateScreen(ScreenId screenId)
        {
            ViewBase res = null;

            return res;
        }

        #endregion

        #region Public Methods

        public ViewBase GetScreen(ScreenId screenId)
        {
            ViewBase res = null;
            if(ScreenDic.ContainsKey(screenId))
            {
                res = ScreenDic[screenId];
            }
            else
            {
                res = CreateScreen(screenId);
            }

            return res;
        }


        #endregion

        #region Events



        #endregion
    }
}
