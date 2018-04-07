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

        public static readonly DependencyProperty CurrentViewProperty = DependencyProperty.Register(
            "CurrentView", typeof(DictionatyWpf.Views.ViewBase), typeof(MainWindow), new PropertyMetadata(default(DictionatyWpf.Views.ViewBase)));

        public DictionatyWpf.Views.ViewBase CurrentView
        {
            get { return (DictionatyWpf.Views.ViewBase) GetValue(CurrentViewProperty); }
            set { SetValue(CurrentViewProperty, value); }
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

            ViewManager.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "CurrentView")
                {
                    CurrentView = ViewManager.CurrentView;
                }
            };

            CreateLeftMenu();

            //IUnityContainer unitycontainer = new UnityContainer();
            //unitycontainer.RegisterType<ICompany, Company>();

            //Employee emp = unitycontainer.Resolve<Employee>();
        }

        private void CreateLeftMenu()
        {
            MenuItemsSource = new ObservableCollection<LeftMenuItem>();

            CreateLeftMenuItem("Manage Dictionaries", ScreenId.Dictionaries);
            CreateLeftMenuItem("Manage Words", ScreenId.Words);
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
            if (e != null && e.Parameter != null)
            {
                var screen = ScreenId.None;
                if (ScreenId.TryParse(e.Parameter.ToString(), out screen) && screen != ScreenId.None)
                {
                    ViewManager.OpenScreen(screen);
                }
            }
        }

    }
}
