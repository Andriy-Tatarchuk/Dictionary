using System.Collections.ObjectModel;
using System.Windows;
using DictionatyWpf.Data;
using DictionatyWpf.Models;

namespace DictionatyWpf.ViewModels
{
    public class VMWords : VMBase
    {
        #region Declarations

        #endregion

        #region Properties

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            "ItemsSource", typeof(ObservableCollection<Word>), typeof(VMWords), new PropertyMetadata(default(ObservableCollection<Word>)));

        public ObservableCollection<Word> ItemsSource
        {
            get { return (ObservableCollection<Word>) GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        #endregion

        #region Constructorss

        public VMWords(DataManager dm, object param) : base(dm)
        {
            LoadWords();
        }

        #endregion


        #region Private Methods

        private void LoadWords()
        {
            if (DM != null)
            {
                ItemsSource = new ObservableCollection<Word>(DM.GetAllWords());
            }
        }

        #endregion

        #region Public Methods




        #endregion

        #region Events



        #endregion
    }
}