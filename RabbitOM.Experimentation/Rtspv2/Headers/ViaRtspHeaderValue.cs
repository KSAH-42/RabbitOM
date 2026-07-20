using System;

namespace RabbitOM.Streaming.RtspV2.Headers
{
    using RabbitOM.Streaming.RtspV2.Headers.DataTypes;

    public sealed class ViaRtspHeaderValue
    {
        public ProxyRtspHeaderValueCollection Values { get; } = new ProxyRtspHeaderValueCollection();

        public static bool TryParse( string input , out ViaRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new ViaRtspHeaderValue();

                foreach ( var token in tokens )
                {
                    if ( ProxyRtspHeaderValue.TryParse( token , out var proxy ) )
                    {
                        header.Values.TryAdd( proxy );
                    }
                }

                if ( header.Values.Count > 0 )
                {
                    result = header;
                }
            }

            return result != null;
        }

        public override string ToString()
        {
            return string.Join( ", " , Values );
        }
    }
}
