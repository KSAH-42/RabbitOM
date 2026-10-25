using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace RabbitOM.Player.Converters
{
    [ValueConversion(typeof(Visibility),typeof(bool))]
    public sealed class VisibilityBooleanValueConverter : IValueConverter
    {
        public object Convert( object value , Type targetType , object parameter , CultureInfo culture )
        {
            return value is Visibility visibility && visibility == Visibility.Visible;
        }

        public object ConvertBack( object value , Type targetType , object parameter , CultureInfo culture )
        {
            return value is bool status && status ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
