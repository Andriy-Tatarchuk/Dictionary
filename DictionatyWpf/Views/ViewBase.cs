using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DictionatyWpf.Data;
using DictionatyWpf.ViewModels;

namespace DictionatyWpf.Views
{
    public class ViewBase : UserControl  
    {
        #region Declarations


        #endregion

        #region Properties

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(VMBase), typeof(ViewBase), new PropertyMetadata(default(VMBase), (o, args) =>
            {
                var obj = o as ViewBase;
                if (obj != null)
                {
                    obj.DataContext = args.NewValue;
                }

            }));

        public VMBase ViewModel
        {
            get { return (VMBase) GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        #endregion

        #region Constructorss


        #endregion


        #region Private Methods



        #endregion

        #region Public Methods




        #endregion

        #region Events



        #endregion
    }
}
