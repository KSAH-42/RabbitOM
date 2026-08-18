using System;
using System.Globalization;
using System.Windows.Data;

namespace RabbitOM.Player.Converters
{
    [ValueConversion(typeof(double),typeof(double))]
    public sealed class DoubleAdditionValueConverter : IValueConverter
    {
        public double OffsetValue { get; set; }

        public object Convert( object value , Type targetType , object parameter , CultureInfo culture )
        {
           return double.TryParse( value?.ToString() , out var result ) ? result + OffsetValue : OffsetValue;
        }

        public object ConvertBack( object value , Type targetType , object parameter , CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
