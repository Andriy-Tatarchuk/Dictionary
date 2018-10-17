using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Enigma.Autocomplete;

namespace Enigma.Shell.Controls
{
    public class AutoCompleteTextBox : TextBox
    {
        private bool _internalChanges = false;

        public static readonly DependencyProperty OnLostFocusCommandProperty = DependencyProperty.Register(
            "OnLostFocusCommand", typeof(ICommand), typeof(AutoCompleteTextBox), new PropertyMetadata(default(ICommand)));

        public ICommand OnLostFocusCommand
        {
            get { return (ICommand) GetValue(OnLostFocusCommandProperty); }
            set { SetValue(OnLostFocusCommandProperty, value); }
        }

        public static readonly DependencyProperty CompleterProperty = DependencyProperty.Register(
            "Completer", typeof(ICompleter), typeof(AutoCompleteTextBox), new PropertyMetadata(default(ICompleter)));

        public ICompleter Completer
        {
            get { return (ICompleter) GetValue(CompleterProperty); }
            set { SetValue(CompleterProperty, value); }
        }

        public AutoCompleteTextBox()
        {
            TextChanged += AutoCompleteTextBox_TextChanged;
            LostFocus += AutoCompleteTextBox_LostFocus;
        }

        private void AutoCompleteTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (OnLostFocusCommand != null)
            {
                OnLostFocusCommand.Execute(null);
            }
        }

        private async void AutoCompleteTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsFocused && !_internalChanges && ((TextChange)e.Changes.First()).AddedLength > 0 && Completer != null)
            {
                _internalChanges = true;
                try
                {
                    var completedText = await Completer.GetFirstWordStartedWith(Text);
                    if (completedText != null)
                    {
                        var oldLength = Text.Length;
                        Text = completedText;
                        var selectionStart = oldLength;
                        var selectionLength = completedText.Length - oldLength;
                        CaretIndex = oldLength - 1;

                        Select(selectionStart, selectionLength);
                    }
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                finally
                {
                    _internalChanges = false;
                }

            }
        }

        
    }
}
