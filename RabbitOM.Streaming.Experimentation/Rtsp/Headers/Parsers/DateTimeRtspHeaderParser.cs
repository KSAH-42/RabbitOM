using System;
using System.Globalization;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Parsers
{
    public static class DateTimeRtspHeaderParser
    {
        private static readonly char[] SpaceWithQuotesChars = { ' ' , '\'' , '\"' , '`' };

        public static string Format( DateTime value )
        {
            value = value.Kind == DateTimeKind.Local ? value.ToUniversalTime() : value;

            return value.ToString( "r" , CultureInfo.InvariantCulture );
        }

        public static bool TryParse( string input , out DateTime result )
        {
            return DateTime.TryParse( input?.Trim( SpaceWithQuotesChars ) , CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal , out result );
        }
    }
}
