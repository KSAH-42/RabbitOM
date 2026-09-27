using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace RabbitOM.Player.Converters
{
    [ValueConversion(typeof(bool),typeof(Stretch))]
    public sealed class BooleanStretchValueConverter : IValueConverter
    {
        public object Convert( object value , Type targetType , object parameter , CultureInfo culture )
        {
            if (value is Stretch stretch)
            {
                return stretch == Stretch.Fill;
            }

            return false;
        }

        public object ConvertBack( object value , Type targetType , object parameter , CultureInfo culture )
        {
            if (value is bool isStretched)
            {
                return isStretched ? Stretch.Fill : Stretch.Uniform;
            }

            return Stretch.Uniform;
        }
    }
}
