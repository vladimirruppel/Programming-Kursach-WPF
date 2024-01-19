using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace prog3_kursach.Converters
{
    class BooleanToMediaStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool val = (bool)value;
            MediaState res = val ? MediaState.Play : MediaState.Stop;
            return res;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
