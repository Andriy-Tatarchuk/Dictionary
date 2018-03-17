using System;
using System.Collections.Generic;
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

namespace DictionatyWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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

            DM.AddDictionary("First_Dictionary");
        }

        private void MenuCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
        }

        private void MenuCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
        }
    }
}
