using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace RabbitOM.Sample.Player.Converters
{
    [ValueConversion(typeof(bool),typeof(Visibility))]
    public sealed class BooleanVisibilityValueConverter : IValueConverter
    {
        public object Convert( object value , Type targetType , object parameter , CultureInfo culture )
        {
            return value is bool status && status ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack( object value , Type targetType , object parameter , CultureInfo culture )
        {
            return value is Visibility visibility && visibility == Visibility.Visible;
        }
    }
}
