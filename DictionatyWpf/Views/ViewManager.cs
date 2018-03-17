using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionatyWpf.Views
{
    public class ViewManager
    {
        
        #region Declarations

        #endregion

        #region Properties

        private Dictionary<ScreenId, ViewBase> ScreenDic = new Dictionary<ScreenId, ViewBase>();
       

        #endregion

        #region Constructorss

       

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
