using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace RabbitOM.Player.Converters
{
    [ValueConversion(typeof(bool),typeof(Stretch))]
    public sealed class BooleanStretchValueConverter : IValueConverter
    {
        public object Convert( object value , Type targetType , object parameter , CultureInfo culture )
        {
            return value is bool status && status ? Stretch.Fill : Stretch.Uniform;
        }

        public object ConvertBack( object value , Type targetType , object parameter , CultureInfo culture )
        {
            return value is Stretch stretch && stretch == Stretch.Fill;
        }
    }
}
