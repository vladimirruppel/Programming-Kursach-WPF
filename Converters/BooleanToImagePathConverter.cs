using System;
using System.Globalization;
using System.Windows.Data;

namespace prog3_kursach.Converters
{
    public class BooleanToImagePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isAdded = (bool)value;
            string imageFileName = isAdded ? "like.png" : "not-like.png";

            return $"pack://application:,,,/Images/{imageFileName}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
