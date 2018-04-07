﻿

using System.Windows.Input;
using ViewLayer.Views;

namespace ViewLayer.Views
{
    /// <summary>
    /// Interaction logic for Dictionaries.xaml
    /// </summary>
    public partial class Dictionaries : ViewBase
    {
        public static ICommand AddEditDicCommand;

        static Dictionaries()
        {
            AddEditDicCommand = new RoutedCommand();
        }

        public Dictionaries()
        {
            InitializeComponent();
            CommandBindings.Add(new CommandBinding(AddEditDicCommand, AddEditDicCommand_Executed,
                AddEditDicCommand_CanExecute));
        }

        private void AddEditDicCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (ViewModel != null)
            {
                e.CanExecute = ViewModel.Command_CanExecute(ViewLayer.Models.Command.AddEditDic, e.Parameter);
            }
        }

        private void AddEditDicCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (ViewModel != null)
            {
                ViewModel.Command_Executed(ViewLayer.Models.Command.AddEditDic, e.Parameter);
            }
        }
    }
}