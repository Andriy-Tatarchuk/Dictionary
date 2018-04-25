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
using Enigma.Data;

namespace Enigma.Shell.Controls
{
    /// <summary>
    /// Interaction logic for LoadingDialog.xaml
    /// </summary>
    public partial class LoadingDialog : Window
    {
        public LoadingDialog()
        {
            InitializeComponent();
            Loaded += LoadingDialog_Loaded;
        }

        private void LoadingDialog_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeDBConnecction();
        }

        private async void InitializeDBConnecction()
        {
            DialogResult = await DataManager.InitializeDataContextAsync();
        }
    }
}
