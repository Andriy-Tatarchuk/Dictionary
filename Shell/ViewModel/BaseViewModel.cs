using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using Enigma.Data;
using GalaSoft.MvvmLight;
using Enigma.Shell.Navigation;
using GalaSoft.MvvmLight.Command;

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

        private object _Parameter;

        public object Parameter
        {
            get { return _Parameter; }
            set
            {
                _Parameter = value;
                RaisePropertyChanged("Parameter");
            }
        }

        public bool IsDataLoaded { get; set; }

        public RelayCommand GoBackCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the BaseViewModel class.
        /// </summary>
        public BaseViewModel(DataManager dataMgr, IFrameNavigationService navigationService)
        {
            DataManager = dataMgr;
            NavigationService = navigationService;

            GoBackCommand = new RelayCommand(GoBackCommandExecuted);

            DataManager.DataContextInisialized += DataManager_DataContextInisialized;
        }

        private async void GoBackCommandExecuted()
        {
            IsLoading = true;
            await Save();
            IsLoading = false;

            NavigationService.GoBack();
        }

        void DataManager_DataContextInisialized(object sender, System.EventArgs e)
        {
            OnLoaded();
        }

        public void OnLoaded()
        {
            if (!IsDataLoaded && DataManager.IsDataContextInisialized)
            {
                LoadData(Parameter);
                IsDataLoaded = true;
            }
        }

        public virtual void LoadData(object param)
        {

        }

        public virtual async Task Navigated(object param)
        {

        }

        public virtual async Task Save()
        {

        }

        public void RefreshData()
        {
            IsDataLoaded = false;
            OnLoaded();
        }
    }
}