using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DictionatyWpf.Data;
using DictionatyWpf.ViewModels;

namespace DictionatyWpf.Views
{
    public class ViewBase : UserControl  
    {
        #region Declarations

        public static ICommand Command;
        public static ICommand DeleteCommand;

        #endregion

        #region Properties

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(VMBase), typeof(ViewBase), new PropertyMetadata(default(VMBase), (o, args) =>
            {
                var obj = o as ViewBase;
                if (obj != null)
                {
                    obj.DataContext = args.NewValue;
                }

            }));

        public VMBase ViewModel
        {
            get { return (VMBase) GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        #endregion

        #region Constructorss

        static ViewBase()
        {
            Command = new RoutedCommand();
            DeleteCommand = new RoutedCommand();
        }

        public ViewBase()
        {
            CommandBindings.Add(new CommandBinding(Command, Command_Executed, Command_CanExecute));
            CommandBindings.Add(new CommandBinding(DeleteCommand, DeleteCommand_Executed, DeleteCommand_CanExecute));
        }

        private void DeleteCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (ViewModel != null)
            {
                e.CanExecute = ViewModel.Command_CanExecute(Views.Command.Delete, e.Parameter);
            }
        }

        private void DeleteCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (ViewModel != null)
            {
                ViewModel.Command_Executed(Views.Command.Delete, e.Parameter);
            }
        }

        #endregion


        #region Private Methods

        private void Command_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (ViewModel != null)
            {
                var command = Views.Command.None;
                if (Views.Command.TryParse(e.Parameter.ToString(), out command) && command != Views.Command.None)
                {
                    e.CanExecute = ViewModel.Command_CanExecute(command, e.Parameter);
                }
            }
        }

        private void Command_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (ViewModel != null)
            {
                var command = Views.Command.None;
                if (Views.Command.TryParse(e.Parameter.ToString(), out command) && command != Views.Command.None)
                {
                    ViewModel.Command_Executed(command, e.Parameter);
                }
            }
        }

        #endregion

        #region Public Methods




        #endregion

        #region Events



        #endregion
    }
}
