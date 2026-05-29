using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public static class RtspHeaderValueSanitizer
    {
        private static readonly char[] SpaceAndQuotesChars = { ' ' , '\"' , '\'' , '`' };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsSpaceOrQuoteValue( in char value )
        {
            Debug.Assert( SpaceAndQuotesChars.Length == 4 );
            return value == SpaceAndQuotesChars[0]
                || value == SpaceAndQuotesChars[1]
                || value == SpaceAndQuotesChars[2]
                || value == SpaceAndQuotesChars[3];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsQuoteValue( in char value )
        {
            Debug.Assert( SpaceAndQuotesChars.Length == 4 );
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

            StringBuilder builder = null;
            bool f1 = false;
            bool f2 = false;
            var k = 0;
            var i = 0;
            var j = value.Length - 1;

            while ( k < j )
            {
                if ( ! f1 && IsSpaceOrQuoteValue( value[ i ] ) )
                {
                    i++;
                }
                else
                {
                    f1 = true;
                }

                if ( ! f2 && IsSpaceOrQuoteValue( value[ j - 1 ] ) )
                {
                    j--;
                }
                else
                {
                    f2 = true;
                }

                if ( k < j )
                {
                    if ( f1 && ! IsQuoteValue( value[k] ) )
                    {
                        ( builder ?? ( builder = new StringBuilder() ) ).Append( value[k] );
                    }

                    k++;
                }
            }

            return builder?.ToString() ?? value;
        }
    }
}
