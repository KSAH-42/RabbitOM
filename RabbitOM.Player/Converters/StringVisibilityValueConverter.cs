using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace RabbitOM.Player.Converters
{
    [ValueConversion(typeof(bool),typeof(Visibility))]
    public sealed class StringVisibilityValueConverter : IValueConverter
    {
        public string Text { get; set; }

        public object Convert( object value , Type targetType , object parameter , CultureInfo culture )
        {
            return StringComparer.OrdinalIgnoreCase.Equals( ( value as string ) ?? string.Empty , Text ?? string.Empty ) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack( object value , Type targetType , object parameter , CultureInfo culture )
        {
            return value is Visibility visibility && visibility == Visibility.Visible ? Text : null;
        }
    }
}
