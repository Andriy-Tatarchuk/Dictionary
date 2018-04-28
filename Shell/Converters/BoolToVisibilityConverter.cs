using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Enigma.Shell.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        private bool _invert = false;
        public bool Invert
        {
            get { return _invert;}
            set { _invert = value; }
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var res = Visibility.Collapsed;

            if (value is bool && ((bool)value ^ Invert))
            {
                res = Visibility.Visible;
            }

            return res;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
