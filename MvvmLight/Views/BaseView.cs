using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MvvmLight.ViewModel;

namespace MvvmLight.Views
{
    public class BaseView : UserControl
    {
        public BaseView()
        {
            Loaded+=BaseView_Loaded;
        }

        private void BaseView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var viewModel = DataContext as BaseViewModel;
            if (viewModel != null)
            {
                viewModel.OnLoaded();
            }
        }
    }
}
