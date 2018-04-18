using System.Windows.Controls;
using System.Windows.Navigation;
using Enigma.Data;
using GalaSoft.MvvmLight;
using Enigma.Shell.Navigation;

namespace Enigma.Shell.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class BaseViewModel : ViewModelBase
    {
        public DataManager DataManager { get; private set; }
        public IFrameNavigationService NavigationService { get; private set; }

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
        public BaseViewModel(DataManager dataMgr, IFrameNavigationService navigationService)
        {
            DataManager = dataMgr;
            NavigationService = navigationService;
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

        public virtual void Navigated(object param)
        {

        }
    }
}