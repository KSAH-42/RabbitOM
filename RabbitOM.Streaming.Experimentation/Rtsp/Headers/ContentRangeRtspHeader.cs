using System;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class ContentRangeRtspHeader 
    {
        public static readonly string TypeName = "Content-Range";
        





        public string Unit { get; private set; } = string.Empty;

        public long? Start { get; set; }

        public long? End { get; set; }

        public long? Size { get; set; }







        public void SetUnit( string value )
        {
            Unit = RtspHeaderValueNormalizer.Normalize( value );
        }

        public void SetRange( string value )
        {
            Start = null;
            End = null;

            if ( StringParameter.TryParse( RtspHeaderValueNormalizer.Normalize( value ) , "-" , out var range ) )
            {
                if ( long.TryParse( range.Name , out var number ) )
                {
                    Start = number;
                }

                if ( long.TryParse( range.Value , out number ) )
                {
                    End = number;
                }
            }
        }

        public void SetSize( string value )
        {
            Size = null;

            if ( long.TryParse( RtspHeaderValueNormalizer.Normalize( value ) , out var result ) )
            {
                Size = result;
            }
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
            
            if ( StringRtspHeaderParser.TryParse( RtspHeaderValueNormalizer.Normalize( input ) , " " , out var tokens ) )
            {
                if ( StringRtspHeaderParser.TryParse( tokens.ElementAtOrDefault( 1 ) , "/" , out var tokensRange ) )
                {
                    var header = new ContentRangeRtspHeader();

                    header.SetUnit( tokens.ElementAtOrDefault( 0 ) );
                    header.SetRange( tokensRange.ElementAtOrDefault( 0 ) );
                    header.SetSize( tokensRange.ElementAtOrDefault( 1 ) );

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
