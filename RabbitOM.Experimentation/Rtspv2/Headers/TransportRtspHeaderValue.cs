using System;

namespace RabbitOM.Streaming.RtspV2.Headers
{
    using RabbitOM.Streaming.RtspV2.Headers.DataTypes;

    public sealed class TransportRtspHeaderValue
    {
        public TransportInfoRtspHeaderValueCollection Values { get; } = new TransportInfoRtspHeaderValueCollection();

        public static bool TryParse( string input , out TransportRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new TransportRtspHeaderValue();

                foreach ( var token in tokens )
                {
                    if ( TransportInfoRtspHeaderValue.TryParse( token , out var transport ) )
                    {
                        header.Values.TryAdd( transport );
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
