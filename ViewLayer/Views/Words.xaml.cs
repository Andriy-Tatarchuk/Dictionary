

using System.Windows.Input;
using ViewLayer.Views;

namespace ViewLayer.Views
{
    /// <summary>
    /// Interaction logic for Dictionaries.xaml
    /// </summary>
    public partial class Words : ViewBase
    {
        public static ICommand AddEditWordCommand;

        static Words()
        {
            AddEditWordCommand = new RoutedCommand();
        }
        public Words()
        {
            InitializeComponent();
            CommandBindings.Add(new CommandBinding(AddEditWordCommand, AddEditWordCommand_Executed,
                AddEditWordCommand_CanExecute));
        }

        private void AddEditWordCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (ViewModel != null)
            {
                e.CanExecute = ViewModel.Command_CanExecute(ViewLayer.Models.Command.AddEditWord, e.Parameter);
            }
        }

        private void AddEditWordCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (ViewModel != null)
            {
                ViewModel.Command_Executed(ViewLayer.Models.Command.AddEditWord, e.Parameter);
            }
        }
    }
}
