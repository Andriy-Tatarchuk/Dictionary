using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CommonServiceLocator;
using Enigma.Data;
using GalaSoft.MvvmLight.Ioc;
using Enigma.Autocomplete;

namespace Enigma.Shell.Controls
{
    /// <summary>
    /// Interaction logic for LoadingDialog.xaml
    /// </summary>
    public partial class LoadingDialog : Window
    {
        private IDataManager _DataManager;
        private ICompleter _Autocompleter;
        public LoadingDialog(IDataManager dataManager, ICompleter autoCompleter)
        {
            InitializeComponent();
            _DataManager = dataManager;
            _Autocompleter = autoCompleter;
            Loaded += LoadingDialog_Loaded;
        }

        private void LoadingDialog_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeDBConnecction();
            InitializeAutocompleter();
        }

        private async void InitializeDBConnecction()
        {
            DialogResult = _DataManager != null ? await _DataManager.InitializeDataContextAsync() : false;
        }

        private async void InitializeAutocompleter()
        {
            await _Autocompleter.InitializeWordsAsync();
        }
    }
}
