using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    internal static class RtspHeaderValueSanitizer
    {
        private static readonly char[] SpaceAndQuotesChars = { ' ' , '\"' , '\'' , '`' };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsSpaceAndQuotesAt( string value , int index )
        {
            Debug.Assert( SpaceAndQuotesChars.Length == 4 );
            return value[index] == SpaceAndQuotesChars[0]
                || value[index] == SpaceAndQuotesChars[1]
                || value[index] == SpaceAndQuotesChars[2]
                || value[index] == SpaceAndQuotesChars[3];
        }

        public static string UnQuotesWithTrim( string value )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                return string.Empty;
            }

            if ( IsSpaceAndQuotesAt( value , 0 ) )
            {
                return value.Trim( SpaceAndQuotesChars );
            }

            var lastIndex = value.Length - 1;

            if ( lastIndex > 0 && IsSpaceAndQuotesAt( value , lastIndex ) )
            {
                return value.Trim( SpaceAndQuotesChars );
            }

            return value;
        }

        public static string TrimWithRemoveAllQuotesNormalizer( string value )
        {
            // TODO: this code is inefficient, optimize this BUT we have 2 calls counts

            return value?.Replace( "\'" , "" ).Replace( "\"" , "" ).Replace( "`" , "" ).Trim() ?? string.Empty;
        }
    }
}
