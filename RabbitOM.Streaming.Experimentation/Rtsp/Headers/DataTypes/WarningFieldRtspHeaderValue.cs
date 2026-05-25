using System;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.DataTypes
{
    public sealed class WarningFieldRtspHeaderValue
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
            set => _agent = RtspHeaderValueValidator.EnsureWellFormedToken( RtspHeaderValueSanitizer.UnQuotesWithTrim( value ) );
        }
        
        public string Comment
        {
            get => _comment;
            set => _comment = RtspHeaderValueValidator.EnsureWellFormedTokenOrEmpty( RtspHeaderValueSanitizer.UnQuotesWithTrim( value ) );
        }
        


        public static bool TryParse( string input , out WarningFieldRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , " " , out string[] tokens ) )
            {
                if ( ! int.TryParse( tokens.ElementAtOrDefault( 0 ) , out var code ) )
                {
                    return false;
                }
                
                result = new WarningFieldRtspHeaderValue()
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
