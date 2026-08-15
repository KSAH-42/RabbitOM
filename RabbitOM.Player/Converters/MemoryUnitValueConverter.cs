using System;
using System.Globalization;
using System.Collections.Generic;
using System.Windows.Data;

namespace RabbitOM.Player.Converters
{
    [ValueConversion(typeof(byte), typeof(string))]
    public sealed class MemoryUnitValueConverter : IValueConverter
    {
        private readonly IReadOnlyList<string> Units = new string[]
        {
            "bytes",
            "Kbits",
            "Mbits",
            "Gbits",
            "Tbits"
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var index = 0;

            if ( double.TryParse( value?.ToString() , out var size ) && (size *= 8) > 0 )
            {
                size = double.IsInfinity( size ) || double.IsNaN( size ) ? 0 : size;

                var temp = (long) size;

                while ( (temp /= 1024) > 0 && ++ index < Units.Count - 1 );
            }

            return string.Format( "{0:0.##} {1}" , size / Math.Pow( 1024 , index ) , Units[ index ] );
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}