using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    internal static class RtspHeaderValueSanitizer
    {
        private static readonly char[] SpaceAndQuotesChars = { ' ' , '\'' , '\"' , '`' };

        public static string UnQuotesWithTrim( string value )
        {
            return value?.Trim( SpaceAndQuotesChars ) ?? string.Empty;
        }

        public static string TrimWithRemoveAllQuotesNormalizer( string value )
        {
            return value?.Replace( "\'" , "" ).Replace( "\"" , "" ).Replace( "`" , "" ).Trim() ?? string.Empty;
        }
    }
}
