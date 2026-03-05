using System;
using System.Globalization;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Parsers
{
    public static class FloatRtspHeaderParser
    {
        private static readonly char[] SpaceWithQuotesChars = { ' ' , '\'' , '\"' , '`' };

        public static string Format( float value )
        {
            return value.ToString( "G2" , CultureInfo.InvariantCulture );
        }

        public static bool TryParse( string input , out float result )
        {
            return float.TryParse( input?.Trim( SpaceWithQuotesChars ).Replace( "," , "." ) , NumberStyles.Float , CultureInfo.InvariantCulture , out result );
        }
    }
}
