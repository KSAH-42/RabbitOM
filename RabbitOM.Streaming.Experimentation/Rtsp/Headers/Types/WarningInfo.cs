using System;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types
{
    public sealed class WarningInfo
    {
    private int _code;
        private string _agent = string.Empty;
        private string _comment = string.Empty;
        


        public int Code
        {
            get => _code;
            set => _code = value;
        }

        public string Agent
        {
            get => _agent;
            set => _agent = EnsureValue( value );
        }
        
        public string Comment
        {
            get => _comment;
            set => _comment = EnsureValue( value );
        }
        


        private static string EnsureValue( string value )
        {
            var result = RtspHeaderValueSanitizer.UnQuotesWithTrim( value );

            RtspHeaderValueValidator.EnsureWellFormedToken( result );
            RtspHeaderValueValidator.EnsureNotNullOrWhiteSpace( result );

            return result;
        }

        public static bool TryParse( string input , out WarningInfo result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , " " , out string[] tokens ) )
            {
                if ( ! int.TryParse( tokens.ElementAtOrDefault( 0 ) , out var code ) )
                {
                    return false;
                }
                
                result = new WarningInfo()
                {
                    _code = code ,
                    _agent = RtspHeaderValueSanitizer.UnQuotesWithTrim( tokens.ElementAtOrDefault( 1 ) ),
                    _comment = RtspHeaderValueSanitizer.UnQuotesWithTrim( tokens.ElementAtOrDefault( 2 ) ),
                };
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
            
            return builder.ToString().Trim();
        }
    }
}
