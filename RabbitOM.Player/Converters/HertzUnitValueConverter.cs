using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Linq;

namespace RabbitOM.Player.Converters
{
    [ValueConversion(typeof(byte), typeof(string))]
    public sealed class HertzUnitValueConverter : IValueConverter
    {
        private readonly IReadOnlyList<string> FormatUnits = new string[]
        {
            "{0} Hz",
            "{0} KHz",
            "{0} MHz",
            "{0} GHz",
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var index = 0;

            if ( long.TryParse( value?.ToString() , out var size ) )
            {
                size = size > 0 ? size : 0;

                var temp = size;

                while ( (temp /= 1000) > 0 )
                {
                    index ++;
                }
            }

            var format = FormatUnits.ElementAtOrDefault( index ) ?? FormatUnits.LastOrDefault();

            return string.Format( format , size / Math.Pow( 1000 , index ) );
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}