using System;
using System.Windows;
using System.Windows.Navigation;
using MvvmLight.ViewModel;
using MvvmLight.Views;

namespace MvvmLight
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