using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class ContentRangeRtspHeaderValue
    {
        private string _unit = string.Empty;
        private long? _start;
        private long? _end;
        private long? _size;
        


        public string Unit
        {
            get => _unit;
            set => _unit = RtspHeaderValueValidator.EnsureWellFormedToken( RtspHeaderValueSanitizer.UnQuotesWithTrim( value ) );
        }

        public long? Start
        {
            get => _start;
            set => _start = value;
        }

        public long? End
        {
            get => _end;
            set => _end = value;
        }

        public long? Size
        {
            get => _size;
            set => _size = value;
        }







        public static bool TryParse( string input , out ContentRangeRtspHeaderValue result )
        {
            result = null;
            
            if ( RtspHeaderValueParser.TryParse( input , " " , out string[] tokens ) )
            {
                if ( RtspHeaderValueParser.TryParse( tokens.ElementAtOrDefault( 1 ) , "/" , out string[] tokensRange ) )
                {
                    var header = new ContentRangeRtspHeaderValue()
                    { 
                        _unit = RtspHeaderValueSanitizer.UnQuotesWithTrim( tokens.FirstOrDefault() ) 
                    };

                    if ( RtspHeaderValueParser.TryParse( tokensRange.ElementAtOrDefault( 0 ) , "-" , out KeyValuePair<string,string> range ) )
                    {
                        if ( long.TryParse( RtspHeaderValueSanitizer.UnQuotesWithTrim( range.Key ) , out long number ) )
                        {
                            header._start = number;
                        }

                        if ( long.TryParse( RtspHeaderValueSanitizer.UnQuotesWithTrim( range.Value ) , out number ) )
                        {
                            header._end = number;
                        }
                    }

                    if ( long.TryParse( RtspHeaderValueSanitizer.UnQuotesWithTrim( tokensRange.ElementAtOrDefault(1) ) , out long size ) )
                    {
                        header._size = size;
                    }                    

                    if ( RtspHeaderValueValidator.IsWellFormedToken( header._unit ) )
                    {
                        if ( header._start.HasValue && header._end.HasValue )
                        {
                            result = header;
                        }
                    
                        else if ( header._size.HasValue && ! header._start.HasValue && ! header._end.HasValue )
                        {
                            result = header;
                        }
                    }
                }
            }

            return result != null;
        }
        








        public override string ToString()
        {
            if ( string.IsNullOrWhiteSpace( Unit ) )
            {
                return string.Empty;
            }
            
            var builder = new StringBuilder();

            builder.Append( $"{Unit} " );

            if ( Start.HasValue && End.HasValue )
            {
                builder.Append( $"{Start}-{End}" );
            }
            else
            {
                builder.Append( "*" );
            }

            builder.Append( "/" );

            if ( Size.HasValue )
            {
                builder.Append( $"{Size}" );
            }
            else
            {
                builder.Append( "*" );
            }

            return builder.ToString().Trim();
        }
    }
}
