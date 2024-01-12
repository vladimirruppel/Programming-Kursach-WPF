using System;
using System.Globalization;
using System.Windows.Data;

namespace prog3_kursach.Converters
{
    public class DurationSecondsNumberToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "NULL";

            int duration = (int)value;

            int minutes = duration / 60;
            int seconds = duration % 60;

            string result = $"{minutes}:{seconds}";
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
