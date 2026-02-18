using System;
using System.Globalization;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Formatting
{
    public static class DateTimeRtspHeaderParser
    {
        private const string GMTFormat = "ddd, dd MMM yyyy HH:mm:ss GMT";

        public static string Format( DateTime value )
        {
            return value.ToUniversalTime().ToString( GMTFormat , CultureInfo.InvariantCulture );
        }

        public static bool TryParse( string input , out DateTime result )
        {
            return DateTime.TryParse( input , CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out result );
        }
    }
}
