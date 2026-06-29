using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.DataTypes;

    public sealed class UserAgentRtspHeaderValue
    {
        public ProductInfoRtspHeaderValueCollection Values { get; } = new ProductInfoRtspHeaderValueCollection();

        public static bool TryParse( string input , out UserAgentRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new UserAgentRtspHeaderValue();

                foreach ( var token in tokens )
                {
                    if ( ProductInfoRtspHeaderValue.TryParse( token , out var element ) )
                    {
                        header.Values.TryAdd( element );
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
