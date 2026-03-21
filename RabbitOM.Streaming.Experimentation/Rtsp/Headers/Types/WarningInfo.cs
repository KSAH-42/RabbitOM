using System;
using System.Text;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters;
    
    // TODO: remove setter, make it as immutable type
    public sealed class WarningInfo
    {
        public static readonly StringComparer ValueComparer = StringComparer.OrdinalIgnoreCase;
        public static readonly StringValueAdapter ValueAdapter = StringValueAdapter.TrimWithUnQuoteAdapter;
                    

        private int _code = 0;
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
            set => _agent = ValueAdapter.Adapt( value );
        }
        
        public string Comment
        {
            get => _comment;
            set => _comment = ValueAdapter.Adapt( value );
        }
        

        public static bool TryParse( string input , out WarningInfo result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( input , " " , out string[] tokens ) )
            {
                if ( tokens.Length >= 4 )
                {
                    return false;
                }

                if ( ! int.TryParse( tokens.ElementAtOrDefault( 0 ) , out var code ) )
                {
                    return false;
                }

                var header = new WarningInfo()
                {
                    Code = code,
                    Agent = tokens.ElementAtOrDefault( 1 ),
                    Comment = tokens.ElementAtOrDefault( 2 ),
                };

                if ( header.Code >= 0 && RtspHeaderProtocolValidator.IsValidAgent( header.Agent ) )
                {
                    result = header;
                }
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
