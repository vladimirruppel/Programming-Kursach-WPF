using System;
using System.Globalization;
using System.Windows.Data;

namespace prog3_kursach.Converters
{
    class BooleanToPlayButtonPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool val = (bool)value;
            string fileName = val ? "pause" : "play-player";
            string imagePath = $"pack://application:,,,/Images/{fileName}.png";
            return imagePath;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
