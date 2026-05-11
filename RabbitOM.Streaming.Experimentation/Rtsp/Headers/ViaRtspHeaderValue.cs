using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;

    public sealed class ViaRtspHeaderValue
    { 
        public ProxyInfoRtspHeaderValueCollection Proxies { get; } = new ProxyInfoRtspHeaderValueCollection();
        
        public override string ToString()
        {
            return string.Join( ", " , Proxies );
        }

        public static bool TryParse( string input , out ViaRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new ViaRtspHeaderValue();

                foreach ( var token in tokens )
                {
                    if ( ProxyInfo.TryParse( token , out var proxy ) )
                    {
                        header.Proxies.TryAdd( proxy );
                    }
                }

                if ( header.Proxies.Count > 0 )
                {
                    result = header;
                }
            }

            return result != null;
        }        
    }
}
