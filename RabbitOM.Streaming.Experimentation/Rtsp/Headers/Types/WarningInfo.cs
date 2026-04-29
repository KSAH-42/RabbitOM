using System;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Compliances;
    
    public sealed class WarningInfo
    {
        private static readonly StringValueNormalizer ValueNormalizer = StringValueNormalizer.TrimWithUnQuoteNormalizer;
    


        
        public WarningInfo( int code , string agent )
            : this( code , agent , null )
        {
        }

        public WarningInfo( int code , string agent , string comment )
        {
            if ( ! IsValidCode( code ) )
            {
                throw new ArgumentException( $"invalid value value:{code}" , nameof( code ) );
            }

            if ( ! IsValidAgent( agent ) )
            {
                throw new ArgumentException( "invalid agent name" , nameof( agent ) );
            }

            if ( ! IsValidComment( comment ) )
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
        



        public static bool IsValidCode( int value )
        {
            return value >= 0;
        }

        public static bool IsValidAgent( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            return Token.IsValid( value ,  x => x != ' ' );
        }

        public static bool IsValidComment( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            return Token.IsValid( value ,  x => ! Token.ParenthesisChars.Contains( x ) );
        }

       

        public static bool TryParse( string input , out WarningInfo result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , " " , out string[] tokens ) )
            {
                if ( tokens.Length >= 4 )
                {
                    return false;
                }

                if ( ! int.TryParse( tokens.ElementAtOrDefault( 0 ) , out var code ) || ! IsValidCode( code ) )
                {
                    return false;
                }

                if ( ! IsValidAgent( tokens.ElementAtOrDefault( 1 ) ) || ! IsValidComment( tokens.ElementAtOrDefault( 2 ) ) )
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
