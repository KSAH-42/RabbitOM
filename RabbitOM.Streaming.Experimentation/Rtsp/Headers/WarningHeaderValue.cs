using System;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Normalizers;

    public sealed class WarningHeaderValue
    {
        private static readonly StringValueNormalizer ValueNormalizer = StringValueNormalizer.TrimWithUnQuoteNormalizer;
                    

        
        public WarningHeaderValue( int code , string agent )
            : this( code , agent , null )
        {
        }

        public WarningHeaderValue( int code , string agent , string comment )
        {
            if ( ! HeaderProtocolValidator.IsValidWarningCode( code ) )
            {
                throw new ArgumentException( $"invalid code value:{code}" , nameof( code ) );
            }

            if ( ! HeaderProtocolValidator.IsValidWarningAgent( agent ) )
            {
                throw new ArgumentException( "invalid agent name" , nameof( agent ) );
            }

            Code = code;
            Agent = ValueNormalizer.Normalize( agent );
            Comment = ValueNormalizer.Normalize( comment );
        }



        public int Code { get; }

        public string Agent { get; }
        
        public string Comment { get; }
        



        public static bool TryParse( string input , out WarningHeaderValue result )
        {
            result = null;

            if ( HeaderParser.TryParse( input , " " , out string[] tokens ) )
            {
                if ( tokens.Length >= 4 )
                {
                    return false;
                }

                if ( ! int.TryParse( tokens.ElementAtOrDefault( 0 ) , out var code ) || code < 0 )
                {
                    return false;
                }

                if ( ! HeaderProtocolValidator.IsValidWarningAgent( tokens.ElementAtOrDefault( 1 ) ) )
                {
                    return false;
                }

                result = new WarningHeaderValue( code , tokens.ElementAtOrDefault(1) , tokens.ElementAtOrDefault(2) );
            }
            
            return result != null;
        }




        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append( Code );

            if ( ! string.IsNullOrWhiteSpace( Agent ) )
            {
                builder.AppendFormat( " {0}" , Agent.Replace( " " , "" ) );
            }

            if ( ! string.IsNullOrWhiteSpace( Comment ) )
            {
                builder.AppendFormat( " \"{0}\"" , Comment );
            }
            
            return builder.ToString();
        }
    }
}
