using System.Collections.ObjectModel;
using System.Windows;
using DictionatyWpf.Data;
using DictionatyWpf.Models;

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
            LoadDictionaries();
        }

        #endregion


        #region Private Methods

        private void LoadDictionaries()
        {
            if (DM != null)
            {
                ItemsSource = new ObservableCollection<Dictionary>(DM.GetAllDictionaries());
            }
        }

        #endregion

        #region Public Methods




        #endregion

        #region Events



        #endregion
    }
}