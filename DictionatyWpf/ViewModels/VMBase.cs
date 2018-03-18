using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DictionatyWpf.Data;
using DictionatyWpf.Views;

namespace DictionatyWpf.ViewModels
{
    public class VMBase : DependencyObject
    {
        #region Declarations
        protected DataManager DM { get; set; }

        #endregion

        #region Properties

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            "Header", typeof(string), typeof(VMBase), new PropertyMetadata(default(string)));

        public string Header
        {
            get { return (string) GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        #endregion

        #region Constructorss

        public VMBase(DataManager dm)
        {
            DM = dm;
        }

        #endregion


        #region Private Methods



        #endregion

        #region Public Methods

        public virtual bool Command_CanExecute(Command command, object param)
        {
            var res = false;

            return res;
        }

        public virtual void Command_Executed(Command command, object param)
        {
        }


        public void OpenScreen(ScreenId screenId, object param = null)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.OpenScreen(screenId, param);
            }
        }

        #endregion

        #region Events



        #endregion

        
    }
}
