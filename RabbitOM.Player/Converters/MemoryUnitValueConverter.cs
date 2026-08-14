using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Linq;

namespace RabbitOM.Player.Converters
{
    [ValueConversion(typeof(byte), typeof(string))]
    public sealed class MemoryUnitValueConverter : IValueConverter
    {
        private readonly IReadOnlyList<string> FormatUnits = new List<string>()
        {
            "{0:0.##} bytes",
            "{0:0.##} Kbits",
            "{0:0.##} Mbits",
            "{0:0.##} Gbits",
            "{0:0.##} Tbits"
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var size = System.Convert.ToDouble( value ) * 8;
            var temp = (long) size;
            var index = 0;

            while ( (temp /= 1024) > 0 )
            {
                index ++;
            }

            var format = FormatUnits.ElementAtOrDefault( index ) ?? FormatUnits.LastOrDefault();

            return string.Format( format , size / Math.Pow( 1024 , index ) );
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}