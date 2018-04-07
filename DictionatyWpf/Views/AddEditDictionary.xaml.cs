﻿

using System.Windows.Input;

namespace DictionatyWpf.Views
{
    /// <summary>
    /// Interaction logic for Dictionaries.xaml
    /// </summary>
    public partial class AddEditDictionary : ViewBase
    {
        public static ICommand AddWordToDicCommand;

        static AddEditDictionary()
        {
            AddWordToDicCommand = new RoutedCommand();
        }

        public AddEditDictionary()
        {
            InitializeComponent();
            CommandBindings.Add(new CommandBinding(AddWordToDicCommand, AddWordToDicCommand_Executed,
                AddWordToDicCommand_CanExecute));
        }

        private void AddWordToDicCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (ViewModel != null)
            {
                e.CanExecute = ViewModel.Command_CanExecute(Views.Command.AddWordToDic, e.Parameter);
            }
        }

        private void AddWordToDicCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (ViewModel != null)
            {
                ViewModel.Command_Executed(Views.Command.AddWordToDic, e.Parameter);
            }
        }

    }
}