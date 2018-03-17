using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
using DictionatyWpf.Data;
using DictionatyWpf.Models;
using DictionatyWpf.Views;

namespace DictionatyWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewManager ViewManager { get; set; }

        public static readonly DependencyProperty MenuItemsSourceProperty = DependencyProperty.Register(
            "MenuItemsSource", typeof(ObservableCollection<LeftMenuItem>), typeof(MainWindow), new PropertyMetadata(default(ObservableCollection<LeftMenuItem>)));

        public ObservableCollection<LeftMenuItem> MenuItemsSource
        {
            get { return (ObservableCollection<LeftMenuItem>)GetValue(MenuItemsSourceProperty); }
            set { SetValue(MenuItemsSourceProperty, value); }
        }

        public static ICommand MenuCommand;

        static MainWindow()
        {
            MenuCommand = new RoutedCommand();
        }

        public MainWindow()
        {
            InitializeComponent();

            CommandBindings.Add(new CommandBinding(MenuCommand, MenuCommand_Executed, MenuCommand_CanExecute));

            var DM = new DataManager();

            ViewManager = new ViewManager(DM);

            CreateLeftMenu();
        }

        private void CreateLeftMenu()
        {
            MenuItemsSource = new ObservableCollection<LeftMenuItem>();

            CreateLeftMenuItem("Dictionaries", ScreenId.Dictionaries);
            CreateLeftMenuItem("Words", ScreenId.Words);
        }

        private void CreateLeftMenuItem(string text, ScreenId screenId)
        {
            MenuItemsSource.Add(new LeftMenuItem(text, screenId));
        }

        private void MenuCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void MenuCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
    }
}
