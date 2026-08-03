using System;
using System.Globalization;
using System.Windows.Data;

namespace RabbitOM.Player.Converters
{
    [ValueConversion(typeof(byte), typeof(string))]
    public class MemorySizeConverter : IValueConverter
    {
        private const long UnitKb = 1024;

        private const long UnitMb = UnitKb * 1024;

        private const long UnitGb = UnitMb * 1024;

        private const long UnitTb = UnitGb * 1024;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var size = System.Convert.ToDouble( value ) * 8;

            if ( 0 <= size && size < MemorySizeConverter.UnitKb )
            {
                return string.Format("{0:0} bytes", size );
            }

            if ( MemorySizeConverter.UnitKb <= size && size < MemorySizeConverter.UnitMb )
            {
                return string.Format( "{0:0.##} Kbits" , size / MemorySizeConverter.UnitKb );
            }

            if ( MemorySizeConverter.UnitMb <= size && size < MemorySizeConverter.UnitGb )
            {
                return string.Format("{0:0.##} Mbits", size / MemorySizeConverter.UnitMb );
            }

            if ( MemorySizeConverter.UnitGb <= size && size < MemorySizeConverter.UnitTb )
            {
                return string.Format("{0:0.##} Gbits", size / MemorySizeConverter.UnitGb );
            }

            return string.Format("{0:0.##} Tbits", size / MemorySizeConverter.UnitTb );
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}