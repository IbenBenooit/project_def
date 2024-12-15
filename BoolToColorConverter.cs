using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace LEDControllerApp
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isPressed)
            {
                return isPressed ? Brushes.LightGreen : Brushes.LightGray;
            }
            return Brushes.LightGray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}