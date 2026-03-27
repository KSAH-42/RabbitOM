using System;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Headers.Types
{
    using RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Headers.Normalizers;

    public sealed class WarningInfo : RtspHeaderValue
    {
        private static readonly StringValueNormalizer ValueNormalizer = StringValueNormalizer.TrimWithUnQuoteNormalizer;
                    

        
        public WarningInfo( int code , string agent )
            : this( code , agent , null )
        {
        }

        public WarningInfo( int code , string agent , string comment )
        {
            if ( ! RtspHeaderProtocolValidator.IsValidWarningCode( code ) )
            {
                throw new ArgumentException( $"invalid code value:{code}" , nameof( code ) );
            }

            if ( ! RtspHeaderProtocolValidator.IsValidWarningAgent( agent ) )
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
        



        public static bool TryParse( string input , out WarningInfo result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( input , " " , out string[] tokens ) )
            {
                if ( tokens.Length >= 4 )
                {
                    return false;
                }

                if ( ! int.TryParse( tokens.ElementAtOrDefault( 0 ) , out var code ) || code < 0 )
                {
                    return false;
                }

                if ( ! RtspHeaderProtocolValidator.IsValidWarningAgent( tokens.ElementAtOrDefault( 1 ) ) )
                {
                    return false;
                }

                result = new WarningInfo( code , tokens.ElementAtOrDefault(1) , tokens.ElementAtOrDefault(2) );
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
