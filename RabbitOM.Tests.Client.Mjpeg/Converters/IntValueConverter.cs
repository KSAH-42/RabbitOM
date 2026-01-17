using System;
using System.Globalization;
using System.Windows.Data;
using static System.Net.Mime.MediaTypeNames;

namespace RabbitOM.Tests.Client.Mjpeg.Converters
{
    [ValueConversion(typeof(int),typeof(string))]
    public sealed class IntValueConverter : IValueConverter
    {
        public int FallBackValue { get; set; }

        public object Convert( object value , Type targetType , object parameter , CultureInfo culture )
        {
            return value is int ? value?.ToString() ?? FallBackValue.ToString() : FallBackValue.ToString();
        }

        public object ConvertBack( object value , Type targetType , object parameter , CultureInfo culture )
        {
            return int.TryParse( value as string , out var result ) ? result : FallBackValue;
        }
    }
}
