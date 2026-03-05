using System;
using System.Globalization;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Parsers
{
    public static class DoubleRtspHeaderParser
    {
        private static readonly char[] SpaceWithQuotesChars = { ' ' , '\'' , '\"' , '`' };

        public static string Format( double value )
        {
            return value.ToString( "F3" , CultureInfo.InvariantCulture );
        }

        public static bool TryParse( string input , out double result )
        {
            return double.TryParse( input?.Trim( SpaceWithQuotesChars ).Replace( "," , "." ) , NumberStyles.Float , CultureInfo.InvariantCulture , out result );
        }
    }
}
