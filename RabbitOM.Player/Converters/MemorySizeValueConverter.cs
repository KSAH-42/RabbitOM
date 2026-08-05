using System;
using System.Globalization;
using System.Windows.Data;

namespace RabbitOM.Player.Converters
{
    [ValueConversion(typeof(byte), typeof(string))]
    public class MemorySizeValueConverter : IValueConverter
    {
        private const long UnitKb = 1024;

        private const long UnitMb = UnitKb * 1024;

        private const long UnitGb = UnitMb * 1024;

        private const long UnitTb = UnitGb * 1024;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var size = System.Convert.ToDouble( value ) * 8;

            if ( 0 <= size && size < MemorySizeValueConverter.UnitKb )
            {
                return string.Format("{0:0} bytes", size );
            }

            if ( MemorySizeValueConverter.UnitKb <= size && size < MemorySizeValueConverter.UnitMb )
            {
                return string.Format( "{0:0.##} Kbits" , size / MemorySizeValueConverter.UnitKb );
            }

            if ( MemorySizeValueConverter.UnitMb <= size && size < MemorySizeValueConverter.UnitGb )
            {
                return string.Format("{0:0.##} Mbits", size / MemorySizeValueConverter.UnitMb );
            }

            if ( MemorySizeValueConverter.UnitGb <= size && size < MemorySizeValueConverter.UnitTb )
            {
                return string.Format("{0:0.##} Gbits", size / MemorySizeValueConverter.UnitGb );
            }

            return string.Format("{0:0.##} Tbits", size / MemorySizeValueConverter.UnitTb );
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}