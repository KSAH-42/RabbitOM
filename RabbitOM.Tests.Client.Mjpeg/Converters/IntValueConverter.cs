using System;
using System.Globalization;
using System.Windows.Data;

namespace RabbitOM.Tests.Client.Mjpeg.Converters
{
    [ValueConversion(typeof(int),typeof(string))]
    public sealed class IntValueConverter : IValueConverter
    {
        public int FallBackValue { get; set; }

        public object Convert( object value , Type targetType , object parameter , CultureInfo culture )
        {
             return value?.ToString() ?? FallBackValue.ToString();
        }

        public object ConvertBack( object value , Type targetType , object parameter , CultureInfo culture )
        {
           return int.TryParse( value as string , out var result ) ? result : FallBackValue;
        }
    }
}
