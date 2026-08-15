using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;

namespace RabbitOM.Player.Converters
{
    [ValueConversion(typeof(byte), typeof(string))]
    public sealed class HertzUnitValueConverter : IValueConverter
    {
        private readonly IReadOnlyList<string> Units = new string[]
        {
            "Hz",
            "KHz",
            "MHz",
            "GHz",
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var index = 0;

            if ( long.TryParse( value?.ToString() , out var size ) )
            {
                size = size > 0 ? size : 0;

                var temp = size;

                while ( (temp /= 1000) > 0 && ++ index < Units.Count - 1 );
            }

            return string.Format( "{0} {1}" , size / Math.Pow( 1000 , index ) , Units[ index ] );
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}