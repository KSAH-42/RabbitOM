using System;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types.Compliances;

    public sealed class IfMatchRtspHeaderValue
    {
        private static readonly StringValueNormalizer ValueNormalizer = StringValueNormalizer.TrimWithUnQuoteNormalizer;

        public StringCollection ETags { get; } = new StringCollection( StringValueValidator.DefaultValidator.TryValidate );

        public static bool TryParse( string input , out IfMatchRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new IfMatchRtspHeaderValue();

                foreach ( var token in tokens )
                {
                    header.ETags.TryAdd( token );
                }
            
                if ( header.ETags.Count > 0 )
                {
                    result = header;
                }
            }

            return result != null;
        }

        public override string ToString()
        {
            return string.Join( ", " , ETags.Select( element => $"\"{element}\"" ) );
        }
    }
}
