using System;
using System.Globalization;
using System.Windows.Data;

namespace RabbitOM.Player.Converters
{
    [ValueConversion( typeof(bool) , typeof( string ) ) ]
    public sealed class BooleanStringValueConverter : IValueConverter
    {
        public string TrueString { get; set; } = "True";

        public string FalseString { get; set; } = "False";

        public string UnknownString { get; set; } = "Unknown";

        public object Convert( object value , Type targetType , object parameter , CultureInfo culture )
        {
            if ( value is bool status )
            {
                return status ? TrueString : FalseString;
            }

            return "Unknown";
        }

        public object ConvertBack( object value , Type targetType , object parameter , CultureInfo culture )
        {
            return null;
        }
    }
}
