using RabbitOM.Net.Rtsp.Clients;
using System;
using System.Globalization;
using System.Windows.Data;

namespace RabbitOM.Player.Converters
{
	public sealed class RevertBooleanValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,object parameter, CultureInfo culture)
        {
            if ( value is bool boolVal )
            {
                return ! boolVal;
            }

            return true;
        }

        public object ConvertBack(object value, Type targetType,object parameter, CultureInfo culture)
            => null;
    }
}
