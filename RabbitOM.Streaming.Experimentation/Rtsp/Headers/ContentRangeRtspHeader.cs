using System;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Core;

    public sealed class ContentRangeRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "Content-Range";
        
        public static readonly StringRtspHeaderFilter ValueFilter = StringRtspHeaderFilter.UnQuoteFilter;
        


        private string _unit = string.Empty;
        private long? _start;
        private long? _end;
        private long? _size;




        public string Unit
        {
            get => _unit;
            set => _unit = ValueFilter.Filter( value );
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





        public static bool TryParse( string input , out ContentRangeRtspHeader result )
        {
            result = null;
            
            if ( RtspHeaderParser.TryParse( input , " " , out var tokens ) )
            {
                if ( RtspHeaderParser.TryParse( tokens.ElementAtOrDefault( 1 ) , "/" , out var tokensRange ) )
                {
                    var header = new ContentRangeRtspHeader();

                    header.Unit = tokens.ElementAtOrDefault( 0 );

                    if ( RtspHeaderProperty.TryParse( tokensRange.ElementAtOrDefault( 0 ) , "-" , out var range ) )
                    {
                        if ( RtspHeaderParser.TryParse( range.Name , out long number ) )
                        {
                            header.Start = number;
                        }

                        if ( RtspHeaderParser.TryParse( range.Value , out number ) )
                        {
                            header.End = number;
                        }
                    }

                    if ( RtspHeaderParser.TryParse( tokensRange.ElementAtOrDefault(1) , out long size ) )
                    {
                        header.Size = size;
                    }                    

                    if ( header.Start.HasValue && header.End.HasValue )
                    {
                        result = header;
                    }
                    
                    else if ( header.Size.HasValue && ! header.Start.HasValue && ! header.End.HasValue )
                    {
                        result = header;
                    }
                }
            }

            return result != null;
        }
    }
}
