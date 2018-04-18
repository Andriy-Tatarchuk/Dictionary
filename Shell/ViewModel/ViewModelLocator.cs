/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:MvvmLight"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using Enigma.Data;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Enigma.Shell.Navigation;
using System;

namespace Enigma.Shell.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<DataManager>();

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<WordsViewModel>();
            SimpleIoc.Default.Register<DictionariesViewModel>();

            SetupNavigation();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public WordsViewModel Words
        {
            get
            {
                return ServiceLocator.Current.GetInstance<WordsViewModel>();
            }
        }

        public DictionariesViewModel Dictionaries
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DictionariesViewModel>();
            }
        }

        private static void SetupNavigation()
        {
            var navigationService = new FrameNavigationService();
            navigationService.Configure("WordsView", new Uri("../Views/WordsView.xaml", UriKind.Relative));
            navigationService.Configure("DictionariesView", new Uri("../Views/DictionariesView.xaml", UriKind.Relative));

            SimpleIoc.Default.Register<IFrameNavigationService>(() => navigationService);
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}