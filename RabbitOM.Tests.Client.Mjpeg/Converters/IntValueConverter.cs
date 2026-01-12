using System;
using System.Globalization;
using System.Windows.Data;

namespace RabbitOM.Tests.Client.Mjpeg.Converters
{
    [ValueConversion(typeof(string),typeof(int))]
    public sealed class IntValueConverter : IValueConverter
    {
        public object Convert( object value , Type targetType , object parameter , CultureInfo culture )
        {
             return value?.ToString() ?? "0";
        }

        public object ConvertBack( object value , Type targetType , object parameter , CultureInfo culture )
        {
           return int.TryParse( value as string , out var result ) ? result : 0;
        }
    }
}
