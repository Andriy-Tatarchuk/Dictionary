using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DictionatyWpf.Data;

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




        #endregion

        #region Events



        #endregion
    }
}
