using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Enigma.Autocomplete;

namespace Enigma.Shell.Controls
{
    public class AutoCompleteTextBox : TextBox
    {
        private bool _internalChanges = false;

        public AutoCompleteTextBox()
        {
            TextChanged += AutoCompleteTextBox_TextChanged;
        }

        private async void AutoCompleteTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_internalChanges)
            {
                _internalChanges = true;
                try
                {
                    var completedText = await Autocompleter.GetFirstWordStartedWith(Text);
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
