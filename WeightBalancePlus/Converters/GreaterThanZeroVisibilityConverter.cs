using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WeightBalancePlus.Converters
{
    public class GreaterThanZeroVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double doubleVal)
                return doubleVal > 0 ? Visibility.Visible : Visibility.Collapsed;
            if (value is int intVal)
                return intVal > 0 ? Visibility.Visible : Visibility.Collapsed;

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
