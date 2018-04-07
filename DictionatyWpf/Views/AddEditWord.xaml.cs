

using System.Windows.Input;

namespace DictionatyWpf.Views
{
    /// <summary>
    /// Interaction logic for Dictionaries.xaml
    /// </summary>
    public partial class AddEditWord : ViewBase
    {
        public static ICommand AddDicToWordCommand;

        static AddEditWord()
        {
            AddDicToWordCommand = new RoutedCommand();
        }

        public AddEditWord()
        {
            InitializeComponent();
            CommandBindings.Add(new CommandBinding(AddDicToWordCommand, AddDicToWordCommand_Executed,
                AddDicToWordCommand_CanExecute));
        }

        private void AddDicToWordCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (ViewModel != null)
            {
                e.CanExecute = ViewModel.Command_CanExecute(Views.Command.AddDicToWord, e.Parameter);
            }
        }

        private void AddDicToWordCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (ViewModel != null)
            {
                ViewModel.Command_Executed(Views.Command.AddDicToWord, e.Parameter);
            }
        }

    }
}
