﻿using System.Windows;
using CommonServiceLocator;
using Enigma.Data;
using Enigma.Shell.Controls;
using Enigma.Shell.ViewModel;
using GalaSoft.MvvmLight.Threading;

namespace Enigma.Shell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            DispatcherHelper.Initialize();
        }

        private void ApplicationStart(object sender, StartupEventArgs e)
        {
            //Disable shutdown when the dialog closes
            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            var locator = Resources["Locator"] as ViewModelLocator;
            var dataManager = locator != null ? locator.DataManager : null;
            var autoCompleter = locator != null ? locator.AutoCompleter : null;

            var dialog = new LoadingDialog(dataManager, autoCompleter);
            
            if (dialog.ShowDialog() == true)
            {
                var mainWindow = new MainWindow();
                //Re - enable normal shutdown mode.
                Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                Current.MainWindow = mainWindow;
                mainWindow.Show();
            }
            else
            {
                MessageBox.Show("Unable to load data.", "Error", MessageBoxButton.OK);
                Current.Shutdown(-1);
            }
        }

    }
}
