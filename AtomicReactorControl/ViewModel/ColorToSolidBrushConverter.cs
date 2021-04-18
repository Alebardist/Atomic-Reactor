using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace AtomicReactorControl.ViewModel
{
    internal class ColorToSolidBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((Color)value == Colors.Black)
            {
                return new SolidColorBrush(Colors.Black);
            }
            else if ((Color)value == Colors.Orange)
            {
                return new SolidColorBrush(Colors.Orange);
            }
            else if ((Color)value == Colors.Green)
            {
                return new SolidColorBrush(Colors.Green);
            }
            else throw new ArgumentException($"{value} is wrong color");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}