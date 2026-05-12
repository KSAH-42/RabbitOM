using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;
 
    public sealed class PublicRtspHeaderValue
    {
        public RtspMethodHeaderValueCollection Methods { get; } = new RtspMethodHeaderValueCollection();
        
        public static bool TryParse( string input , out PublicRtspHeaderValue result )
        {
            result = null;
            
            if ( RtspHeaderValueParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new PublicRtspHeaderValue();

                foreach( var token in tokens )
                {
                    if ( RtspMethod.TryParse( token , out var method ) )
                    {
                        header.Methods.TryAdd( method );
                    }
                }

                if ( header.Methods.Count > 0 )
                {
                    result = header;
                }
            }

            return result != null;
        }

        public override string ToString()
        {
            return string.Join( ", " , Methods );
        }
    }
}
