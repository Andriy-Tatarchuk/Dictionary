using System.Windows;
using System.Windows.Controls;

namespace Enigma.Shell.Views
{
    /// <summary>
    /// Description for DictionariesView.
    /// </summary>
    public partial class AddEditWordView : BaseView
    {
        /// <summary>
        /// Initializes a new instance of the DictionariesView class.
        /// </summary>
        public AddEditWordView()
        {
            InitializeComponent();
        }

        private void NameTextBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            
        }
    }
}