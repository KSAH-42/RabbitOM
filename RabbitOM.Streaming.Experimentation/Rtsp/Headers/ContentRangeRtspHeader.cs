using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters;
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Validation;

    public sealed class ContentRangeRtspHeader
    {
        public static readonly string TypeName = "Content-Range";
        
        public static readonly StringValueAdapter ValueAdapter = StringValueAdapter.TrimWithUnQuoteAdapter;
        public static readonly StringValueValidator ValueValidator = StringValueValidator.TokenValidator;


        private string _unit = string.Empty;
        private long? _start;
        private long? _end;
        private long? _size;




        public string Unit
        {
            get => _unit;
            set => _unit = ValueAdapter.Adapt( value );
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


        public static bool TryParse( string input , out ContentRangeRtspHeader result )
        {
            result = null;
            
            if ( RtspHeaderParser.TryParse( input , " " , out string[] tokens ) )
            {
                if ( RtspHeaderParser.TryParse( tokens.ElementAtOrDefault( 1 ) , "/" , out string[] tokensRange ) )
                {
                    var header = new ContentRangeRtspHeader() { Unit = tokens.FirstOrDefault() };

                    if ( header.Unit == tokensRange.FirstOrDefault() )
                    {
                        return false;
                    }

                    if ( RtspHeaderParser.TryParse( tokensRange.ElementAtOrDefault( 0 ) , "-" , out KeyValuePair<string,string> range ) )
                    {
                        if ( long.TryParse( ValueAdapter.Adapt( range.Key ) , out long number ) )
                        {
                            header.Start = number;
                        }

                        if ( long.TryParse( ValueAdapter.Adapt( range.Value ) , out number ) )
                        {
                            header.End = number;
                        }
                    }

                    if ( long.TryParse( ValueAdapter.Adapt( tokensRange.ElementAtOrDefault(1) ) , out long size ) )
                    {
                        header.Size = size;
                    }                    

                    if ( ValueValidator.TryValidate( header.Unit ) )
                    {
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
