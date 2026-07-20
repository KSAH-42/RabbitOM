using System;

namespace RabbitOM.Streaming.RtspV2.Headers
{
    internal static class RtspHeaderValueSanitizer
    {
        private static readonly char[] SpaceAndQuotesChars = { ' ' , '\"' , '\'' , '`' };

        public static string UnQuotesWithTrim( string value )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                return string.Empty;
            }

            return value.Trim( SpaceAndQuotesChars );
        }

        public static string TrimWithRemoveAllQuotes( string value )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                return string.Empty;
            }

            return value.Trim( SpaceAndQuotesChars )
                    .Replace( SpaceAndQuotesChars[1].ToString() , "" )
                    .Replace( SpaceAndQuotesChars[2].ToString() , "" )
                    .Replace( SpaceAndQuotesChars[3].ToString() , "" );
        }
    }
}
