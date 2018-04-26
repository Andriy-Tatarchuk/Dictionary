using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using Enigma.Shell.ViewModel;
using Enigma.Shell.Views;

namespace Enigma.Shell
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly DependencyProperty MainFrameSourceProperty = DependencyProperty.Register(
            "MainFrameSource", typeof(Uri), typeof(MainWindow), new PropertyMetadata(default(Uri)));

        public Uri MainFrameSource
        {
            get { return (Uri)GetValue(MainFrameSourceProperty); }
            set { SetValue(MainFrameSourceProperty, value); }
        }

        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }

        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            Exception exception = (Exception)e.Exception;
            FireException(exception);
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception exception = (Exception)e.ExceptionObject;
            FireException(exception);
        }

        private void FireException(Exception exception)
        {
            string errorMessage = string.Format("An unhandled exception occurred: {0}", exception.Message);
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            Close();
        }

        private void MainFrame_OnNavigated(object sender, NavigationEventArgs e)
        {
            var view = MainFrame.Content as BaseView;
            if (view != null)
            {
                var viewModel = view.DataContext as BaseViewModel;
                if (viewModel != null)
                {
                    viewModel.Navigated(e.ExtraData);
                }
            }
        }
    }
}