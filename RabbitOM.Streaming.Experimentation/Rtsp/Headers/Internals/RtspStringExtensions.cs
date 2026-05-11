using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public static class RtspStringExtensions
    {
        private static readonly char[] SpaceAndQuotesChars = { ' ' , '\'' , '\"' , '`' };

        public static string TrimWithUnQuotes( this string value )
        {
            return value?.Trim( SpaceAndQuotesChars ) ?? string.Empty;
        }

        public static string TrimWithQuotesRemoval( this string value )
        {
            return value?.Replace( "\'" , "" ).Replace( "\"" , "" ).Replace( "`" , "" ).Trim() ?? string.Empty;
        }
    }
}
