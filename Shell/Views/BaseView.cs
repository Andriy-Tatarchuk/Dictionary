using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Enigma.Shell.ViewModel;

namespace Enigma.Shell.Views
{
    public class BaseView : UserControl
    {
        public static readonly DependencyProperty ParameterProperty = DependencyProperty.Register(
            "Parameter", typeof(object), typeof(BaseView), new PropertyMetadata(default(object), (o, args) =>
            {
                var obj = o as BaseView;
                if (obj != null)
                {
                    obj.ParameterChanged();
                }
            }));

        public object Parameter
        {
            get { return (object) GetValue(ParameterProperty); }
            set { SetValue(ParameterProperty, value); }
        }

        private void ParameterChanged()
        {
            var viewModel = DataContext as BaseViewModel;
            if (viewModel != null)
            {
                viewModel.Parameter = Parameter;
                viewModel.RefreshData();
            }
        }

        public BaseView()
        {
            Loaded+=BaseView_Loaded;
        }

        private void BaseView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var viewModel = DataContext as BaseViewModel;
            if (viewModel != null)
            {
                viewModel.Parameter = Parameter;
                viewModel.OnLoaded();
            }
        }
    }
}
