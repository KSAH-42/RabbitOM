using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;
    
    public sealed class AllowRtspHeaderValue
    {
        public MethodRtspHeaderValueCollection Methods { get; } = new MethodRtspHeaderValueCollection();
        
        public override string ToString()
        {
            return string.Join( ", " , Methods );
        }

        public static bool TryParse( string input , out AllowRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new AllowRtspHeaderValue();

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
    }
}
