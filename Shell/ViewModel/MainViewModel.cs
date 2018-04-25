using System;
using System.Windows;
using System.Windows.Threading;
using Enigma.Data;
using Enigma.Shell.Model;
using GalaSoft.MvvmLight;
using Enigma.Shell.Navigation;

namespace Enigma.Shell.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private DataManager _DataManager;
        private IFrameNavigationService _navigationService;

        private string _StatusMsg;

        public string StatusMsg
        {
            get { return _StatusMsg; }
            set
            {
                _StatusMsg = value;
                RaisePropertyChanged("StatusMsg");
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(DataManager dataMgr, IFrameNavigationService navigationService)
        {
            _DataManager = dataMgr;
            _navigationService = navigationService; 

            
            //InitializeDBConnecction();



            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
            
            
        }

        public async void InitializeDBConnecction()
        {
            Dispatcher.CurrentDispatcher.Invoke(() => { StatusMsg = "Connecting to DB..."; });

            if (!await DataManager.InitializeDataContextAsync())
            {
                StatusMsg = "Connection error";
            }
            else
            {
                StatusMsg = String.Empty;
                _navigationService.NavigateTo(ScreenId.DictionariesView.ToString(), null);
            }

        }
    }
}