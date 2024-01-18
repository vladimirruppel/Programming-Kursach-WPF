using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace prog3_kursach.Converters
{
    public class BooleanToVisibilityPropertyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool b = (bool)value;
            Visibility visibility = b ? Visibility.Visible : Visibility.Hidden;
            return visibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
