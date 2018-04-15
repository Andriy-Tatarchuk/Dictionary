using System.Windows.Controls;
using System.Windows.Navigation;
using GalaSoft.MvvmLight;

namespace MvvmLight.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class BaseViewModel : ViewModelBase
    {
        private bool _IsLoading;

        public bool IsLoading
        {
            get { return _IsLoading; }
            set
            {
                _IsLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }

        private bool IsDataLoaded { get; set; }

        /// <summary>
        /// Initializes a new instance of the BaseViewModel class.
        /// </summary>
        public BaseViewModel()
        {

        }

        public void OnLoaded()
        {
            if (!IsDataLoaded)
            {
                LoadData();
                IsDataLoaded = true;
            }
        }

        public virtual void LoadData()
        {

        }
    }
}