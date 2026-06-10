using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public static class RtspHeaderValueSanitizer
    {
        private static readonly char[] SpaceAndQuotesChars = { ' ' , '\"' , '\'' , '`' };

        // noloop are used here, there is less IL code generated if we are using this way

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsSpaceOrQuoteValue( in char value )
        {
            return value == SpaceAndQuotesChars[0]
                || value == SpaceAndQuotesChars[1]
                || value == SpaceAndQuotesChars[2]
                || value == SpaceAndQuotesChars[3];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsQuoteValue( in char value )
        {
            return value == SpaceAndQuotesChars[1]
                || value == SpaceAndQuotesChars[2]
                || value == SpaceAndQuotesChars[3];
        }

        // most of the time space and quotes are not present
        public static string UnQuotesWithTrim( string value )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                return string.Empty;
            }

            if ( IsSpaceOrQuoteValue( value[0] ) )
            {
                return value.Trim( SpaceAndQuotesChars );
            }

            var lastIndex = value.Length - 1;

            if ( lastIndex > 0 && IsSpaceOrQuoteValue( value[lastIndex] ) )
            {
                return value.Trim( SpaceAndQuotesChars );
            }

            return value;
        }

        // most of the time space and quotes are not present
        // input  = "  'd ''' f '''' "
        // output = "d  f"
        
        public static string TrimWithRemoveAllQuotes( string value )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                return string.Empty;
            }

            var i = 0;
            var j = value.Length - 1;

            while ( IsSpaceOrQuoteValue( value[i] ) ) {  i ++; }
            while ( IsSpaceOrQuoteValue( value[j] ) ) {  j --; }

            if ( i == 0 && j == value.Length - 1 )
            {
                return value;
            }

            StringBuilder builder = null;
            
            while ( i <= j )
            {
                var element = value[i++];

                if ( ! IsQuoteValue( element ) )
                {
                    if ( builder == null )
                    {
                        builder = new StringBuilder(50);
                    }

                    builder.Append( element );
                }
            }

            return builder?.ToString() ?? value;
        }
    }
}
