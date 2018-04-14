using System;
using System.Windows;
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

    }
}